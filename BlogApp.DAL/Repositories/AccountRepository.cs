using BlogApp.DAL.Models;
using BlogApp.DAL.UoW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories
{
    public class AccountRepository : Repository<UserEntity>
    {
        public AccountRepository(BlogDBContext db) : base(db)
        {
        }

        public async Task<List<UserEntity>> GetAllAccountsAsynс()
        {
            //ToDo Добавить после отладки роли
            var users = Set.AsQueryable();
            if (users is null)
            {
                return new List<UserEntity>();
            }
            else
            {
                return await users.ToListAsync();
            }
        }

        public async Task<UserEntity> GetAccountByIdAsync(string id)
        {
            var user = Set.AsQueryable().Where(u => u.Id == id);
            if (user is null)
            {
                return new UserEntity();
            }
            else
            {
                return await user.FirstAsync();
            }
        }

        public async Task UpdateAccountAsync(UserEntity user)
        {
            await Update(user);
        }

        public async Task DeleteAccountAsync(UserEntity user)
        {
            await Delete(user);
        }
    }
}
