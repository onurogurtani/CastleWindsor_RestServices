using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rest.Services.CommonTypes.Response
{
    [DataContract]
    public class EntityResponse<TRequestType> : PrimitiveResponse
    {
        private List<TRequestType> _entityDataList = new List<TRequestType>();

        [DataMember]
        public List<TRequestType> EntityDataList
        {
            get { return _entityDataList; }
            set { _entityDataList = value; }
        }

        [DataMember]
        public TRequestType EntityData { get; set; }
    }
}