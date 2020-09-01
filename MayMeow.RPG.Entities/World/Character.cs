using MayMeow.RPG.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Entities.World
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }

        //
        public int Experience { get; set; }
        public int Money { get; set; }

        // Attributes
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }

        // Stats
        public int HitPoints { get; set; }
        public int TotalHitPoints { get; set; }

        // Information About Owner
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }

        // Time informations
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Race information
        public int RaceId { get; set; }
        [ForeignKey("RaceId")]
        public Race Race { get; set; }
    }

    public enum Gender
    {
        Female,
        Male
    }
}
