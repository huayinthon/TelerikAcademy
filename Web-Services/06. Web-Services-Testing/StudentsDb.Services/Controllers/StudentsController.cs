﻿using StudentsDb.Models;
using StudentsDb.Repositories;
using StudentsDb.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentsDb.Services.Controllers
{
    public class StudentsController : ApiController
    {
        private ApiControllerHelper apiControllerHelper;

        public StudentsController()
        {
            var includes = new[] { "School", "Marks" };
            apiControllerHelper = new ApiControllerHelper(new StudentsDbRepository(), includes);
        }

        // GET api/<controller>
        public IEnumerable<StudentDto> Get()
        {
            var students = apiControllerHelper.Get<Student>().AsQueryable();
            var result =
                from student in students
                select new StudentDto
                {
                    Id = student.Id,
                    SchoolId = student.SchoolId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Grade = student.Grade
                };

            return result;
        }

        public StudentDto Get(int id)
        {
            var student = apiControllerHelper.Get<Student>(id);
            if (student == null)
            {
                throw new ArgumentException(string.Format("Student with id = {0} doesn't exist.", id));
            }

            return new StudentDto
            {
                Id = student.Id,
                SchoolId = student.SchoolId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Grade = student.Grade,
                Marks =
                (from mark in student.Marks
                 select new MarkDto
                 {
                     Id = mark.Id,
                     Subject = mark.Subject,
                     Value = mark.Value
                 }).AsEnumerable()
            };
        }

        public HttpResponseMessage Post(StudentDto value)
        {
            var newStudent = new Student
            {
                SchoolId = value.SchoolId,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Age = value.Age,
                Grade = value.Grade
            };

            HttpResponseMessage response;

            try
            {
                apiControllerHelper.Post<Student>(newStudent);

                if (value.Marks != null)
                {
                    foreach (var mark in value.Marks)
                    {
                        apiControllerHelper.Post<Mark>(new Mark
                        {
                            StudentId = newStudent.Id,
                            Subject = mark.Subject,
                            Value = mark.Value
                        });
                    }
                }

                var createdStudentDto = new StudentDto()
                {
                    Id = newStudent.Id,
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    Age = newStudent.Age,
                    Grade = newStudent.Grade
                };

                response = Request.CreateResponse<StudentDto>(HttpStatusCode.Created, createdStudentDto);
                var resourceLink = Url.Link("DefaultApi", new { id = createdStudentDto.Id });

                response.Headers.Location = new Uri(resourceLink);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return response;
        }
    }
}
