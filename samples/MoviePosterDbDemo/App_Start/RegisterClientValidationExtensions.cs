using MoviePosterDbDemo;

using WebActivator;

[assembly: PreApplicationStartMethod(typeof(RegisterClientValidationExtensions), "Start")]

namespace MoviePosterDbDemo
{
    using DataAnnotationsExtensions.ClientValidation;

    public static class RegisterClientValidationExtensions
    {
        public static void Start()
        {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();
        }
    }
}