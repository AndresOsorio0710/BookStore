using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Interfaces;

namespace Backend.Aplications.Interfaces
{
    public interface IServiceBase<TEntity, TEntityID> :
        IAdd<TEntity>, IDelete<TEntityID>, IEdit<TEntity>, IGet<TEntity, TEntityID>, IGetPagination
    {
    }
}
