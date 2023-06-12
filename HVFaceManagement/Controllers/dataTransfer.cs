using FaceManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using HVFaceManagement.Models;
using Microsoft.AspNetCore.Http;
using System.Data.OleDb;
using ClosedXML.Excel;

namespace HVFaceManagement.Controllers
{
    public class dataTransferController : Controller
    {
        private readonly IConfiguration config;
        private readonly GTRDBContext db;
        private IWebHostEnvironment HostEnvironment;
        private string ComId;
        public dataTransferController(GTRDBContext _db, IWebHostEnvironment hostEnvironment, IConfiguration config)
        {
            db = _db;
            HostEnvironment = hostEnvironment;
            this.config = config;
            ComId = config.GetValue<string>("ComId");

        }

        string pic;
        byte[] image;
        byte[] byFingerData;
        int iRet;

        public static DeviceInfo struDeviceInfo;
        public List<DeviceInfo> deviceInfos = new List<DeviceInfo>();

        List<empData> empData = new List<empData>();

        public IActionResult Index()
        {
            return View();
        }

        public string ActionISAPI(string szUrl, string szRequest, string szMethod, DeviceInfo struDeviceInfo)
        {

            string szResponse = string.Empty;
            if (struDeviceInfo == null)
            {

                return szResponse;
            }
            if (!szUrl.Substring(0, 4).Equals("http"))
            {
                szUrl = "http://" + struDeviceInfo.strDeviceIP + ":" + struDeviceInfo.strHttpPort + szUrl;
            }
            HttpClient clHttpClient = new HttpClient();
            byte[] byResponse = { 0 };
            iRet = 0;
            string szContentType = string.Empty;

            switch (szMethod)
            {
                case "GET":
                    iRet = clHttpClient.HttpRequest(struDeviceInfo.strUsername, struDeviceInfo.strPassword, szUrl, szMethod, ref byResponse, ref szContentType);
                    break;
                case "PUT":
                    iRet = clHttpClient.HttpPut(struDeviceInfo.strUsername, struDeviceInfo.strPassword, szUrl, szMethod, szRequest, ref szResponse);
                    break;
                case "POST":
                    iRet = clHttpClient.HttpPut(struDeviceInfo.strUsername, struDeviceInfo.strPassword, szUrl, szMethod, szRequest, ref szResponse);
                    break;
                default:
                    break;
            }

            if (iRet == (int)HttpClient.HttpStatus.Http200)
            {
                if ((!szMethod.Equals("GET")) || (szContentType.IndexOf("application/xml") != -1))
                {
                    if (szResponse != string.Empty)
                    {
                        return szResponse;
                    }

                    if (szMethod.Equals("GET"))
                    {
                        szResponse = Encoding.Default.GetString(byResponse);
                        return szResponse;
                    }
                }
                else
                {
                    if (byResponse.Length != 0)
                    {
                        szResponse = Encoding.Default.GetString(byResponse);
                        return szResponse;
                    }
                }
            }
            else if (iRet == (int)HttpClient.HttpStatus.HttpOther)
            {
                string szCode = string.Empty;
                string szError = string.Empty;
                clHttpClient.ParserResponseStatus(szResponse, ref szCode, ref szError);
                return ("Request failed! Error code:" + szCode + " Describe:" + szError + "\r\n");
            }
            else if (iRet == (int)HttpClient.HttpStatus.HttpTimeOut)
            {
                return (szMethod + " " + szUrl + "error!Time out");
            }

            return (szResponse);
        }
        private string ActionISAPI2(string szUrl, string szRequest, string szMethod, DeviceInfo struDeviceInfo)
        {
            string szResponse = string.Empty;
            if (struDeviceInfo == null)
            {
                return szResponse;
            }
            if (!struDeviceInfo.bIsLogin)
            {
                return szResponse;
            }
            szUrl = szUrl.Substring(szUrl.IndexOf("/LOCALS"));
            if (!szUrl.Substring(0, 4).Equals("http"))
            {
                szUrl = "http://" + struDeviceInfo.strDeviceIP + ":" + struDeviceInfo.strHttpPort + szUrl;
            }
            HttpClient clHttpClient = new HttpClient();
            byte[] byResponse = { 0 };
            int iRet = 0;
            string szContentType = string.Empty;

            iRet = clHttpClient.HttpRequest2(struDeviceInfo.strUsername, struDeviceInfo.strPassword, szUrl, szMethod, ref byResponse, ref szContentType);

            if (iRet == (int)HttpClient.HttpStatus.Http200)
            {
                if ((!szMethod.Equals("GET")) || (szContentType.IndexOf("application/xml") != -1))
                {
                    if (szResponse != string.Empty)
                    {
                        return szResponse;
                    }

                    if (szMethod.Equals("GET"))
                    {
                        szResponse = Encoding.Default.GetString(byResponse);
                        return szResponse;
                    }
                }
                else
                {
                    if (byResponse.Length != 0)
                    {
                        image = byResponse;
                        szResponse = Encoding.UTF8.GetString(byResponse);

                        //string szPath = string.Format("{0}\\employee.jpg", Environment.CurrentDirectory);
                        pic = szResponse;
                        //try
                        //{
                        //    using (FileStream fs = new FileStream(szPath, FileMode.OpenOrCreate))
                        //    {
                        //        BinaryWriter objBinaryWrite = new BinaryWriter(fs);
                        //        fs.Write(byResponse, 0, byResponse.Length);
                        //        fs.Close();
                        //    }

                        //}
                        //catch (Exception ex)
                        //{

                        //}
                        return szResponse;
                    }

                }
            }
            else if (iRet == (int)HttpClient.HttpStatus.HttpOther)
            {
                string szCode = string.Empty;
                string szError = string.Empty;
                clHttpClient.ParserResponseStatus(szResponse, ref szCode, ref szError);
            }
            else if (iRet == (int)HttpClient.HttpStatus.HttpTimeOut)
            {
                //MessageBox.Show(szMethod + " " + szUrl + "error!Time out");
            }
            return szResponse;
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public IActionResult SetFingerPrint(List<empData> SelectedData, List<HR_MachineNoHIK> Device)
        {
            List<DeviceInfo> deviceInfos = new List<DeviceInfo>();
            List<viewModel> data = new List<viewModel>();

            // old code. 

            foreach (var a in SelectedData)
            {
                viewModel vm = new viewModel();
                vm.CardNo = a.cardNo;
                vm.empName = a.empName;

                var dbdata = (from d in db.HR_Emp_DeviceInfoHIK where d.cardNo == a.cardNo && d.comId == ComId select d).FirstOrDefault();


                vm.EmpImage = dbdata.empImage;
                vm.FingerData = dbdata.fingerData;

                data.Add(vm);
            }

            string szUrl;
            string szResponse = string.Empty;
            string szRequest;
            string szMethod;

            foreach (var dd in Device)
            {
                struDeviceInfo = new DeviceInfo();
                struDeviceInfo.strUsername = dd.Hikuser;
                struDeviceInfo.strPassword = dd.Hikpassword;
                struDeviceInfo.strDeviceIP = dd.IpAddress;
                struDeviceInfo.strHttpPort = dd.PortNo;

                if (Security.Login(struDeviceInfo))
                {
                    // user check success
                    struDeviceInfo.bIsLogin = true;
                    deviceInfos.Add(struDeviceInfo);

                }
                else
                {
                    ViewBag.msg = "" + struDeviceInfo.strDeviceIP + " Couldn't connect";
                    continue;

                }
            }
            if (deviceInfos.Count != 0)
            {
                foreach (var struDeviceInfo in deviceInfos)
                {
                    foreach (viewModel a in data)
                    {
                        szUrl = "/ISAPI/AccessControl/UserInfo/Search?format=json";
                        szResponse = string.Empty;
                        szRequest = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + a.CardNo + "\"}]}}";
                        szMethod = "POST";


                        //查询是否存在工号
                        szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);


                        var condition = !string.IsNullOrEmpty(szResponse);

                        if (condition)
                        {
                            UserInfoSearchRoot us = new UserInfoSearchRoot();
                            us = JsonConvert.DeserializeObject<UserInfoSearchRoot>(szResponse);

                            if (1 == us.UserInfoSearch.numOfMatches)
                            {
                                continue;
                            }
                        }
                        szUrl = "/ISAPI/AccessControl/UserInfo/SetUp?format=json";
                        szRequest = "{\"UserInfo\":{\"employeeNo\":\"" + a.CardNo +
                           "\",\"name\":\"" + a.empName +
                           "\",\"userType\":\"normal\",\"Valid\":{\"enable\":true,\"beginTime\":\"2019-08-01T17:30:08\",\"endTime\":\"2035-08-01T17:30:08\"},\"doorRight\": \"1\",\"RightPlan\":[{\"doorNo\":1,\"planTemplateNo\":\"" + "1" + "\"}]}}";
                        szMethod = "PUT";

                        //下发人员信息,若人员存在则修改人员信息 ------//Send the personnel information, if the personnel exists, modify the personnel information

                        szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                        if (szResponse != string.Empty && iRet == 0)
                        {
                            ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                            if ("1" == rs.statusCode)
                            {
                                ViewBag.setinfo = ("Set UserInfo Succ!");
                            }
                            else
                            {
                                ViewBag.setinfo2 = ("Set UserInfo fail!");
                            }
                        }
                        else
                        {
                            return Json("Setting UserInfo failed!");
                        };

                    }
                    /// delete pic if exist
                    /// 
                    foreach (viewModel a in data)
                    {

                        if (a.EmpImage != null)
                        {
                            szUrl = "/ISAPI/Intelligent/FDLib/FDSearch?format=json";

                            szResponse = string.Empty;
                            szRequest = "{\"searchResultPosition\":0,\"maxResults\":30,\"faceLibType\":\"blackFD\",\"FDID\":\"1\",\"FPID\":\"" + a.CardNo + "\"}";

                            szMethod = "POST";

                            szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                            if (szResponse != string.Empty && iRet == 0)
                            {
                                Root rt = JsonConvert.DeserializeObject<Root>(szResponse);
                                if (rt.statusCode == 1)
                                {
                                    if (rt.totalMatches != 0)
                                    {
                                        szUrl = "/ISAPI/Intelligent/FDLib/FDSearch/Delete?format=json&FDID=1&faceLibType=blackFD";
                                        szResponse = string.Empty;
                                        szRequest = "{\"FPID\":[{\"value\":\"" + a.CardNo + "\"}]}";
                                        szMethod = "PUT";

                                        szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                                        if (szResponse != string.Empty)
                                        {
                                            ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                                            if (!rs.statusCode.Equals("1"))
                                            {
                                                return Ok();
                                            }
                                        }
                                    }
                                }
                            }


                            /// set pic
                            szUrl = "/ISAPI/Intelligent/FDLib/FaceDataRecord?format=json";

                            if (!szUrl.Substring(0, 4).Equals("http"))
                            {
                                szUrl = "http://" + struDeviceInfo.strDeviceIP + ":" + struDeviceInfo.strHttpPort + szUrl;
                            }
                            HttpClient clHttpClient = new HttpClient();
                            szResponse = string.Empty;
                            szRequest = "{\"faceLibType\":\"blackFD\",\"FDID\":\"1\",\"FPID\":\"" + a.CardNo + "\"}";
                            //string filePath = string.Format(@".\Image\{0}.jpg", a.CardNo);
                            byte[] imgfile = a.EmpImage;
                            if (imgfile != null)
                            {

                                szResponse = clHttpClient.HttpPostData(struDeviceInfo.strUsername, struDeviceInfo.strPassword, szUrl, imgfile, szRequest);
                                ResponseStatus res = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                                if (res != null && res.statusCode.Equals("1"))
                                {

                                    ViewBag.successmsg = "pic has been set";
                                }

                            }
                        }


                    }
                    foreach (viewModel a in data)
                    {
                        szUrl = "/ISAPI/AccessControl/FingerPrint/SetUp?format=json";
                        szResponse = string.Empty;
                        string fingerData;

                        if (a.FingerData != null)
                        {
                            try
                            {
                                fingerData = Convert.ToBase64String(a.FingerData);
                            }
                            catch
                            {
                                fingerData = null;
                            }

                            szRequest = "{\"FingerPrintCfg\":{\"employeeNo\":\"" + a.CardNo +
                                "\",\"enableCardReader\":[1],\"fingerPrintID\":1,\"fingerType\":\"normalFP\",\"fingerData\":\"" + fingerData + "\"}}";
                            //System.IO.File.WriteAllText(@"C:\Users\chendaliang\Desktop\WriteText.txt", szRequest);
                            szMethod = "POST";

                            szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);

                            if (szResponse != string.Empty && iRet == 0)
                            {
                                FPRoot fr = JsonConvert.DeserializeObject<FPRoot>(szResponse);
                                foreach (StatusListItem item in fr.FingerPrintStatus.StatusList)
                                {
                                    if (item.id.ToString() == "1")
                                    {
                                        if (item.cardReaderRecvStatus != 1)
                                        {
                                            ViewBag.error = ("Set fingerData failed! errorMsg:" + item.errorMsg);
                                        }
                                        else
                                        {
                                            ViewBag.success = ("Set fingerData succeed!");
                                        }
                                        break;
                                    }
                                }
                            }

                        }
                    }
                }
                return Json("Data Transfered Successfully!");
            }
            else { return Content("Log in unsuccessfull"); }



        }


        [HttpPost]
        public IActionResult GetFingerFace(List<HR_MachineNoHIK> Device)
        {
            int CountData = 0;
            List<empData> edlist = new List<empData>();
            List<HR_Emp_DeviceInfoHIK> deviceData = new List<HR_Emp_DeviceInfoHIK>();
            List<DeviceInfo> deviceInfos = new List<DeviceInfo>();
            UserInfoSearchRoot us = new UserInfoSearchRoot();

            foreach (var dd in Device)
            {
                struDeviceInfo = new DeviceInfo();
                struDeviceInfo.strUsername = dd.Hikuser;
                struDeviceInfo.strPassword = dd.Hikpassword;
                struDeviceInfo.strDeviceIP = dd.IpAddress;
                struDeviceInfo.strHttpPort = dd.PortNo;

                if (Security.Login(struDeviceInfo))
                {
                    // user check success
                    struDeviceInfo.bIsLogin = true;
                    deviceInfos.Add(struDeviceInfo);

                }
                else
                {
                    ViewBag.msg = ("" + struDeviceInfo.strDeviceIP + "_Couldn't connect . Please check Device Connection!");
                    continue;
                }
            }

            foreach (var struDeviceInfo in deviceInfos)
            {
                string szUrl = "/ISAPI/AccessControl/UserInfo/Search?format=json";
                string szResponse = string.Empty;
                string szRequest = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":" + 0 + ",\"maxResults\":30}}";
                //string szRequest = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + i + "\"}]}}";
                string szMethod = "POST";
                szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                var condition = !string.IsNullOrEmpty(szResponse);
                if (condition)
                {
                    us = JsonConvert.DeserializeObject<UserInfoSearchRoot>(szResponse);
                    var user = us.UserInfoSearch.UserInfo;
                    CountData = us.UserInfoSearch.totalMatches;
                }
                //查询是否存在工号
                for (int i = 0; i <= CountData; i += 30)
                {
                    szUrl = "/ISAPI/AccessControl/UserInfo/Search?format=json";
                    szResponse = string.Empty;
                    szRequest = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":" + i + ",\"maxResults\":30}}";
                    //string szRequest = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + i + "\"}]}}";
                    szMethod = "POST";
                    szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                    condition = !string.IsNullOrEmpty(szResponse);
                    if (condition)
                    {
                        us = JsonConvert.DeserializeObject<UserInfoSearchRoot>(szResponse);
                        var user = us.UserInfoSearch.UserInfo;
                        CountData = us.UserInfoSearch.totalMatches;
                        if (0 == us.UserInfoSearch.numOfMatches)
                        {
                            continue;
                        }
                        foreach (var uu in user)
                        {
                            empData ui = new empData();
                            if (uu == null) { continue; }
                            ui.cardNo = uu.employeeNo;
                            ui.empName = uu.name;

                            var dbCompare = (from dc in db.HR_Emp_DeviceInfoHIK where dc.cardNo == uu.employeeNo && dc.comId == ComId select dc).FirstOrDefault();
                            if (dbCompare == null)
                            {
                                edlist.Add(ui);
                            }
                        }

                        //查询人脸库是否存在
                        //Query whether the face database exists
                    }
                    else
                    {
                        return Content("Please Connect Device First!");
                    };
                    edlist.Count();
                }

                foreach (var dd in edlist)
                {
                    string fingerData;
                    string fingerPath = "";
                    string picPath = "";
                    
                    szUrl = "/ISAPI/AccessControl/FingerPrintUpload?format=json";
                    szResponse = string.Empty;
                    int searchID = 1;
                    szRequest = "{\"FingerPrintCond\":{\"searchID\":\"" + searchID + "\",\"employeeNo\":\"" + dd.cardNo +
                           "\",\"cardReaderNo\":1,\"fingerPrintID\":1}}";
                    szMethod = "POST";
                    szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                    if (szResponse != string.Empty && iRet == 0)
                    {
                        FPInfo fi = JsonConvert.DeserializeObject<FPInfo>(szResponse);
                        if (szResponse != string.Empty)
                        {
                            if (fi.FingerPrintInfo.status.Equals("OK"))
                            {
                                foreach (FingerPrintListItem item in fi.FingerPrintInfo.FingerPrintList)
                                {
                                    fingerData = item.fingerData;

                                    byFingerData = Convert.FromBase64String(fingerData);
                                    //fingerPath = string.Format(@".\finger\{0}.dat", dd.empNo);
                                }
                            }
                            else if (fi.FingerPrintInfo.status.Equals("NoFP"))
                            {
                                byFingerData = null;
                            }
                        }
                    }
                    szUrl = "/ISAPI/Intelligent/FDLib/FDSearch?format=json";
                    szResponse = string.Empty;
                    szRequest = "{\"searchResultPosition\":0,\"maxResults\":5,\"faceLibType\":\"blackFD\",\"FDID\":\" 1\",\"FPID\":\"" + dd.cardNo + "\"}";
                    szMethod = "POST";

                    szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                    if (szResponse != string.Empty && iRet == 0)
                    {
                        Root rt = JsonConvert.DeserializeObject<Root>(szResponse);
                        if (rt.statusCode == 1)
                        {

                            if (rt.totalMatches == 1)
                            {
                                string picData = string.Empty;
                                foreach (MatchListItem item in rt.MatchList)
                                {
                                    picData = item.modelData;
                                    string url = item.faceURL;
                                    string data = ActionISAPI2(url, szRequest, "GET", struDeviceInfo);
                                }
                            }
                            else { image = null; }
                        }
                    }
                    HR_Emp_DeviceInfoHIK ff = new HR_Emp_DeviceInfoHIK();
                    ff.cardNo = dd.cardNo;
                    ff.comId = ComId;
                    ff.empName = dd.empName;
                    ff.fingerData = byFingerData;
                    ff.empImage = image;
                    ff.IpAddress = struDeviceInfo.strDeviceIP;
                    var dbdata = (from d in db.HR_Emp_DeviceInfoHIK where d.cardNo == dd.cardNo && d.comId == ComId select d).FirstOrDefault();

                    if (dbdata == null)
                    {
                        db.HR_Emp_DeviceInfoHIK.Add(ff);
                        db.SaveChanges();
                    }
                    deviceData.Add(ff);
                }
            }
            return Json("Data Saved Successfully!");
        }


        [HttpGet]
        public IActionResult Empinfo()
        {
            List<viewModel> viewModels = new List<viewModel>();
            var dbconfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
            try
            {
                var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
                string sql = $"exec prcGetDevicesDataTranHIK'{ComId}'";
                using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandTimeout = 0;
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            viewModel vm = new viewModel();
                            vm.EmpCode = dataReader["EmpCode"].ToString();
                            vm.CardNo = dataReader["CardNo"].ToString();
                            vm.empName = dataReader["EmpName"].ToString();
                            vm.DeptName = dataReader["DeptName"].ToString();
                            vm.SectName = dataReader["SectName"].ToString();
                            vm.desigName = dataReader["DesigName"].ToString();
                            vm.floor = dataReader["Floor"].ToString();
                            vm.line = dataReader["Line"].ToString();
                            vm.IpAddress = dataReader["IPAddress"].ToString();
                            vm.emp_image = dataReader["empImage"].ToString();
                            vm.finger_Data = dataReader["fingerData"].ToString();
                            viewModels.Add(vm);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(viewModels);
        }
        [HttpGet]
        public IActionResult deviceInfo()
        {

            var a = (from b in db.HR_MachineNoHIK where b.ComId == ComId select b).ToList();
            return Json(a);
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public IActionResult btnDel(List<empData> data, List<HR_MachineNoHIK> Device)
        {
            List<DeviceInfo> deviceInfos = new List<DeviceInfo>();
            foreach (var dd in Device)
            {
                struDeviceInfo = new DeviceInfo();
                struDeviceInfo.strUsername = dd.Hikuser;
                struDeviceInfo.strPassword = dd.Hikpassword;
                struDeviceInfo.strDeviceIP = dd.IpAddress;
                struDeviceInfo.strHttpPort = dd.PortNo;

                if (Security.Login(struDeviceInfo))
                {
                    // user check success
                    struDeviceInfo.bIsLogin = true;
                    deviceInfos.Add(struDeviceInfo);
                }
                else
                {
                    return Content("Couldn't connect device, Please check your Network!");
                }
            }
            foreach (var i in data)
            {
                UserInfoSearchRoot us = new UserInfoSearchRoot();

                string szUrl;/* = "/ISAPI/AccessControl/UserInfo/Search?format=json";*/
                string szResponse = string.Empty;
                string szRequest;
                //= "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + i + "\"}]}}";
                string szMethod;/*= "POST"*/

                foreach (var struDeviceInfo in deviceInfos)
                {
                    ////查询是否存在工号
                    //szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                    //var condition = !string.IsNullOrEmpty(szResponse);
                    //if (condition)
                    //{
                    //    us = JsonConvert.DeserializeObject<UserInfoSearchRoot>(szResponse);
                    //    if (0 == us.UserInfoSearch.totalMatches)
                    //    {
                    //        continue;
                    //    }
                    //}

                    szUrl = "/ISAPI/AccessControl/UserInfo/Delete?format=json";
                    szResponse = string.Empty;
                    szRequest = "{\"UserInfoDelCond\":{\"EmployeeNoList\":[{\"employeeNo\":\"" + i.cardNo + "\"}]}}";
                    szMethod = "PUT";

                    szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                    if (szResponse != string.Empty && iRet == 0)
                    {
                        ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                        if (rs.statusString.Equals("OK"))
                        {
                            continue;
                        }
                        else
                        {
                            return Content("Data hasn't been deleted");
                        }
                    }
                }
            }

            return Content("Device Data deleted Successfully!");
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public IActionResult btnDelfromDB(List<empData> data)
        {
            foreach (var dd in data)
            {
                var dbconfig = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json").Build();
                try
                {
                    var dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];
                    string sql = $"exec prcDeleteDevicesDataTranHIK '{ComId}','{dd.cardNo}'";
                    using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                    {
                        SqlCommand command = new SqlCommand(sql, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return Json("Data Deleted");
        }
        [HttpPost]
        public IActionResult showImg(string cardNo)
        {
            var dbdata = (from d in db.HR_Emp_DeviceInfoHIK where d.cardNo == cardNo && d.comId == ComId select d).FirstOrDefault();
            if (dbdata != null)
            {
                var img = dbdata.empImage;
                return Json(img);
            }
            //var imgData = Convert.ToBase64String(img, 0, img.Length);
            return null;
          

        }
        [HttpPost]
        public IActionResult downImg(List<empData> data)
        {
            foreach (var cc in data)
            {
                var dbdata = (from d in db.HR_Emp_DeviceInfoHIK where d.cardNo == cc.cardNo && d.comId == ComId select d).FirstOrDefault();
                var img = dbdata.empImage;
                var szPath = string.Format(@".\image\{0}.jpg", cc.cardNo);

                try
                {
                    using (FileStream fs = new FileStream(szPath, FileMode.OpenOrCreate))
                    {

                        BinaryWriter objBinaryWrite = new BinaryWriter(fs);
                        fs.Write(img, 0, img.Length);
                        fs.Close();


                    }
                    //MessageBox.Show("FaceData GET SUCCEED", "SUCCESSFUL", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Json("image saved");

        }
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = Int32.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public IActionResult SetImg(List<IFormFile> img, string DeviceData)
        {
            List<DeviceInfo> deviceInfos = new List<DeviceInfo>();
            List<viewModel> data = new List<viewModel>();
            var Device = JsonConvert.DeserializeObject<List<HR_MachineNoHIK>>(DeviceData);
            // old code. 

            foreach (var a in img)
            {

                if (a.Length > 0)
                {

                    viewModel vm = new viewModel();
                    vm.CardNo = Path.GetFileNameWithoutExtension(a.FileName);
                    using (var ms = new MemoryStream())
                    {
                        a.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        vm.EmpImage = fileBytes;
                    }
                    data.Add(vm);
                }



            }

            string szUrl;
            string szResponse = string.Empty;
            string szRequest;
            string szMethod;

            foreach (var dd in Device)
            {
                struDeviceInfo = new DeviceInfo();
                struDeviceInfo.strUsername = dd.Hikuser;
                struDeviceInfo.strPassword = dd.Hikpassword;
                struDeviceInfo.strDeviceIP = dd.IpAddress;
                struDeviceInfo.strHttpPort = dd.PortNo;

                if (Security.Login(struDeviceInfo))
                {
                    // user check success
                    struDeviceInfo.bIsLogin = true;
                    deviceInfos.Add(struDeviceInfo);

                }
                else
                {
                    ViewBag.msg = "" + struDeviceInfo.strDeviceIP + " Couldn't connect";
                    continue;

                }
            }
            if (deviceInfos.Count != 0)
            {
                foreach (viewModel a in data)
                {
                    szUrl = "/ISAPI/AccessControl/UserInfo/Search?format=json";
                    szResponse = string.Empty;
                    szRequest = "{\"UserInfoSearchCond\":{\"searchID\":\"1\",\"searchResultPosition\":0,\"maxResults\":30,\"EmployeeNoList\":[{\"employeeNo\":\"" + a.CardNo + "\"}]}}";
                    szMethod = "POST";

                    foreach (var struDeviceInfo in deviceInfos)
                    {
                        //查询是否存在工号
                        szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);


                        var condition = !string.IsNullOrEmpty(szResponse);

                        if (condition)
                        {
                            UserInfoSearchRoot us = new UserInfoSearchRoot();
                            us = JsonConvert.DeserializeObject<UserInfoSearchRoot>(szResponse);

                            if (1 == us.UserInfoSearch.numOfMatches)
                            {
                                continue;
                            }
                        }
                        szUrl = "/ISAPI/AccessControl/UserInfo/SetUp?format=json";
                        szRequest = "{\"UserInfo\":{\"employeeNo\":\"" + a.CardNo +
                           "\",\"name\":\"" + a.empName +
                           "\",\"userType\":\"normal\",\"Valid\":{\"enable\":true,\"beginTime\":\"2019-08-01T17:30:08\",\"endTime\":\"2035-08-01T17:30:08\"},\"doorRight\": \"1\",\"RightPlan\":[{\"doorNo\":1,\"planTemplateNo\":\"" + "1" + "\"}]}}";
                        szMethod = "PUT";

                        //下发人员信息,若人员存在则修改人员信息 ------//Send the personnel information, if the personnel exists, modify the personnel information

                        szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                        if (szResponse != string.Empty && iRet == 0)
                        {
                            ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                            if ("1" == rs.statusCode)
                            {
                                ViewBag.setinfo = ("Set UserInfo Succ!");
                            }
                            else
                            {
                                ViewBag.setinfo2 = ("Set UserInfo fail!");
                            }
                        }
                        else
                        {
                            return Json("Setting UserInfo failed!");
                        };
                    }
                }
                /// delete pic if exist
                /// 
                foreach (viewModel a in data)
                {
                    szUrl = "/ISAPI/Intelligent/FDLib/FDSearch?format=json";

                    szResponse = string.Empty;
                    szRequest = "{\"searchResultPosition\":0,\"maxResults\":30,\"faceLibType\":\"blackFD\",\"FDID\":\"1\",\"FPID\":\"" + a.CardNo + "\"}";

                    szMethod = "POST";
                    foreach (var struDeviceInfo in deviceInfos)
                    {
                        szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                        if (szResponse != string.Empty && iRet == 0)
                        {
                            Root rt = JsonConvert.DeserializeObject<Root>(szResponse);
                            if (rt.statusCode == 1)
                            {
                                if (rt.totalMatches != 0)
                                {
                                    szUrl = "/ISAPI/Intelligent/FDLib/FDSearch/Delete?format=json&FDID=1&faceLibType=blackFD";
                                    szResponse = string.Empty;
                                    szRequest = "{\"FPID\":[{\"value\":\"" + a.CardNo + "\"}]}";
                                    szMethod = "PUT";

                                    szResponse = ActionISAPI(szUrl, szRequest, szMethod, struDeviceInfo);
                                    if (szResponse != string.Empty)
                                    {
                                        ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                                        if (!rs.statusCode.Equals("1"))
                                        {
                                            return Ok();
                                        }
                                    }
                                }
                            }
                        }


                        /// set pic
                        szUrl = "/ISAPI/Intelligent/FDLib/FaceDataRecord?format=json";

                        if (!szUrl.Substring(0, 4).Equals("http"))
                        {
                            szUrl = "http://" + struDeviceInfo.strDeviceIP + ":" + struDeviceInfo.strHttpPort + szUrl;
                        }
                        HttpClient clHttpClient = new HttpClient();
                        szResponse = string.Empty;
                        szRequest = "{\"faceLibType\":\"blackFD\",\"FDID\":\"1\",\"FPID\":\"" + a.CardNo + "\"}";
                        //string filePath = string.Format(@".\Image\{0}.jpg", a.CardNo);
                        byte[] imgfile = a.EmpImage;
                        if (imgfile != null)
                        {

                            szResponse = clHttpClient.HttpPostData(struDeviceInfo.strUsername, struDeviceInfo.strPassword, szUrl, imgfile, szRequest);
                            if (szResponse == "System.Net.WebException: The remote server returned an error: (400) Bad Request") {
                                continue;
                            }
                            ResponseStatus res = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                            if (res != null && res.statusCode.Equals("1"))
                            {

                                ViewBag.successmsg = "pic has been set";
                            }
                            else {
                                continue;
                            }

                        }

                    }
                }

                return Json("Data Transfered Successfully!");
            }
            else { return Content("Log in unsuccessfull"); }



        }
        public IActionResult readExl(List<IFormFile> exl, string DeviceData)
        {
            List<DeviceInfo> deviceInfos = new List<DeviceInfo>();
            List<empData> data = new List<empData>();
            var Device = JsonConvert.DeserializeObject<List<HR_MachineNoHIK>>(DeviceData);
            // old code. 

            foreach (var a in exl)
            {
                var ext = Path.GetExtension(a.FileName);
                if (a.Length > 0 && Path.GetExtension(a.FileName) == ".xlsx")
                {
                    string path = Path.Combine(this.HostEnvironment.WebRootPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //Save the uploaded Excel file.
                    string fileName = Path.GetFileName(a.FileName);
                    string filePath = Path.Combine(path, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        a.CopyTo(stream);
                    }

                    using var wbook = new XLWorkbook(Path.GetFullPath(filePath));

                    var ws1 = wbook.Worksheet(1);
                    var rows = ws1.RangeUsed().RowsUsed().Skip(1);
                    foreach (var row in rows)
                    {
                        empData vm = new empData();
                        var rowNumber = row.RowNumber();
                        // Process the row
                        if (ws1.Cell($"A{rowNumber}").GetValue<string>() != "")
                        {
                            vm.cardNo = ws1.Cell($"A{rowNumber}").GetValue<string>();
                            data.Add(vm);
                        }
                        vm.cardNo = ws1.Cell($"B{rowNumber}").GetValue<string>();
                        data.Add(vm);
                    }
                    btnDel(data, Device);

                   

                }
                
            }return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            GC.Collect();
        }
    }

}

