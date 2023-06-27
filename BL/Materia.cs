﻿using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LescogidoProgramacionNcapasGjContext context = new DL.LescogidoProgramacionNcapasGjContext())

                {

                    var query = context.Materia.FromSqlRaw($"MateriaGetAll").ToList();
                    // var query = context.MateriaGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (DL.Materium obj in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            materia.Creditos = obj.Creditos.Value;
                            materia.Semestre = new ML.Semestre(); //instancia de la propiedad de navegación, solo se instancia una vez
                            materia.Semestre.IdSemestre = obj.IdSemestre.Value;
                            materia.Semestre.Nombre = obj.NombreSemestre;



                            result.Objects.Add(materia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

    }
}