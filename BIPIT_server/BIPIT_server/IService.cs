using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace BIPIT_server
{
    [ServiceContractAttribute]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "/books")]
        [return: MessageParameter(Name = "Data")]
        List<List<string>> Books();
        
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/users")]
        [return: MessageParameter(Name = "Data")]
        List<List<string>> Users();

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        UriTemplate = "/all_data")]
        [return: MessageParameter(Name = "Data")]
        List<List<string>> Get_Data();

        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        UriTemplate = "/add")]
        [return: MessageParameter(Name = "Data")]
        bool Add_Rec(int id_b, int id_u, string dateFrom, string dateTo, string dateFact = "");

    }

}
