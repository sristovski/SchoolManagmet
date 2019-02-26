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
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            StudentsDataView result = new StudentsDataView();
            result.list  = _studentRepository.Query().Select(x=> new Student()
            {
                Id=x.StudentId,
                FirstName=x.FirstName,
                LastName=x.LastName,
                Email=x.Email,
                Phone=x.Phone
            }).ToList();

            return View(result);
        }

        public ActionResult Edit(int Id)
        {
            var std = _studentRepository.Query().Where(x => x.StudentId == Id).Select(x => new Student()
            {
                Id = x.StudentId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone
            }).FirstOrDefault();
            return View(std);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                var student = await _studentRepository.GetAsync(std.Id);

                if (student == null)
                {
                    var nStudent = new Data.Student
                    {
                        FirstName = std.FirstName,
                        LastName = std.LastName,
                        Email = std.Email,
                        Phone = std.Phone
                    };
                    await _studentRepository.InsertAsync(nStudent);
                }
                else
                {
                    student.FirstName = std.FirstName;
                    student.LastName = std.LastName;
                    student.Email = std.Email;
                    student.Phone = std.Phone;
                    await _studentRepository.UpdateAsync(student);
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
