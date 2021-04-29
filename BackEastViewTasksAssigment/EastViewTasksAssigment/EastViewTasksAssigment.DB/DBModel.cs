using EastViewTasksAssignment.DB.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastViewTasksAssignment.DB
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

        public async Task<Citizens> getCitizenByIdCard(string citizenIdCard)
        {
            return await context.Citizens.FirstOrDefaultAsync(c => c.citizenIdCard == citizenIdCard);
        }

        public async Task<List<Citizens>> getCitizensList()
        {
            return await context.Citizens.ToListAsync();
        }

        public async Task<List<Citizens>> getCitizensListWithTasksByDay(byte weekDay)
        {
            return await context.Citizens.Include(x=>x.Tasks.Where(x=>x.taskDay == weekDay)).ToListAsync();
        }

        public async Task addCitizen(Citizens newRecord)
        {
            await context.Citizens.AddAsync(newRecord);
            await context.SaveChangesAsync();
        }

        public async Task updateCitizen(Citizens editedRecord)
        {
            Citizens citizen = await getCitizen(editedRecord.citizenId);
            citizen.citizenIdCard = editedRecord.citizenIdCard;
            citizen.citizenName = editedRecord.citizenName;
            citizen.citizenLastName = editedRecord.citizenLastName;

            context.Citizens.Update(citizen);
            await context.SaveChangesAsync();
        }

        public async Task deleteCitizen(long citizenId)
        {
            Citizens citizen = await getCitizen(citizenId);
            context.Citizens.Remove(citizen);
            await context.SaveChangesAsync();
        }

        public async Task toggleCitizenAlive(long citizenId)
        {
            Citizens citizen = await getCitizen(citizenId);
            citizen.citizenAlive = !citizen.citizenAlive;
            context.Citizens.Update(citizen);
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

        public async Task updateTask(Tasks editedRecord)
        {
            Tasks task = await getTask(editedRecord.taskId);
            task.taskDay = editedRecord.taskDay;
            task.taskDescription = editedRecord.taskDescription;
            task.taskCitizenId = editedRecord.taskCitizenId;
            task.taskTime = editedRecord.taskTime;

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
