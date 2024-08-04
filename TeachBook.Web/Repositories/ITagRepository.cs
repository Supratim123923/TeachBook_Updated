using TeachBook.Web.Models.Domain_Model;

namespace TeachBook.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagAsync(Guid Id);
        Task<Tag> AddTagAsync(Tag Tag);
        Task<Tag?> UpdateTagAsync(Tag Tag);
        Task<Tag?> DeleteTagAsync(Guid Id);

    }
}
