﻿namespace BlogApp.DAL.UoW.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T?> Get(string id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(T item);
    }
}
