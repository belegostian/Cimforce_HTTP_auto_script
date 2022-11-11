using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;



namespace Cimforce_HTTP_auto_script
{
    class Program
    {
        //Socket 初始化
        private static readonly String _socket_server_ip = "127.0.0.1";
        private static readonly int _socket_server_port = 50000;

        //CNC Client 初始化
        private static readonly HttpClient _cnc_client = new HttpClient();
        private static readonly String _cnc_client_uri = "http://localhost:9500/cimforce/service/";
        

        public static async Task Main(string[] args)
        {
            TcpListener tcp_listener = null;

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
                            new Tool() { Value = new List<double> { length_geom_offset_1, 0, 0, 0 }, No = 1 },
                            new Tool() { Value = new List<double> { length_geom_offset_2, 0, 0, 0 }, No = 2 }
                        };

            //PMC參數
            sPMC cycle_start = new sPMC { adr_type = 12, Id = 9008, BitIndex = 1, BitValue = 1 };
            rPMC pmc_read_sample = new rPMC { adr_type = 3, data_type = 0, startNum = 300, endNum = 303 };
            PmcInfo pmc_Info_sample = new PmcInfo
            {
                adr_type = 12,
                data_type = 0,
                startNum = 9008,
                endNum = 9009,
                //pmc = wPmcs
                pmc = new List<wPmc>() 
                { 
                    new wPmc() { id = 9009, value = 0 }, 
                    new wPmc() { id = 9009, value = 0 } 
                }
            };

            //MACRO參數
            int start_id = 1;
            int end_id = 2;
            List<FanucMacro> macro_list_sample = new List<FanucMacro>
                        {
                            new FanucMacro() {Id = 3000, Value = 1, Empty = false}
                        };


            //TCP connect
            try
            {
                // Set the Tcp listener
                tcp_listener = new TcpListener(System.Net.IPAddress.Parse(_socket_server_ip), _socket_server_port);
                tcp_listener.Start();
                Console.WriteLine("Server is running on: " + _socket_server_ip + ": " + _socket_server_port + "\n");

                // Set the HTTP client
                _cnc_client.DefaultRequestHeaders.Accept.Clear();
                _cnc_client.BaseAddress = new Uri(_cnc_client_uri);

                // Buffer for reading data
                Byte[] bytes = new Byte[1024];
                String socket_req = null;
                String socket_repo = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Client connect
                    TcpClient client = tcp_listener.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    NetworkStream stream = client.GetStream();

                    // Loop to receive all the data sent by the client.
                    int str_len;
                    while ((str_len = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        socket_req = Encoding.ASCII.GetString(bytes, 0, str_len);
                        Console.WriteLine("Received: {0}", socket_req);

                        // Excute functions by receive data
                        if (socket_req == "disconnect")
                        {
                            break;
                        }

                        Functions fcns = new Functions();
                        switch (socket_req)
                        {
                            case "status info":
                                try
                                {
                                    var repo_si = fcns.StatusInfo(name, sysnum, _cnc_client);
                                    socket_repo = "execute function: status info\n" + "result code: " + repo_si.Result.ResultCode;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("SocketException: {0}", e);
                                }
                                break;

                            case "download nc file":
                                try
                                {
                                    stream.Write(Encoding.ASCII.GetBytes("enter nc file name\n"), 0, Encoding.ASCII.GetBytes("enter nc file name\n").Length);
                                    str_len = stream.Read(bytes, 0, bytes.Length);
                                    socket_req = Encoding.ASCII.GetString(bytes, 0, str_len);

                                    var repo_dlnf = fcns.DownloadLocalNCFile(name, sysnum, local_path, remote_path, socket_req, _cnc_client);
                                    socket_repo = "execute function: download nc file\n" + "result code: " + repo_dlnf.Result.ResultCode;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("SocketException: {0}", e);
                                }
                                break;

                            case "delete nc file":
                                try
                                {
                                    stream.Write(Encoding.ASCII.GetBytes("enter nc file name\n"), 0, Encoding.ASCII.GetBytes("enter nc file name\n").Length);
                                    str_len = stream.Read(bytes, 0, bytes.Length);
                                    socket_req = Encoding.ASCII.GetString(bytes, 0, str_len);

                                    var repo_dnf = fcns.DeleteLocalNCFile(name, sysnum, remote_path, nc_name, _cnc_client);
                                    socket_repo = "execute function: delete nc file\n" + "result code: " + repo_dnf.Result.ResultCode;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("SocketException: {0}", e);
                                }
                                break;

                            case "upload nc file":
                                try
                                {
                                    stream.Write(Encoding.ASCII.GetBytes("enter nc file name\n"), 0, Encoding.ASCII.GetBytes("enter nc file name\n").Length);
                                    str_len = stream.Read(bytes, 0, bytes.Length);
                                    socket_req = Encoding.ASCII.GetString(bytes, 0, str_len);

                                    var repo_ulnf = fcns.UploadLocalNCFile(name, sysnum, local_path, remote_path, nc_name, _cnc_client);
                                    socket_repo = "execute function: upload nc file\n" + "result code: " + repo_ulnf.Result.ResultCode;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("SocketException: {0}", e);
                                }
                                break;

                            case "specify nc file":
                                try
                                {
                                    stream.Write(Encoding.ASCII.GetBytes("enter nc file name\n"), 0, Encoding.ASCII.GetBytes("enter nc file name\n").Length);
                                    str_len = stream.Read(bytes, 0, bytes.Length);
                                    socket_req = Encoding.ASCII.GetString(bytes, 0, str_len);

                                    var repo_snf = fcns.SpecifyNCFile(name, sysnum, remote_path, nc_name, _cnc_client);
                                    socket_repo = "execute function: specify nc file\n" + "result code: " + repo_snf.Result.ResultCode;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("SocketException: {0}", e);
                                }
                                break;

                            case "cycle start":
                                try
                                {
                                    var repo_cs = fcns.CycleStart(name, sysnum, nc_name, remote_path, _cnc_client);
                                    socket_repo = "execute function: cycle start\n" + "result code: " + repo_cs.Result.ResultCode;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("SocketException: {0}", e);
                                }
                                break;

                            case "enforce cycle start":
                                try
                                {
                                    var repo_wsp = fcns.WriteSinglePMC(name, sysnum, cycle_start, _cnc_client);
                                    socket_repo = "execute function: enforce cycle start\n" + "result code: " + repo_wsp.Result.ResultCode;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("SocketException: {0}", e);
                                }
                                break;

                            default:
                                socket_repo = "undefined function\n";
                                break;
                        }
                        byte[] msg = Encoding.ASCII.GetBytes(socket_repo);
                        stream.Write(msg, 0, msg.Length);
                    }
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                tcp_listener.Stop();
            }
        }
    }
}






///*基本控制指令******************************************************************************************/
//Functions fcns = new Functions();

////請求機台狀態
//var repo_si = fcns.StatusInfo(name, sysnum, _cnc_client);

////下載CNC加工檔 (限本地記憶體)
//var repo_dlnf = fcns.DownloadLocalNCFile(name, sysnum, local_path, remote_path, nc_name, _cnc_client);

////刪除CNC加工檔 (限本地記憶體)
//var repo_dnf = fcns.DeleteLocalNCFile(name, sysnum, remote_path, nc_name, _cnc_client);

////上傳PC加工檔 (限本地記憶體)
//var repo_ulnf = fcns.UploadLocalNCFile(name, sysnum, local_path, remote_path, nc_name, _cnc_client);

////指定加工主程式
//var repo_snf = fcns.SpecifyNCFile(name, sysnum, remote_path, nc_name, _cnc_client);

////開始加工
//var repo_cs = fcns.CycleStart(name, sysnum, nc_name, remote_path, _cnc_client);

////讀取刀具捕正
//var repo_rto = fcns.ReadToolOffset(name, sysnum, start_num, end_num, _cnc_client);

////寫入刀具捕正
//var repo_wto = fcns.WriteToolOofset(name, sysnum, start_num, end_num, offset_list_sample, _cnc_client);


///*進階控制指令******************************************************************************************/

////讀取PMC-Multi Addr
//var repo_rmp = fcns.ReadMultiPMC(name, sysnum, pmc_read_sample, _cnc_client);

////寫入PMC-Single Addr (Cycle Start)
//var repo_wsp = fcns.WriteSinglePMC(name, sysnum, cycle_start, _cnc_client);

////寫入PMC-Multi Addr
//var repo_wmp = fcns.WriteMultiPMC(name, sysnum, pmc_Info_sample, _cnc_client);

////讀取MACRO
//var repo_rm = fcns.ReadMacro(name, sysnum, start_id, end_id, _cnc_client);

////寫入MACRO
//var repo_wm = fcns.WriteMacro(name, sysnum, macro_list_sample, _cnc_client);