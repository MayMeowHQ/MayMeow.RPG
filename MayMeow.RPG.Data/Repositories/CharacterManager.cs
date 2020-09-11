using MayMeow.RPG.Entities.Identity;
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

        public async Task<Character> GetActiveCharacter(ApplicationUser owner)
        {
            return await _dbContext.Characters.FirstOrDefaultAsync(c => c.OwnerId == owner.Id && c.IsActive == true);
        }

        public async Task<Location> GetCurrentLocation(Character character)
        {
            var location = await _dbContext.Locations
                .Include(l => l.ParrentLocations).ThenInclude(c => c.Parent)
                .Include(l => l.ChildLocations).ThenInclude(p => p.Child)
                .Include(l => l.Characters)
                .FirstOrDefaultAsync(m => m.Id == character.CurrentLocationId);

            return location;
        }

        public async Task MoveTo(Character character, int locationId)
        {
            character.CurrentLocationId = locationId;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Set attributes and location based on race
        /// </summary>
        /// <param name="character"></param>
        /// <param name="playable"></param>
        /// <returns></returns>
        public async Task<Character> Prepare(Character character, bool playable = false)
        {
            var race = await _dbContext.Races.FirstOrDefaultAsync(r => r.Id == character.RaceId);

            // Settings for NPC characters
            character.Playable = playable;

            // Set attributes
            character.Strength = race.Strength;
            character.Agility = race.Agility;
            character.Charisma = race.Charisma;
            character.Constitution = race.Constitution;
            character.Intelligence = race.Intelligence;

            // Set starting location as current location
            character.CurrentLocationId = race.StartingLocationId;

            // Set character date time informations
            var currentDate = DateTime.UtcNow;
            character.CreatedAt = currentDate;
            character.UpdatedAt = currentDate;

            return character;
        }

        /// <summary>
        /// Set Character as Active
        /// 1 User can have only 1 character active at time
        /// </summary>
        /// <param name="character"></param>
        /// <param name="OwnerId"></param>
        /// <returns></returns>
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
                    // Do nothing character we want is already active
                    continue;
                }
                else if (characters[i].IsActive == false && character.Id == characters[i].Id)
                {
                    // This is character we want so active it
                    characters[i].IsActive = true;
                }
                else
                {
                    // Deactivate all other characters
                    characters[i].IsActive = false;
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
