using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Server.data.IRepository;
using SchoolProject.Server.Data.IRepository;
using SchoolProject.Server.Data;
using SchoolProject.Server.Models;
using SchoolProject.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRentalManagement.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Cart> _carts;
        private IGenericRepository<CheckOutTransaction> _checkouttransactions;
        private IGenericRepository<Cpu> _cpus;
        private IGenericRepository<Gpu> _gpus;
        private IGenericRepository<Laptop> _laptops;
        private IGenericRepository<OS> _oss;
        private IGenericRepository<Payment> _payments;
        private IGenericRepository<Ram> _rams;
        private IGenericRepository<Reviews> _review;
        private IGenericRepository<Screen> _screens;
        private IGenericRepository<User> _users;
        private IGenericRepository<Wifi> _wifis;


        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Cart> Carts
            => _carts ??= new GenericRepository<Cart>(_context);
        public IGenericRepository<CheckOutTransaction> CheckOutTransactions
            => _checkouttransactions ??= new GenericRepository<CheckOutTransaction>(_context);
        public IGenericRepository<Cpu> Cpus
            => _cpus ??= new GenericRepository<Cpu>(_context);
        public IGenericRepository<Gpu> Gpus
            => _gpus ??= new GenericRepository<Gpu>(_context);
        public IGenericRepository<Laptop> Laptops
            => _laptops ??= new GenericRepository<Laptop>(_context);
        public IGenericRepository<OS> OSs
            => _oss ??= new GenericRepository<OS>(_context);
        public IGenericRepository<Payment> Payments
        => _payments ??= new GenericRepository<Payment>(_context);
        public IGenericRepository<Ram> Rams
        => _rams ??= new GenericRepository<Ram>(_context);
        public IGenericRepository<Reviews> Review
        => _review ??= new GenericRepository<Reviews>(_context);
        public IGenericRepository<Screen> Screens
        => _screens ??= new GenericRepository<Screen>(_context);
        public IGenericRepository<User> Users
        => _users ??= new GenericRepository<User>(_context);
        public IGenericRepository<Wifi> Wifis
        => _wifis ??= new GenericRepository<Wifi>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}