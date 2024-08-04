using Microsoft.EntityFrameworkCore;
using TeachBook.Web.Models.Domain_Model;
namespace TeachBook.Web.Data
{
    public class TeachBookDBContext : DbContext
    {
        public TeachBookDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
