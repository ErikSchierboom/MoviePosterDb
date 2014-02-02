namespace MoviePosterDb
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The MoviePosterDb API result.
    /// </summary>
    [DataContract]
    public class MoviePosterDbResult
    {
        /// <summary>
        /// Gets or sets the movie's IMDb ID.
        /// </summary>
        [DataMember(Name = "imdb")]
        public string ImdbMovieId { get; set; }

        /// <summary>
        /// Gets or sets the title of the movie.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the year of release for the movie.
        /// </summary>
        [DataMember(Name = "year")]
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets the result page.
        /// </summary>
        [DataMember(Name = "page")]
        public string Page { get; set; }

        /// <summary>
        /// Gets or sets the movie's posters.
        /// </summary>
        [DataMember(Name = "posters")]
        public MoviePosterDbPoster[] Posters { get; set; }
    }
}