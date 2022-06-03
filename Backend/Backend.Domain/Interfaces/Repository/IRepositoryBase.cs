using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity, TEntityID> :
        IAdd<TEntity>, IDelete<TEntityID>, IEdit<TEntity>, IGet<TEntity, TEntityID>, IGetPagination, ITransaction
    {
    }
}
