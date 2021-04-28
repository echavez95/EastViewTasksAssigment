using EastViewTasksAssignment.DB;
using EastViewTasksAssignment.DB.Classes;
using EastViewTasksAssignment.DB.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EastViewTasksAssignment.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private DBModel model;
        public TasksController(DBContext context)
        {
            model = new DBModel(context);
        }

        [HttpPost]
        public async Task<JsonResult> saveTask(Tasks task)
        {
            try
            {
                if (task.taskId == 0)
                {
                    await model.addTask(task);
                    return new JsonResult(new
                    {
                        type = "success",
                        message = "Tarea agregada!"
                    });
                }
                else
                {
                    await model.updateTask(task);
                    return new JsonResult(new
                    {
                        type = "success",
                        message = "Tarea actualizada!"
                    });
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    type = "error",
                    message = e.InnerException != null ? e.InnerException.Message.ToString() : e.Message.ToString()
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> getTask(long taskId)
        {
            try
            {
                var record = await model.getTask(taskId);
                if (record == null)
                {
                    return new JsonResult(new
                    {
                        type = "error",
                        message = "La tarea no existe!"
                    });
                }
                return new JsonResult(new
                {
                    type = "success",
                    message = record
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    type = "error",
                    message = e.InnerException != null ? e.InnerException.Message.ToString() : e.Message.ToString()
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> getCitizenTasksList(long citizenId)
        {
            try
            {
                List<Tasks> list = await model.getCitizenTasksList(citizenId);

                return new JsonResult(new
                {
                    type = "success",
                    message = list.OrderBy(x=>x.taskDay).ToList()
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    type = "error",
                    message = e.InnerException != null ? e.InnerException.Message.ToString() : e.Message.ToString()
                });
            }
        }

        [HttpDelete]
        public async Task<JsonResult> deleteTask(long taskId)
        {
            try
            {
                var record = await model.getTask(taskId);
                if (record == null)
                {
                    return new JsonResult(new
                    {
                        type = "error",
                        message = "La tarea no existe!"
                    });
                }
                else
                {
                    await model.deleteTask(taskId);
                    return new JsonResult(new
                    {
                        type = "success",
                        message = "La tarea ha sido eliminada!"
                    });
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    type = "error",
                    message = e.InnerException != null ? e.InnerException.Message.ToString() : e.Message.ToString()
                });
            }
        }

        [HttpGet]
        public JsonResult weekDays()
        {
            try
            {
                List<WeekDay> DiasSemana = new List<WeekDay>();
                DiasSemana.Add(new WeekDay()
                {
                    DayNumber = (byte)DayOfWeek.Monday,
                    DayName = "Lunes"
                });
                DiasSemana.Add(new WeekDay()
                {
                    DayNumber = (byte)DayOfWeek.Tuesday,
                    DayName = "Martes"
                });
                DiasSemana.Add(new WeekDay()
                {
                    DayNumber = (byte)DayOfWeek.Wednesday,
                    DayName = "Miercoles"
                });
                DiasSemana.Add(new WeekDay()
                {
                    DayNumber = (byte)DayOfWeek.Thursday,
                    DayName = "Jueves"
                });
                DiasSemana.Add(new WeekDay()
                {
                    DayNumber = (byte)DayOfWeek.Friday,
                    DayName = "Viernes"
                });
                DiasSemana.Add(new WeekDay()
                {
                    DayNumber = (byte)DayOfWeek.Saturday,
                    DayName = "Sábado"
                });
                DiasSemana.Add(new WeekDay()
                {
                    DayNumber = (byte)DayOfWeek.Sunday,
                    DayName = "Domingo"
                });

                return new JsonResult(new
                {
                    type = "success",
                    message = DiasSemana
                });
                
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    type = "error",
                    message = e.InnerException != null ? e.InnerException.Message.ToString() : e.Message.ToString()
                });
            }
        }
    }
}
