using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey=true,
                  ValidIssuer = "MyApplication",  // API'den aldýðýnýz issuer'ý burada kullanýn
                  ValidAudience = "MyApplicationUsers",  // API'den aldýðýnýz audience'ý burada kullanýn
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9ca62ffa5a37050cad2ff0aaea56a6a4eaa7fd2ebf4282c4d22eb221b067f3bd825651ad5ec577b43556eedbc6ada542a1ba7ee96ca920b2d5e9ff7b2e7411e9"))
              };
          });
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // Memory cache kullanarak session verisini saklayýn
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session'ýn süresini ayarlayabilirsiniz
    options.Cookie.IsEssential = true; // Cookie'nin önemli olduðunu belirtin
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
