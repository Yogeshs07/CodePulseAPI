using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repository.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost category);

        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetByIdAsync(Guid id);

        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);

        Task<BlogPost?> UpdateAsync(BlogPost id);

        Task<BlogPost?> DeleteAsync(Guid id);

    }
}
