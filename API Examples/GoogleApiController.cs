
using Schooldesk.Helpers;
using Schooldesk.Api.ApiModels;
using Schooldesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Models.ModelExtensions;
using Models.Backend;
using Backend.Credentials;
using Models.Backend.Google;

namespace Schooldesk.Api
{
    [RoutePrefix("api/google")]
    public class GoogleApiController : ApiController, IExternalIntegration
    {
        private GoogleRecaptchaCredentials _Credentials;

        public GoogleApiController()
        {
            GetCredentials();
        }

        [HttpGet]
        [Route("recaptcha")]
        public async Task<ResponseInfo> RecaptchaVerify(string token)
        {
            var response = new ResponseInfo();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, _Credentials.ApiAddress);
                    var keyValues = new List<KeyValuePair<string, string>>();
                    keyValues.Add(new KeyValuePair<string, string>("secret", _Credentials.SecretKey));
                    keyValues.Add(new KeyValuePair<string, string>("response", token));
                    request.Content = new FormUrlEncodedContent(keyValues);

                    var httpResponse = await httpClient.SendAsync(request);
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    var recaptchaResponse = responseString.Deserialize<ResponseToken>();
                    if (recaptchaResponse != null)
                    {
                        if (recaptchaResponse.Success == true)
                        {
                            return ResponseInfo.Success();
                        }
                        else
                        {
                            response.ErrorMessage = "Recaptcha did not verify.";
                            if (recaptchaResponse.ErrorCodes != null && recaptchaResponse.ErrorCodes.Count > 0)
                            {
                                foreach(var error in recaptchaResponse.ErrorCodes)
                                {
                                    response.ErrorDetailedMessage += error;
                                }
                            }
                        }
                    }
                    else
                    {
                        response.ErrorMessage = "Failed to get a response from Google Recaptcha";
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorMessage = "Failed to verify Google Recaptcha";
                    response.ErrorDetailedMessage = ex.Message;
                }
            }
            
            return response;
        }
        public IExternalCredentials GetCredentials()
        {
            var credentialsHelper = new CredentialsHelper();
            _Credentials = credentialsHelper.GetCredentials<GoogleRecaptchaCredentials>();
            return _Credentials;
        }
    }
}