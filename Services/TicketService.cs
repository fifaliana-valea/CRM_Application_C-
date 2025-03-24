using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using crmcsharp.Models;

namespace crmcsharp.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:8080/api/tickets/");
        }

        public async Task<List<TicketHisto>> GetTicketsAsync()
        {
            try
            { 
                string response = await _httpClient.GetStringAsync("all");
                List<TicketHisto> ticketHistos = JsonConvert.DeserializeObject<List<TicketHisto>>(response);
                return ticketHistos;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs (journalisation, etc.)
                throw new ApplicationException("Erreur lors de la récupération des tickets.", ex);
            }
        }

       public async Task<bool> DeleteTicket(int ticketId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"delete-ticket/{ticketId}");

                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
                else
                {
                    string content = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"Erreur lors de la suppression du ticket. Code: {(int)response.StatusCode} - {response.ReasonPhrase}. Contenu: {content}");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erreur lors de la suppression du ticket.", ex);
            }
        }


        public async Task<TicketHisto> GetTicketByIdAsync(int id)
        {
            try
            {
                string response = await _httpClient.GetStringAsync($"{id}");
                TicketHisto ticket = JsonConvert.DeserializeObject<TicketHisto>(response);
                return ticket;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs (journalisation, etc.)
                throw new ApplicationException("Erreur lors de la récupération des détails du ticket.", ex);
            }
        }
    }

}