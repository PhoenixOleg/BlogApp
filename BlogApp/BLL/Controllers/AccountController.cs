using AutoMapper;
using BlogApp.BLL.Services;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Article;
using BlogApp.BLL.ViewModels.User;
using BlogApp.DAL.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.BLL.Controllers
{
    public class AccountController : Controller
    {
        IAccountService _accountService;
        IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        //Методы Register И Login идут через библиотеку Identity

        [Route("EditAccount")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditAccount(string id)
        {
            var viewEdit = new EditUserViewModel { Id = id };
            var accountShow = await _accountService.ShowAccountById(id);

            var editArticleViewModel = _mapper.Map<EditArticleViewModel>(accountShow);
            return View(editArticleViewModel);
        }

        [Route("EditAccount")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditAccount(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _accountService.EditAccount(model);
                return RedirectToAction("ShowAllAccounts", "Account");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("ShowAllAccounts")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowAllAccounts()
        {
            var users = await _accountService.ShowAllAccounts();
            return View(users);
        }

        [Route("ShowAccount")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowArticle(string id)
        {
            var viewShow = await _accountService.ShowAccountById(id);
            return View(viewShow);
        }

        [Route("DeleteAccount")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            //TODO Сделать подтверждение удаления
            //TODO Либо сделать встроенного админа, которого нельзя удалить,
            //либо проверять, если удаляется юзер, являющий последним админом, то отказывать.
            await _accountService.RemoveAccount(id);

            return RedirectToAction("ShowAllAccounts", "Account");
        }
    }
}
