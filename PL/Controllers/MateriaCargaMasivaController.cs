using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace PL.Controllers
{
    public class MateriaCargaMasivaController : Controller
    {
        public IActionResult GetCargaMasiva()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostCargaMasiva(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                // Manejar el caso en que no se haya seleccionado ningún archivo.
                return RedirectToAction("GetCargaMasiva");
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split('|'); // Asumiendo que los valores están separados por comas

                    ML.Materia materia = new ML.Materia();

                    materia.Nombre = values[0];
                    materia.Creditos = byte.Parse(values[1]);

                    materia.Semestre = new ML.Semestre();
                    materia.Semestre.IdSemestre = byte.Parse(values[2]);
                    materia.FechaCreacion = values[3];

                    materia.Horario = new ML.Horario();
                    materia.Horario.Turno = values[4];

                    materia.Horario.Grupo = new ML.Grupo();
                    materia.Horario.Grupo.IdGrupo = int.Parse(values[5]);

                    ML.Result result = BL.Materia.Add(materia);

                    if (result.Correct)
                    {
                        //crear lista para saber cuantos registros se agregaron
                    }
                    else
                    {
                        //crear lista de errores y gurardalos en un bloc de notas
                    }

                }
            }

            //_dbContext.SaveChanges();

            // Redireccionar a una vista o realizar otra acción según tus necesidades
            return RedirectToAction("GetAll");
        }

    }
}
