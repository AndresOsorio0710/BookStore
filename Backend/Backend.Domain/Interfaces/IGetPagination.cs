using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
    public interface IGetPagination
    {
        Task<Object> Get(int page, int rows);
        Task<Object> Get(int page, int rows, string search);
    }
}
