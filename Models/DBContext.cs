using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Pasquinelli.Martina._5H.SecondaWeb.Models
{
  public class DBContext : IdentityDbContext
  {
    private readonly DbContextOptions _options;

    public DBContext(DbContextOptions options) : base(options)
    {
      _options = options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

    public DbSet<Prenotazione> Prenotazioni { get; set; }
  }

}



