using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//part of Session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//part of Session
app.UseSession();

app.UseAuthentication();    
app.UseAuthorization();

app.MapRazorPages();

app.Run();
