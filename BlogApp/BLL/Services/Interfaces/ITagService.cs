using Azure;
using BlogApp.BLL.ViewModels.Tag;
using BlogApp.DAL.Models;

namespace BlogApp.BLL.Services.Interfaces
{
    public interface ITagService
    {
        // 1. Создание тега
        Task<Guid> CreateTag(CreateTagViewModel model);

        // 2. Редактирование тега
        Task EditTag(EditTagViewModel model);

        // 3. Удаление тега
        Task DeleteTag(Guid id);

        // 4. Получение тега(ов)
        Task<List<TagEntity>> GetAllTags();
    }
}
