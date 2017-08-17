﻿using flightR.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace flightR.Provider
{
    public class ServiceManager
    {
        private string Url = "http://flightrapi.azurewebsites.net/api/";
        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        // kayıt işlemleri
        private async Task<MobileResult> Process(Record model, string procsType)
        {
            HttpClient client = await GetClient();
            var response = await client.PostAsync(Url + procsType, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            var mobileResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MobileResult>(mobileResult);
            return result;
        }

        // point işlemleri
        private async Task<MobileResult> Process(Point model, string procsType)
        {
            HttpClient client = await GetClient();
            var url = Url + procsType;
            var serialized = JsonConvert.SerializeObject(model);
            var response = client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")).Result;
            var mobileResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MobileResult>(mobileResult);
            return result;
        }

        // point'leri getir
        public async Task<IEnumerable<Point>> GetPoints()
        {
            HttpClient client = await GetClient();
            var result = await client.GetStringAsync(Url + "getall");
            var mobileResult = JsonConvert.DeserializeObject<MobileResult>(result);
            return JsonConvert.DeserializeObject<IEnumerable<Point>>
                (mobileResult.Data.ToString());
        }

        // kayıtları getir
        public async Task<IEnumerable<Record>> GetRecords()
        {
            HttpClient client = await GetClient();
            var result = await client.GetStringAsync(Url + "record/getall/1");
            var mobileResult = JsonConvert.DeserializeObject<MobileResult>(result);
            return JsonConvert.DeserializeObject<IEnumerable<Record>>
                (mobileResult.Data.ToString());
        }

        // son kaydı getir
        public async Task<Record> GetLastRecord(int id)
        {
            HttpClient client = await GetClient();
            var result = await client.GetStringAsync(Url + "record/getlast/"+id);
            var record = JsonConvert.DeserializeObject<Record>(result);
            return record;
        }

        // yeni kayıt olustur
        public async Task<MobileResult> NewRecord(Record list)
        {
            return await Insert(list);
        }

        // kayıt olustur
        public async Task<MobileResult> Insert(Record model)
        {
            return await Process(model, "record/insert");
        }

        // point olustur
        public async Task<MobileResult> Insert(Point model)
        {
            return await Process(model, "point/insert");
        }

        public async Task<MobileResult> Delete(Point model)
        {
            return await Process(model, "delete");
        }

        public async Task<MobileResult> Update(Point model)
        {
            return await Process(model, "update");
        }
    }
}
