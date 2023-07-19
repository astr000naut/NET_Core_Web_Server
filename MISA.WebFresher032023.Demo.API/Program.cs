using MISA.WebFresher032023.Demo.API.Middleware;
using MISA.WebFresher032023.Demo.BusinessLayer.Services;
using MISA.WebFresher032023.Demo.BusinessLayer.Services.AccountSvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Services.GroupSvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Services.LocationSvc;
using MISA.WebFresher032023.Demo.BusinessLayer.Services.ReceiptSvc;
using MISA.WebFresher032023.Demo.Common.Configs;
using MISA.WebFresher032023.Demo.DataLayer;
using MISA.WebFresher032023.Demo.DataLayer.Repositories;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.AccountRepo;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.GroupRepo;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.LocationRepo;
using MISA.WebFresher032023.Demo.DataLayer.Repositories.ReceiptRepo;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{   
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:8080");
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Dependency Injection

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped <ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped <IDepartmentRepository   , DepartmentRepository>();
builder.Services.AddScoped <IEmployeeRepository , EmployeeRepository>();
builder.Services.AddScoped <IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<ILocationService, LocationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();