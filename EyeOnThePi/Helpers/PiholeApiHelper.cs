using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EyeOnThePi.Models;

namespace EyeOnThePi
{
    class PiholeApiHelper
    {
        // make this adjustable later
        public static string piholeAddress = "192.168.254.101";

        public static async Task<PiholeStatsJsonModel> GetStatsAsync()
        {
            string piholeJsonResponse = await GetPiholeJsonAsync();

            PiholeStatsJsonModel piholeStats = JsonSerializer.Deserialize<PiholeStatsJsonModel>(piholeJsonResponse);

            return piholeStats;
        }

        private static async Task<string> GetPiholeJsonAsync()
        {
            string apiEndpoint = $"http://{piholeAddress}/admin/api.php";

            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(apiEndpoint))
            using (var content = response.Content)
            {
                var result = await content.ReadAsStringAsync();

                return result;
            }
        }
    }
}
