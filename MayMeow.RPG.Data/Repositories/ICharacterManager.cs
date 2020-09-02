using MayMeow.RPG.Entities.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Data.Repositories
{
    public interface ICharacterManager
    {
        public Task<Character> Prepare(Character character);
    }
}
