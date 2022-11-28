using System.Text.Json;
using Azure.Identity;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

var containerName = "blob-container";
var containerClient = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=advmsftcop;AccountKey=Jf0Jv6M+SvJ9+htTzqY5YBDQUxokFR/yk1qMhhObAYZco9iIzFvbs1HmJuibnMlUSUEoX/mCU21t+AStIp9gUA==;EndpointSuffix=core.windows.net", containerName);
var blobName = "hello-world-blob";

app.MapGet("/", () => "hello world");
app.MapGet("/executing-instance", () => app.Configuration["WEBSITE_INSTANCE_ID"][..4]);
app.MapGet("/update-blob", async () =>
{
    await containerClient.UploadBlobAsync(blobName, BinaryData.FromString(DateTime.UtcNow.ToString()));
});

app.MapGet("/read-blob", async () =>
{
    var blob = await containerClient.GetBlobClient(blobName).DownloadContentAsync();
    return blob.Value.Content.ToString();
});

app.MapGet("/read-blob-mi", async () =>
{
    try
    {
        var miClient = new BlobContainerClient(new($"https://advmsftcop.blob.core.windows.net/{containerName}"), new DefaultAzureCredential());
        var blob = await miClient.GetBlobClient(blobName).DownloadContentAsync();
        return blob.Value.Content.ToString();
    }
    catch (Exception e)
    {
        return JsonSerializer.Serialize(new
        {
            e.Message,
            e.StackTrace,
        });
    }
});
app.Run();