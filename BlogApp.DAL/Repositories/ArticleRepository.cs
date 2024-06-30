using BlogApp.DAL.Models;
using BlogApp.DAL.UoW;
using BlogApp.DAL.UoW.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Repositories
{
    public class ArticleRepository : Repository<ArticleEntity>
    {
        public ArticleRepository(BlogDBContext db) : base(db)
        {
        }

        /// <summary>
        /// Создание статьи (публикация)
        /// </summary>
        /// <param name="article">Экземпляр создаваемой статьи</param>
        /// <returns>Асинхронная задача</returns>
        public async Task CreateArticleAsync(ArticleEntity article)
        {
            await Create(article);
        }

        /// <summary>
        /// Редактирование статьи
        /// </summary>
        /// <param name="article">Экземпляр Редактируемой статьи</param>
        /// <returns>Асинхронная задача</returns>
        public async Task UpdateArticleAsync(ArticleEntity article)
        {
            await Update(article);
        }

        /// <summary>
        /// Удаление статьи
        /// </summary>
        /// <param name="article">кземпляр удаляемой статьи</param>
        /// <returns>Асинхронная задача</returns>
        public async Task DeleteArticleAsync(ArticleEntity article)
        {
            await Delete(article);
        }

        /// <summary>
        /// Получение статьи по ID автора
        /// </summary>
        /// <param name="id">Идентификатор статьи</param>
        /// <returns>Список найденных статей или пустой список</returns>
        public async Task<List<ArticleEntity>> GetArticleByAuthorIdAsync(string id)
        {
            //ToDo Добавить после отладки комменты
            var articles = Set.Include(t => t.Tags).AsQueryable().Where(a => a.UserId == id);
             
            if (articles is null)
            // articles?.Count() == 0
            {
                return new List<ArticleEntity>();
            }
            else
            {
                return await articles.ToListAsync();
            }
        }

        public async Task<List<ArticleEntity>> GetAllArticlesAsynс()
        {
            //ToDo Добавить после отладки комменты
            var articles = Set.Include(t => t.Tags).AsQueryable();
            if (articles is null)
            {  
                return new List<ArticleEntity>(); 
            }
            else
            {
                return await articles.ToListAsync();
            }
        }

        public async Task<ArticleEntity> GetArticleByIDAsync(Guid id)
        {
            var article = Set.Include(t => t.Tags).Include(c => c.Comments).AsQueryable().Where(a => a.Id == id);
            if (article is null)
            {
                return new ArticleEntity();
            }
            else
            {
                return await article.FirstAsync();
            }
        }
    }
}
