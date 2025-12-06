using MyApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddIdentityServices(builder.Configuration);// Add DbContext and Identity
builder.Services.AddJwtAuthentication(builder.Configuration);// Add JWT Authentication
builder.Services.AddApplicationServices();// Add Application Services (Repositories, Services, AutoMapper)
builder.Services.AddAngularCors(builder.Configuration);// Add CORS

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAngularApp");
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


await app.SeedSuperAdminAsync();

app.Run();
