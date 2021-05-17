using System;
using System.Collections.Generic;
using System.Text;
using TMDB.Data.Entities;
using TMDB.Data.Security;

namespace TMDB.IService
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);
        void RemoveRefreshToken(string RefreshToken);
    }
}
