using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {

        public DbSet<AppUser>  Users { set; get;} 

        public DataContext(DbContextOptions options ) : base(options) {
            
        }

    }
}