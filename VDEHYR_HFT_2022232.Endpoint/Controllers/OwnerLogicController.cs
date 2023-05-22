using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VDEHYR_HFT_2022232.Logic.Interfaces;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnerLogicController : ControllerBase
    {
        IOwnerLogic logic;

        public OwnerLogicController(IOwnerLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Owner value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}