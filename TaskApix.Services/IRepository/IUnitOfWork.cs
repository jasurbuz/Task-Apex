using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApix.Data.Models;

namespace TaskApix.Services.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save();
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Region> Regions { get; }
    }
}
