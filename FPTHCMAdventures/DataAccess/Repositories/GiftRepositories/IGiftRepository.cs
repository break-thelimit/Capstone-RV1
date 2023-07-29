using BusinessObjects.Model;
using DataAccess.Dtos.GiftDto;
using DataAccess.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.GiftRepositories
{
    public interface IGiftRepository : IGenericRepository<Gift>
    {
    }
}
