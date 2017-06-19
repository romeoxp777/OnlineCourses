﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OnlineCourses.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string ImageURL{get; set;}
        public string AboutMe { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string Skype { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<Course> CreatedCourses { get; set; }
        public List<Comment> Comments { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; }
        
        public List<Subscription> ValidSubscriptions()=> FamilyMembers == null
                    ? Subscriptions
                    : Subscriptions
                        .Concat(FamilyMembers.SelectMany(m => m.Member.Subscriptions)).Distinct().ToList();
         

        public string FullName => $"{LastName} {FirstName}";
        public string ValidImageURL => string.IsNullOrWhiteSpace(ImageURL) ? "img/no_image.png" : ImageURL;
    }


}
