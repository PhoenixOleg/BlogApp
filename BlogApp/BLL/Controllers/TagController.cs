using AutoMapper;
using BlogApp.BLL.Services;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Comment;
using BlogApp.BLL.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogApp.BLL.Controllers
{
    public class TagController : Controller
    {
        private ITagService _tagService;
        private IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [Route("AddTag")]
        [HttpGet]
        [Authorize]
        public IActionResult AddTag()
        {
            return View(new CreateTagViewModel());
        }

        [Route("AddTag")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTag(CreateTagViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _tagService.CreateTag(model);
                return RedirectToAction("ShowTags", "Tag");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("EditTag")]
        [HttpGet]
        [Authorize]
        public IActionResult EditTag(Guid id, string name)
        {
            var viewEdit = new EditTagViewModel { Id = id, Name = name };
            return View(viewEdit);
        }

        [Route("EditTag")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditTag(EditTagViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _tagService.EditTag(model);
                return RedirectToAction("ShowTags", "Tag");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        [Route("DeleteTag")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            //ToDo Сделать подтверждение удаления
            await _tagService.DeleteTag(id);

            return RedirectToAction("ShowTags", "Tag");
        }

        [Route("ShowAllTags")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowAllTags()
        {
            var tags = await _tagService.GetAllTags();

            List<ShowTagViewModel> showTagViewModel = new();
            foreach (var tag in tags)
            {
                showTagViewModel.Add(_mapper.Map<ShowTagViewModel>(tag));
            }

            return View(showTagViewModel);
        }

        [Route("ShowTag")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowTag(Guid id)
        {
            var viewShow = new ShowTagViewModel { Id = id };

            var tagEntitie = await _tagService.GetTagById(id);
            var showTagViewModel = _mapper.Map<ShowCommentViewModel>(tagEntitie);
            return View(showTagViewModel);
        }
    }
}
