using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using WebProjectAPI.Data;
using WebProjectAPI.Controllers;
using WebProjectAPI.Interface;
using WebProjectAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect to DB
builder.Services.AddDbContext<ProjectApiDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectNetCore")));

builder.Services.AddAuthentication("Basic")
    .AddScheme<BasicAuthenticationOptions, CustomAuthenticationHandler>
    ("Basic",null);

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AllUsers", policy =>
        policy.RequireRole("Administrator", "Member", "Guest"));
    x.AddPolicy("UsersCanComment", policy =>
        policy.RequireRole("Administrator", "Member"));
}
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddSingleton<IJwtAuthentication>(new JwtAuthentication(key));
builder.Services.AddSingleton<ICustomAuthentication, CustomAuthentication>();
builder.Services.AddScoped<IPhotos, PhotosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
