using Core.DataBase.Data;
using Core.DataBase.Models;

namespace Core.DataBase.Repository
{
    public class AccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Account? GetAccountByUserName(string UserName)
        {
            var account = _context.Accounts!.FirstOrDefault(x => x.UserName == UserName && x.Status == 1);
            return account;
        }
    }
}
