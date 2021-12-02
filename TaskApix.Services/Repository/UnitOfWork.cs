using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApix.Data.Context;
using TaskApix.Data.Models;
using TaskApix.Services.IRepository;

namespace TaskApix.Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IGenericRepository<Country> _countries;
        private IGenericRepository<Region> _regions;

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Region> Regions => _regions ??= new GenericRepository<Region>(_context);

        public void Dispose()
        {
            _context.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
