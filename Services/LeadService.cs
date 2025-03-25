using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using crmcsharp.Models.entity;

namespace crmcsharp.Services
{
    public class LeadService
    {
        private readonly HttpClient _httpClient;

        public LeadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:8080/api/leads/");
        }

        public async Task<List<TriggerLeadHisto>> GetLeadsAsync()
        {
            try
            {
                string response = await _httpClient.GetStringAsync("all");

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                };

                return JsonConvert.DeserializeObject<List<TriggerLeadHisto>>(response, settings);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la récupération des leads.", ex);
            }
        }

        public async Task<TriggerLeadHisto> GetLeadByIdAsync(int id)
        {
            try
            {
                string response = await _httpClient.GetStringAsync($"{id}");
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                };

                return JsonConvert.DeserializeObject<TriggerLeadHisto>(response,settings);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la récupération du lead.", ex);
            }
        }

        public async Task<bool> DeleteLead(int leadId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"delete-lead/{leadId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new ApplicationException("Lead non trouvé.");
                }
                else
                {
                    throw new ApplicationException($"Erreur lors de la suppression du lead. Code : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la suppression du lead.", ex);
            }
        }
    }
}
