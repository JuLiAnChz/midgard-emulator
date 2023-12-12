using Core.DataBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataBase.Repository
{
    public class CharacterRepository
    {
        private readonly ApplicationDbContext _context;

        public CharacterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CountUsersOnline()
        {
            var charsOnline = _context.Characters!.Count(x =>  x.IsOnline);
            return charsOnline;
        }
    }
}
