using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cimforce_HTTP_auto_script
{
    //CNC 機台狀態
    internal class Request_StatusInfo
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }
    }

    //CNC 下載內存加工檔
    internal class Request_DownloadLocalNCFile
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("LocalPath")]
        public string LocalPath { get; set; }

        [JsonPropertyName("RemotePath")]
        public string RemotePath { get; set; }

        [JsonPropertyName("NCName")]
        public string NCName { get; set; }
    }

    //CNC 上傳本地端加工檔
    internal class Request_UploadLocalNCFile
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("LocalPath")]
        public string LocalPath { get; set; }

        [JsonPropertyName("RemotePath")]
        public string RemotePath { get; set; }

        [JsonPropertyName("NCName")]
        public string NCName { get; set; }
    }

    //CNC 指定加工檔
    internal class Request_SpecifyNCFile
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("NCName")]
        public string NCName { get; set; }
    }
}
