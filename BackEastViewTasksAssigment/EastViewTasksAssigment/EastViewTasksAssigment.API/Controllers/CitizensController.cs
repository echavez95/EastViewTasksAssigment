using EastViewTasksAssignment.DB;
using EastViewTasksAssignment.DB.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EastViewTasksAssignment.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private DBModel model;
        public CitizensController(DBContext context)
        {
            model = new DBModel(context);
        }

        [HttpPost]
        public async Task<JsonResult> saveCitizen(Citizens citizen)
        {
            try
            {
                if (citizen.citizenId == 0)
                {
                    await model.addCitizen(citizen);
                    return new JsonResult(new
                    {
                        type = "success",
                        message = "Ciudadano agregado!"
                    });
                }
                else
                {
                    await model.updateCitizen(citizen);
                    return new JsonResult(new
                    {
                        type = "success",
                        message = "Ciudadano actualizado!"
                    });
                }
            }
            catch(Exception e)
            {
                return new JsonResult(new
                {
                    type = "error",
                    message = e.InnerException != null ? e.InnerException.Message.ToString() : e.Message.ToString()
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> getCitizenByIdCard(string citizenIdCard)
        {
            try
            {
                var record = await model.getCitizenByIdCard(citizenIdCard);
                if(record == null)
                {
                    return new JsonResult(new
                    {
                        type = "error",
                        message = "El ciudadano no existe!"
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
        public async Task<JsonResult> getCitizen(long citizenId)
        {
            try
            {
                var record = await model.getCitizen(citizenId);
                if (record == null)
                {
                    return new JsonResult(new
                    {
                        type = "error",
                        message = "El ciudadano no existe!"
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
        public async Task<JsonResult> getCitizensList()
        {
            try
            {
                List<Citizens> list = await model.getCitizensList();
                
                return new JsonResult(new
                {
                    type = "success",
                    message = list
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
        public async Task<JsonResult> deleteCitizen(long citizenId)
        {
            try
            {
                var record = await model.getCitizen(citizenId);
                if (record == null)
                {
                    return new JsonResult(new
                    {
                        type = "error",
                        message = "El ciudadano no existe!"
                    });
                }
                else
                {
                    await model.deleteCitizen(citizenId);
                    return new JsonResult(new
                    {
                        type = "success",
                        message = "El ciudadano ha sido eliminado!"
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

        [HttpPut]
        public async Task<JsonResult> toggleCitizenAlive(long citizenId)
        {
            try
            {
                var record = await model.getCitizen(citizenId);
                if (record == null)
                {
                    return new JsonResult(new
                    {
                        type = "error",
                        message = "El ciudadano no existe!"
                    });
                }
                else
                {
                    await model.toggleCitizenAlive(citizenId);
                    return new JsonResult(new
                    {
                        type = "success",
                        message = "El estado del ciudadano ha sido editado!"
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
    }
}
