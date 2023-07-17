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
            ML.Result result = new ML.Result();
            return View(result);
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
        public ActionResult GetCargaMasiva(int? valor)
        {
            //Session - C#
            //app setting
            //inyeccion de dependencias - patrones de diseño
            IFormFile file = Request.Form.Files["file"];
            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (file != null)
                {
                    //obtener el nombre de nuestro archivo
                    string fileName = Path.GetFileName(file.FileName);
                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extensionAceptada = configuration["TipoExcel"];
                    string folderPath = configuration["PathFolder:ruta"];
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
                            string connString = configuration["ExcelConString:value"] + filePath;//cadena de conexion y ruta especifica del archivo

                            //crear un metodo en BL.Materia
                            ML.Result resultExcelDt = BL.Materia.ConvertExcelToDataTable(connString);

                            if (resultExcelDt.Correct)
                            {
                                ML.Result resultValidacion = BL.Materia.ValidarExcel(resultExcelDt.Objects);

                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);//crear la session -- aqui nos quedamos

                                }

                                return View(resultValidacion);
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
                return View();
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = configuration["ExcelConString:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Materia.ConvertExcelToDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Materia materiaItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Materia.Add(materiaItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Semestre con nombre: " + materiaItem.Nombre + " Error: " + resultAdd.Message);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        string fileError = Path.Combine(environment.WebRootPath, @"~\Files\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Las Alumnos No han sido registrados correctamente";
                    }
                    else
                    {
                        //borrar session
                        ViewBag.Message = "Las Alumnos han sido registrados correctamente";
                    }

                }

                return View("GetAll");
            }
        }
    }
}
