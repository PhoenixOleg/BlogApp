using BlogApp.BLL.ViewModels.User;
using BlogApp.DAL.Models;

namespace BlogApp.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task EditAccount(EditUserViewModel model);

        Task RemoveAccount(string id);

        Task<List<ShowUserViewModel>> ShowAllAccounts();
        Task<ShowUserViewModel> ShowAccountById(string id);
    }
}
