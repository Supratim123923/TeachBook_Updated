using Microsoft.EntityFrameworkCore;
using TeachBook.Web.Data;
using TeachBook.Web.Models.Domain_Model;

namespace TeachBook.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TeachBookDBContext _teachBookDBContext;

        public TagRepository(TeachBookDBContext teachBookDBContext)
        {
            this._teachBookDBContext = teachBookDBContext;
        }
        public async Task<Tag> AddTagAsync(Tag Tag)
        {
            await _teachBookDBContext.Tags.AddAsync(Tag);
            await _teachBookDBContext.SaveChangesAsync();
            return Tag;
        }

        public async Task<Tag?> DeleteTagAsync(Guid Id)
        {
            var FindTag = await _teachBookDBContext.Tags.FindAsync(Id);
            if (FindTag != null)
            {
                _teachBookDBContext.Tags.Remove(FindTag);
                await _teachBookDBContext.SaveChangesAsync(true);
                return FindTag;
            }
            else { return null; }
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
           return await _teachBookDBContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagAsync(Guid Id)
        {
            var Tag = await _teachBookDBContext.Tags.FirstOrDefaultAsync(x => x.Id == Id);
            return Tag;
        }

        public async Task<Tag?> UpdateTagAsync(Tag Tag)
        {
            var FindTag = await _teachBookDBContext.Tags.FindAsync(Tag.Id);
            if (FindTag != null)
            {
                FindTag.DisplayName = Tag.DisplayName;
                FindTag.Name = Tag.Name;
                await _teachBookDBContext.SaveChangesAsync();
                return FindTag;
            }
            else
            {
               return null;
            }


        }
    }
}
