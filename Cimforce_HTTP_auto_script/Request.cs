using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cimforce_HTTP_auto_script
{
    /*請求機台狀態******************************************************************************************/
    internal class Request_General
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }
    }

    /*指定加工主程式****************************************************************************************/
    internal class Request_SpecifyNCFile
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("NCName")]
        public string NCName { get; set; }
    }

    /*上傳、下載CNC加工檔 (限本地記憶體)********************************************************************/
    internal class Request_loadLocalNCFile
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
    internal class Request_CycleStart
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

    /*讀取PMC-Multi Addr************************************************************************************/
    internal class Request_ReadMultiPMC
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }

        [JsonPropertyName("pmc")]
        public Pmc pmc { get; set; }
    }
    public class Pmc
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
    internal class Request_WriteSinglePMC
    {
        [JsonPropertyName("PMC")]
        public PMC PMC { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }
    }
    internal class PMC
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
    internal class Request_WriteMultiPMC
    {
        [JsonPropertyName("pmcInfo")]
        public PmcInfo pmcInfo { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("SystemNum")]
        public int SystemNum { get; set; }
    }
    internal class PmcInfo
    {
        [JsonPropertyName("adr_type")]
        public int adr_type { get; set; }

        [JsonPropertyName("data_type")]
        public int data_type { get; set; }

        [JsonPropertyName("startNum")]
        public int startNum { get; set; }

        [JsonPropertyName("endNum")]
        public int endNum { get; set; }

        [JsonPropertyName("pmc")]
        public List<Pmc_req> pmc { get; set; }
    }
    internal class Pmc_req // 引入格式pmc = { new Pmc() { id = 9008, value = 1 } }
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("value")]
        public int value { get; set; }
    } 

    /*寫入刀具捕正******************************************************************************************/
    internal class Request_WriteToolOofset
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
    internal class Tool
    {
        [JsonPropertyName("Value")]
        public List<double> Value { get; set; }

        [JsonPropertyName("No")]
        public int No { get; set; }
    }
}
