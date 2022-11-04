using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

//using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace API_Test
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            var req_status_info = new Request_StatusInfo
            {
                Name = "MV66A",
                SystemNum = 1
            };
            var repo_status_info = await StatusInfo(req_status_info);


            var req_download_local_nc_file = new Request_DownloadLocalNCFile
            {
                Name = "MV66A",
                SystemNum = 1,
                LocalPath = "C:\\Users\\b0630\\Desktop\\MV66A_nc_file",
                RemotePath = "",
                NCName = "O7101"
            };
            var repo_download_local_nc_file = await DownloadLocalNCFile(req_download_local_nc_file);


            var req_upload_local_nc_file = new Request_UploadLocalNCFile
            {
                Name = "MV66A",
                SystemNum = 1,
                LocalPath = "C:\\Users\\b0630\\Desktop\\MV66A_nc_file",
                RemotePath = "",
                NCName = "O1412"
            };
            var repo_upload_local_nc_file = await UploadLocalNCFile(req_upload_local_nc_file);


            var req_specify_nc_file = new Request_SpecifyNCFile
            {
                Name = "MV66A",
                SystemNum = 1,
                NCName = "O1412"
            };
            var repo_specify_nc_file = await SpecifyNCFile(req_specify_nc_file);


        }

        // 獲取CNC機台狀態
        private static async Task<Response_statusInfo> StatusInfo(Request_StatusInfo req)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri("http://localhost:9500/cimforce/service/");

            using StringContent jsonContent = new(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("statusInfo", jsonContent);

            var Jstring = await response.Content.ReadAsStringAsync();
            var repo = await response.Content.ReadFromJsonAsync<Response_statusInfo>();

            JObject parsed = JObject.Parse(Jstring);
            foreach (var item in parsed)
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
            return repo;
        }

        // 下載CNC內存加工檔
        private static async Task<Response_General> DownloadLocalNCFile(Request_DownloadLocalNCFile req)
        {
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.BaseAddress = new Uri("http://localhost:9500/cimforce/service/");

            using StringContent jsonContent = new(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("downloadNCFromMemory1", jsonContent);

            var Jstring = await response.Content.ReadAsStringAsync();
            var repo = await response.Content.ReadFromJsonAsync<Response_General>();

            JObject parsed = JObject.Parse(Jstring);
            foreach (var item in parsed)
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
            return repo;
        }

        // 上傳本地端加工檔
        private static async Task<Response_General> UploadLocalNCFile(Request_UploadLocalNCFile req)
        {
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.BaseAddress = new Uri("http://localhost:9500/cimforce/service/");

            using StringContent jsonContent = new(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("uploadNCToMemory1", jsonContent);

            var Jstring = await response.Content.ReadAsStringAsync();
            var repo = await response.Content.ReadFromJsonAsync<Response_General>();

            JObject parsed = JObject.Parse(Jstring);
            foreach (var item in parsed)
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
            return repo;
        }

        // 指定加工檔
        private static async Task<Response_General> SpecifyNCFile(Request_SpecifyNCFile req)
        {
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.BaseAddress = new Uri("http://localhost:9500/cimforce/service/");

            using StringContent jsonContent = new(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("setMainNC", jsonContent);

            var Jstring = await response.Content.ReadAsStringAsync();
            var repo = await response.Content.ReadFromJsonAsync<Response_General>();



            JObject parsed = JObject.Parse(Jstring);
            foreach (var item in parsed)
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
            return repo;
        }

    }
}