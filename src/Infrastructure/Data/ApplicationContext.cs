using ApplicationCore.Entities;
using ApplicationCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<Organization> Organizations { get; set; }
        
        public DbSet<Country> Countries { get; set; }

        public DbSet<Business> Businesses { get; set; }

        public DbSet<Family> Families { get; set; }

        public DbSet<Offering> Offerings { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Organization>(ConfigureOrganization);
            modelBuilder.Entity<Country>(ConfigureCountry);
            modelBuilder.Entity<Business>(ConfigureBusiness);
            modelBuilder.Entity<Family>(ConfigureFamily);
            modelBuilder.Entity<Offering>(ConfigureOffering);
            modelBuilder.Entity<Department>(ConfigureDepartment);
        }

        private void ConfigureUser(EntityTypeBuilder<User> typeBuilder)
        {
            typeBuilder.HasKey(u => u.Id);

            typeBuilder.Property(u => u.Name)
                .IsRequired();

            typeBuilder.Property(u => u.Surname)
                .IsRequired();

            typeBuilder.Property(u => u.Email)
                .IsRequired();

            typeBuilder.OwnsOne(u => u.Address)
                .Property(a => a.City)
                .IsRequired();

            typeBuilder.OwnsOne(u => u.Address)
                .Property(a => a.Country)
                .IsRequired();
        }

        private void ConfigureOrganization(EntityTypeBuilder<Organization> typeBuilder)
        {
            typeBuilder.HasKey(o => o.Id);

            typeBuilder.Property(o => o.Name)
                .IsRequired();

            typeBuilder.Property(o => o.Code)
                .IsRequired();

            typeBuilder.Property(o => o.OrganizationType)
                .IsRequired();

            typeBuilder.Property(o => o.Owner)
                .IsRequired();

            typeBuilder.HasMany(o => o.Countries)
                .WithOne(c => c.Parent)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureCountry(EntityTypeBuilder<Country> typeBuilder)
        {
            typeBuilder.HasKey(c => c.Id);

            typeBuilder.Property(c => c.Name)
                .IsRequired();

            typeBuilder.Property(c => c.Code)
                .IsRequired();

            typeBuilder.HasMany(c => c.Businesses)
                .WithOne(b => b.Parent)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureBusiness(EntityTypeBuilder<Business> typeBuilder)
        {
            typeBuilder.HasKey(b => b.Id);

            typeBuilder.Property(b => b.Name)
                .IsRequired();

            typeBuilder.HasMany(b => b.Families)
                .WithOne(f => f.Parent)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureFamily(EntityTypeBuilder<Family> typeBuilder)
        {
            typeBuilder.HasKey(f => f.Id);

            typeBuilder.Property(f => f.Name)
                .IsRequired();

            typeBuilder.HasMany(f => f.Offerings)
                .WithOne(o => o.Parent)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureOffering(EntityTypeBuilder<Offering> typeBuilder)
        {
            typeBuilder.HasKey(o => o.Id);

            typeBuilder.Property(o => o.Name)
                .IsRequired();

            typeBuilder.HasMany(o => o.Departments)
                .WithOne(d => d.Parent)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureDepartment(EntityTypeBuilder<Department> typeBuilder)
        {
            typeBuilder.HasKey(d => d.Id);

            typeBuilder.HasAlternateKey(d => d.Name);

            typeBuilder.Property(d => d.Name)
                .IsRequired();
        }
    }
}
