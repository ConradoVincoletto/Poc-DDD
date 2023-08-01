using PocDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocDDD.Domain.Interfaces
{
    public interface IUserRespository
    {
        Task<int> InsertAsync(User user);
    }
}
