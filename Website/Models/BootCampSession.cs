using System.Collections.Generic;

namespace Website.Models
{
    public class BootCampSession
    {
        public int Id { get; set; }
        public int BootcampId { get; set; }
        public Bootcamp Bootcamp { get; set; }
        public string Hour { get; set; }
        public string Title { get; set; }
        public string Speakers { get; set; }
        public string Description { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}