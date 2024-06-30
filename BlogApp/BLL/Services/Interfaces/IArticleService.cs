using BlogApp.BLL.ViewModels.Article;
using BlogApp.DAL.Models;
using Microsoft.Extensions.Hosting;

namespace BlogApp.BLL.Services.Interfaces
{
    public interface IArticleService
    {
        // 1. Создание статьи
        Task<Guid> CreateArticle(CreateArticleViewModel model);

        // 2. Редактирование статьи
        Task EditArticle(EditArticleViewModel model);

        // 3. Удаление статьи
        Task DeleteArticle(Guid id);

        // 4. Поиск статьи
        Task<List<ArticleEntity>> ShowAllArticles();
        Task<ArticleEntity> ShowArticle(ShowArticleViewModel model);
        Task<List<ArticleEntity>> ShowArticleByAuthorId(string id);
    }
}
