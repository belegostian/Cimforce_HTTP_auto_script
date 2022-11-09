using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using System.Collections.Generic;

//using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Cimforce_HTTP_auto_script
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            //Client 初始化
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri("http://localhost:9500/cimforce/service/");

            //CNC機台參數
            string name = "MV66A";
            int sysnum = 1;

            //加工檔參數 (remote_path 只對海德漢機種生效)
            string nc_name = "O6666";
            string local_path = "C:\\Users\\b0630\\Desktop\\MV66A_nc_file";
            string remote_path = "";

            //刀具補正參數
            int start_num = 1;
            int end_num = 2;
            double length_geom_offset_1 = -100.0;
            double length_geom_offset_2 = -110.0;
            List<Tool> offset_list_sample = new List<Tool>
            {
                new Tool() { Value = { length_geom_offset_1, 0, 0, 0}, No = 1 },
                new Tool() { Value = { length_geom_offset_2, 0, 0, 0}, No = 1 }
            };

            //PMC參數
            sPMC cycle_start = new sPMC{adr_type = 12, Id = 9008, BitIndex = 1, BitValue = 1};
            rPMC pmc_read_sample = new rPMC{adr_type = 3, data_type = 0, startNum = 300, endNum = 303};
            PmcInfo pmc_Info_sample = new PmcInfo { adr_type = 12, data_type = 0, startNum = 9008, endNum = 9009,
                pmc =
                {
                    new wPmc { id = 9008, value = 1 },
                    new wPmc { id = 9009, value = 0 }
                }
            };

            //MACRO參數
            int start_id = 1;
            int end_id = 2;
            List<FanucMacro> macro_list_sample = new List<FanucMacro>
            {
                new FanucMacro() {Id = 3000, Value = 1, Empty = false}
            };


            /*基本控制指令******************************************************************************************/
            Functions fcns = new Functions();

            //請求機台狀態
            var repo_si = fcns.StatusInfo(name, sysnum, client);

            //下載CNC加工檔 (限本地記憶體)
            var repo_dlnf = fcns.DownloadLocalNCFile(name, sysnum, local_path, remote_path, nc_name, client);

            //刪除CNC加工檔 (限本地記憶體)
            var repo_dnf = fcns.DeleteLocalNCFile(name, sysnum, remote_path, nc_name, client);

            //上傳PC加工檔 (限本地記憶體)
            var repo_ulnf = fcns.UploadLocalNCFile(name, sysnum, local_path, remote_path, nc_name, client);

            //指定加工主程式
            var repo_snf = fcns.SpecifyNCFile(name, sysnum, remote_path, nc_name, client);

            //開始加工
            var repo_cs = fcns.CycleStart(name, sysnum, nc_name, remote_path, client);

            //讀取刀具捕正
            var repo_rto = fcns.ReadToolOffset(name, sysnum, start_num, end_num, client);

            //寫入刀具捕正
            var repo_wto = fcns.WriteToolOofset(name, sysnum, start_num, end_num, offset_list_sample, client);


            /*進階控制指令******************************************************************************************/

            //讀取PMC-Multi Addr
            var repo_rmp = fcns.ReadMultiPMC(name, sysnum, pmc_read_sample, client);

            //寫入PMC-Single Addr (Cycle Start)
            var repo_wsp = fcns.WriteSinglePMC(name, sysnum, cycle_start, client);

            //寫入PMC-Multi Addr
            var repo_wmp = fcns.WriteMultiPMC(name, sysnum, pmc_Info_sample, client);

            //讀取MACRO
            var repo_rm = fcns.ReadMacro(name, sysnum, start_id, end_id, client);

            //寫入MACRO
            var repo_wm = fcns.WriteMacro(name, sysnum, macro_list_sample, client);
        }
    }
}