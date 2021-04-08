using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pasquinelli.Martina._5H.SecondaWeb.Models;

namespace Pasquinelli.Martina._5H.SecondaWeb
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();
      //stringa di connessione per SQLite
      string cn = "Data Source=database.db";

      // Registrazione globale (DI) del dbcontext per creare 
      // automaticamente il db quando serve. Dai un'occhiata al 
      // costruttore di DBContext, ti accorgerai che vuole un "option" ...
      // Gli arriva da qui, e contiene la stringa di connessione.
      services.AddDbContext<DBContext>(
          options => options.UseSqlite(cn)
      );
      services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DBContext>();
      //    ... il resto del metodo ...
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();


      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
