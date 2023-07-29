using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.NPCDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.NPCRepository
{
    public class NpcRepository : GenericRepository<Npc>, INpcRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public NpcRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NpcDto> GetNpcByName(string npcName)
        {
            var npc = await _dbContext.Set<Npc>().FirstOrDefaultAsync(x => x.Name == npcName);          
            return _mapper.Map<NpcDto>(npc);
        }

    }
}
