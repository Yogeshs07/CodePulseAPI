﻿namespace CodePulse.API.Models.DTO
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ShortDiscription { get; set; }

        public string Content { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishDate { get; set; }

        public string Auther { get; set; }

        public bool IsVisible { get; set; }

        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
