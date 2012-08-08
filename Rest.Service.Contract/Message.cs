using System.Runtime.Serialization;

namespace Rest.Service.Contract
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}