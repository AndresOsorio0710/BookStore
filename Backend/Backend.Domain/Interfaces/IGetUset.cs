using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
    public interface IGetUset<TEntity>
    {
        TEntity Get(string user, string password);
    }
}
