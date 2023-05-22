using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VDEHYR_HFT_2022232.Logic.Interfaces;
using VDEHYR_HFT_2022232.Models;

namespace VDEHYR_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DogLogicController : ControllerBase
    {
        IDogLogic logic;

        public DogLogicController(IDogLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Dog value)
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