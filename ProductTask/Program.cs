
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProductBL.ProductBL;
using ProductDataAL.Data.Context;
using ProductDataAL.Repositories.ProductRepository;

var builder = WebApplication.CreateBuilder(args);

var MyAllowOrigins = "_myAllowcOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();



                      });
});

// Add services to the container.

builder.Services.AddScoped<ProductContext, ProductContext>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductBLayer, ProductBLayer>();




#region Default 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion



#region Context

var connectionString = builder.Configuration.GetConnectionString("ProductConnectionString");
builder.Services.AddDbContext<ProductContext>(option =>
option .UseSqlServer(connectionString));


#endregion


#region RegisteringAutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion


# region MiddleWares

var app = builder.Build();

app.UseCors(MyAllowOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});


app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion