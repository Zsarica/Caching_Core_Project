using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSqlServerCache.Infrastructure.DbContexts
{
    //burada design time da database e migration yapabilmemeiz için uyguladığımız bir yöntemi yapıyoruz.
    //DbContextimizin ihtiyaç duyduğu optionları da DbContextOptionsBuilder ile oluşturuyoruz. Bu sayede uygulamanın tamamını ayağa kaldırmak zorunda kalmadan
    //design time da migration işlemlerin yapabileceğiz.
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<CacheDbContext>
    {
        public CacheDbContext CreateDbContext(string[] args)
        {
            var conStr = "Data Source=DESKTOP-AUNU480\\SQLEXPRESS;Initial Catalog=CacheDb;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
            var optionBuilder = new DbContextOptionsBuilder<CacheDbContext>();
            optionBuilder.UseSqlServer(conStr, opt =>
            {
                opt.EnableRetryOnFailure(maxRetryCount:3);
                opt.MigrationsAssembly(typeof(CacheDbContext).Assembly.FullName);
            });
            //throw new Exception(conStr);

            return new CacheDbContext(optionBuilder.Options);
        }
    }
}
