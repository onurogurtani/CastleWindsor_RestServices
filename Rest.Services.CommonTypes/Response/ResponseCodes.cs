using System.Runtime.Serialization;

namespace Rest.Services.CommonTypes.Response
{
    [DataContract]
    public enum ResponseCodes
    {
        [EnumMember] Success = 0,
        [EnumMember] SystemError = 1,
        [EnumMember] DbError = 2,
        [EnumMember] NoPermissionFound = 3,
        [EnumMember] NoRecordFound = 4,
        [EnumMember] WorkflowError = 5,
        [EnumMember] ServiceError = 6,
        [EnumMember] IncorrectLoginCredentials = 8,
        [EnumMember] EmailAddressDefinedAlready = 9,
        [EnumMember] Authorized = 10,
        [EnumMember] NotAuthorized = 11,
        [EnumMember] AlreadyDefined = 12,
        [EnumMember] MissingData = 13,
        [EnumMember] ProvisionAlreadyTaken = 14,
        [EnumMember] TicketAlreadyTaken = 15,
        [EnumMember] ProvisionNotFound = 16,
        [EnumMember] SomeDataNotUpdated = 17,
        [EnumMember] ProvisionExpired = 18,
        [EnumMember] TransactionAlreadyCancelled = 19,
        [EnumMember] InquiryError = 20,
        [EnumMember] PaymentError = 21,
        [EnumMember] RefundError = 22,
        [EnumMember] VoidError = 23,
        [EnumMember] NotImplemented = 24,
        [EnumMember] AgentHasToManyMail = 25
    }
}