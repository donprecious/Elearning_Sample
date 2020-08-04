using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ELearningTest.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using X.PagedList;

namespace ELearningTest.Controllers
{
    public class UserCourseController : Controller
    {
        private ElearningDbContext _context;
        private IWebHostEnvironment _hostingEnvironment;

        public UserCourseController(ElearningDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async  Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var userCourse = await _context.UserCourses
                .Include(a=>a.User)
                .Include(a=>a.Course)
                .ToPagedListAsync(pageNumber, 20);

            return View(userCourse);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCourse()
        {
            var file = Request.Form.Files[0];
            string folderName = "UploadedFiles";
            string webRootPath = _hostingEnvironment.WebRootPath;

            string newPath = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            var courses = new List<Course>();
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName)?.ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName ?? throw new InvalidOperationException());

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }

                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);

                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        var course = new Course()
                        {
                            Title = row.GetCell(1).ToString(),
                            Description = row.GetCell(2).ToString(),
                        };
                        courses.Add(course);
                    }

                   await _context.Courses.AddRangeAsync(courses);
                   await _context.SaveChangesAsync();
                   return Ok(new {message = "uploaded successfully"});
                }
             
            }

            return BadRequest("Something went wrong");
        }
    }
}