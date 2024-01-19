using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Server.Configurations.Entities;
using SchoolProject.Server.Models;
using SchoolProject.Shared.Domain;

namespace SchoolProject.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Cpu> Cpus { get; set; }
        public DbSet<Cart> Carts {  get; set; }
        public DbSet<CheckOutTransaction> CheckOutTransactions { get; set; }
        public DbSet<Gpu> Gpus { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<OS> OSs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Reviews> Review { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Wifi> Wifis { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CpuSeed());
        }
    }
}
