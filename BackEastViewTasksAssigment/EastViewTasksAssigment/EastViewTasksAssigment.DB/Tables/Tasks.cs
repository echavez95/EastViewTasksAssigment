using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EastViewTasksAssignment.DB.Tables
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long taskId { get; set; }
        public byte taskDay { get; set; }
        [MaxLength(300)]
        public string taskDescription { get; set; }
        public TimeSpan taskTime { get; set; }

        public long taskCitizenId { get; set; }
        public Citizens Citizen { get; set; }
    }
}
