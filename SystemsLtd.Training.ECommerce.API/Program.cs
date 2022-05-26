using Microsoft.EntityFrameworkCore;
using SystemsLtd.Training.ECommerce.API.Middleware;
using SystemsLtd.Training.ECommerce.Repository;
using SystemsLtd.Training.ECommerce.Repository.Interface;
using SystemsLtd.Training.ECommerce.Service;
using SystemsLtd.Training.ECommerce.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ECommerceDBContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();
if(builder.Configuration.GetValue<bool>("Repository:IsStatic"))
{
    builder.Services.AddScoped<IProductRepository, ProductRepositoryStatic>();
}
else
{
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<HeaderValueMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
