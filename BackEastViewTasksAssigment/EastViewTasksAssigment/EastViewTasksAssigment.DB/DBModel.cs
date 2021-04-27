using EastViewTasksAssigment.DB.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastViewTasksAssigment.DB
{
    public class DBModel
    {
        private DBContext context;
        public DBModel(DBContext Context)
        {
            context = Context;
        }

        #region CITIZENS
        public async Task<Citizens> getCitizen(long citizenId)
        {
            return await context.Citizens.FirstOrDefaultAsync(c=>c.citizenId == citizenId);
        }
        public async Task<List<Citizens>> getCitizensList()
        {
            return await context.Citizens.ToListAsync();
        }

        public async Task addCitizen(Citizens newRecord)
        {
            await context.Citizens.AddAsync(newRecord);
            await context.SaveChangesAsync();
        }

        public async Task updateCitizen(long citizenId, Citizens editedRecord)
        {
            Citizens citizen = await getCitizen(citizenId);
            citizen.citizenIdCard = editedRecord.citizenIdCard;
            citizen.citizenName = editedRecord.citizenName;
            citizen.citizenName = editedRecord.citizenLastName;

            context.Citizens.Update(citizen);
            await context.SaveChangesAsync();
        }

        public async Task deleteCitizen(long citizenId)
        {
            Citizens citizen = await getCitizen(citizenId);
            context.Citizens.Remove(citizen);
            await context.SaveChangesAsync();
        }
        #endregion

        #region TASKS
        public async Task<Tasks> getTask(long taskId)
        {
            return await context.Tasks.FirstOrDefaultAsync(t => t.taskId == taskId);
        }
        public async Task<List<Tasks>> getCitizenTasksList(long citizenId)
        {
            return await context.Tasks.Where(x=>x.taskCitizenId == citizenId).ToListAsync();
        }

        public async Task addTask(Tasks newRecord)
        {
            await context.Tasks.AddAsync(newRecord);
            await context.SaveChangesAsync();
        }

        public async Task updateTask(long taskId, Tasks editedRecord)
        {
            Tasks task = await getTask(taskId);
            task.taskDay = editedRecord.taskDay;
            task.taskDescription = editedRecord.taskDescription;
            task.taskCitizenId = editedRecord.taskCitizenId;

            context.Tasks.Update(task);
            await context.SaveChangesAsync();
        }

        public async Task deleteTask(long taskId)
        {
            Tasks task = await getTask(taskId);
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
        }
        #endregion
    }
}
