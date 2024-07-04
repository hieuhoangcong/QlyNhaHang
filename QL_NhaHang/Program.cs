using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using QL_NhaHang.Models;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using QL_NhaHang;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<QlNhaHangContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddAuthenticationCore();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
