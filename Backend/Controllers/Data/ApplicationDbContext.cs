using Backend.Models;
using Backend.Models.XML;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<CasaDeMarcat> CaseDeMarcat { get; set; }
        public DbSet<Msj> Mesaje { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<User>()
                .HasMany(u => u.CaseDeMarcat)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CasaDeMarcat>()
                .HasMany(c => c.MesajXML)
                .WithOne(m => m.CasaDeMarcat)
                .HasForeignKey(m => m.CasaDeMarcatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Msj>().HasKey(m => m.IdM);
            builder.Entity<Msj>().OwnsOne(m => m.Bon, bon =>
            {
                bon.OwnsMany(b => b.Cote); // Bon are o lista de Cote
            });

            builder.Entity<Msj>().OwnsOne(m => m.RB, rb =>
            {
                rb.OwnsMany(r => r.CoteZList); // RB are o lista de CoteZ
                rb.OwnsOne(r => r.Pl); // RB contine un singur PL
                rb.OwnsOne(r => r.Av);//contine un singur av
            });

            builder.Entity<Msj>().OwnsOne(m => m.ME, me =>
            {
                me.OwnsMany(m => m.Ev); // ME are o list de Ev
            });
        }
    }
}
