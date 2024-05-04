using _334_group_project_web_api.DBSettings;
using _334_group_project_web_api.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<UserAccountDatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.Configure<GrocerDatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.Configure<MealPlanDatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.Configure<OrderDatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.Configure<ProductDatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.Configure<ShoppingListDatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.AddSingleton<UserAccountService>();
builder.Services.AddSingleton<ShoppingListService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<GrocerService>();
builder.Services.AddSingleton<MealPlanService>();
builder.Services.AddSingleton<OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());


app.MapControllers();

app.Run();
