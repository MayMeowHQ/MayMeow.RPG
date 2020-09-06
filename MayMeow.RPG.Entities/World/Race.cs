using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Entities.World
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Attributes
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }

        // Home location information
        public int StartingLocationId { get; set; }
        [ForeignKey("StartingLocationId")]
        public Location StartingLocation { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
