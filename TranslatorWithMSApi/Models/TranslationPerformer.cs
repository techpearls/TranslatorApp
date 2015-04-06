using System;
using System.Net;
using System.IO;

namespace TranslatorWithMSApi.Models
{
    /// <summary>
    /// Handles the translation
    /// </summary>
    public static class TranslationPerformer
    {
        /// <summary>
        /// The translator URI
        /// </summary>
        public static readonly string TranslatorUri = "http://api.microsofttranslator.com/v2/Ajax.svc/Translate?";

        /// <summary>
        /// Performs the translation.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="text">The text.</param>
        /// <param name="source">The source language.</param>
        /// <param name="target">The target language.</param>
        /// <returns></returns>
        internal static string Perform(AccessToken token, string text, string source, string target)
        {            
            try
            {
                string headerValue = "Bearer " + token.access_token; 
                string requestUri = TranslatorUri + "text=" + System.Web.HttpUtility.UrlEncode(text) + "&from=" + source + "&to=" + target;
                WebRequest translationWebRequest = WebRequest.Create(requestUri);
                translationWebRequest.Headers.Add("Authorization", headerValue);
                HttpWebResponse response = null;
                string data= null; 
                response = (HttpWebResponse)translationWebRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(responseStream);
                    data = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    responseStream.Close();
                    response.Close();
                }
                return data;
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}