using BusinessObjects.Model;
using DataAccess.Dtos.RankDto;
using DataAccess.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.RankRepositories
{
    public interface IRankRepository : IGenericRepository<Rank>
    {
        //Task<List<GetRankDto>> GetAllRankAsync();
    }
}
