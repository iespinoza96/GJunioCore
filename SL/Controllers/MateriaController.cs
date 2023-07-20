using BL;
using Microsoft.AspNetCore.Mvc;
using ML;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/materia")] //direccion base
    [ApiController]
    public class MateriaController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()//hacer uso del getall de materia
        {
            ML.Materia materia = new ML.Materia();
            materia.Semestre = new ML.Semestre();
            ML.Result result = BL.Materia.GetAll(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }


        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] ML.Materia materia)
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

    
    }
}
