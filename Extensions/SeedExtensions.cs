using MovieApi.Data;
using System.Diagnostics;

namespace MovieApi.Extensions;

public static class SeedExtensions
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
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
