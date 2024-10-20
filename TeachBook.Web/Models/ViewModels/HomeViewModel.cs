using TeachBook.Web.Models.Domain_Model;

namespace TeachBook.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Tag> Tags { get; set; }
        public List<BlogPost> Blogs { get; set; }
    }
}
