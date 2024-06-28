using AutoMapper;
using Azure;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Tag;
using BlogApp.DAL.Models;
using BlogApp.DAL.Repositories;
using BlogApp.DAL.UoW.Interfaces;

namespace BlogApp.BLL.Services
{
    public class TagService : ITagService
    {
        //private readonly TagRepository _targetRepo;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public TagService (IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateTag(CreateTagViewModel model)
        {
            //Мапим бизнес-модель в модель данных
            var tag = _mapper.Map<TagEntity>(model);

            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            //ToDo Можно вставить дубль
            await _targetRepo.CreateTagAsync(tag);
            return tag.Id;
        }

        public async Task DeleteTag(Guid id)
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var tag = await _targetRepo.GetTagByID(id); 
            if (tag != null)
            {
                //await _targetRepo.Delete(tag);
                await _targetRepo.DeleteTagAsync(tag);
            }
            else
            {
                //ToDo Что-то не так...
                throw new ArgumentNullException();
            }
        }

        public async Task EditTag(EditTagViewModel model)
        {
            //Мапим бизнес-модель в модель данных
            var tag = _mapper.Map<TagEntity>(model);

            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            //ToDo Можно вставить дубль

            //await _targetRepo.Update(tag);
            await _targetRepo.UpdateTagAsync(tag);
        }

        public async Task<List<TagEntity>> GetAllTags()
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var tags = await _targetRepo.GetAllTagsAsynс();
            return tags;
        }

        private TagRepository GetRepo()
        {
            var _targetRepo = _unitOfWork.GetRepository<TagEntity>() as TagRepository;

            if (_targetRepo is null)
            {
                //ToDo Что-то не так...
                throw new ArgumentNullException();
            }

            return _targetRepo;
        }
    }
}
