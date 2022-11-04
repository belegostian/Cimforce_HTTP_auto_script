using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cimforce_HTTP_auto_script
{
    /*請求機台狀態******************************************************************************************/
    internal class Request_StatusInfo
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

    /*下載CNC加工檔 (限本地記憶體)**************************************************************************/
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

    /*上傳PC加工檔 (限本地記憶體)***************************************************************************/
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

    /*寫入PMC1**********************************************************************************************/
    internal class Request_WritePMC1
    {
        public PMC PMC { get; set; }
        public string Name { get; set; }
        public int SystemNum { get; set; }
    }
    public class PMC
    {
        public int adr_type { get; set; }
        public int Id { get; set; }
        public int BitIndex { get; set; }
        public int BitValue { get; set; }
    }

    /*寫入PMC2**********************************************************************************************/
    internal class Request_WritePMC2
    {
        public PmcInfo pmcInfo { get; set; }
        public string Name { get; set; }
        public int SystemNum { get; set; }
    }
    public class PmcInfo
    {
        public int adr_type { get; set; }
        public int data_type { get; set; }
        public int startNum { get; set; }
        public int endNum { get; set; }
        public List<Pmc> pmc { get; set; }
    }
    public class Pmc
    {
        public int id { get; set; }
        public int value { get; set; }
    } // 引入格式pmc = { new Pmc() { id = 9008, value = 1 } }
}
