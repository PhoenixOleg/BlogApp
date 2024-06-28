using AutoMapper;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Article;
using BlogApp.DAL.Models;
using BlogApp.DAL.Repositories;
using BlogApp.DAL.UoW.Interfaces;

namespace BlogApp.BLL.Services
{
    public class ArticleService : IArticleService
    {
        //private ArticleRepository _mainRepo;
        //private ITagRepository _tagRepo;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public ArticleService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper; 
            _unitOfWork = unitOfWork;
        }

        //public Task<CreateArticleViewModel> CreateArticle()
        //{
        //    ArticleEntity articleEntity = new();

        //    //ToDo Надо дозаполнить тегами


        //    //Возврат пустой вью модели
        //    CreateArticleViewModel viewModel = new CreateArticleViewModel
        //    {

        //    };

        //    return Task.FromResult(viewModel);
        //}

        public async Task<Guid> CreateArticle(CreateArticleViewModel model)
        {
            //Мапим бизнес-модель в модель данных
            var article = _mapper.Map<ArticleEntity>(model);

            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            await _targetRepo.CreateArticleAsync(article);
            //ToDo Надо дозаполнить тегами

            //ArticleEntity article  = new ArticleEntity
            //{
            //    Id = model.Id,
            //    Title = model.Title,
            //    Description = model.Description,
            //    Content = model.Content,
            //    PostDate = model.PostDate,
            //    UserId = model.AuthorId
            //};

            return article.Id;
        }

        public Task DeleteArticle(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EditArticleViewModel> EditArticle(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task EditArticle(EditArticleViewModel model, Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticleEntity>> ShowAllArticles()
        {
            throw new NotImplementedException();
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
