using EnglishTrainer.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Sourses
{
    public class DbContextProvider
    {
        private static EnglishTrainerContext _context;

        public static EnglishTrainerContext GetContext()
        {
            return _context ??= new EnglishTrainerContext();
        }
    }
}
