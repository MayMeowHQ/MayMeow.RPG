using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MayMeow.RPG.Data;
using MayMeow.RPG.Entities.World;
using MayMeow.RPG.Web.ViewModels.Charts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MayMeow.RPG.Web.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldMapController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public WorldMapController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<WorldMap> Index()
        {
            var worldMap = new WorldMap();
            worldMap.Edges = new List<ChartEdge>();
            worldMap.Nodes = new List<ChartNode>();

            var locations = await this.dbContext.Locations.ToListAsync();

            var edges = await this.dbContext.ConnectedLocations.Include(cl => cl.Parent).Include(cl => cl.Child).ToListAsync();

            foreach (var location in locations)
            {
                worldMap.Nodes.Add(new ChartNode { Id = location.Name });
            }

            foreach (var edge in edges)
            {
                worldMap.Edges.Add(new ChartEdge { From = edge.Parent.Name, To = edge.Child.Name });
            }

            return worldMap;
        }
    }
}
