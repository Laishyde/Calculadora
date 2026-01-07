using Calculadora_teste.Services;
using Calculadora_teste.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Razor Pages
builder.Services.AddRazorPages();

// Registra o HttpClient + serviço
builder.Services.AddHttpClient<ICalcApiService, CalcApiService>(client =>
{
    client.BaseAddress = new Uri("http://intranet.pekus.com.br/calcapi/");
    client.DefaultRequestHeaders.Add("ApiKey", "LAIS-0107");
});

var app = builder.Build();

// Configura pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
