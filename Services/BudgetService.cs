using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using crmcsharp.Models.entity;

namespace crmcsharp.Services
{
    public class BudgetService
    {
        private readonly HttpClient _httpClient;

        public BudgetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:8080/api/budget/");
        }

        public async Task<List<Budget>> GetBudgetsBetweenDatesAsync()
        {
            try
            {
                string url = $"condition";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    DateFormatString = "yyyy-MM-dd"
                };

                var result = JsonConvert.DeserializeObject<List<Budget>>(responseContent, settings);
                return result ?? new List<Budget>(); // Correction du warning CS8603
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la récupération des budgets.", ex);
            }
        }
    
    }
}