using flightR.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace flightR.Provider
{
    public class ServiceManager
    {
        private string Url = "http://flightrapi.azurewebsites.net/api/record/";
        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        private async Task<MobileResult> Process(Record model, string procsType)
        {
            HttpClient client = await GetClient();
            var response = await client.PostAsync(Url + procsType, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            var mobileResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MobileResult>(mobileResult);
            return result;
        }

        public async Task<IEnumerable<Record>> GetAll()
        {
            HttpClient client = await GetClient();
            var result = await client.GetStringAsync(Url + "getall");
            var mobileResult = JsonConvert.DeserializeObject<MobileResult>(result);
            return JsonConvert.DeserializeObject<IEnumerable<Record>>
                (mobileResult.Data.ToString());
        }

        public async Task<MobileResult> Insert(Record model)
        {
            return await Process(model, "insert");
        }

        public async Task<MobileResult> Delete(Record model)
        {
            return await Process(model, "delete");
        }

        public async Task<MobileResult> Update(Record model)
        {
            return await Process(model, "update");
        }
    }
}
