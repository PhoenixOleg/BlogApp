using AutoMapper;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.BLL.ViewModels.Article;
using BlogApp.BLL.ViewModels.User;
using BlogApp.DAL.Models;
using BlogApp.DAL.Repositories;
using BlogApp.DAL.UoW.Interfaces;

namespace BlogApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task EditAccount(EditUserViewModel model)
        {
            //Мапим бизнес-модель в модель данных
            var account = _mapper.Map<UserEntity>(model);

            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            await _targetRepo.UpdateAccountAsync(account);
        }

        public async Task<ShowUserViewModel> ShowAccountById(string id)
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var accountEntity = await _targetRepo.GetAccountByIdAsync(id);
            if (accountEntity != null)
            {
                return _mapper.Map<ShowUserViewModel>(accountEntity);
            }
            else
            {
                return new ShowUserViewModel();
            }
        }

        public async Task<List<ShowUserViewModel>> ShowAllAccounts()
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var accountsEntity = await _targetRepo.GetAllAccountsAsynс();

            List<ShowUserViewModel> userShowViewModel = new();
            foreach (var account in accountsEntity)
            {
                userShowViewModel.Add(_mapper.Map<ShowUserViewModel>(account));
            }
            return userShowViewModel;
        }

        public async Task RemoveAccount(string id)
        {
            //Получаем целевой репозиторий из UoW
            var _targetRepo = GetRepo();

            var account = await _targetRepo.GetAccountByIdAsync(id);
            if (account != null)
            {
                await _targetRepo.DeleteAccountAsync(account);
            }
            else
            {
                //ToDo Что-то не так...
                throw new ArgumentNullException();
            }
        }

        private AccountRepository GetRepo()
        {
            var _userRepo = _unitOfWork.GetRepository<UserEntity>() as AccountRepository;

            if (_userRepo is null)
            {
                //ToDo Что-то не так...
                throw new ArgumentNullException();
            }

            return _userRepo;
        }
    }
}
