using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.BLL.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleService _articleService;

        public ArticleController(IArticleService articleService ) 
        {
            _articleService = articleService;
        }

        [Route("AddArticle")]
        [HttpGet]
        public IActionResult CreateArticle()
        {
            var model = new CreateArticleViewModel(); //await _articleService.CreateArticle();

            return View(model);
        }

        [Route("AddArticle")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult CreateArticle(CreateArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = _articleService.CreateArticle(model);
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
