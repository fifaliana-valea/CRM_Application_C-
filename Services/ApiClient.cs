// using System;
// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Newtonsoft.Json;

// public class ApiClient
// {
//     private readonly HttpClient _httpClient;
//     private readonly string _baseUrl;

//     // Constructeur : initialise le client HTTP avec l'URL de base de l'API
//     public ApiClient(string baseUrl)
//     {
//         _httpClient = new HttpClient();
//         _baseUrl = baseUrl;
//     }

//     // Méthode générique pour effectuer une requête GET
//     public async Task<T> (string endpoint)
//     {
//         var response = await _httpClient.GetAsync($"{_baseUrl}{endpoint}");
//         response.EnsureSuccessStatusCode(); // Vérifie que la requête a réussi
//         var content = await response.Content.ReadAsStringAsync();
//         return JsonConvert.DeserializeObject<T>(content); // Convertit la réponse JSON en objet C#
//     }

//     // Méthode générique pour effectuer une requête POST
//     public async Task<T> PostAsync<T>(string endpoint, object data)
//     {
//         var json = JsonConvert.SerializeObject(data); // Convertit l'objet C# en JSON
//         var content = new StringContent(json, Encoding.UTF8, "application/json");
//         var response = await _httpClient.PostAsync($"{_baseUrl}{endpoint}", content);
//         response.EnsureSuccessStatusCode();
//         var responseContent = await response.Content.ReadAsStringAsync();
//         return JsonConvert.DeserializeObject<T>(responseContent);
//     }

//     // Méthode pour effectuer une requête DELETE
//     public async Task DeleteAsync(string endpoint)
//     {
//         var response = await _httpClient.DeleteAsync($"{_baseUrl}{endpoint}");
//         response.EnsureSuccessStatusCode();
//     }
// }

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    // Constructeur : initialise le client HTTP avec l'URL de base de l'API
    public ApiClient(string baseUrl)
    {
        _httpClient = new HttpClient();
        _baseUrl = baseUrl;
    }

    // Méthode générique pour effectuer une requête GET
    public async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}{endpoint}");
        response.EnsureSuccessStatusCode(); // Vérifie que la requête a réussi
        var content = await response.Content.ReadAsStringAsync();

        // Désérialiser la réponse JSON en objet C#
        var result = JsonConvert.DeserializeObject<T>(content);

        // Retourner null si la désérialisation échoue
        return result;
    }

    // Méthode générique pour effectuer une requête POST
    public async Task<T?> PostAsync<T>(string endpoint, object data)
    {
        var json = JsonConvert.SerializeObject(data); // Convertit l'objet C# en JSON
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}{endpoint}", content);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();

        // Désérialiser la réponse JSON en objet C#
        var result = JsonConvert.DeserializeObject<T>(responseContent);

        // Retourner null si la désérialisation échoue
        return result;
    }

    // Méthode pour effectuer une requête DELETE
    public async Task DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}{endpoint}");
        response.EnsureSuccessStatusCode();
    }
}

