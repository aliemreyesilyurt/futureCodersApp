﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("QuestionOption", Schema = "exam")]
    public class QuestionOption
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool IsTrue { get; set; } = false;

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
