using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeachBook.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
       
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImgUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public string Visible { get; set; }
        public IEnumerable<SelectListItem> Tags { get; set; }
        public string[] SelectedItems { get; set; } = Array.Empty<string>();
    }
}
