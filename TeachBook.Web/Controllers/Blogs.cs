using Microsoft.AspNetCore.Mvc;
using TeachBook.Web.Repositories;

namespace TeachBook.Web.Controllers
{
    public class Blogs : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public Blogs(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task<IActionResult> Index(string urlHandle)
        {
            var Blog = await blogPostRepository.GetBlogPostByUrl(urlHandle);
            return View(Blog);
        }
    }
}
