using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "PetFolio API",
            Version = "v1.0",
            Description = "API de registro do seu Pet",
            Contact = new()
            {
                Name = "Projeto de Desenvolvimento Petfolio",
                Email = "joaoangello10@gmail.com"
            }
        };
        return Task.CompletedTask;
    });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/api-docs", (options, httpContext) =>
    {
        var isAdmin = httpContext.User.IsInRole("Admin");
        options
            .WithTitle(isAdmin ? "Admin Api" : "Public Api")
            .WithTitle($"Petfolio Api Documentation \nApi para {httpContext.User.Identity?.Name}")
            .WithTheme(ScalarTheme.DeepSpace)
            .EnableDarkMode()
            .ExpandAllTags()
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithSearchHotKey("k")
            .HideClientButton();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();