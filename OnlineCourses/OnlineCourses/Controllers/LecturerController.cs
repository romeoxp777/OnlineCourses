using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Data;
using OnlineCourses.Models;
using OnlineCourses.Models.LecturerViewModels;

namespace OnlineCourses.Controllers
{
    [Authorize(Roles = RolesData.Lecturer)]
    public class LecturerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public LecturerController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        [HttpGet("Lecturer/CreateCourse")]
        public IActionResult CreateCourse()
        {
            return View();
        }


        // POST: api/Lecturer
        [HttpPost("Lecturer/CreateCourse")]
        public async Task<IActionResult> CreateCourse(CreateCourseViewModel createCourseViewModel)
        {
            try
            {
                //image upload
                string imageUrl = null;
                if (createCourseViewModel.Image != null)
                {
                    string path = Path.Combine("images","courseLogos",Guid.NewGuid()+Path.GetExtension(createCourseViewModel.Image.FileName));

                    // saving image in wwwroot
                    using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                    {
                        await createCourseViewModel.Image.CopyToAsync(fileStream);
                    }
                    imageUrl = path;
                }

                var course = new Course()
                {
                    Title = createCourseViewModel.Title,
                    Description = createCourseViewModel.Description,
                    Author = GetCurrentUser().Result,
                    CreationDate = DateTime.Today,
                    ModificationDate = DateTime.Today,
                    Estimate = createCourseViewModel.Estimate,
                    Price = createCourseViewModel.Price,
                    ImageURL = imageUrl
                };
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return View("CourseEditor");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourse([FromBody] int courseID)
        {
            try
            {
                _context.Courses.Remove(_context.Courses.Find(courseID));

                await _context.SaveChangesAsync();
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { result = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson([FromBody] Lesson lesson)
        {
            try
            {
                _context.Lessons.Add(new Lesson()
                {
                    Order = lesson.Order,
                    Course = lesson.Course,
                    Title = lesson.Title,
                    Description = lesson.Description
                });

                await _context.SaveChangesAsync();
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { result = false });
            }
        }

        public async Task<IActionResult> DeleteLesson([FromBody] int lessonID)
        {
            try
            {
                _context.Lessons.Remove(_context.Lessons.Find(lessonID));

                await _context.SaveChangesAsync();
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { result = false });
            }
        }

        public async Task<IActionResult> AddTextBlock([FromBody] TextBlock textBlock)
        {
            try
            {
                _context.TextBlocks.Add(new TextBlock()
                {
                    Lesson = textBlock.Lesson,
                    Text = textBlock.Text
                });

                await _context.SaveChangesAsync();
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { result = false });
            }
        }

        public async Task<IActionResult> DeleteTextBlock([FromBody] int textBlockID)
        {
            try
            {
                _context.TextBlocks.Remove(_context.TextBlocks.Find(textBlockID));

                await _context.SaveChangesAsync();
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { result = false });
            }
        }

        public async Task<IActionResult> AddVideoBlock([FromBody] TextBlock videoBlock)
        {
            try
            {
                _context.VideoBlocks.Add(new VideoBlock()
                {
                    Lesson = videoBlock.Lesson,
                    //VideoURL = videoBlock.
                });

                await _context.SaveChangesAsync();
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { result = false });
            }
        }

        /*public async Task<IActionResult> DeleteVideoBlock([FromBody] int textBlockID)
        {
            try
            {
                _context.TextBlocks.Remove(_context.TextBlocks.Find(textBlockID));

                await _context.SaveChangesAsync();
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { result = false });
            }
        }*/

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}