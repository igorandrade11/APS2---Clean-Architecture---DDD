using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Repositories;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configura MVC e o caminho personalizado das Views
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        // Limpa os caminhos padrões
        options.ViewLocationFormats.Clear();

        // Caminho personalizado para as Views dentro de /Web/Views
        options.ViewLocationFormats.Add("/Web/Views/{1}/{0}.cshtml");     // {1} = Controller, {0} = Action
        options.ViewLocationFormats.Add("/Web/Views/Shared/{0}.cshtml");  // Views compartilhadas (_Layout, etc.)
    })
    .AddRazorRuntimeCompilation(); // Atualiza views sem precisar recompilar

// Configuração do banco de dados (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Injeção de dependências (IoC)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configuração de erros e segurança
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Rota padrão (Home/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}"
);

app.Run();
