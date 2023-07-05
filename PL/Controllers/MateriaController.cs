﻿using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAll();
            ML.Materia materia = new ML.Materia();

            if (result.Correct)
            {
                materia.Materias = result.Objects;
                return View(materia);
            }
            else
            {
                //materia.Materias = new List<object> ();
                ViewBag.Message = result.Message;
                return View(materia);
            }

        }

        [HttpGet]
        public ActionResult Form(int? idMateria)
        {
            ML.Result resultSemestre = BL.Semestre.GetAll();//mandamos a llamar a getall de semestres 
            ML.Result resultPlantel = BL.Plantel.GetAll();

            ML.Materia materia = new ML.Materia(); //objeto global
            materia.Semestre = new ML.Semestre();
            materia.Horario = new ML.Horario();
            materia.Horario.Grupo = new ML.Grupo();
            materia.Horario.Grupo.Plantel = new ML.Plantel();

            materia.Semestre.Semestres = resultSemestre.Objects; // guardamos la lista de semestre en un objeto materia
            materia.Horario.Grupo.Plantel.Planteles = resultPlantel.Objects;

            if (idMateria == null)
            {
                //AGREGAR
                //MOSTRAR EL FORMULARIO VACIO 
                ViewBag.Titulo = "Agregar";

                return View(materia);
            }
            else
            {
                //GetById(IdMateria)
                ML.Result result = BL.Materia.GetById(idMateria.Value);// variable local

                if (result.Correct)
                {
                    materia = (ML.Materia)result.Object; //unboxing
                    materia.Semestre.Semestres = resultSemestre.Objects;
                    materia.Horario.Grupo.Plantel.Planteles = resultPlantel.Objects;

                    ML.Result resultGrupo = BL.Grupo.GetByIdPlantel(materia.Horario.Grupo.Plantel.IdPlantel);
                    ViewBag.Titulo = "Actualizar";
                    return View(materia);
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }

                //return View(materia);


            }

        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if (!ModelState.IsValid)
            {
                IFormFile image = Request.Form.Files["fileImage"];

                //valido si traigo imagen
                if (image != null)
                {
                    //llamar al metodo que convierte a bytes la imagen
                    byte[] ImagenBytes = ConvertToBytes(image);
                    //convierto a base 64 la imagen y la guardo en mi objeto materia
                    materia.Imagen = Convert.ToBase64String(ImagenBytes);
                }
                if (materia.IdMateria == 0)
                {
                    //AGREGAR
                    ML.Result result = BL.Materia.Add(materia);

                    if (result.Correct)
                    {
                        ViewBag.Titulo = "Registro Exitoso";
                        ViewBag.Message = result.Message;
                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Titulo = "ERROR";
                        ViewBag.Message = result.Message;
                        return View("Modal");
                    }

                }
                else
                {
                    //ACTUALIZAR
                    //ML.Result result = BL.Materia.
                    return View();


                }

            }

            else
            {
                ML.Result resultSemestre = BL.Semestre.GetAll();//mandamos a llamar a getall de semestres 
                ML.Result resultPlantel = BL.Plantel.GetAll();

                
                materia.Semestre = new ML.Semestre();
                materia.Horario = new ML.Horario();
                materia.Horario.Grupo = new ML.Grupo();
                materia.Horario.Grupo.Plantel = new ML.Plantel();

                materia.Semestre.Semestres = resultSemestre.Objects; // guardamos la lista de semestre en un objeto materia
                materia.Horario.Grupo.Plantel.Planteles = resultPlantel.Objects;

                return View(materia);
            }
          
           
        }

        [HttpGet]
        public JsonResult GetGrupos(int idPlantel)
        {
            
            ML.Result result = BL.Grupo.GetByIdPlantel(idPlantel);
            return Json(result.Objects);
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
