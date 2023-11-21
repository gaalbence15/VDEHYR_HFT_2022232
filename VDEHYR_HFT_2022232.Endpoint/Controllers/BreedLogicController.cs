using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using VDEHYR_HFT_2022232.Endpoint.Services;
using VDEHYR_HFT_2022232.Logic.Interfaces;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BreedLogicController : ControllerBase
    {
        IBreedLogic logic;

        IHubContext<SignalRHub> hub;

        public BreedLogicController(IBreedLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Breed> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Breed Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Breed value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("BreedCreated", value);

        }

        [HttpPut]
        public void Update([FromBody] Breed value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("BreedUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deleted = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("BreedDeleted", deleted);
        }
    }
}
