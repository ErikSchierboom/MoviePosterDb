namespace MoviePosterDbDemo.Controllers
{
    using System;
    using System.Web.Mvc;

    using MoviePosterDb;

    using MoviePosterDbDemo.Models;

    public class HomeController : Controller
    {
        [OutputCache(Duration = 3600)]
        public ViewResult Index(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var moviePosterDbService = new MoviePosterDbService(model.ApiKey, model.ApiSecret);

                if (model.ImdbMovieId.HasValue)
                {
                    model.MoviePosterDbResult = moviePosterDbService.Search(model.ImdbMovieId.Value, model.ImageWidth);
                }
                else
                {
                    model.MoviePosterDbResult = moviePosterDbService.Search(new Uri(model.ImdbMovieUrl), model.ImageWidth);
                }
            }
            
            return this.View(model);
        }
    }
}