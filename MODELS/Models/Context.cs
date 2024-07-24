using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MODELS.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> option) : base(option)
        {

        }
        public Context()
        {
                
        }
        public DbSet<Booklet> Booklets { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booklet>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                // ניתן להוסיף הגדרות נוספות כפי הצורך
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.OrderingName).IsRequired();
                entity.Property(e => e.DateOrder).IsRequired();
                // ניתן להוסיף הגדרות נוספות כפי הצורך
                entity.Property(e => e.MyAllBooklet)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, new JsonSerializerOptions { }),
                  v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, new JsonSerializerOptions { })
              );
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.mail)
                .IsUnique();
        }
    }
}
