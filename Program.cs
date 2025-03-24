using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using crmcsharp.Services; // Assurez-vous d'importer l'espace de noms de votre service

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur.
builder.Services.AddControllersWithViews();

// Enregistrer le service TicketService
builder.Services.AddHttpClient<TicketService>(); // Si TicketService utilise HttpClient
// OU
builder.Services.AddScoped<TicketService>(); // Si TicketService ne nécessite pas HttpClient
builder.Services.AddHttpClient<TicketExpenseService>(); // Si TicketService utilise HttpClient
// OU
builder.Services.AddScoped<TicketExpenseService>(); // Si TicketService ne nécessite pas HttpClient
builder.Services.AddScoped<RateConfigService>();

var app = builder.Build();

// Configurer le pipeline de requêtes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Utiliser HSTS (HTTP Strict Transport Security) pour les scénarios de production.
    app.UseHsts();
}

app.UseHttpsRedirection(); // Rediriger les requêtes HTTP vers HTTPS.
app.UseStaticFiles(); // Servir les fichiers statiques (CSS, JS, images, etc.).

app.UseRouting(); // Configurer le middleware de routage.

app.UseAuthorization(); // Activer l'autorisation.

// Configurer les routes par défaut.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Démarrer l'application.

// app.Run();
// using System;
// using System.Collections.Generic;
// using System.Net.Http;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using crmcsharp.Models;

// namespace crmcsharp
// {
//     public class Program
//     {
//         private static readonly HttpClient client = new HttpClient();

//         public static async Task Main(string[] args)
//         {
//             try
//             {
//                 string apiUrl = "http://localhost:8080/api/tickets/all"; // Remplacez par l'URL de votre API
//                 string response = await client.GetStringAsync(apiUrl);

//                 // Désérialisation de la réponse JSON en liste d'objets C#
//                 List<TicketHisto> ticketHistos = JsonConvert.DeserializeObject<List<TicketHisto>>(response);

//                 // Affichage des informations de chaque ticket
//                 foreach (var ticketHisto in ticketHistos)
//                 {
//                     Console.WriteLine($"Ticket ID: {ticketHisto.Id}");
//                     Console.WriteLine($"Subject: {ticketHisto.Subject}");
//                     Console.WriteLine($"Status: {ticketHisto.Status}");
//                     Console.WriteLine($"Priority: {ticketHisto.Priority}");

//                     if (ticketHisto.Manager != null)
//                     {
//                         Console.WriteLine($"Manager: {ticketHisto.Manager.Username}");
//                     }

//                     if (ticketHisto.Customer != null)
//                     {
//                         Console.WriteLine($"Customer: {ticketHisto.Customer.Name}");
//                     }

//                     Console.WriteLine("-----------------------------");
//                 }
//             }
//             catch (HttpRequestException e)
//             {
//                 Console.WriteLine($"Erreur HTTP : {e.Message}");
//             }
//             catch (JsonException e)
//             {
//                 Console.WriteLine($"Erreur de désérialisation JSON : {e.Message}");
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine($"Erreur inattendue : {e.Message}");
//             }
//         }
//     }
// }