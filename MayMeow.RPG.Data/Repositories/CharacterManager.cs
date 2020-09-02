using MayMeow.RPG.Entities.World;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Data.Repositories
{
    public class CharacterManager : ICharacterManager
    {
        private readonly ApplicationDbContext _dbContext;

        public CharacterManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Set attributes and location based on race
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public async Task<Character> Prepare(Character character)
        {
            var race = await _dbContext.Races.FirstOrDefaultAsync(r => r.Id == character.RaceId);

            // Set attributes
            character.Strength = race.Strength;
            character.Agility = race.Agility;
            character.Charisma = race.Charisma;
            character.Constitution = race.Constitution;
            character.Intelligence = race.Intelligence;

            // Set character date time informations
            var currentDate = DateTime.UtcNow;
            character.CreatedAt = currentDate;
            character.UpdatedAt = currentDate;

            return character;
        }

        public async Task SetAsActive(Character character, string OwnerId)
        {
            // select all users characters
            var characters = await _dbContext.Characters
                .Where(o => o.OwnerId == OwnerId)
                .ToListAsync();

            for (int i=0; i < characters.Count; i++)
            {
                if (characters[i].IsActive == true && character.Id == characters[i].Id)
                {
                    continue;
                }
                else if (characters[i].IsActive == false && character.Id == characters[i].Id)
                {
                    characters[i].IsActive = true;
                }
                else
                {
                    characters[i].IsActive = false;
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
