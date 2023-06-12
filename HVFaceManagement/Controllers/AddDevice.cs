using FaceManagement;
using HVFaceManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace HVFaceManagement.Controllers
{
    public class AddDevice : Controller
    {
        public static DeviceInfo struDeviceInfo;

        public IActionResult logFrom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(List<TblMachineNoHik> Device)
        {
            foreach (var dd in Device)
            {
                struDeviceInfo = new DeviceInfo();
                struDeviceInfo.strUsername = dd.Hikuser;
                struDeviceInfo.strPassword = dd.Hikpassword;
                struDeviceInfo.strDeviceIP = dd.IpAddress;
                struDeviceInfo.strHttpPort = "80";

                if (Security.Login(struDeviceInfo))
                {
                    // user check success
                    struDeviceInfo.bIsLogin = true;
                   
                }
                else
                {
                    return Content(dd.IpAddress+"Login Fail");
                }
            }
            return Ok("Connected");
        }
       
    }
}
