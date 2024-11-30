using AdminPanelApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32))));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Requires email confirmation for account activation
    options.Password.RequireDigit = true;         // Enforce strong passwords
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
})
    .AddRoles<IdentityRole>() // Add Role support
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed Roles (Admin, Employee)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Create roles if they don't exist
        var roles = new[] { "Admin", "Employee" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Optionally: Seed an Admin user
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var adminEmail = "admin@example.com"; // Change as required
        var adminPassword = "Admin@1234";    // Change as required

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding roles and admin user: {ex.Message}");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add Authentication Middleware
app.UseAuthorization();  // Add Authorization Middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Auth}/{id?}"); // Redirect to Login if no user is authenticated

app.MapRazorPages();

app.Run();
