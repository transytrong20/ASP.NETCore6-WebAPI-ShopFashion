using Shop.WebApp.Web.Infrastructures.Configurations;
using Shop.WebApp.Web.Infrastructures.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.SwaggerServiceConfig();
// Add services to the container.

//customErrors
//builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//    options.InvalidModelStateResponseFactory = actionContext =>
//    {
//        var modelState = actionContext.ModelState.Values;
//        return new BadRequestObjectResult(new ErrorModel
//        {
//            Status =(int) HttpStatusCode.BadRequest,
//            Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
//            Error = modelState.SelectMany(x => x.Errors, (x,y)=> y.ErrorMessage).ToList(),
//        });
//    };
//});

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

//app.UseExceptionHandler(errorApp =>
//{
//    errorApp.Run(async context =>
//    {
//        context.Response.StatusCode = 500;
//        context.Response.ContentType = "text/html";

//        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
//        var exception = exceptionHandlerPathFeature?.Error;

//        await context.Response.WriteAsync($"<h1>Error: {exception.Message}</h1>").ConfigureAwait(false);
//    });
//});

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