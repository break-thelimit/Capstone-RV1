using BusinessObjects.Model;
using DataAccess.Dtos.PlayerDto;
using DataAccess.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.PlayerRepositories
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        Task<string> GetTotalPlayerToday();
    }
}
