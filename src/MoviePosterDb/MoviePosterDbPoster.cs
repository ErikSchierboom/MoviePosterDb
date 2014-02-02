namespace MoviePosterDb
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A MoviePosterDb poster.
    /// </summary>
    [DataContract]
    public class MoviePosterDbPoster
    {
        /// <summary>
        /// Gets or sets the URL to the poster image. 
        /// </summary>
        [DataMember(Name = "image_location")]
        public string Url { get; set; }
    }
}