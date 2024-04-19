using Microsoft.EntityFrameworkCore;
using CRUD.DataStructures.DataModel;

namespace CRUD.Database.Data
{
    /// <summary>
    /// Ist dafür zuständig, eine Verbindung zwischen dem Code und der Datenbank herzustellen.
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<TableModel> TableModels { get; set; }
        public DbSet<ReservationModel> ReservationModels { get; set; }
        public string DbPath { get; }

        public DataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "reservation_service.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
