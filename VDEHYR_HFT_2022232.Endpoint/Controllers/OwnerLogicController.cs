using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using VDEHYR_HFT_2022232.Endpoint.Services;
using VDEHYR_HFT_2022232.Logic.Interfaces;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        IOwnerLogic logic;

        IHubContext<SignalRHub> hub;

        public OwnerController(IOwnerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Owner> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Owner Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Owner value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("OwnerCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Owner value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("OwnerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deleted = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("OwnerDeleted", deleted);
        }
    }
}