using System.IO;
using ContactsList.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsList
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB17;AttachDbFilename=%CONTENTROOTPATH%\\ContactsDB.mdf;Integrated Security=True; MultipleActiveResultSets=True".Replace("%CONTENTROOTPATH%", Directory.GetCurrentDirectory()));
        }
        
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactInfo> ContactInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>()
                .HasMany(x => x.ContactInfos)
                .WithOne(x => x.Contact)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
