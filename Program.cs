using Microsoft.EntityFrameworkCore;
using AngularApiAssignment1.Data;
using AngularApiAssignment1.Data.Abstract;
using AngularApiAssignment1.Data.Repositories;
using AngularApiAssignment1.AutomapperConfig;
using AngularApiAssignment1.Data.Abstract;
using AngularApiAssignment1.Data.Repositories;
using AngularApiAssignment1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ManageEmployeesContext>(
                o => o.UseNpgsql(builder.Configuration.GetConnectionString("Ef_Postgres_Db"))
            );
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddAutoMapper(typeof(MapperConfig));
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
