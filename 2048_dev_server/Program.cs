using _2048_dev_server.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5000");

// CORS 설정 추가
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()   // 혹은 .WithOrigins("http://localhost:3000") 처럼 특정 Origin만 허용
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// PostgreSQL 연결 문자열
var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=gamepassword;Database=game2048db";

// DB 연결
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseNpgsql(connectionString));

// 컨트롤러 활성화
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll"); // 🔥 이 줄 추가

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    Console.WriteLine("✔ Swagger UI is being configured");
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () => "2048 server is running"); // 🔥 루트 응답 추가

// 컨트롤러 엔드포인트
app.MapControllers();

app.Run();