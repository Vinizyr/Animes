using AnimeCatalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeCatalogo.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Anime> Animes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anime>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Nome).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Diretor).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Resumo).IsRequired().HasMaxLength(1000);
                entity.Property(a => a.Ativo).HasDefaultValue(true);
            });
        }
    }
}
