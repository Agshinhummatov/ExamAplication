using ExamAplication.Admin.Services;
using ExamAplication.Admin.Services.Inerfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStudentApiService, StudentApiService>();
builder.Services.AddScoped<ILessonApiService, LessonApiService>();
builder.Services.AddHttpClient("ExamApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:7700/api/");
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");


app.Run();