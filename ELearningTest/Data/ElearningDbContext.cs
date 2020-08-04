using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ELearningTest.Data
{
    public class ElearningDbContext  : DbContext
    { 

      
            public virtual DbSet<User> Users { get; set; }
            public virtual DbSet<Course> Courses { get; set; }
            public virtual DbSet<UserCourse> UserCourses { get; set; }

            public ElearningDbContext(DbContextOptions<ElearningDbContext> options)
                : base(options)
            {  }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

            }
      
        

    }
}
