using Web.Authentication;
using Web.Authentication.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var cfgAuth = builder.Configuration.GetSection("Auth").Get<AuthenticationOptions>();
builder.Services.AddCustomAuthentication(cfgAuth);

//var cfgXML = builder.Configuration.GetSection("XML").Get<XMLReaderOptions>();
//builder.Services.AddXmlDataSource(cfgXML);

//#if DEBUG
// DEBUG: Убираем CORS 

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//#endif

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

//#if DEBUG
app.UseCors();
//#endif

app.Run();
