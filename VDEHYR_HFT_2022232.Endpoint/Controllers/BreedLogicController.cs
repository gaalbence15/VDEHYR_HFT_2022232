using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VDEHYR_HFT_2022232.Logic.Interfaces;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BreedLogicController : ControllerBase
    {
        IBreedLogic logic;

        public BreedLogicController(IBreedLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Breed value)
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
