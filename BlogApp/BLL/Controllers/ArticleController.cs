using AutoMapper;
using BlogApp.BLL.Services;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Article;
using BlogApp.BLL.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.BLL.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleService _articleService;
        private IMapper _mapper;

        public ArticleController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }

        [Route("AddArticle")]
        [HttpGet]
        [Authorize]
        public IActionResult CreateArticle()
        {
            return View(new CreateArticleViewModel());
        }

        [Route("AddArticle")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateArticle(CreateArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _articleService.CreateArticle(model);
                return RedirectToAction("ShowAllArticles", "Article");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("EditArticle")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditArticle(Guid id)
        {
            var viewShow = new ShowArticleViewModel { Id = id };
            var articleEntitie = await _articleService.ShowArticle(viewShow);

            var editArticleViewModel = _mapper.Map<EditArticleViewModel>(articleEntitie);
            return View(editArticleViewModel);
        }

        [Route("EditArticle")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditArticle(EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _articleService.EditArticle(model);
                return RedirectToAction("ShowAllArticles", "Article");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("ShowAllArticles")]
        [HttpGet]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowAllArticles()
        {
            var articleEntities = await _articleService.ShowAllArticles();

            List<ShowArticleViewModel> showArticleViewModel = new();
            foreach (var article in articleEntities)
            {
                showArticleViewModel.Add(_mapper.Map<ShowArticleViewModel>(article));
            }

            return View(showArticleViewModel);
        }

        [Route("ShowArticle")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowArticle(Guid id)
        {
            var viewShow = new ShowArticleViewModel { Id = id };

            var articleEntitie = await _articleService.ShowArticle(viewShow);
            var showArticleViewModel = _mapper.Map<ShowArticleViewModel>(articleEntitie);
            return View(showArticleViewModel);
        }

        [Route("ShowAuthorArticles")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowAuthorArticles(string id)
        {
            var articleEntities = await _articleService.ShowArticleByAuthorId(id);

            List<ShowArticleViewModel> showArticleViewModel = new();
            foreach (var article in articleEntities)
            {
                showArticleViewModel.Add(_mapper.Map<ShowArticleViewModel>(article));
            }

            return View(showArticleViewModel);
        }

        [Route("DeleteArticle")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            //ToDo Сделать подтверждение удаления
            await _articleService.DeleteArticle(id);

            return RedirectToAction("ShowAllArticles", "Article");
        }
    }
}
