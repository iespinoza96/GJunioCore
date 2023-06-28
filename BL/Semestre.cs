using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Semestre
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LescogidoProgramacionNcapasGjContext context = new DL.LescogidoProgramacionNcapasGjContext())

                {

                    var query = context.Semestres.FromSqlRaw($"SemestreGetAll").ToList();
                    // var query = context.MateriaGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (DL.Semestre obj in query)
                        {
                            ML.Semestre semestre = new ML.Semestre();

                            semestre.IdSemestre = obj.IdSemestre;
                            semestre.Nombre = obj.Nombre;



                            result.Objects.Add(semestre);
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
