using System;
using System.Collections.Generic;
using System.Text;

namespace TMDB.Data.DTOs
{
    public class StmpDTO
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Body { get; set; }
    }
}
