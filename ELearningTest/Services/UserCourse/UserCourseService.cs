using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using ELearningTest.Data;
using Microsoft.EntityFrameworkCore;

namespace ELearningTest.Services.UserCourse
{
    public class UserCourseService : IUserCourseService
    {
        private ElearningDbContext _dbContext;

        public UserCourseService(ElearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeUserCourse()
        {
            await InitializeUser();
            await InitializeCourse();
            
            var countusercourse = await _dbContext.UserCourses.CountAsync();

            if(countusercourse >= 100) return;

            var timeStamp = new List<TimeSpan>()
            {
                TimeSpan.FromHours(3),
                TimeSpan.FromHours(4),
                TimeSpan.FromSeconds(30),
                TimeSpan.FromMinutes(5),
                TimeSpan.FromMinutes(10),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(30),
            }; ;
            var userId = await _dbContext.Users.Select(a => a.Id).Take(100).ToListAsync();
            var courseId = await _dbContext.Courses.Select(a => a.Id).Take(100).ToListAsync();

          

            var userCourse = new Faker<Data.UserCourse>()
                .RuleFor(a=>a.CourseId , c=>c.PickRandom(courseId))
                .RuleFor(a=>a.UserId , c=>c.PickRandom(userId))
                .RuleFor(a=>a.Duration , c=>c.PickRandom(timeStamp))
                .Generate(100)
                ;
          await  _dbContext.UserCourses.AddRangeAsync(userCourse);
          await _dbContext.SaveChangesAsync();
        }

        public async Task InitializeUser()
        {
            var userCount = await _dbContext.Users.CountAsync();
            if (userCount >= 100)
            {
                return;
            }

            var users = new Faker<User>().RuleFor(a => a.FirstName, c => c.Name.FirstName())
                .RuleFor(a => a.LastName, c => c.Name.LastName())
                .RuleFor(a => a.Email, c => c.Person.Email)
                .Generate(100);
            await _dbContext.Users.AddRangeAsync(users);
            await _dbContext.SaveChangesAsync();
        }

        public async Task InitializeCourse()
        {
            var courseCount = await _dbContext.Courses.CountAsync();
            if (courseCount >= 100)
            {
                return;
            }

            var courses = new Faker<Course>().RuleFor(a => a.Title, c => c.Lorem.Sentence(1))
                .RuleFor(a => a.Description, c=>c.Lorem.Paragraph(1))
                .RuleFor(a => a.DateAdded, () => DateTime.Now)
             
                .Generate(100);
            await _dbContext.Courses.AddRangeAsync(courses);
            await _dbContext.SaveChangesAsync();
        }
    }
}
