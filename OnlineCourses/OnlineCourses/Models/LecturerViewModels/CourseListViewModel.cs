﻿using System.Collections;
using System.Collections.Generic;

namespace OnlineCourses.Models.LecturerViewModels
{
    public class CourseListViewModel:IEnumerable<Course>
    {
        public IEnumerable<Course> Courses { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string SearchString { get; set; }

        public IEnumerator<Course> GetEnumerator()
        {
            return Courses.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}