using DebtTracker.Models.DTOs;
using DebtTracker.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace DebtTracker.Services
{
    public class FileProcessor : IFileProcessor
    {
        private readonly HttpClient _httpClient;

        public FileProcessor(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Debt.Processor");
        }

        public async Task<Dictionary<string, List<string>>> ProcessDebts(byte[] debtFile)
        {
            var fileContent = new StreamContent(new MemoryStream(debtFile));

            var response = await _httpClient.PostAsync("api/debt", fileContent);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var error = await response.Content.ReadFromJsonAsync<CustomErrorDto>();

                var resp = new Dictionary<string, List<string>>
                {
                    { "Error", new List<string> { error.Description } }
                };

                return resp;
            }

            var responseDto = await response.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();

            return responseDto;
        }

    }
}
