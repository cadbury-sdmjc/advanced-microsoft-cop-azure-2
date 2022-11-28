var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapGet("/", () => "hello world");

app.Run();