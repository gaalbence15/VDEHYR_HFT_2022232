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
    public class DogController : ControllerBase
    {
        IDogLogic logic;

        IHubContext<SignalRHub> hub;

        public DogController(IDogLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Dog> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Dog Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Dog value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("DogCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Dog value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("DogUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deleted = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("DogUpdated", deleted);
        }
    }
}