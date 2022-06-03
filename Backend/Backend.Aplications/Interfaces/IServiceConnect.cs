using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Interfaces;

namespace Backend.Aplications.Interfaces
{
    public interface IServiceConnect<TEntity, TEntiryID> :
        IConnect<TEntity, TEntiryID>
    {
    }
}
