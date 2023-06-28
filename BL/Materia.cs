using Microsoft.EntityFrameworkCore;

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
                            materia.FechaCreacion = obj.FechaCreacion.Value.ToString("dd/MM/yyyy");
                            materia.Imagen = obj.Imagen;
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

        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LescogidoProgramacionNcapasGjContext context = new DL.LescogidoProgramacionNcapasGjContext())
                {
                    int queryResult = context.Database.ExecuteSql($"MateriaAdd '{materia.Nombre}', {materia.Creditos}, {materia.Semestre.IdSemestre}");

                    if (queryResult > 0)
                    {
                        result.Correct = true;
                        result.Message = "Registro insertado correctamente";
                    }


                }


            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result GetById(int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LescogidoProgramacionNcapasGjContext context = new DL.LescogidoProgramacionNcapasGjContext())
                {
                    var query = context.Materia.FromSqlRaw($"MateriaGetById {idMateria}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = query.IdMateria;
                        materia.Nombre = query.Nombre;
                        materia.Creditos = query.Creditos.Value;
                        materia.FechaCreacion = query.FechaCreacion.Value.ToString("dd/MM/yyyy");
                        materia.Imagen = query.Imagen;


                        materia.Semestre = new ML.Semestre();
                        materia.Semestre.IdSemestre = query.IdSemestre.Value;
                        materia.Semestre.Nombre = query.NombreSemestre;


                        result.Object = materia;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}