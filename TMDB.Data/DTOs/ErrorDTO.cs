using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMDB.Data.DTOs
{
    public class ErrorDTO
    {
        public ErrorDTO()
        {
            Errors = new List<string>();
        }

        public List<String> Errors { get; set; }
        public int Status { get; set; }
    }
}
