using Microsoft.EntityFrameworkCore;
using PaymentAPiInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(x =>
    {
        x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});



string connectionString = builder.Configuration["ConnectionStrings:PaymentDB"];

var serverVersion = new MariaDbServerVersion(new Version(10, 3, 22));

builder.Services.AddDbContext<PaymentDbContext>(
dbContextOptions => dbContextOptions
   .UseMySql(connectionString, serverVersion, mySqlOptions =>
   {
       mySqlOptions.EnableRetryOnFailure();
   })
   // The following three options help with debugging, but should
   // be changed or removed for production.
   .LogTo(Console.WriteLine, LogLevel.Information)
   .EnableSensitiveDataLogging()
   .EnableDetailedErrors()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors(); //API uygulamasını dışarıya ehrhangi bir kral policy uygulamadan açmayayararan kod paraçası
app.UseStaticFiles(); //Resimler ve static osya ile çalışmka için kullanılır.
app.MapControllers();

app.Run();
