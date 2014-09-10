using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class SessionSummary
    {
        public BootCampSession BootCampSession { get; set; }
        public int CommentCount { get; set; }
        public double AverageSpeakerNote { get; set; }
        public double AverageContentNote { get; set; }
        public double AverageSupportNote { get; set; }
    }
}