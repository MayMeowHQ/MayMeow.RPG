using MayMeow.RPG.Entities.Identity;
using MayMeow.RPG.Entities.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Data.Repositories
{
    public interface ICharacterManager
    {
        public Task<Character> GetActiveCharacter(ApplicationUser owner);
        public Task<Character> Prepare(Character character, bool playable = false);

        public Task SetAsActive(Character character, string OwnerId);

        public Task<Location> GetCurrentLocation(Character character);

        public Task MoveTo(Character character, int locationId);
    }
}
