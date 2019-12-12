using System;
using System.Collections.Generic;

namespace JhonnySe.Models
{
    public class PersonalInformation
    {
        public bool Employed { get; set; }
        public string CurrentEmployer { get; set; }
        public DateTime EmployedSince { get; set; }
        public IList<Employer> PreviousEmployers { get; set; } = new List<Employer>();
    }
}
