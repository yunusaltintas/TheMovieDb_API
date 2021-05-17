using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDB.Data;
using TMDB.Service.IUnitOfWorks;

namespace TMDB.Service.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TMDbContext _context;

        public UnitOfWork(TMDbContext tMDbContext)
        {
            _context = tMDbContext;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
