using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDB.Data.DTOs;
using TMDB.Data.Entities;

namespace TMDB.Data.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
