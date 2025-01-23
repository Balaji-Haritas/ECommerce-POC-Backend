using EcommercePOC.DataAccess;
using EcommercePOC.Repository;
using EcommercePOC.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Adding DB Configuration
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceDBConnection")));

//Adding Repositories
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();

//Adding Cross platform connection
builder.Services.AddCors
    (options => { 
        options.AddPolicy("AllowAllOrigins", builder => { 
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader(); 
        });
    });

//Adding Controller
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
