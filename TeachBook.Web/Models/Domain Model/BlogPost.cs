﻿namespace TeachBook.Web.Models.Domain_Model
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImgUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get;set; }
        public string Author { get; set; }
        public string Visible { get; set; }
        public ICollection<Tag> Tags { get; set; }


    }
}
