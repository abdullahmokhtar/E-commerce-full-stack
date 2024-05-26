﻿using Backend.BLL.DataSeed;
using Backend.DAL.Context;

namespace Backend.API.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    
                    await AppContextSeed.SeedAsync(context);
                }catch(Exception ex)
                {
                     Console.WriteLine(ex);
                }
            }
        }
    }
}
