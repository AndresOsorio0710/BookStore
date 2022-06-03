using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
    public interface IGet<TEntity, TEntityID>
    {
        List<TEntity> GetAll();
        TEntity Get(TEntityID entityID);
    }
}
