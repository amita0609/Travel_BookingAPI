using Microsoft.EntityFrameworkCore;
using TravelBookingAPI;
using TravelBookingAPI.Data;
using TravelBookingAPI.Repository;
using TravelBookingAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(Option =>
{
    Option.UseSqlServer(builder.Configuration.GetConnectionString("DefaulSQLConnection"));
});
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
