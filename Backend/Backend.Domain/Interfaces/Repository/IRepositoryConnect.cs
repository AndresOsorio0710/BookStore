using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces.Repository
{
    public interface IRepositoryConnect<TEntity, TEntityID> :
        IConnect<TEntity, TEntityID>, ITransaction
    {
    }
}
