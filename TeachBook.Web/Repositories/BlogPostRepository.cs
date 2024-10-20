using Microsoft.EntityFrameworkCore;
using TeachBook.Web.Data;
using TeachBook.Web.Models.Domain_Model;

namespace TeachBook.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly TeachBookDBContext _teachBookDBContext;

        public BlogPostRepository(TeachBookDBContext teachBookDBContext)
        {
            this._teachBookDBContext = teachBookDBContext;
        }
        public async Task<BlogPost> AddBlogAsync(BlogPost blogPost)
        {
            await _teachBookDBContext.BlogPosts.AddAsync(blogPost);
            await _teachBookDBContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogAsync(Guid Id)
        {
            var Findblog = await _teachBookDBContext.BlogPosts.FindAsync(Id);
            if (Findblog != null)
            {
                _teachBookDBContext.BlogPosts.Remove(Findblog);
                await _teachBookDBContext.SaveChangesAsync(true);
                return Findblog;
            }
            else { return null; }
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogAsync()
        {
            return await _teachBookDBContext.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetBlogAsync(Guid Id)
        {
            var Blog = await _teachBookDBContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == Id);
            return Blog;
        }

        public async Task<BlogPost?> GetBlogPostByUrl(string url)
        {
            var Blog = await _teachBookDBContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == url.Trim());
            return Blog;//9831505262
        }

        public async Task<BlogPost?> UpdateBlogAsync(BlogPost blogPost)
        {
            var FindBlog = await _teachBookDBContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id == blogPost.Id);
            if (FindBlog != null)
            {
                FindBlog.Author = blogPost.Author;
                FindBlog.ShortDescription = blogPost.ShortDescription;
                FindBlog.PublishedDate = blogPost.PublishedDate;
                FindBlog.Content = blogPost.Content;
                FindBlog.FeaturedImgUrl = blogPost.FeaturedImgUrl;
                FindBlog.Heading = blogPost.Heading;
                FindBlog.PageTitle = blogPost.PageTitle;
                FindBlog.UrlHandle = blogPost.UrlHandle;
                FindBlog.Visible = blogPost.Visible;
                FindBlog.Tags = blogPost.Tags;
                await _teachBookDBContext.SaveChangesAsync();
                return FindBlog;
            }
            else
            {
                return null;
            }
        }
    }
}
