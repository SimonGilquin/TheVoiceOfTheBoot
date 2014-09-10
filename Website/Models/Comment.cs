using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class Comment
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public BootCampSession Session { get; set; }
        [Range(0, 4)]
        public int SpeakerNote { get; set; }
        [Range(0, 4)]
        public int SessionContentNote { get; set; }
        [Range(0, 4)]
        public int SupportNote { get; set; }
        public string CommentText { get; set; }
        public string ImageUrl { get; set; }

        public string UserLogin { get; set; }
    }
}