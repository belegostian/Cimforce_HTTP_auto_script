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


            /*控制指令**********************************************************************************************/
            Functions fcns = new Functions();

            //請求機台狀態
            var repo_si = fcns.StatusInfoAsync(name, sysnum, client);

            //指定加工主程式
            var repo_snf = fcns.SpecifyNCFile(name, sysnum, nc_name, client);


            /*下載CNC加工檔 (限本地記憶體)**************************************************************************/
            var req_dlnf = new Request_loadLocalNCFile
            {
                Name = name,
                SystemNum = sysnum,
                LocalPath = local_path,
                RemotePath = remote_path,
                NCName = nc_name
            };
            Connect<Request_loadLocalNCFile, Response_General> download_nc_file =
                new Connect<Request_loadLocalNCFile, Response_General>();
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
            Connect<Request_loadLocalNCFile, Response_General> upload_nc_file =
                new Connect<Request_loadLocalNCFile, Response_General>();
            var repo_ulnf = await upload_nc_file.ConnectTask(req_ulnf, "uploadNCToMemory1", client);


            /*開始加工**********************************************************************************************/
            var req_cs = new Request_CycleStart
            {
                Name = name,
                SystemNum = sysnum,
                NCName = nc_name,
                RemotePath = remote_path
            };
            Connect<Request_CycleStart, Response_General> cycle_start =
                new Connect<Request_CycleStart, Response_General>();
            var repo_cs = await cycle_start.ConnectTask(req_cs, "start", client);


            /*寫入PMC-Cycle Start***********************************************************************************/
            var req_wp = new Request_WriteSinglePMC
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
            Connect<Request_WriteSinglePMC, Response_General> write_pmc =
                new Connect<Request_WriteSinglePMC, Response_General>();
            var repo_wp = await write_pmc.ConnectTask(req_wp, "fanuc/setPMCInfo2", client);


            /*讀取刀具捕正******************************************************************************************/
            var req_rto = new Request_General
            {
                Name = name,
                SystemNum = sysnum
            };
            Connect<Request_General, Response_ReadToolOofset> read_tool_offset =
                new Connect<Request_General, Response_ReadToolOofset>();
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
            Connect<Request_WriteToolOofset, Response_General> write_tool_offset =
                new Connect<Request_WriteToolOofset, Response_General>();
            var repo_wto = await write_tool_offset.ConnectTask(req_wto, "setScopeToolOffset", client);


            /*讀取PMC-Multi Addr***********************************************************************************/
            var req_rmp = new Request_ReadMultiPMC
            {
                Name = name,
                SystemNum = sysnum,
                pmc =
                {
                    // E type
                    adr_type = 12,

                    data_type = 0,
                    startNum = 9008,
                    endNum = 9008
                },
            };
            Connect<Request_ReadMultiPMC, Response_ReadMultiPMC> read_multi_pmc =
                new Connect<Request_ReadMultiPMC, Response_ReadMultiPMC>();
            var repo_rmp = await read_multi_pmc.ConnectTask(req_rmp, "fanuc/setPMCInfo2", client);


            /*寫入PMC-Single Addr***********************************************************************************/
            var req_wsp = new Request_WriteSinglePMC
            {
                Name = name,
                SystemNum = sysnum,
                PMC =
                {
                    adr_type = 12,

                    //Cycle Start
                    Id = 9008,

                    BitIndex = 1,
                    BitValue = 1
                },
            };
            Connect<Request_WriteSinglePMC, Response_General> write_single_pmc =
                new Connect<Request_WriteSinglePMC, Response_General>();
            var repo_wsp = await write_single_pmc.ConnectTask(req_wsp, "fanuc/setPMCInfo2", client);


            /*寫入PMC-Multi Addr***********************************************************************************/
            var req_wmp = new Request_WriteMultiPMC
            {
                Name = name,
                SystemNum = sysnum,
                pmcInfo =
                {
                    adr_type = 12,
                    data_type = 0,
                    startNum = 9008,
                    endNum = 9008,
                    pmc =
                    {
                        new Pmc_req() { id = 9008, value = 1 }
                    }
                },
            };
            Connect<Request_WriteMultiPMC, Response_General> write_multi_pmc =
                new Connect<Request_WriteMultiPMC, Response_General>();
            var repo_wmp = await write_multi_pmc.ConnectTask(req_wmp, "fanuc/setPMCInfo2", client);


            /*讀取MACRO********************************************************************************************/
            var req_rm = new Request_ReadMacro
            {
                Name = name,
                SystemNum = sysnum,
                StartId = 3000,
                EndInd = 3000
            };
            Connect<Request_ReadMacro, Response_ReadMacro> read_macro =
                new Connect<Request_ReadMacro, Response_ReadMacro>();
            var repo_rm = await read_macro.ConnectTask(req_rm, "fanuc/readMacro", client);


            /*寫入MACRO********************************************************************************************/
            var req_wm = new Request_WriteMacro
            {
                Name = name,
                SystemNum = sysnum,
                Macros =
                { 
                    new FanucMacro() {Id = 3000, Value = 1, Empty = false}    
                },
            };
            Connect<Request_WriteMacro, Response_General> write_macro =
                new Connect<Request_WriteMacro, Response_General>();
            var repo_wm = await write_macro.ConnectTask(req_wm, "fanuc/writeMacro", client);
        }
    }
}