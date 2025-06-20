namespace CodePulse.API.Models.DTO
{
    public class CreateBlogPostRequestDto
    {
        public string Title { get; set; }

        public string ShortDiscription { get; set; }

        public string Content { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string Urlhandle { get; set; }

        public DateTime PublishDate { get; set; }

        public string Auther { get; set; }

        public bool IsVisible { get; set; }

        public Guid[] Categories { get; set; }
    }
}
