using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMDB.Data.DTOs
{
    public class PopulerMovieListDTO:ErrorDTO
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("results")]
        public List<MovieResultDTO> Results { get; set; }


    }
}
