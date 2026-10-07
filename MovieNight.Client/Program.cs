var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add DB. Schema automatically generated.
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlite("Data Source=movie_night.db")
);
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlite("Data Source=movie_night.db")
);
builder.Services.AddDbContext<ActorContext>(options =>
    options.UseSqlite("Data Source=movie_night.db")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
