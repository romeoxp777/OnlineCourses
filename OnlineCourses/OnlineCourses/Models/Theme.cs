﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourses.Models
{
    public class Theme
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<CourseTheme> CourseThemes { get; set; }
    }
}