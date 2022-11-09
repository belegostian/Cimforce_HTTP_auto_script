using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cimforce_HTTP_auto_script
{
    public class Functions
    {
        /*請求機台狀態******************************************************************************************/
        public async Task<Response_StatusInfo> StatusInfo
            (string name, int sysnum, HttpClient client)
        {
            //指令參數，格式必須是Request.cs中有定義的
            var req_si = new Request_General
            {
                Name = name,
                SystemNum = sysnum
            };

            //範型實例化，依請求內容修改<Request_, Response_>
            Connect<Request_General, Response_StatusInfo> status_info =
                new Connect<Request_General, Response_StatusInfo>();

            //執行連線Task，依請求內容輸入(指令參數, URI路徑, Client)；返回JSON格式數據
            return await status_info.ConnectTask(req_si, "statusInfo", client);
        }

        /*下載CNC加工檔 (限本地記憶體)**************************************************************************/
        public async Task<Response_General> DownloadLocalNCFile
            (string name, int sysnum, string local_path, string remote_path, string nc_name, HttpClient client)
        {
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
            return await download_nc_file.ConnectTask(req_dlnf, "downloadNCFromMemory1", client);
        }

        /*上傳PC加工檔 (限本地記憶體)***************************************************************************/
        public async Task<Response_General> UploadLocalNCFile
            (string name, int sysnum, string local_path, string remote_path, string nc_name, HttpClient client)
        {
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
            return await upload_nc_file.ConnectTask(req_ulnf, "uploadNCToMemory1", client);
        }

        /*指定加工主程式****************************************************************************************/
        public async Task<Response_General> SpecifyNCFile
            (string name, int sysnum, string nc_name, HttpClient client)
        {
            var req_snf = new Request_SpecifyNCFile
            {
                Name = name,
                SystemNum = sysnum,
                NCName = nc_name
            };
            Connect<Request_SpecifyNCFile, Response_General> specify_nc_file =
                new Connect<Request_SpecifyNCFile, Response_General>();
            return await specify_nc_file.ConnectTask(req_snf, "setMainNC", client);
        }

        /*開始加工**********************************************************************************************/
        public async Task<Response_General> CycleStart
            (string name, int sysnum, string nc_name, string remote_path, HttpClient client)
        {
            var req_cs = new Request_CycleStart
            {
                Name = name,
                SystemNum = sysnum,
                NCName = nc_name,
                RemotePath = remote_path
            };
            Connect<Request_CycleStart, Response_General> cycle_start =
                new Connect<Request_CycleStart, Response_General>();
            return await cycle_start.ConnectTask(req_cs, "start", client);
        }

        /*讀取刀具捕正******************************************************************************************/
        public async Task<Response_ReadToolOofset> ReadToolOffset
            (string name, int sysnum, int start_num, int end_num, HttpClient client)
        {
            var req_rto = new Request_ReadToolOofset
            {
                Name = name,
                SystemNum = sysnum,
                StartNo = start_num,
                EndNo = end_num
            };
            Connect<Request_ReadToolOofset, Response_ReadToolOofset> read_tool_offset =
                new Connect<Request_ReadToolOofset, Response_ReadToolOofset>();
            return await read_tool_offset.ConnectTask(req_rto, "getScopeToolOffsetInfo", client);
        }

        /*寫入刀具捕正******************************************************************************************/
        public async Task<Response_General> WriteToolOofset
            (string name, int sysnum, int start_num, int end_num, List<Tool> offset_list, HttpClient client)
        {
            var req_wto = new Request_WriteToolOofset
            {
                Name = name,
                SystemNum = sysnum,
                StartNo = start_num,
                EndNo = end_num,
                toolTitle = { "LENGTH GEOM", "LENGTH WEAR", "RADIUS GEOM", "RADIUS WEAR" },  //依控制器版本而定，暫時寫死
                tool = offset_list
            };
            Connect<Request_WriteToolOofset, Response_General> write_tool_offset =
                new Connect<Request_WriteToolOofset, Response_General>();
            return await write_tool_offset.ConnectTask(req_wto, "setScopeToolOffset", client);
        }

        /*讀取PMC-Multi Addr***********************************************************************************/
        public async Task<Response_ReadMultiPMC> ReadMultiPMC
            (string name, int sysnum, rPMC pmc, HttpClient client)
        {
            var req_rmp = new Request_ReadMultiPMC
            {
                Name = name,
                SystemNum = sysnum,
                pmc = pmc
            };
            Connect<Request_ReadMultiPMC, Response_ReadMultiPMC> read_multi_pmc =
                new Connect<Request_ReadMultiPMC, Response_ReadMultiPMC>();
            return await read_multi_pmc.ConnectTask(req_rmp, "fanuc/readPMCInfo", client);
        }

        /*寫入PMC-Single Addr**********************************************************************************/
        public async Task<Response_General> WriteSinglePMC
            (string name, int sysnum, sPMC pmc, HttpClient client)
        {
            var req_wsp = new Request_WriteSinglePMC
            {
                Name = name,
                SystemNum = sysnum,
                PMC = pmc
            };
            Connect<Request_WriteSinglePMC, Response_General> write_single_pmc =
                new Connect<Request_WriteSinglePMC, Response_General>();
            return await write_single_pmc.ConnectTask(req_wsp, "fanuc/setPMCInfo2", client);
        }

        /*寫入PMC-Multi Addr***********************************************************************************/
        public async Task<Response_General> WriteMultiPMC
            (string name, int sysnum, PmcInfo pmc_Info_sample, HttpClient client)
        {
            var req_wmp = new Request_WriteMultiPMC
            {
                Name = name,
                SystemNum = sysnum,
                pmcInfo = pmc_Info_sample
            };
            Connect<Request_WriteMultiPMC, Response_General> write_multi_pmc =
                new Connect<Request_WriteMultiPMC, Response_General>();
            return await write_multi_pmc.ConnectTask(req_wmp, "fanuc/setPMCInfo", client);
        }

        /*讀取MACRO********************************************************************************************/
        public async Task<Response_ReadMacro> ReadMacro(string name, int sysnum, int start_id, int end_id, HttpClient client)
        {
            var req_rm = new Request_ReadMacro
            {
                Name = name,
                SystemNum = sysnum,
                StartId = start_id,
                EndInd = end_id
            };
            Connect<Request_ReadMacro, Response_ReadMacro> read_macro =
                new Connect<Request_ReadMacro, Response_ReadMacro>();
            return await read_macro.ConnectTask(req_rm, "fanuc/readMacro", client);
        }

        /*寫入MACRO********************************************************************************************/
        public async Task<Response_General> WriteMacro(string name, int sysnum, List<FanucMacro> macro_list_sample, HttpClient client)
        {
            var req_wm = new Request_WriteMacro
            {
                Name = name,
                SystemNum = sysnum,
                Macros = macro_list_sample
            };
            Connect<Request_WriteMacro, Response_General> write_macro =
                new Connect<Request_WriteMacro, Response_General>();
            return await write_macro.ConnectTask(req_wm, "fanuc/writeMacro", client);
        }
    }
}
