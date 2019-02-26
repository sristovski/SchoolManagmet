using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Models;
using SchoolManagment.Services.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolManagment.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            CourseDataView result = new CourseDataView();
            result.list  = _courseRepository.Query().Select(x=> new Course()
            {
                Id=x.CourseId,
                ShortName=x.ShortName,
                FullName=x.FullName
            }).ToList();

            return View(result);
        }

        public ActionResult Edit(int Id)
        {
            var std = _courseRepository.Query().Where(x => x.CourseId == Id).Select(x => new Course()
            {
                Id = x.CourseId,
                ShortName = x.ShortName,
                FullName = x.FullName
            }).FirstOrDefault();
            return View(std);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                var cCourse = await _courseRepository.GetAsync(course.Id);

                if (cCourse == null)
                {
                    var nCourse = new Data.Course
                    {
                        ShortName = course.ShortName,
                        FullName = course.FullName
                    };
                    await _courseRepository.InsertAsync(nCourse);
                }
                else
                {
                    cCourse.ShortName = course.ShortName;
                    cCourse.FullName = course.FullName;
                    await _courseRepository.UpdateAsync(cCourse);
                }


                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
    }
}
