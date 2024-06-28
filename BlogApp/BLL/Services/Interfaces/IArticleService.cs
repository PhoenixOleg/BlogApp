using BlogApp.BLL.ViewModels.Article;
using BlogApp.DAL.Models;
using Microsoft.Extensions.Hosting;

namespace BlogApp.BLL.Services.Interfaces
{
    public interface IArticleService
    {
        // 1. Создание статьи
        //Task<CreateArticleViewModel> CreateArticle();
        Task<Guid> CreateArticle(CreateArticleViewModel model);

        // 2. Редактирование статьи
        Task<EditArticleViewModel> EditArticle(Guid Id);
        Task EditArticle(EditArticleViewModel model, Guid Id);

        // 3. Удаление статьи
        Task DeleteArticle(Guid id);

        // 4. Поиск статьи
        Task<List<ArticleEntity>> ShowAllArticles();
        Task<ArticleEntity> ShowArticleByAuthor(Guid id);
    }
}
