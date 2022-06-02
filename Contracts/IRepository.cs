using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts

{
    public interface IRepositoryBase<T> where T : class
    {
        // this interface is like a collection, it does not have methods like update and save
        // repositories should return IEnumerable not Iquaryable
        IEnumerable<T> FindAll(bool trackChanges);
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expresssion, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
