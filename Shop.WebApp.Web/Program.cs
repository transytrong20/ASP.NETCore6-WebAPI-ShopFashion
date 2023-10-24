using Shop.WebApp.Web.Infrastructures.Configurations;
using Shop.WebApp.Web.Infrastructures.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.SwaggerServiceConfig();
// Add services to the container.

builder.Services.AutoRegister(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.JWTConfiguration(builder.Configuration);
builder.UseSerialLog();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "dev-policy",
        builder =>
        {
            builder
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("Content-Disposition");
        });
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseCors("dev-policy");
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
//         "D:/CODE/angular/BE_Shop/Shop.Webapp.Web/testimg/news/"), // Đường dẫn tới thư mục tài nguyên tĩnh của bạn
//    RequestPath = "/customfiles" // Đường dẫn URL mà bạn muốn sử dụng để truy cập tới các tệp tài nguyên tĩnh
//});
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//           Path.Combine(builder.Environment.ContentRootPath, "testimg")),
//    RequestPath = "/testimg"
//});

app.UseHttpsRedirection();
app.UseExceptionMiddleWare();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();