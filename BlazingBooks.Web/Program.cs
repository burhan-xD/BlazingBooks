using BlazingBooks.Shared.Interfaces;
using BlazingBooks.Web.Components;
using BlazingBooks.Web.Data;
using BlazingBooks.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<BookContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("BlazingBooks");
    options.UseSqlServer(connectionString);
});

// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddTransient<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
