using AzureSinglePageApp.Server;
using AzureSinglePageApp.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? string.Empty;
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddAuthorization();
        builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapIdentityApi<ApplicationUser>();

        app.MapPost("/logout", async (SignInManager<ApplicationUser> siginInManager) =>
        {
            await siginInManager.SignOutAsync();
            return Results.Ok();
        }).RequireAuthorization();

        app.MapGet("/pingauth", (ClaimsPrincipal user) =>
        {
            var email = user.FindFirstValue(ClaimTypes.Email); // get the user's email from the claim
            return Results.Json(new { Email = email }); //return the email as a plain text response
        }).RequireAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        app.Run();
    }
    
}