using ApiManeroProduct.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsApp", builder => 
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddDbContext<DataContext>(x => x.UseCosmos(builder.Configuration.GetConnectionString("CosmosDB")!,"ManeroDatabase"));

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
