using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.Services.Order;
using eCommerceWebAPI.Services.Orders;
using eCommerceWebAPI.Services.Productcategories;
using eCommerceWebAPI.Services.ProductServices;
using eCommerceWebAPI.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Text;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
    });
});




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


builder.Services.AddScoped<IProductcategoryRepository, ProductcategoryServices>();
builder.Services.AddScoped<IUserRepository, UserServices>();
builder.Services.AddScoped<IProductRepository, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderService>();




var _loggrer = new LoggerConfiguration()
.WriteTo.File("C:\\Users\\User\\Documents\\B E\\demo\\cleanU\\1 alldone,miner chnge hv in struc of prooder\\1\\1 t4 done\\eCommerceWebAPI_3-master\\Logs\\ApiLog-.log", rollingInterval: RollingInterval.Day)
.CreateLogger();
builder.Logging.AddSerilog(_loggrer);

//add cors


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
