using SchoolProject.Server.data.IRepository;
using SchoolProject.Shared.Domain;

namespace SchoolProject.Server.Data.IRepository
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
