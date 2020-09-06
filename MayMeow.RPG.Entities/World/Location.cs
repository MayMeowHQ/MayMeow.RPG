using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Entities.World
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ConnectedLocation> ParrentLocations { get; set; }
        public ICollection<ConnectedLocation> ChildLocations { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
