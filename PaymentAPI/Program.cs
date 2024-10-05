using Microsoft.EntityFrameworkCore;
using PaymentAPiInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Bağlantı dizesini al
string connectionString = builder.Configuration["ConnectionStrings:PaymentDB"];
var serverVersion = new MariaDbServerVersion(new Version(10, 3, 22));

// DbContext'i ekle
builder.Services.AddDbContext<PaymentDbContext>(dbContextOptions =>
    dbContextOptions
        .UseMySql(connectionString, serverVersion, mySqlOptions =>
        {
            mySqlOptions.EnableRetryOnFailure();
        })
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

// CORS ayarlarını ekle
builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(x =>
    {
        x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); // CORS ayarlarını uygulayın
app.UseStaticFiles(); // Statik dosyalar için
app.UseAuthorization();

app.MapControllers();

app.Run();
