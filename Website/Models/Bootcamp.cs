using System;
using System.Collections.Generic;

namespace Website.Models
{
    public class Bootcamp
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public bool Current { get; set; }
        public ICollection<BootCampSession> Sessions { get; set; }
    }
}