using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using TanuEntityFramework;
using TanuEntityFramework.Container;
using TanuEntityFramework.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TanuDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver")));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddCors(p => p.AddPolicy("tanupolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}
));

builder.Services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName: "FixedWindow", options =>
{
    options.Window = TimeSpan.FromSeconds(10);
    options.PermitLimit = 1;
    options.QueueLimit = 0;
    options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
}).RejectionStatusCode=401);

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
app.UseCors("tanupolicy");
app.UseRateLimiter();

app.Run();
