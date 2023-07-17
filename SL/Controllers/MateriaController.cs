﻿using BL;
using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/materia")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        // GET: api/<MateriaController>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            materia.Semestre = new ML.Semestre();

            ML.Result result = BL.Materia.GetAll(materia);

            if (result.Correct)
            {
                return Ok(result);//HTTP 200
            }
            else
            {
                return NotFound();//http 404
            }
        }

        // GET api/<MateriaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MateriaController>
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.Add(materia);

            if (result.Correct)
            {
                return Ok(result);//HTTP 200
            }
            else
            {
                return NotFound();//http 404
            }
        }

        // PUT api/<MateriaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.Add(materia);

            if (result.Correct)
            {
                return Ok(result);//HTTP 200
            }
            else
            {
                return NotFound();//http 404
            }
        }

        // DELETE api/<MateriaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}