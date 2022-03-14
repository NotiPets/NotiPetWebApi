using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Notipet.Domain;
using Utilities;

namespace Notipet.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<NotiPetBdContext>
    {
        public NotiPetBdContext CreateDbContext(string[] args)
        {
            //var connectionString = Methods.GetConnectionString();
            var connectionString = "Server=ec2-3-225-79-57.compute-1.amazonaws.com;Port=5432;Database=d7mtmb42b2odvt;User Id=qdfspkpbgyxsfk;Password=3f417b474c41396d2825fbb070167d086d3f769e463a82e6921c5b21441a83f4;";
            var builder = new DbContextOptionsBuilder<NotiPetBdContext>();
            builder.UseNpgsql(connectionString);
            return new NotiPetBdContext(builder.Options);
        }
    }
    public class NotiPetBdContext : DbContext
    {
        public NotiPetBdContext(DbContextOptions<NotiPetBdContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AssetsServices> AssetsServices { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<DigitalVaccine> DigitalVaccines { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Specialist> Specialists { get; set; }
        public virtual DbSet<Speciality> Specialities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business>()
              .HasIndex(e => e.Rnc)
              .IsUnique();

            modelBuilder.Entity<User>()
              .HasIndex(e => e.Document)
              .IsUnique();

            // AppointmentStatus
            modelBuilder.Entity<Appointment>()
              .Property(e => e.AppointmentStatus)
              .HasConversion<int>();

            modelBuilder
              .Entity<AppointmentStatus>()
              .Property(e => e.Id)
              .HasConversion<int>();

            modelBuilder
              .Entity<AppointmentStatus>().HasData(
                Enum.GetValues(typeof(AppointmentStatusId))
                .Cast<AppointmentStatusId>()
                .Select(e => new AppointmentStatus()
                {
                    Id = e,
                    Name = e.ToString()
                })
              );

            // AssetsServiceType
            modelBuilder
              .Entity<AssetsServices>()
              .Property(e => e.AssetsServiceType)
              .HasConversion<int>();

            modelBuilder
              .Entity<AssetsServiceType>()
              .Property(e => e.Id)
              .HasConversion<int>();

            modelBuilder
              .Entity<AssetsServiceType>().HasData(
                Enum.GetValues(typeof(AssetsServiceTypeId))
                .Cast<AssetsServiceTypeId>()
                .Select(e => new AssetsServiceType()
                {
                    Id = e,
                    Name = e.ToString()
                })
              );

            // DocumentType
            modelBuilder
              .Entity<User>()
              .Property(e => e.DocumentType)
              .HasConversion<int>();

            modelBuilder
              .Entity<DocumentType>()
              .Property(e => e.Id)
              .HasConversion<int>();

            modelBuilder
              .Entity<DocumentType>().HasData(
                Enum.GetValues(typeof(DocumentTypeId))
                .Cast<DocumentTypeId>()
                .Select(e => new DocumentType()
                {
                    Id = e,
                    Name = e.ToString()
                })
              );

            // OrderStatus
            modelBuilder
              .Entity<Order>()
              .Property(e => e.OrderStatus)
              .HasConversion<int>();

            modelBuilder
              .Entity<OrderStatus>()
              .Property(e => e.Id)
              .HasConversion<int>();

            modelBuilder
              .Entity<OrderStatus>().HasData(
                Enum.GetValues(typeof(OrderStatusId))
                .Cast<OrderStatusId>()
                .Select(e => new OrderStatus()
                {
                    Id = e,
                    Name = e.ToString()
                })
              );

            // PetType
            modelBuilder
              .Entity<Pet>()
              .Property(e => e.PetType)
              .HasConversion<int>();

            modelBuilder
              .Entity<PetType>()
              .Property(e => e.Id)
              .HasConversion<int>();

            modelBuilder
              .Entity<PetType>().HasData(
                Enum.GetValues(typeof(PetTypeId))
                .Cast<PetTypeId>()
                .Select(e => new PetType()
                {
                    Id = e,
                    Name = e.ToString()
                })
              );

            // Role
            modelBuilder
              .Entity<UserRole>()
              .Property(e => e.Role)
              .HasConversion<int>();

            modelBuilder
              .Entity<Role>()
              .Property(e => e.Id)
              .HasConversion<int>();

            modelBuilder
              .Entity<Role>().HasData(
                Enum.GetValues(typeof(RoleId))
                .Cast<RoleId>()
                .Select(e => new Role()
                {
                    Id = e,
                    Name = e.ToString()
                })
              );

            // Vendor
            modelBuilder
              .Entity<AssetsServices>()
              .Property(e => e.Vendor)
              .HasConversion<int>();

            modelBuilder
              .Entity<Vendor>()
              .Property(e => e.Id)
              .HasConversion<int>();

            modelBuilder
              .Entity<Vendor>().HasData(
                Enum.GetValues(typeof(VendorId))
                .Cast<VendorId>()
                .Select(e => new Vendor()
                {
                    Id = e,
                    Name = e.ToString()
                })
              );
        }
    }
}
