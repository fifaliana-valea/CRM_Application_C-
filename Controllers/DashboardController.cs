// Controllers/DashboardController.cs
using System.Threading.Tasks;
using crmcsharp.Entities;
using Microsoft.AspNetCore.Mvc;


public class DashboardController : Controller
{
    private readonly ApiClient _apiClient;

    // Constructeur : injecte le client HTTP
    public DashboardController()
    {
        _apiClient = new ApiClient("http://localhost:8080/api");
    }

    // Action pour afficher le dashboard
    public async Task<IActionResult> Index()
    {
        // Récupérer les données de l'API
        var customers = await _apiClient.GetAsync<List<Customer>>("/customers");
        var leads = await _apiClient.GetAsync<List<Lead>>("/leads");
        var tickets = await _apiClient.GetAsync<List<Ticket>>("/tickets");

        // Passer les données à la vue
        ViewBag.Customers = customers;
        ViewBag.Leads = leads;
        ViewBag.Tickets = tickets;

        return View();
    }
}