using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace BIPIT_server
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServiceHost hostWeb = new WebServiceHost(typeof(BIPIT_server.Service));
            //ServiceEndpoint ep = hostWeb.AddServiceEndpoint(typeof(BIPIT_server.IService), new WebHttpBinding(), "");
            ServiceDebugBehavior stp = hostWeb.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            hostWeb.Open();
            
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}-{DateTime.Now.ToShortDateString()}] Service Host started ");
            //{
            //    // Configure a binding with TCP port sharing enabled  
            //    NetTcpBinding binding = new NetTcpBinding();
            //    binding.PortSharingEnabled = true;

            //    // Start a service on a fixed TCP port  
            //    ServiceHost host = new ServiceHost(typeof(Service));
            //    ushort salt = (ushort)new Random().Next();
            //    string address = $"net.tcp://localhost:3081/service/{salt}";
            //    //var sae = new Service();
            //    host.AddServiceEndpoint(typeof(IService), binding, address);
            //    host.Open();
            //}
            Console.Read();
            
        }
        public static void PrintMessage(string text,string port)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}-{DateTime.Now.ToShortDateString()}] {port}: {text}");
        }
    }
}
