using MayMeow.RPG.Entities.World;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Data.Repositories
{
    public class RacesManager : IRacesManager
    {
        public readonly ApplicationDbContext _dbContext;

        public RacesManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Race>> All()
        {
            var races = _dbContext.Races.Include(l => l.StartingLocation);

            return await races.ToListAsync();
        }

        public async Task<Race> Details(int? id)
        {
            return await _dbContext.Races
                .Include(r => r.StartingLocation)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
