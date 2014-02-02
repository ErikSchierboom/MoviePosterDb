namespace MoviePosterDb
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MoviePosterDbResult
    {
        [DataMember(Name = "imdb")]
        public string ImdbMovieId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "year")]
        public string Year { get; set; }

        [DataMember(Name = "page")]
        public string Page { get; set; }

        [DataMember(Name = "posters")]
        public MoviePosterDbPoster[] Posters { get; set; }
    }
}