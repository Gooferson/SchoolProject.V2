using SchoolProject.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolProject.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Cpu> Cpus { get; }
        IGenericRepository<Cart> Carts { get; }
        IGenericRepository<CheckOutTransaction> CheckOutTransactions { get; }
        IGenericRepository<Gpu> Gpus { get; }
        IGenericRepository<Laptop> Laptops { get; }
        IGenericRepository<OS> OSs { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Ram> Rams { get; }
        IGenericRepository<Reviews> Review { get; }
        IGenericRepository<User> Users { get;}
        IGenericRepository<Screen> Screens { get; }
        IGenericRepository<Wifi> Wifis { get; }
    }
}
