using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SongWishing.DAL
{
    public class SongsDbContext : DbContext
    {
        public SongsDbContext() : base("SongsDbContext")
        {

        }

        public DbSet<Song> Songs { get; set; }
        //public DbSet<SongsRequest> SongsRequests { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}