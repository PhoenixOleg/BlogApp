using BlogApp.BLL.ViewModels.Article;
using BlogApp.BLL.ViewModels.Comment;
using BlogApp.DAL.Models;

namespace BlogApp.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        // 1. Добавление коммента
        Task<Guid> CreateComment(CreateCommentViewModel model);

        // 2. Редактирование коммента
        Task EditComment(EditCommentViewModel model);
            
        // 3. Удаление коммента
        Task DeleteComment(Guid id);

        // 4. Поиск коммента
        Task<List<CommentEntity>> ShowAllComments();
        Task<CommentEntity> ShowComment(ShowCommentViewModel model);
        Task<List<CommentEntity>> ShowCommentsByAuthorId(string id);
    }
}
