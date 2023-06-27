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
            //ML.Result resultSemestre = BL.Semestre.GetAll();//mandamos a llamar a getall de semestres 
            //ML.Result resultPlantel = BL.Plantel.GetAll();

            ML.Materia materia = new ML.Materia(); //objeto global
            //materia.Semestre = new ML.Semestre();
            //materia.Horario = new ML.Horario();
            //materia.Horario.Grupo = new ML.Grupo();
            //materia.Horario.Grupo.Plantel = new ML.Plantel();

            //materia.Semestre.Semestres = resultSemestre.Objects; // guardamos la lista de semestre en un objeto materia
            //materia.Horario.Grupo.Plantel.Planteles = resultPlantel.Objects;

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
                //ML.Result result = BL.Materia.GetByIdEF(idMateria.Value);// variable local

                //if (result.Correct)
                //{
                //    materia = (ML.Materia)result.Object; //unboxing
                //    ViewBag.Titulo = "Actualizar";
                //    return View(materia);
                //}
                //else
                //{
                //    ViewBag.Titulo = "Error";
                //    ViewBag.Message = result.Message;
                //    return View("Modal");
                //}

                return View(materia);


            }

        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            //if (materia.IdMateria == 0)
            //{
            //    //AGREGAR
            //    ML.Result result = BL.Materia.AddLINQ(materia);

            //    if (result.Correct)
            //    {
            //        ViewBag.Titulo = "Registro Exitoso";
            //        ViewBag.Message = result.Message;
            //        return View("Modal");
            //    }
            //    else
            //    {
            //        ViewBag.Titulo = "ERROR";
            //        ViewBag.Message = result.Message;
            //        return View("Modal");
            //    }

            //}
            //else
            //{
            //    //ACTUALIZAR
            //    //ML.Result result = BL.Materia.
            //    return View();


            //}
            return View(materia);
        }

        [HttpGet]
        public JsonResult GetGrupos(int idPlantel)
        {

            ML.Result result = new ML.Result();
            //ML.Result result = BL.Grupo.GetByIdPlantel(idPlantel);
            ML.Grupo grupo = new ML.Grupo();

            grupo.Grupos = result.Objects;
            return Json(result);
        }
    }
}