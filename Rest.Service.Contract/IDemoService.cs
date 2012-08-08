using System.ServiceModel;
using System.ServiceModel.Web;
using Rest.Services.CommonTypes.Response;

namespace Rest.Service.Contract
{
    [ServiceContract]
    [ServiceKnownType(typeof (Message))]
    [ServiceKnownType(typeof (PrimitiveResponse))]
    public interface IDemoService
    {
        [OperationContract]
        [WebInvoke(
            UriTemplate = "GetMessagebyId?Id={Id}&key={key}",
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json)]
        EntityResponse<Message> GetMessagebyId(int id, string key);

        [OperationContract]
        [WebInvoke(
            UriTemplate = "PostMessage/{key}",
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PrimitiveResponse PostMessage(Message message, string key);
    }
}