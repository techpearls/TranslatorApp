using System;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web;
using TranslatorWithMSApi.Interfaces;
using System.Configuration;

namespace TranslatorWithMSApi.Models
{
    /// <summary>
    /// This class handles authentication and is responsible for obtaining the access token.
    /// </summary>
    public class Authentication : IAuthentication
    {
        /// <summary>
        /// The datamarket access URI
        /// </summary>
        public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        /// <summary>
        /// Gets the cliend identifier.
        /// </summary>
        /// <value>
        /// The cliend identifier.
        /// </value>
        public string CliendId { get { return ConfigurationManager.AppSettings["clientID"]; } }
        /// <summary>
        /// Gets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret { get { return ConfigurationManager.AppSettings["clientSecret"]; } }
        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public string Request
        {
            get
            {
                return string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com",
                                        HttpUtility.UrlEncode(this.CliendId),
                                        HttpUtility.UrlEncode(this.ClientSecret));
            }
        }

        /// <summary>
        /// Sends a HTTP post request to obtain token information.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="requestDetails">The request details.</param>
        /// <returns></returns>
        private AccessToken HttpPost(string uri, string requestDetails)
        {
            try
            {
                var webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";
                byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
                webRequest.ContentLength = bytes.Length;
                using (Stream outputStream = webRequest.GetRequestStream())
                {
                    outputStream.Write(bytes, 0, bytes.Length);
                }
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    var serializer = new DataContractJsonSerializer(typeof(AccessToken));

                    //Get deserialized object from JSON stream
                    var token = (AccessToken)serializer.ReadObject(webResponse.GetResponseStream());

                    return token;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns>Access token</returns>
        public AccessToken GetAccessToken()
        {
            return HttpPost(DatamarketAccessUri, this.Request);
        }
    }
}