using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnMobileCodingTask.Models
{
    public class UserDomain
    {
        [Required]
        [MaxLength(10,ErrorMessage = "Only 10 digits can be entered")]
        [MinLength(10,ErrorMessage ="Minimum 10 digits should be entered")]
        public double MobileNumber { get; set; }
    }
}