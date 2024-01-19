using SchoolProject.Server.Models;
using SchoolProject.Shared.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolProject.Server.Configurations.Entities
{
    public class CpuSeed : IEntityTypeConfiguration<Cpu>
    {
        public void Configure(EntityTypeBuilder<Cpu> builder)
        {
            builder.HasData(
                new Cpu
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CpuBrand = "AMD",
                    CpuCores = 10,
                    CpuSpeed = "14098Hz",
                    CpuVersion = "System"
                },
                new Cpu
                {
                    Id = 2,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CpuBrand = "Intel",
                    CpuCores = 10,
                    CpuSpeed = "14098Hz",
                    CpuVersion = "System"
                }
            );
        }
    }
}
