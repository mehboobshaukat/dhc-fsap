using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Runtime.InteropServices;

namespace DHC_FSAP.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectingString = config.GetConnectionString("DefaultConnection");
            // optionsBuilder.UseSqlServer(connectingString);
            optionsBuilder.UseMySql(
                connectingString, ServerVersion.AutoDetect(connectingString)
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
