﻿using BlogApp.DAL.UoW.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.UoW
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _db;

        public DbSet<T> Set
        {
            get;
            private set;
        }

        public Repository(BlogDBContext db)
        {
            _db = db;
            var set = _db.Set<T>();
            set.Load();

            Set = set;
        }

        public async Task Create(T item)
        {
            await Set.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(T item)
        {
            Set.Remove(item); //Нет асинхронного аналога
            await _db.SaveChangesAsync();
        }

        public async Task<T?> Get(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Set;
        }

        public async Task Update(T item)
        {
            var keyProperty = _db.Model?.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties[0];
            var keyValue = keyProperty?.PropertyInfo?.GetValue(item);

            var existingEntity = await Set.FindAsync(keyValue);
            if (existingEntity != null)
            {
                _db.Entry(existingEntity).CurrentValues.SetValues(item);
            }
            else
            {
                Set.Update(item);
            }
            await _db.SaveChangesAsync();
        }

        //public async Task Update(T item)
        //{
        //    Set.Update(item);//Нет асинхронного аналога
        //    await _db.SaveChangesAsync();
        //}
    }

}
