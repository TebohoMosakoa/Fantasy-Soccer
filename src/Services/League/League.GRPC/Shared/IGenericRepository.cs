using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace League.GRPC.Shared
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll(bool isDeleted);
        Task<IEnumerable<T>> GetByName(string name);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
    }
}
