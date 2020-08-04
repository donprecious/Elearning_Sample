using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningTest.Data
{
    public class Course 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public  string FileUrls { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
