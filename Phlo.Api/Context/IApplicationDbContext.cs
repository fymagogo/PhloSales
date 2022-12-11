using Microsoft.EntityFrameworkCore;
using Phlo.Api.Models;
using System.Threading.Tasks;

namespace Phlo.Api.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }

        Task<int> SaveChanges();
    }
}