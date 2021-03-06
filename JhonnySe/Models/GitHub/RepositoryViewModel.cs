﻿using System;

namespace JhonnySe.Models.GitHub
{
    public class RepositoryViewModel
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}