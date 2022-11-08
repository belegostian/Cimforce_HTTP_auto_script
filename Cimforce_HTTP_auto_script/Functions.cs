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
        public async Task<Response_StatusInfo> StatusInfoAsync(string name, int sysnum, HttpClient client)
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

        /*指定加工主程式****************************************************************************************/
        public async Task<Response_General> SpecifyNCFile(string name, int sysnum, string nc_name, HttpClient client) 
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


        public async Task<>(, HttpClient client){}



    public async Task<>(, HttpClient client){}




}
}
