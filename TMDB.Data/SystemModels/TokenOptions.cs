using System;
using System.Collections.Generic;
using System.Text;

namespace TMDB.Data.SystemModels
{
    public class TokenOptions
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public int AccessTokenExpiration { get; set; }

        public int RefreshTokenExpiration { get; set; }

        public string SecurityKey { get; set; }
    }
}
