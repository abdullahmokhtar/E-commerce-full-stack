﻿using Backend.BLL;

namespace Backend.Repository.Interfaces
{
    public interface IGenericRepository <T> where T : BaseEntity
    {
        Task<PagedResponse<T>> GetAll(QueryObject queryObject, params string[] includeProperties);
        Task<T> GetById(int id, params string[] includeProperties);
    }
}
