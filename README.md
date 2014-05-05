# MoviePosterDb
A library to access the [API](http://api.movieposterdb.com/) for the [MoviePosterDb website](http://www.movieposterdb.com). This website contains a very large collection of movie posters.

## What can it be used for?
The MoviePosterDB website hosts movie posters. This library allows one to find a movie's posters based on their IMDb movie ID or IMDb movie URL.

## Usage
Let's say we want to find the poster for the movie Inception. The first step is to find its IMDb page, which is located at: http://www.imdb.com/title/tt1375666/reference.
As can be seen in the URL, Inception's IMDb movie ID is **1375666**. We can use this ID (or the complete URL) to search for the movie's posters. 

To retrieve the poster URL we first need to create an instance of the `MoviePosterDbService` class. This class' constructor takes two arguments:

1. `apiKey`: the API key.
2. `apiSecret`: the API secret.

Note: this API key and secret need to be requested from the MoviePosterDB crew, see [this page](http://www.movieposterdb.com/help/contact/#api).

If you have an API key and secret, the `MoviePosterDbService` instance can be created as follows:

```c#
var moviePosterDbService = new MoviePosterDbService("demo key", "demo secret");
```

Now we have two options for finding the movie's posters.

1. Search using an IMDb movie ID (which was **1375666**).
2. Search using an IMDb movie URL (which was **http://www.imdb.com/title/tt1375666/reference**).

We'll show both options, but we'll start with the first one. Retrieving the movie's poster information is as simple as calling the `Search` method on a `MoviePosterDbService` instance:

```c#
var imdbMovieId = 1375666;
var moviePosterDbResult = moviePosterDbService.Search(imdbMovieId);

//{
//  "Title": "Inception",
//  "Year": "2010",
//  "ImdbMovieId": "1375666",
//  "Posters": [
//    { "Url": "http://api.movieposterdb.com/cache/normal/66/1375666/1375666_300.jpg" }
//  ]
//}
```
        
The `Search` method also has an overload that allows one to specify the desired maximum width of the returned poster. This image width parameter must be a number between [30-300].
If you try to find the poster result again using an IMDb movie ID but now with an image width of 100, we get returned a different poster url:

```c#
var imdbMovieId = 1375666;
var imageWidth = 100;
var moviePosterDbResult = moviePosterDbService.Search(imdbMovieId, imageWidth);

//{
//  "Title": "Inception",
//  "Year": "2010",
//  "ImdbMovieId": "1375666",
//  "Posters": [
//    { "Url": "http://api.movieposterdb.com/cache/normal/66/1375666/1375666_100.jpg" }
//  ]
//}
```

Note: some movies only have posters in a single size, which means that regardless of what you pass as the image size you'll get returned the same poster URL.

Option two is to search by the movie's IMDb url:

```c#
var imdbMovieUrl = new Uri("http://www.imdb.com/title/tt1375666/reference");
var moviePosterDbResult = moviePosterDbService.Search(imdbMovieUrl);

//{
//  "Title": "Inception",
//  "Year": "2010",
//  "ImdbMovieId": "1375666",
//  "Posters": [
//    { "Url": "http://api.movieposterdb.com/cache/normal/66/1375666/1375666_300.jpg" }
//  ]
//}
```

This method also has an overload taking in the image width:

```c#
var imdbMovieUrl = new Uri("http://www.imdb.com/title/tt1375666/reference");
var imageWidth = 100;
var moviePosterDbResult = moviePosterDbService.Search(imdbMovieUrl, imageWidth);

//{
//  "Title": "Inception",
//  "Year": "2010",
//  "ImdbMovieId": "1375666",
//  "Posters": [
//    { "Url": "http://api.movieposterdb.com/cache/normal/66/1375666/1375666_100.jpg" }
//  ]
//}
```
   
Note that there both search methods (by ID or by URL) return the same result, it does not matter if you use the URL of ID version.

# Test

The solution also contains a sample website application, which allows you to test the MoviePosterDB API using this library to communicate with the API.

## Get it on NuGet!
The library is available as a [NuGet package](http://www.nuget.org/packages/MoviePosterDb/). You can install it using the following command:

    Install-Package MoviePosterDb

## History
Date       | Version | Changes
---------- | ------- | -------------------
2014-02-05 | 1.1.0   | Streamlined API.
2014-02-01 | 1.0.0   | None. Initial version.

## License
[Apache License 2.0](LICENSE)