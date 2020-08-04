using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningTest.Data
{
    public class UserCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { get; set; } 

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public  User User { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
