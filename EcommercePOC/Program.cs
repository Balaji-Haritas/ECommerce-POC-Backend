using EcommercePOC.Extensions;
using EcommercePOC.Helpers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);

//Adding Cloudinary Configuration.
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

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

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
