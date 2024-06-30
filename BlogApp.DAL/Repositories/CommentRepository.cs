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
    public class CommentRepository : Repository<CommentEntity>
    {
        public CommentRepository(BlogDBContext db) : base(db)
        {
        }

        /// <summary>
        /// Добавление комментария
        /// </summary>
        /// <param name="comment">Экземпляр комментария</param>
        /// <returns>Асинхронная задача</returns>
        public async Task CreateCommentAsync(CommentEntity comment)
        {
            await Create(comment);
        }

        /// <summary>
        /// Редактирование комментария
        /// </summary>
        /// <param name="comment">Экземпляр редактируемго комментария</param>
        /// <returns>Асинхронная задача</returns>
        public async Task UpdateCommentAsync(CommentEntity comment)
        {
            await Update(comment);
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="comment">Экземпляр удаляемого комментария</param>
        /// <returns>Асинхронная задача</returns>
        public async Task DeleteCommentAsync(CommentEntity comment)
        {
            await Delete(comment);
        }


        /// <summary>
        /// Получение комментария по ID автора
        /// </summary>
        /// <param name="id">Идентификатор автора</param>
        /// <returns>Список найденных комментариев или пустой список</returns>
        public async Task<List<CommentEntity>> GetCommentByAuthorIdAsync(string id)
        {
            //ToDo Добавить после отладки комменты
            var comments = Set.Include(t => t.Article).AsQueryable().Where(a => a.UserId == id);

            if (comments is null)
            {
                return new List<CommentEntity>();
            }
            else
            {
                return await comments.ToListAsync();
            }
        }

        public async Task<List<CommentEntity>> GetAllCommentsAsynс()
        {
            var comments = Set.Include(t => t.Article).AsQueryable();
            if (comments is null)
            {
                return new List<CommentEntity>();
            }
            else
            {
                return await comments.ToListAsync();
            }
        }

        public async Task<CommentEntity> GetCommentByIDAsync(Guid id)
        {
            var comment = Set.Include(x => x.Article).AsQueryable().Where(a => a.Id == id);
            if (comment is null)
            {
                return new CommentEntity();
            }
            else
            {
                return await comment.FirstAsync();
            }
        }
    }
}
