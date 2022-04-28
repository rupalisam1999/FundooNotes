using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer
{
   public class NotePostModel
    {
        [Required(ErrorMessage = " Title is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = " BGColor is Required")]
        public string BGColor { get; set; }
        



    }
}
