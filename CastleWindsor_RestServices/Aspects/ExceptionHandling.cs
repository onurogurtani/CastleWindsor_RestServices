using System;
using System.Reflection;
using Castle.DynamicProxy;
using Rest.Services.CommonTypes.Response;

namespace CastleWindsor_RestServices.Aspects
{
    public class ExceptionHandling : IInterceptor
    {
        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                if (invocation.Method.ReturnParameter.ParameterType.FullName ==
                    "Rest.Services.CommonTypes.Response.PrimitiveResponse")
                {
                    invocation.ReturnValue = new PrimitiveResponse
                                                 {
                                                     EntityPrimaryKey = null,
                                                     PrimitiveResponseValue = "",
                                                     ResponseCode =
                                                         (ex is Authorized.AuthenticationExeption)
                                                             ? ResponseCodes.NotAuthorized
                                                             : ResponseCodes.ServiceError
                                                 };
                    TextLogHelper.WriteLog("Inner Exception " + ex.InnerException.StackTrace);
                    TextLogHelper.WriteLog("Error Message " + ex.Message);
                    TextLogHelper.WriteLog("Error StackTrace " + ex.StackTrace);
                    TextLogHelper.WriteLog("Result of " + invocation.Method.Name + " is: " + "Fail");
                    ////throw exceptionWrapper.WrapException(ex);
                    throw ex;
                }
                else
                {
                    Type listType = invocation.Method.ReturnType;
                    object ret = Activator.CreateInstance(listType);
                    Type type = ret.GetType();
                    PropertyInfo responseCode = type.GetProperty("ResponseCode");
                    responseCode.SetValue(ret,
                                          (ex is Authorized.AuthenticationExeption)
                                              ? ResponseCodes.NotAuthorized
                                              : ResponseCodes.ServiceError, null);
                    PropertyInfo responseErrorMessage = type.GetProperty("ResponseErrorMessage");
                    responseErrorMessage.SetValue(ret, "Service Error", null);
                    PropertyInfo responseMessage = type.GetProperty("ResponseMessage");
                    responseMessage.SetValue(ret, "Service Error", null);
                    invocation.ReturnValue = ret;

                    TextLogHelper.WriteLog("Inner Exception " + ex.InnerException.StackTrace);
                    TextLogHelper.WriteLog("Error Message " + ex.Message);
                    TextLogHelper.WriteLog("Error StackTrace " + ex.StackTrace);
                    TextLogHelper.WriteLog("Result of " + invocation.Method.Name + " is: " + "Fail");
                    ////throw exceptionWrapper.WrapException(ex);
                    throw ex;
                }
            }
        }

        #endregion
    }
}