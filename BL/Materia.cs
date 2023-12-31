﻿using Microsoft.EntityFrameworkCore;
using ML;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetAll(ML.Materia materias)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LescogidoProgramacionNcapasGjContext context = new DL.LescogidoProgramacionNcapasGjContext())

                {

                    var query = context.Materia.FromSqlRaw($"MateriaGetAll '{materias.Nombre}', '{materias.Semestre.Nombre}'").ToList();
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
                            materia.Status = obj.Status;

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
                    int queryResult = context.Database.ExecuteSqlRaw($"MateriaAdd '{materia.Nombre}', {materia.Creditos}, {materia.Semestre.IdSemestre},'{materia.Imagen}','{materia.FechaCreacion}','{materia.Horario.Turno}',{materia.Horario.Grupo.IdGrupo}");

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
        public static ML.Result UpdateStatus(bool status, int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LescogidoProgramacionNcapasGjContext context = new DL.LescogidoProgramacionNcapasGjContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"MateriaStatusUpdate {status}, {idMateria}");
                   
                    if(query > 0)
                    {
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
                result.Ex=ex;
                result.Message=ex.Message;
            }
            return result;
        }
        public static ML.Result ConvertExcelToDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Hoja1$]";
                    //string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableAlumno = new DataTable();

                        da.Fill(tableAlumno);

                        if (tableAlumno.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableAlumno.Rows)
                            {
                                ML.Materia materia = new ML.Materia();

                                materia.Nombre = row[0].ToString();
                                materia.Creditos = byte.Parse(row[1].ToString());
                                materia.FechaCreacion = row[3].ToString();
                                result.Objects.Add(materia);
                            }

                            result.Correct = true;

                        }

                        result.Object = tableAlumno;

                        if (tableAlumno.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No existen registros en el excel";
                        }
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
        public static ML.Result ValidarExcel(List<object> Objects)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();

                int i = 1;
                foreach (ML.Materia materia in Objects)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    //operador ternario - hacer una validacion para cambiar un valor
                    materia.Nombre = (materia.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : materia.Nombre;

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;

            }

            return result;
        }
    }


}