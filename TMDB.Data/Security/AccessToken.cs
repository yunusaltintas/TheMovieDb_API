using System;
using System.Collections.Generic;
using System.Text;

namespace TMDB.Data.Security
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public string RefreshToken { get; set; }
    }
}
