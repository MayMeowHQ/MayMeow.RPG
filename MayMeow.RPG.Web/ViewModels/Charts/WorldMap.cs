using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayMeow.RPG.Web.ViewModels.Charts
{
    public class WorldMap
    {
        public List<ChartNode> Nodes { get; set; }
        public List<ChartEdge> Edges { get; set; }
    }
}
