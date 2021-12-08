using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace WebMovie.Models

{
    public class Movie
    {

        public string Title { get; set; }
        public string Genre { get; set; }
        public int Years { get; set; }
        public string Runtime { get; set; }

    }
}
