using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Aesth.Infrastructure.Persistence.DbContexts
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql("Host=db.apsnxcnzwhygiruighvy.supabase.co;Port=6543;Database=postgres;Username=postgres;Password=EV6n6OYL35j5uZFh;SSL Mode=Require;Trust Server Certificate=true;Timeout=15;Command Timeout=30;Pooling=true;Maximum Pool Size=100");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}