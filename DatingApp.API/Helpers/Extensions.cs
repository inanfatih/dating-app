using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    //Class i static yaptigimiz icin bunu kullanabilmek icin instance larini olusturmak gerekmiyor.
    public static class Extensions
    {
        // Extension methods are defined as static methods but are called by using instance method syntax. Their first parameter specifies which type the method operates on. The parameter is preceded by the this modifier. Extension methods are only in scope when you explicitly import the namespace into your source code with a using directive.
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            // Asagidaki eklemeler sayesinde "AddApplicationError" ile asagidaki hata kodlarini eklemis oluyoruz response Header'a 
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}