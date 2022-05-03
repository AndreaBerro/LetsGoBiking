using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Routing
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRoutingService" in both code and config file together.
    [ServiceContract]
    public interface IRoutingService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetStations")]
        string GetStations();


        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetPath?location1={location1}&location2={location2}")]
        Task<string> GetPathAsync(string location1, string location2);

    }
}
