namespace TranslatorWithMSApi.Models
{
    /// <summary>
    /// Model class to hold the access token
    /// </summary>
    public class AccessToken
    {
        public string access_token { get; set; }
        
        public string token_type { get; set; }
        
        public string expires_in { get; set; }
        
        public string scope { get; set; }
    }
}