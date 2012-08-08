using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Rest.Service.Contract;
using Rest.Services.CommonTypes.Response;

namespace Rest.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DemoService : IDemoService
    {
        private static readonly List<Message> messages = new List<Message>();

        #region IDemoService Members

        public EntityResponse<Message> GetMessagebyId(int id, string key)
        {
            IEnumerable<Message> response = messages.Where(a => a.Id == id);
            return new EntityResponse<Message>
                       {
                           EntityData = response.FirstOrDefault(),
                           ResponseCode = response.Count() > 0
                                              ? ResponseCodes.Success
                                              : ResponseCodes.NoRecordFound
                       };
        }

        public PrimitiveResponse PostMessage(Message message, string key)
        {
            messages.Add(message);
            return new PrimitiveResponse
                       {EntityPrimaryKey = message.Id.ToString(), ResponseCode = ResponseCodes.Success};
        }

        #endregion
    }
}