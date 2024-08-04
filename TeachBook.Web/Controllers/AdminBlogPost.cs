using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeachBook.Web.Models.ViewModels;
using TeachBook.Web.Repositories;

namespace TeachBook.Web.Controllers
{
    public class AdminBlogPost : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminBlogPost(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        public async Task<IActionResult> Add()
        {
            var Alltags = await tagRepository.GetAllTagsAsync();
            var model = new AddBlogPostRequest { Tags = Alltags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() }) };
            //var Tags = Alltags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            return View(addBlogPostRequest);
        }
    }
}
