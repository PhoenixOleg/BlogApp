using AutoMapper;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Article;
using BlogApp.DAL.Models;
using BlogApp.DAL.Repositories;
using BlogApp.DAL.UoW.Interfaces;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.BLL.Services
{
    public class ArticleService : IArticleService
    {
        //private ArticleRepository _mainRepo;
        //private ITagRepository _tagRepo;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private UserManager<UserEntity> _userManager;
        private SignInManager<UserEntity> _signInManager;

        public ArticleService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _mapper = mapper; 
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Guid> CreateArticle(CreateArticleViewModel model)
        {
            string? userName = _signInManager?.Context?.User?.Identity?.Name;
            
            if (userName is null)
            {
                return Guid.Empty;
            }

            UserEntity? user = await _userManager.FindByNameAsync(userName);

            model.UserId = user.Id;
            model.PostDate = DateTime.Now;
            
            //Мапим бизнес-модель в модель данных
            var article = _mapper.Map<ArticleEntity>(model);

            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            await _targetRepo.CreateArticleAsync(article);
            //ToDo Надо дозаполнить тегами

            return article.Id;
        }

        public async Task DeleteArticle(Guid id)
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var article = await _targetRepo.GetArticleByIDAsync(id);
            if (article != null)
            {
                await _targetRepo.DeleteArticleAsync(article);
            }
            else
            {
                //ToDo Что-то не так...
                throw new ArgumentNullException();
            }
        }

        public async Task EditArticle(EditArticleViewModel model) //ToDo
        {
            //string? userName = _signInManager?.Context?.User?.Identity?.Name;

            //if (userName is null)
            //{
            //    return;
            //}

            //UserEntity? user = await _userManager.FindByNameAsync(userName);

            //model.UserId = user.Id;
            model.ModifyDate = DateTime.Now;

            //Мапим бизнес-модель в модель данных
            var article = _mapper.Map<ArticleEntity>(model);

            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            await _targetRepo.UpdateArticleAsync(article);
            //ToDo Надо дозаполнить тегами
        }

        public async Task<List<ArticleEntity>> ShowAllArticles()
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var articles = await _targetRepo.GetAllArticlesAsynс();
            
            return articles;
        }

        public async Task<ArticleEntity> ShowArticle(ShowArticleViewModel model)
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var article = await _targetRepo.GetArticleByIDAsync(model.Id);

            return article;
        }

        public Task<ArticleEntity> ShowArticleByAuthor(Guid id)
        {
            throw new NotImplementedException();
        }

        private ArticleRepository GetRepo()
        {
            var _articleRepo = _unitOfWork.GetRepository<ArticleEntity>() as ArticleRepository;

            if (_articleRepo is null)
            {
                //ToDo Что-то не так...
                throw new ArgumentNullException();
            }

            return _articleRepo;
        }
    }
}
