using BlogApp.DAL.Models;
using BlogApp.DAL.UoW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories
{
    public class TagRepository : Repository<TagEntity>
    {
        public TagRepository(BlogDBContext db) : base(db)
        { }

        public async Task CreateTagAsync(TagEntity tag)
        {
            await Create(tag);
        }

        public async Task UpdateTagAsync(TagEntity tag)
        {
            await Update(tag);
        }

        public async Task DeleteTagAsync(TagEntity tag)
        {
            await Delete(tag);
        }

        public async Task<List<TagEntity>> GetAllTagsAsynс()
        {
            var tags = await Set.OrderBy(t => t.Name).ToListAsync();

            if (tags is null)
            {
                return new List<TagEntity>();
            }
            else
            {
                return tags;
            }
        }

        public async Task<TagEntity?> GetTagByIdAsync(Guid id)
        {
            return await Get(id);
        }
    }
}
