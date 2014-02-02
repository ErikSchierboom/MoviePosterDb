namespace MoviePosterDbDemo.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using DataAnnotationsExtensions;

    using MoviePosterDb;

    public class IndexViewModel : IValidatableObject
    {
        public IndexViewModel()
        {
            this.ImageWidth = 300;
        }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string ApiSecret { get; set; }

        [Min(1)]
        public int? ImdbMovieId { get; set; }
        
        [RegularExpression(@"https?://.*?imdb.com/title/tt(\d{7})/?.*", ErrorMessage = "Not a valid IMDb movie URL.")]
        public string ImdbMovieUrl { get; set; }

        [Range(30, 300)]
        public int ImageWidth { get; set; }

        public MoviePosterDbResult MoviePosterDbResult { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ImdbMovieId == null && string.IsNullOrEmpty(this.ImdbMovieUrl))
            {
                yield return new ValidationResult("Either an IMDb movie ID or IMDb movie URL must be specified.");    
            }
        }
    }
}