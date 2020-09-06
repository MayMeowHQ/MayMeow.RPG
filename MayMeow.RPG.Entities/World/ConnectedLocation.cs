using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Entities.World
{
    public class ConnectedLocation
    {
        public int Id { get; set; }

        public int ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Location Parent { get; set; }

        public int ChildId { get; set; }
        [ForeignKey("ChildId")]
        public Location Child { get; set; }
    }
}
