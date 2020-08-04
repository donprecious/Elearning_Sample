using System.Linq;
using System.Threading.Tasks;
using ELearningTest.Services.UserCourse;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace StackgipEcommerce.Services.InitilizerService
{
    public class AppInitializer: IAsyncInitializer
    {

        private IWebHostEnvironment _webHost;
        private IUserCourseService _userCourseService;
        public AppInitializer(IWebHostEnvironment webHost, IUserCourseService userCourseService)
        {
            _webHost = webHost;
            _userCourseService = userCourseService;
        }

        public async Task InitializeAsync()
        {
           
            //for development purpose  
            if (_webHost.IsDevelopment())
            {
                await _userCourseService.InitializeUserCourse();
            }
        }
    }
}
