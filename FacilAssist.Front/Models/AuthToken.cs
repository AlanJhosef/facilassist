using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacilAssist.Front.Models
{
    public class AuthToken
    {
        [JsonProperty("token")]
        public string AccessToken { get; set; }        
    }
    public class AuthRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}