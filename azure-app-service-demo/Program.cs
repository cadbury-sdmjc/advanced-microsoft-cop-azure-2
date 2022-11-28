var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapGet("/", () => "hello world");
app.MapGet("/executing-instance", () => app.Configuration["WEBSITE_INSTANCE_ID"][..4]);
app.Run();