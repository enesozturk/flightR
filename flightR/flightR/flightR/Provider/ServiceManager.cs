using flightR.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace flightR.Provider
{
    public class ServiceManager
    {
        private string Url = "http://localhost/fligtRWebAPI/api/";
        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Record>> GetAll()
        {
            HttpClient client = await GetClient();

        }
    }
}
