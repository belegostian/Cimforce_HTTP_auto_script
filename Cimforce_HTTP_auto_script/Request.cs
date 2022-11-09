using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cimforce_HTTP_auto_script
{
    /*請求機台狀態******************************************************************************************/
    public class Request_General
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }
    }

    /*指定加工主程式****************************************************************************************/
    public class Request_SpecifyNCFile  //與刪除加工程式功能共用
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("RemotePath")]
        public string RemotePath { get; set; }

        [JsonPropertyName("NCName")]
        public string NCName { get; set; }
    }

    /*上傳、下載CNC加工檔 (限本地記憶體)********************************************************************/
    public class Request_loadLocalNCFile
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

    /*開始加工**********************************************************************************************/
    public class Request_CycleStart
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("NCName")]
        public string NCName { get; set; }

        [JsonPropertyName("RemotePath")]
        public string RemotePath { get; set; }
    }

    /*讀取刀具捕正******************************************************************************************/
    public class Request_ReadToolOofset
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("StartNo")]
        public int StartNo { get; set; }

        [JsonPropertyName("EndNo")]
        public int EndNo { get; set; }
    }

    /*寫入刀具捕正******************************************************************************************/
    public class Request_WriteToolOofset
    {
        [JsonPropertyName("toolTitle")]
        public List<string> toolTitle { get; set; }

        [JsonPropertyName("tool")]
        public List<Tool> tool { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("StartNo")]
        public int StartNo { get; set; }

        [JsonPropertyName("EndNo")]
        public int EndNo { get; set; }
    }
    public partial class Tool
    {
        [JsonPropertyName("Value")]
        public List<double> Value { get; set; }

        [JsonPropertyName("No")]
        public int No { get; set; }
    }

    /*讀取PMC-Multi Addr************************************************************************************/
    public class Request_ReadMultiPMC
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("pmc")]  //已確認
        public rPMC pmc { get; set; }
    }
    public partial class rPMC
    {
        [JsonPropertyName("adr_type")]
        public int adr_type { get; set; }

        [JsonPropertyName("data_type")]
        public int data_type { get; set; }

        [JsonPropertyName("startNum")]
        public int startNum { get; set; }

        [JsonPropertyName("endNum")]
        public int endNum { get; set; }
    }

    /*寫入PMC-Single Addr***********************************************************************************/
    public class Request_WriteSinglePMC
    {
        [JsonPropertyName("PMC")]  //已確認
        public sPMC PMC { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }
    }
    public partial class sPMC
    {
        [JsonPropertyName("adr_type")]
        public int adr_type { get; set; }

        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("BitIndex")]
        public int BitIndex { get; set; }

        [JsonPropertyName("BitValue")]
        public int BitValue { get; set; }
    }

    /*寫入PMC-Multi Addr***********************************************************************************/
    public class Request_WriteMultiPMC
    {
        [JsonPropertyName("pmcInfo")]
        public PmcInfo pmcInfo { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }
    }
    public partial class PmcInfo
    {
        [JsonPropertyName("adr_type")]
        public int adr_type { get; set; }

        [JsonPropertyName("data_type")]
        public int data_type { get; set; }

        [JsonPropertyName("startNum")]
        public int startNum { get; set; }

        [JsonPropertyName("endNum")]
        public int endNum { get; set; }

        [JsonPropertyName("pmc")]  //已確認
        public List<wPmc> pmc { get; set; }
    }
    public partial class wPmc // 引入格式pmc = { new Pmc() { id = 9008, value = 1 } }
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("value")]
        public int value { get; set; }
    }

    /*讀取Macro********************************************************************************************/
    public class Request_ReadMacro
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("StartId")]
        public int StartId { get; set; }

        [JsonPropertyName("EndInd")]
        public int EndInd { get; set; }
    }

    /*寫入Macro********************************************************************************************/
    public class Request_WriteMacro
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("Macros")]
        public List<FanucMacro> Macros { get; set; }
    }
}
