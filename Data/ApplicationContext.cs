using Microsoft.EntityFrameworkCore;
using WebMatrixUploader.Data.DataForMatrix;

namespace ItmoPhysics.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        public DbSet<UserFile> UsersFiles { get; set; }
        public DbSet<CurveData> CurveData { get; set; }
        public DbSet<Abscissa> Abscissa { get; set; }
        public DbSet<Ordinate> Ordinates { get; set; }
    }
}
    