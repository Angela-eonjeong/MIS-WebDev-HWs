using System;
using System.ComponentModel.DataAnnotations;

namespace Kim_Angela_Eonjeong_HW3.Models
{ 
    public class SearchViewModel
    {
        [Display(Name = "Title")]
        public String Title { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "MPAA Rating")]
        public Rating? SearchRating { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "User Rating")]
        [Range(minimum:0, maximum:10, ErrorMessage = "Rating must be between 0.0 and 10.0")]
        public Decimal? SelectedUserRating { get; set; }

        [Display(Name = "Publisehd Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public DateTime? SearchReleaseDate { get; set; }


    }
}