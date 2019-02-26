using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagment.Models;
using SchoolManagment.Services.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolManagment.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly ICourseRepository _courseRepository;

        public ClassroomController(IClassroomRepository classroomRepository, ICourseRepository courseRepository)
        {
            _classroomRepository = classroomRepository;
            _courseRepository = courseRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            var result = _classroomRepository.GetAllClassroom().ToList();

            return View(result);
        }

        public ActionResult Edit(int Id)
        {
            Classroom fResult = new Classroom();
            var result = _classroomRepository.GetClassroomById(Id).ToList();
            if (result.Count != 0)
            {
                fResult = result.Select(x => new Classroom()
                {
                    Id = x.ClassroomId,
                    ClassroomName = x.ClassroomName,
                    StartDate = x.StartDate,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    SelectedCourse = x.Courses.CourseId,
                }).Single();
            }
            var allCourses= _courseRepository.Query().Select(x => new Course()
            {
                Id = x.CourseId,
                ShortName = x.ShortName,
                FullName = x.FullName
            }).ToList();
            var fromDatabaseEF = new SelectList(allCourses, "Id", "ShortName");

            ViewData["DBMySkills"] = fromDatabaseEF;
            ViewBag.InsigniaList = new SelectList(allCourses.AsEnumerable(), "Id", "ShortName", Id);

            return View(fResult);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                bool overlap = false;
                var cClassroom = await _classroomRepository.GetAsync(classroom.Id);

                if (cClassroom == null)
                {


                    //check if there is overlap
                    overlap = chkOverlap(classroom);
                    if (!overlap)
                    {
                        var nClassroom = new Data.Classroom
                        {
                            ClassroomName = classroom.ClassroomName,
                            StartDate = classroom.StartDate,
                            StartTime = classroom.StartTime,
                            EndTime = classroom.EndTime,
                            CourseId = classroom.SelectedCourse
                        };
                        await _classroomRepository.InsertAsync(nClassroom);
                    }
  
                }
                else
                {
                    //check if there is overlap
                    overlap = chkOverlap(classroom);

                    if (!overlap)
                    {
                        cClassroom.ClassroomName = classroom.ClassroomName;
                        cClassroom.StartDate = classroom.StartDate;
                        cClassroom.StartTime = classroom.StartTime;
                        cClassroom.EndTime = classroom.EndTime;
                        cClassroom.CourseId = classroom.SelectedCourse;
                        await _classroomRepository.UpdateAsync(cClassroom);
                    }



                }


                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public bool chkOverlap(Classroom classroom)
        {
            bool overlap = false;

            //find classroom and check if there is overlap
            var chkClassroom = _classroomRepository.Query().Where(x => x.ClassroomName == classroom.ClassroomName);
            if (chkClassroom != null)
            {
                foreach (var item in chkClassroom)
                {
                    if (classroom.StartDate == item.StartDate)
                    {
                        overlap = classroom.StartTime < item.EndTime && item.StartTime < classroom.EndTime;
                        if (overlap)
                        {
                            break;
                        }
                    }

                }
            }

            //find course and check overlap
            var chkCourse = _classroomRepository.Query().Where(x => x.CourseId == classroom.SelectedCourse);
            if (chkCourse != null)
            {
                foreach (var item in chkCourse)
                {
                    if (classroom.StartDate == item.StartDate)
                    {
                        overlap = classroom.StartTime < item.EndTime && item.StartTime < classroom.EndTime;
                        if (overlap)
                        {
                            break;
                        }
                    }

                }
            }
            return overlap;
        }
    }
}
