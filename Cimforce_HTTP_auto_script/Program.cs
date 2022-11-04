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

            //CNC參數
            string name = "MV66A";
            int sysnum = 1;

            //加工檔參數
            string nc_name = "O6666";
            string local_path = "C:\\Users\\b0630\\Desktop\\MV66A_nc_file";
            string remote_path = "";

            //刀具補正參數
            int tool_num = 1;
            double tool_geom_offset = 200.0;


            /*請求機台狀態******************************************************************************************/

            //指令參數，格式必須是Request.cs中有定義的
            var req_si = new Request_General
            {
                Name = name,
                SystemNum = sysnum
            };

            //範型實例化，依請求內容修改<Request_, Response_>
            Generic<Request_General, Response_StatusInfo> status_info = 
                new Generic<Request_General, Response_StatusInfo>();

            //執行連線Task，依請求內容輸入(指令參數, URI路徑, Client)；返回JSON格式數據
            var repo_si = await status_info.ConnectTask(req_si, "statusInfo", client);


            /*指定加工主程式****************************************************************************************/
            var req_snf = new Request_SpecifyNCFile
            {
                Name = name,
                SystemNum = sysnum,
                NCName = nc_name
            };
            Generic<Request_SpecifyNCFile, Response_General> specify_nc_file =
                new Generic<Request_SpecifyNCFile, Response_General>();
            var repo_snf = await specify_nc_file.ConnectTask(req_snf, "setMainNC", client);


            /*下載CNC加工檔 (限本地記憶體)**************************************************************************/
            var req_dlnf = new Request_loadLocalNCFile
            {
                Name = name,
                SystemNum = sysnum,
                LocalPath = local_path,
                RemotePath = remote_path,
                NCName = nc_name
            };
            Generic<Request_loadLocalNCFile, Response_General> download_nc_file =
                new Generic<Request_loadLocalNCFile, Response_General>();
            var repo_dlnf = await download_nc_file.ConnectTask(req_dlnf, "downloadNCFromMemory1", client);


            /*上傳PC加工檔 (限本地記憶體)***************************************************************************/
            var req_ulnf = new Request_loadLocalNCFile
            {
                Name = name,
                SystemNum = sysnum,
                LocalPath = local_path,
                RemotePath = remote_path,
                NCName = nc_name
            };
            Generic<Request_loadLocalNCFile, Response_General> upload_nc_file =
                new Generic<Request_loadLocalNCFile, Response_General>();
            var repo_ulnf = await upload_nc_file.ConnectTask(req_ulnf, "uploadNCToMemory1", client);


            /*開始加工**********************************************************************************************/
            var req_cs = new Request_CycleStart
            {
                Name = name,
                SystemNum = sysnum,
                NCName = nc_name,
                RemotePath = remote_path
            };
            Generic<Request_CycleStart, Response_General> cycle_start =
                new Generic<Request_CycleStart, Response_General>();
            var repo_cs = await cycle_start.ConnectTask(req_cs, "start", client);


            /*寫入PMC-Cycle Start***********************************************************************************/
            var req_wp = new Request_WritePMC1
            {
                Name = name,
                SystemNum = sysnum,
                PMC =
                {
                    adr_type = 12,
                    Id = 9008,
                    BitIndex = 1,
                    BitValue = 1,
                },
            };
            Generic<Request_WritePMC1, Response_General> write_pmc =
                new Generic<Request_WritePMC1, Response_General>();
            var repo_wp = await write_pmc.ConnectTask(req_wp, "fanuc/setPMCInfo2", client);


            /*讀取刀具捕正******************************************************************************************/
            var req_rto = new Request_General
            {
                Name = name,
                SystemNum = sysnum
            };
            Generic<Request_General, Response_ReadToolOofset> read_tool_offset =
                new Generic<Request_General, Response_ReadToolOofset>();
            var repo_rto = await read_tool_offset.ConnectTask(req_rto, "setScopeToolOffset", client);


            /*寫入刀具捕正******************************************************************************************/
            var req_wto = new Request_WriteToolOofset
            {
                Name = name,
                SystemNum = sysnum,
                StartNo = 1,
                EndNo = 1,
                toolTitle = { "LENGTH GEOM", "LENGTH WEAR", "RADIUS GEOM", "RADIUS WEAR" },
                tool =
                {
                    new Tool() { Value = { tool_geom_offset, 0, 0, 0}, No = tool_num }
                }
            };
            Generic<Request_WriteToolOofset, Response_General> write_tool_offset =
                new Generic<Request_WriteToolOofset, Response_General>();
            var repo_wto = await write_tool_offset.ConnectTask(req_wto, "setScopeToolOffset", client);
        }
    }
}