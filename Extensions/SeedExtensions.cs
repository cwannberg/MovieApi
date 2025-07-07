using MovieApi.Data;
using System.Diagnostics;

namespace MovieApi.Extensions;

public static class SeedExtensions
{
    //Vi lägger till en extensionmetod, alltså vi skapar en egen metod till IApplicationBuilder
    //SeedDataAsync hjälper appen att förbereda databasen med data (seed) när appen startar.
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        //Skapar ett _Tillfälligt_ scope för att hämta tjänster t.ex. databaskoppling. Slängs sen.
        using (var scope = app.ApplicationServices.CreateScope())
        {
            //Tar fram en tjänstekatalog - som en låda med allt som behövs, t.ex. databas (DbContext), logger osv.
            var serviceProvider = scope.ServiceProvider;

            //Hämtar databaskopplingen (MovieApiContext)
            var context = serviceProvider.GetRequiredService<MovieApiContext>;

            try
            {
                await SeedData.InitAsync(context);
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
