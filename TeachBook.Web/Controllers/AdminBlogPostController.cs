using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeachBook.Web.Models.Domain_Model;
using TeachBook.Web.Models.ViewModels;
using TeachBook.Web.Repositories;

namespace TeachBook.Web.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
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
            var Blogpost = new BlogPost
            {
                Author = addBlogPostRequest.Author,
                Content = addBlogPostRequest.Content,
                FeaturedImgUrl = addBlogPostRequest.FeaturedImgUrl,
                Heading = addBlogPostRequest.Heading,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PageTitle = addBlogPostRequest.PageTitle,
                ShortDescription = addBlogPostRequest.ShortDescription,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Visible = addBlogPostRequest.Visible,
            };
            var NewListTags = new List<Tag>();
            foreach (var item in addBlogPostRequest.SelectedItems)
            {
                var id = Guid.Parse(item);
                var Getdata = await tagRepository.GetTagAsync(id);
                if (Getdata != null) { NewListTags.Add(Getdata); }

            }
            Blogpost.Tags = NewListTags;
            await blogPostRepository.AddBlogAsync(Blogpost);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> ListOfBlogPosts()
        {
            var Allblogsdata = await blogPostRepository.GetAllBlogAsync();
            return View(Allblogsdata);
        }
        [HttpGet]
        public async Task<IActionResult> EditBlog(Guid id)
        {
            var Blogdata = await blogPostRepository.GetBlogAsync(id);
            var Tagsdomainmodel = await tagRepository.GetAllTagsAsync();
            if (Blogdata != null)
            {
                var Viewdata = new EditBlogRequest
                {
                    Id = Blogdata.Id,
                    Author = Blogdata.Author,
                    Content = Blogdata.Content,
                    Heading = Blogdata.Heading,
                    FeaturedImgUrl = Blogdata.FeaturedImgUrl,
                    PageTitle = Blogdata.PageTitle,
                    PublishedDate = Blogdata.PublishedDate,
                    ShortDescription = Blogdata.ShortDescription,
                    UrlHandle = Blogdata.UrlHandle,
                    Visible = Blogdata.Visible == "1" ? "true" : "false",
                    Tags= Tagsdomainmodel.Select(x=>new SelectListItem {Text = x.Name,Value=x.Id.ToString() }),
                    SelectedItems = Blogdata.Tags.Select(x=>x.Id.ToString()).ToArray()
                };
                return View(Viewdata);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(EditBlogRequest editBlogRequest)
        {
            var Data = new BlogPost
            {
                Author = editBlogRequest.Author,
                Content = editBlogRequest.Content,
                FeaturedImgUrl= editBlogRequest.FeaturedImgUrl,
                PageTitle = editBlogRequest.PageTitle,
                PublishedDate = editBlogRequest.PublishedDate,
                ShortDescription = editBlogRequest.ShortDescription,
                UrlHandle = editBlogRequest.UrlHandle, Visible = editBlogRequest.Visible,
                Heading = editBlogRequest.Heading,
                Id = editBlogRequest.Id,
            };
            var AllSelecttags = new List<Tag>();
            foreach (var item in editBlogRequest.SelectedItems)
            {
                if(Guid.TryParse(item,  out Guid id)){
                    var Tagname = await tagRepository.GetTagAsync(id);
                    if (Tagname != null) { 
                        AllSelecttags.Add(Tagname);
                    }
                }
            }
            Data.Tags = AllSelecttags;
            var IsSuccess = await blogPostRepository.UpdateBlogAsync(Data);
            if (IsSuccess != null)
            {
               return RedirectToAction("ListOfBlogPosts");
            }
            else {return RedirectToAction("ListOfBlogPosts"); }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlog(EditBlogRequest editBlogRequest)
        {
            var Isdeleted = await blogPostRepository.DeleteBlogAsync(editBlogRequest.Id);
            if (Isdeleted != null)
            {
                return RedirectToAction("ListOfBlogPosts");
            }
            return RedirectToAction("Error","Home");
        }
    }
}
