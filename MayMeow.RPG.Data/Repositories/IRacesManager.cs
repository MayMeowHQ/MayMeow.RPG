using MayMeow.RPG.Entities.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Data.Repositories
{
    public interface IRacesManager
    {
        public Task<List<Race>> All();

        public Task<Race> Details(int? Id);
    }
}
