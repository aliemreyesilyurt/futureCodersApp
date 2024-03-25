﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Blog", Schema = "blog")]
    public class Blog
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
