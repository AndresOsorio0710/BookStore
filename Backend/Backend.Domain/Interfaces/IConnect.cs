using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
    public interface IConnect<TEntity, TEntityID>
    {
        TEntity LogIn(TEntity entity);

        void LogOut(TEntityID entityID);

        TEntity Get(TEntityID entityID);
    }
}
