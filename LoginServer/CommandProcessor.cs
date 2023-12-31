﻿using Core.Network;
using LoginServer.Commands;
using LoginServer.Packets;
using Shared;

namespace LoginServer
{
    public class CommandProcessor
    {
        private Dictionary<byte, ICommand> commandMap = new Dictionary<byte, ICommand>();
        public CommandProcessor() {
            commandMap.Add(LoginPackets.USERNAME_AND_PASSWORD, new LoginUsernameAndPasswordCommand());
        }

        public void ProcessCommand(byte[] packetData, string ipAddress)
        {
            if(packetData.Length > 0)
            {
                byte commandType = packetData[0];

                if(commandMap.TryGetValue(commandType, out ICommand? command))
                {
                    command.Execute(packetData, ipAddress);
                } else
                {
                    Display.Error($"Command packet 0x{commandType:X} is unknown");
                }
            }
        }
    }
}
