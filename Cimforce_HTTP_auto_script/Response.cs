using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cimforce_HTTP_auto_script
{
    /*請求機台狀態******************************************************************************************/
    public class Response_StatusInfo
    {
        [JsonPropertyName("errorInfo")]
        public string ErrorInfo { get; set; }

        [JsonPropertyName("machStatus")]
        public MachStatus MachStatus { get; set; }

        [JsonPropertyName("resultCode")]
        public long ResultCode { get; set; }
    }
    public class MachStatus
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

    /*讀取刀具補正******************************************************************************************/
    public class Response_ReadToolOofset
    {
        [JsonPropertyName("errorInfo")]
        public string errorInfo { get; set; }

        [JsonPropertyName("resultCode")]
        public int resultCode { get; set; }

        [JsonPropertyName("toolInfo")]
        public ToolInfo toolInfo { get; set; }
    }

    public partial class ToolInfo
    {
        [JsonPropertyName("toolOffsets")]
        public List<ToolOffset> toolOffsets { get; set; }

        [JsonPropertyName("toolTitle")]
        public List<string> toolTitle { get; set; }
    }

    public partial class ToolOffset
    {
        [JsonPropertyName("No")]
        public int No { get; set; }

        [JsonPropertyName("Value")]
        public List<double> Value { get; set; }
    }

    /*讀取PMC-Multi Addr***********************************************************************************/
    public class Response_ReadMultiPMC
    {
        [JsonPropertyName("errorInfo")]
        public string ErrorInfo { get; set; }

        [JsonPropertyName("pmc")]
        public List<Pmc_rep> pmc { get; set; }

        [JsonPropertyName("resultCode")]
        public long ResultCode { get; set; }
    }
    public partial class Pmc_rep
    {
        public int id { get; set; }
        public int value { get; set; }
    }

    /*讀取Macro********************************************************************************************/
    public class Response_ReadMacro
    {
        [JsonPropertyName("errorInfo")]
        public string ErrorInfo { get; set; }

        [JsonPropertyName("Macros")]
        public List<FanucMacro> Macros { get; set; }

        [JsonPropertyName("resultCode")]
        public long ResultCode { get; set; }
    }
    public partial class FanucMacro
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Value")]
        public double Value { get; set; }

        [JsonPropertyName("Empty")]
        public bool Empty { get; set; }
    }

    /*******************************************************************************************************/
    public partial class Response_General
    {
        [JsonPropertyName("errorInfo")]
        public string ErrorInfo { get; set; }

        [JsonPropertyName("resultCode")]
        public long ResultCode { get; set; }
    }
}