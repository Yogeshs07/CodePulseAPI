using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repository.Implementation;
using CodePulse.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository,
            ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostRequestDto request)
        {
            //DTO to Domain
            var blogPost = new BlogPost
            {
                Title = request.Title,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishDate = request.PublishDate,
                ShortDiscription = request.ShortDiscription,
                Auther = request.Auther,
                UrlHandle = request.Urlhandle,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories) 
            { 
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory != null) 
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            blogPost = await blogPostRepository.CreateAsync(blogPost);
            //Map Domain to DTO

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDiscription = blogPost.ShortDiscription,
                Auther = blogPost.Auther,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();

            //Map Domain to DTO
            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishDate = blogPost.PublishDate,
                    ShortDiscription = blogPost.ShortDiscription,
                    Auther = blogPost.Auther,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList()
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var existingBlogPosts = await blogPostRepository.GetByIdAsync(id);

            if (existingBlogPosts is null)
            {
                return NotFound();
            }

            //Map Domain to DTO
            var response = new BlogPostDto
            {
                Id = existingBlogPosts.Id,
                Title = existingBlogPosts.Title,
                Content = existingBlogPosts.Content,
                FeaturedImageUrl = existingBlogPosts.FeaturedImageUrl,
                IsVisible = existingBlogPosts.IsVisible,
                PublishDate = existingBlogPosts.PublishDate,
                ShortDiscription = existingBlogPosts.ShortDiscription,
                Auther = existingBlogPosts.Auther,
                UrlHandle = existingBlogPosts.UrlHandle,
                Categories = existingBlogPosts.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            var existingBlogPosts = await blogPostRepository.GetByUrlHandleAsync(urlHandle);

            if (existingBlogPosts is null)
            {
                return NotFound();
            }

            //Map Domain to DTO
            var response = new BlogPostDto
            {
                Id = existingBlogPosts.Id,
                Title = existingBlogPosts.Title,
                Content = existingBlogPosts.Content,
                FeaturedImageUrl = existingBlogPosts.FeaturedImageUrl,
                IsVisible = existingBlogPosts.IsVisible,
                PublishDate = existingBlogPosts.PublishDate,
                ShortDiscription = existingBlogPosts.ShortDiscription,
                Auther = existingBlogPosts.Auther,
                UrlHandle = existingBlogPosts.UrlHandle,
                Categories = existingBlogPosts.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, UpdateBlogPostRequestDto request)
        {
            //Map DTO to Domain
            var blogPost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishDate = request.PublishDate,
                ShortDiscription = request.ShortDiscription,
                Auther = request.Auther,
                UrlHandle = request.Urlhandle,
                Categories = new List<Category>()
            };

            //foreach 
            foreach(var categoryGuid in request.Categories)
            {
                var existingcategories = await categoryRepository.GetById(categoryGuid);

                if(existingcategories != null)
                {
                    blogPost.Categories.Add(existingcategories);
                }
            }

            blogPost = await blogPostRepository.UpdateAsync(blogPost);

            if (blogPost is null)
            {
                return NotFound();
            }

            //Map Domain to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDiscription = blogPost.ShortDiscription,
                Auther = blogPost.Auther,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var blogPost = await blogPostRepository.DeleteAsync(id);

            if (blogPost is null)
            {
                return NotFound();
            }

            //Map Domain to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDiscription = blogPost.ShortDiscription,
                Auther = blogPost.Auther,
                UrlHandle = blogPost.UrlHandle
            };

            return Ok(response);
        }
    }
}
