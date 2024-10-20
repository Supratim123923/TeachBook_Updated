using TeachBook.Web.Models.Domain_Model;

namespace TeachBook.Web.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllBlogAsync();
        Task<BlogPost?> GetBlogAsync(Guid Id);
        Task<BlogPost?> GetBlogPostByUrl(string Url);
        Task<BlogPost> AddBlogAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateBlogAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteBlogAsync(Guid Id);
    }
}
