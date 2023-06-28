using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Grupo
    {
        public static ML.Result GetByIdPlantel(int idPlantel)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LescogidoProgramacionNcapasGjContext context = new DL.LescogidoProgramacionNcapasGjContext())
                {
                    var query = context.Grupos.FromSqlRaw($"GrupoGetByIdPlantel {idPlantel}").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var rowGrupo in query)
                        {
                            ML.Grupo grupo = new ML.Grupo();

                            grupo.IdGrupo = rowGrupo.IdGrupo;
                            grupo.Nombre = rowGrupo.Nombre;

                            grupo.Plantel = new ML.Plantel();
                            grupo.Plantel.IdPlantel = rowGrupo.IdPlantel.Value;

                            result.Objects.Add(grupo);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al realizar la consulta " + ex.Message;
                throw;
            }
            return result;
        }
    }
}
