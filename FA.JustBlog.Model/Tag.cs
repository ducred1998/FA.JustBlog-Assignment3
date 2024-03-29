﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        public string ? Name { get; set; }
        public virtual IEnumerable<TagPost> ? TagPosts { get; set; }

    }
}
