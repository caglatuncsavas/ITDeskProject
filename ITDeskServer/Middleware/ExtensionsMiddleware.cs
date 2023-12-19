using ITDeskServer.Context;
using ITDeskServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITDeskServer.Middleware;

public static class ExtensionsMiddleware
{
    public static void AutoMigration(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var context = scoped.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();//update-database
        }
        
    }
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (!userManager.Users.Any())//if there is no user in the database, create a user
            {
                userManager.CreateAsync(new()
                {
                    Email = "test@test.com",
                    UserName = "test",
                    FirstName = "IT",
                    LastName = "Admin",
                    EmailConfirmed = true,
                }, "Password12*").Wait();
            }
        }
    }

    public static void CreateRoles(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new AppRole()
                {
                   Id=Guid.NewGuid(),
                   Name="Admin",
                   NormalizedName="ADMIN"
                }).Wait();
            }
        }
    }

    public static void CreateUserRoles(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var context= scoped.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            AppUser? user = userManager.Users.FirstOrDefault(p => p.Email == "test@test.com");
            if(user is not null)
            {
                AppRole? role = roleManager.Roles.FirstOrDefault(p => p.Name == "Admin");
                if(role is not null)
                {
                    bool userRoleExist = context.AppUserRoles.Any(p=>p.RoleId == role.Id && p.UserId == user.Id);
                    if (!userRoleExist)
                    {
                        AppUserRole appUserRole = new()
                        {
                            RoleId = role.Id,
                            UserId = user.Id
                        };
                        context.AppUserRoles.Add(appUserRole);
                        context.SaveChanges();
                    }
                    
                   
                }
            }
        }
    }
}

