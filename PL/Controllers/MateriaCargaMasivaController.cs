using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class MateriaCargaMasivaController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public MateriaCargaMasivaController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }
        public ActionResult GetCargaMasiva()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostCargaMasiva(IFormFile file)
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
        [HttpPost]
        public ActionResult CargaMasivaExcel()
        {
            //Session - C#
            //app setting
            //inyeccion de dependencias - patrones de diseño
            IFormFile file = Request.Form.Files["file"];
            if (file != null)
            {
                //obtener el nombre de nuestro archivo
                string fileName = Path.GetFileName(file.FileName);
                string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                string extensionAceptada = ".xlsx";
                string folderPath = "C:\\Users\\digis\\source\\repos\\GJunioCore\\PL\\wwwroot\\CopiasExcel";
                if (extensionArchivo == extensionAceptada)
                {
                    //crear una copia del archivo cargado
                    string filePath = Path.Combine(environment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                    if (!System.IO.File.Exists(filePath))
                    {
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }//Hasta aqui nos quedamos

                        //
                        string connString = configuration["ExcelConString"] + filePath;//cadena de conexion y ruta especifica del archivo

                        //crear un metodo en BL.Materia
                        ML.Result resultExcelDt = BL.Materia.ConvertExcelToDataTable(connString);
                    }

                }
                else
                {
                    ViewBag.Message = "El archivo que se intenta procesar no es un excel";
                }

            }
            else
            {
                ViewBag.Message = "No se ha insertado un archivo";
            }
            return View();
        }
    }
}
