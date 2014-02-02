namespace MoviePosterDb
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MoviePosterDbPoster
    {
        [DataMember(Name = "image_location")]
        public string ImageLocation { get; set; }
    }
}