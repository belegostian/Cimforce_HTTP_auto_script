﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cimforce_HTTP_auto_script
{
    public class Generic<T, U>
    {
        public async Task<U> ConnectTask(T req, string cmd_dir, HttpClient client)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync(cmd_dir, jsonContent);

            var Jstring = await response.Content.ReadAsStringAsync();
            var repo = await response.Content.ReadFromJsonAsync<U>();

            JObject parsed = JObject.Parse(Jstring);
            foreach (var item in parsed)
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
            return repo;
        }
    }
}
