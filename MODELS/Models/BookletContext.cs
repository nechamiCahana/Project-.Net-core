using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MODELS.Models
{
    public class BookletContext : DbContext
    {
        public BookletContext(DbContextOptions<BookletContext> booklet) : base(booklet)
        {

        }
        public BookletContext()
        {
                
        }
        public DbSet<Booklet> Booklets { get; set; }
        public DbSet<Orders> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=נחמי-מחשב\\SQLEXPRESS;Database=Booklet;Trusted_Connection=True;",
                    b => b.MigrationsAssembly("MODELS"));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booklet>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Time).IsRequired();
                // ניתן להוסיף הגדרות נוספות כפי הצורך
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.ID);
                // ניתן להוסיף הגדרות נוספות כפי הצורך
                entity.Property(e => e.MyAllBooklet)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, new JsonSerializerOptions { }),
                  v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, new JsonSerializerOptions { })
              );
            });


        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=נחמי-מחשב\\SQLEXPRESS;Initial Catalog=Booklet;Integrated Security=SSPI;Trusted_Connection=True;");
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Booklet>(entity =>
        //    {
        //        entity.ToTable("Booklet");
        //    });

        //    modelBuilder.Entity<Orders>(entity =>
        //    {
        //        entity.ToTable("Orders");

        //        entity.HasIndex(e => e.ID, "IX_Book_AoutherId");

        //        entity.HasOne(d => d.OrderingName)
        //            .WithMany(p => p.)
        //            .HasForeignKey(d => d.ID);
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
