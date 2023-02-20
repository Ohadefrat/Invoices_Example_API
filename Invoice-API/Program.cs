using Microsoft.EntityFrameworkCore;
using Invoice_API.Controllers.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(options =>options.AddPolicy(name: "UsersOrigins", policy =>
{
    policy.WithOrigins("http://localhost:4200/").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
})); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("UsersOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
