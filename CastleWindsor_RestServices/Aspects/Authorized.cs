using System;
using System.Configuration;
using System.Reflection;
using Castle.DynamicProxy;
using Rest.Services.CommonTypes.Response;

namespace CastleWindsor_RestServices.Aspects
{
    public class Authorized : IInterceptor
    {
        #region IInterceptor Members
   
        public void Intercept(IInvocation invocation)
        {
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                ParameterInfo a = invocation.Method.GetParameters()[i];
                if (a.Name == "key")
                {
                    if (invocation.Arguments[i].ToString() != ConfigurationManager.AppSettings["SecurityKey"])
                    {
                        if (invocation.Method.ReturnParameter.ParameterType.FullName ==
                            "Rest.Services.CommonTypes.Response.PrimitiveResponse")
                        {
                            invocation.ReturnValue = new PrimitiveResponse
                                                         {
                                                             EntityPrimaryKey = null,
                                                             PrimitiveResponseValue = "",
                                                             ResponseCode = ResponseCodes.NotAuthorized,
                                                             ResponseMessage = "Not authorized",
                                                             ResponseErrorMessage = "Not authorized",
                                                             ResponseUserFriendlyMessageKey = "Not authorized"
                                                         };
                            TextLogHelper.WriteLog("Inner Exception " + "Not authorized");
                            TextLogHelper.WriteLog("Error Message " + "Not authorized");
                            TextLogHelper.WriteLog("Error StackTrace " + "Not authorized");
                            TextLogHelper.WriteLog("Result of " + invocation.Method.Name + " is: " + "Fail");
                            return;
                        }
                        else
                        {
                            Type listType = invocation.Method.ReturnType;
                            object ret = Activator.CreateInstance(listType);
                            Type type = ret.GetType();
                            PropertyInfo responseCode = type.GetProperty("ResponseCode");
                            responseCode.SetValue(ret, ResponseCodes.NotAuthorized, null);
                            PropertyInfo responseErrorMessage = type.GetProperty("ResponseErrorMessage");
                            responseErrorMessage.SetValue(ret, "Not authorized", null);
                            PropertyInfo responseMessage = type.GetProperty("ResponseMessage");
                            responseMessage.SetValue(ret, "Not authorized", null);
                            invocation.ReturnValue = ret;

                            TextLogHelper.WriteLog("Inner Exception " + "Not authorized");
                            TextLogHelper.WriteLog("Error Message " + "Not authorized");
                            TextLogHelper.WriteLog("Error StackTrace " + "Not authorized");
                            TextLogHelper.WriteLog("Result of " + invocation.Method.Name + " is: " + "Fail");
                            return;
                        }
                    }
                }
            }
            invocation.Proceed();
        }

        #endregion

        #region Nested type: AuthenticationExeption

        internal class AuthenticationExeption : Exception
        {
            public AuthenticationExeption()
            {
            }

            public AuthenticationExeption(string errorMessage, string actionName)
            {
                ErrorMessage = errorMessage;
                ActionName = actionName;
            }

            public Exception OriginalException { get; set; }

            public string ErrorMessage { get; set; }

            public string ActionName { get; set; }
        }

        #endregion
    }
}