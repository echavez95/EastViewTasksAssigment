using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EastViewTasksAssigment.DB.Tables
{
    [Table("Citizens")]
    public class Citizens
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long citizenId { get; set; }
        [MaxLength(30)]
        public string citizenIdCard { get; set; }
        [MaxLength(150)]
        public string citizenName { get; set; }
        [MaxLength(200)]
        public string citizenLastName { get; set; }

        public List<Tasks> Tasks { get; set; }
    }
}
