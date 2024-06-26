﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("UserCourse", Schema = "user")]
    public class UserCourse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
