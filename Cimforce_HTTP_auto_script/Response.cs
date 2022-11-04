using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cimforce_HTTP_auto_script
{
    //CNC 取得機台狀態
    internal class Response_StatusInfo
    {
        [JsonPropertyName("errorInfo")]
        public string ErrorInfo { get; set; }

        [JsonPropertyName("machStatus")]
        public MachStatus MachStatus { get; set; }

        [JsonPropertyName("resultCode")]
        public long ResultCode { get; set; }
    }
    internal partial class MachStatus
    {
        [JsonPropertyName("alarm")]
        public bool Alarm { get; set; }

        [JsonPropertyName("curProg")]
        public string CurProg { get; set; }

        [JsonPropertyName("curSequence")]
        public long CurSequence { get; set; }

        [JsonPropertyName("mainProg")]
        public string MainProg { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("run")]
        public long Run { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    //CNC 下載加工檔、上傳本地端加工檔、指定加工檔
    internal partial class Response_General
    {
        [JsonPropertyName("errorInfo")]
        public string ErrorInfo { get; set; }

        [JsonPropertyName("resultCode")]
        public long ResultCode { get; set; }
    }

}

