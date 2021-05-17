using System;
using System.Collections.Generic;
using System.Text;

namespace TMDB.Data.DTOs
{
    public class RateDTO:ErrorDTO
    {
        public int VoteRate { get; set; }
    }
}
