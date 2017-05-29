﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Data;
using OnlineCourses.Models;

namespace OnlineCourses.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet]
        public IActionResult Course()
        {
            return View(_context.Courses.ToList());
        }

        [HttpGet("Course/{id}")]
        public IActionResult CourseInfo(int id)
        {
            return View(_context.Courses.Find(id));
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
