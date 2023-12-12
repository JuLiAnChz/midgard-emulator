using Core.DataBase.Models;
using Core.DataBase.Repository;
using LoginServer.Interfaces;
using LoginServer.Packets;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Shared;
using System;
using System.Net;
using System.Text;

namespace LoginServer.Commands
{
    public class LoginUsernameAndPasswordCommand : LoginCommand
    {
        public override void Execute(byte[] packetData, string ipAddress)
        {
            this._packedData = packetData;
            this._packetHeader = packetData[0];

            if(Server.clients.TryGetValue(ipAddress, out var client))
            {
                if(!this.validatePackedLength())
                {
                    client.client.Close();
                    Display.Error($"Client {ipAddress} sending unknowns packets to login");
                    return;
                }

                byte[] usernameBytes = new byte[Constants.NAME_LENGTH];
                Array.Copy(packetData, 6, usernameBytes, 0, usernameBytes.Length);

                byte[] passwordBytes = new byte[Constants.PASSWORD_LENGTH];
                Array.Copy(packetData, 30, passwordBytes, 0, passwordBytes.Length);

                byte[] clientTypeBytes = new byte[1];
                Array.Copy(packetData, 54, clientTypeBytes, 0, clientTypeBytes.Length);

                int clientType = clientTypeBytes[0];
                string username = StringManager.ByteToString(usernameBytes, usernameBytes.Length);
                string password = StringManager.ByteToString(passwordBytes, passwordBytes.Length);

                LoginData ld = new LoginData { UserName = username, Password = password, ClientType = clientType };
                AccountRepository accountRepo = new AccountRepository(Core.dbConn!);
                Account? accountDB = accountRepo.GetAccountByUserName(ld.UserName);

                if (accountDB == null)
                {
                    byte[] errorResByte = new byte[30];
                    ushort value = 0x83e;
                    BitConverter.GetBytes(value).CopyTo(errorResByte, 0);
                    BitConverter.GetBytes(ErrorCodes.USERNAME_PASSWORD_INCORRECT).CopyTo(errorResByte, 2);
                    BitConverter.GetBytes(26).CopyTo(errorResByte, 26);

                    client.client.Client.Send(errorResByte);
                    return;
                }

                if (!PasswordHasher.VerifyPassword(ld.Password, accountDB.Password))
                {
                    byte[] errorResByte = new byte[30];
                    ushort value = 0x83e;
                    BitConverter.GetBytes(value).CopyTo(errorResByte, 0);
                    BitConverter.GetBytes(ErrorCodes.USERNAME_PASSWORD_INCORRECT).CopyTo(errorResByte, 2);
                    BitConverter.GetBytes(26).CopyTo(errorResByte, 26);

                    client.client.Client.Send(errorResByte);
                    return;
                }

                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    CharacterRepository characterRepo = new CharacterRepository(Core.dbConn!);
                    int usersOnline = characterRepo.CountUsersOnline();
                    Display.Info($"Users online: {usersOnline}");

                    int cmd = 0xac4;
                    int header = 64;
                    int size = 160;
                    int server_num = 1;

                    bw.Write((ushort)cmd);
                    bw.Write((ushort)(header + size * server_num));
                    bw.Write((uint)accountDB.AccountId);//Token 1
                    bw.Write((uint)accountDB.AccountId);
                    bw.Write((uint)accountDB.AccountId);//Token 2
                    bw.Write((uint)0);
                    bw.Write(new byte[24]);
                    bw.Write((ushort)0);
                    bw.Write(Encoding.UTF8.GetBytes(accountDB.Gender));
                    bw.Write(new byte[Constants.WEB_AUTH_TOKEN_LENGTH]);
                    bw.Write((uint)BitConverter.ToInt32(IPAddress.Parse(Core.config!.charServerConfig.Ip).GetAddressBytes(), 0));
                    bw.Write((ushort)Core.config!.charServerConfig.Port);
                    bw.Write((char[])Core.config!.emulatorConfig.ServerName.ToCharArray());
                    bw.BaseStream.Seek(bw.BaseStream.Position + (20 - Core.config!.emulatorConfig.ServerName.Length), SeekOrigin.Begin);
                    bw.Write((ushort)GetUserCount(usersOnline));// Fake user count
                    bw.Write((ushort)0);// server type 0=normal, 1=maintenance, 2=over 18, 3=paying, 4=P2P
                    bw.Write((ushort)0);// Should display as new
                    bw.Write(new byte[128]);

                    client.client.Client.Send(ms.ToArray());
                }
            }
        }

        private int GetUserCount(int usersCount)
        {
            if (!Core.config!.loginServerConfig.EnabledUserCount) return 4;// Removes count and colorization completely
            if (usersCount <= Core.config!.loginServerConfig.UserCountLow) return 0;// Green => Smooth
            if (usersCount <= Core.config!.loginServerConfig.UserCountMedium) return 1;// Yellow => Normal
            if (usersCount <= Core.config!.loginServerConfig.UserCountHigh) return 2;// Red => Busy

            return 3;// Purple => Crowded
        }
    }
}
