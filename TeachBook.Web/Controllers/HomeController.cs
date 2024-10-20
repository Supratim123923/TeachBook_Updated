using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeachBook.Web.Models;
using TeachBook.Web.Models.ViewModels;
using TeachBook.Web.Repositories;

namespace TeachBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger,IBlogPostRepository blogPostRepository,ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Blogs = await blogPostRepository.GetAllBlogAsync();
            var tags = await tagRepository.GetAllTagsAsync();
            var data = new HomeViewModel { Blogs = Blogs.ToList(), Tags = tags.ToList() };
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
