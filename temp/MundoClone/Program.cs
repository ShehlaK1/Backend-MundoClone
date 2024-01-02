using ORM.DatabaseContext;
using RESTCore.ServiceExtensions;
using RESTCore.Services;

var builder = WebApplication.CreateBuilder(args);

//Adding DB Context
builder.Services.AddDbContext<AppDbContext>();

//builder.Services.AddScoped<UserGenericRepo>();

// Add services to the container.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TargetSkillService>();
builder.Services.AddScoped<YearService>();
builder.Services.AddScoped<GradeService>();
builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<AssessmentService>();
builder.Services.AddScoped<CompositeLevelService>();
builder.Services.RegisterRepositories(builder.Environment)

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

app.UseAuthorization();

app.MapControllers();

app.Run();
