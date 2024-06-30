using AutoMapper;
using BlogApp.BLL.Services;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Article;
using BlogApp.BLL.ViewModels.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.BLL.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService _commentService;
        private IMapper _mapper;


        public CommentController (ICommentService commentService, IMapper mapper )
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [Route("AddComment")]
        [HttpGet]
        [Authorize]
        public IActionResult CreateComment(Guid ArticleId)
        {
            return View(new CreateCommentViewModel());
        }

        [Route("AddComment")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CreateCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.CreateComment(model);
                //TODO Перенаправление на просмотр статьи По ID
                return RedirectToAction("ShowAllArticles", "Article");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("EditComment")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditComment(Guid id)
        {
            var viewShow = new ShowCommentViewModel { Id = id };
            var commentEntitie = await _commentService.ShowComment(viewShow);

            var editCommentViewModel = _mapper.Map<EditCommentViewModel>(commentEntitie);
            return View(editCommentViewModel);
        }

        [Route("EditComment")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditComment(EditCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.EditComment(model);
                //TODO Перенаправление на просмотр статьи По ID
                return RedirectToAction("ShowAllArticles", "Article");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("ShowAllComments")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowAllComments()
        {
            var commentEntities = await _commentService.ShowAllComments();

            List<ShowCommentViewModel> showCommentViewModel = new();
            foreach (var comment in commentEntities)
            {
                showCommentViewModel.Add(_mapper.Map<ShowCommentViewModel>(comment));
            }

            return View(showCommentViewModel);
        }

        [Route("ShowComment")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowComment(Guid id)
        {
            var viewShow = new ShowCommentViewModel { Id = id };

            var commentEntitie = await _commentService.ShowComment(viewShow);
            var showCommentViewModel = _mapper.Map<ShowCommentViewModel>(commentEntitie);
            return View(showCommentViewModel);
        }

        [Route("ShowAuthorComments")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowAuthorComments(string id)
        {
            var commentEntities = await _commentService.ShowCommentsByAuthorId(id);

            List<ShowCommentViewModel> showCommentViewModel = new();
            foreach (var comment in commentEntities)
            {
                showCommentViewModel.Add(_mapper.Map<ShowCommentViewModel>(comment));
            }

            return View(showCommentViewModel);
        }

        [Route("DeleteComment")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            //ToDo Сделать подтверждение удаления
            await _commentService.DeleteComment(id);

            return RedirectToAction("ShowAllArticles", "Article");
        }
    }
}
