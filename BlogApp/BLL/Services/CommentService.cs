using AutoMapper;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Article;
using BlogApp.BLL.ViewModels.Comment;
using BlogApp.DAL.Models;
using BlogApp.DAL.Repositories;
using BlogApp.DAL.UoW.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.BLL.Services
{
    public class CommentService : ICommentService
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private UserManager<UserEntity> _userManager;
        private SignInManager<UserEntity> _signInManager;

        public CommentService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Guid> CreateComment(CreateCommentViewModel model)
        {
            string? userName = _signInManager?.Context?.User?.Identity?.Name;

            if (userName is null)
            {
                return Guid.Empty;
            }

            UserEntity? user = await _userManager.FindByNameAsync(userName);

            model.UserId = user.Id;
            model.Created = DateTime.Now;

            //Мапим бизнес-модель в модель данных
            var comment = _mapper.Map<CommentEntity>(model);

            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            await _targetRepo.CreateCommentAsync(comment);
            //ToDo Надо дозаполнить тегами

            return comment.Id;
        }

        public async Task DeleteComment(Guid id)
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var comment = await _targetRepo.GetCommentByIDAsync(id);
            if (comment != null)
            {
                await _targetRepo.DeleteCommentAsync(comment);
            }
            else
            {
                //ToDo Что-то не так...
                throw new ArgumentNullException();
            }
        }

        public async Task EditComment(EditCommentViewModel model)
        {
            model.Updated = DateTime.Now;

            //Мапим бизнес-модель в модель данных
            var comment = _mapper.Map<CommentEntity>(model);

            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            await _targetRepo.UpdateCommentAsync(comment);
            //ToDo Надо дозаполнить тегами
        }

        public async Task<List<CommentEntity>> ShowAllComments()
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var comments = await _targetRepo.GetAllCommentsAsynс();

            return comments;
        }

        public async Task<CommentEntity> ShowComment(ShowCommentViewModel model)
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var comment = await _targetRepo.GetCommentByIDAsync(model.Id);

            return comment;
        }

        public async Task<List<CommentEntity>> ShowCommentsByAuthorId(string id)
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var comments = await _targetRepo.GetCommentByAuthorIdAsync(id);

            return comments;
        }

        private CommentRepository GetRepo()
        {
            var _commentRepo = _unitOfWork.GetRepository<CommentEntity>() as CommentRepository;

            if (_commentRepo is null)
            {
                //ToDo Что-то не так...
                throw new ArgumentNullException();
            }

            return _commentRepo;
        }
    }
}
