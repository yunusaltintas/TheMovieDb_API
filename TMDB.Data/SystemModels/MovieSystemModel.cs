using System;
using System.Collections.Generic;
using System.Text;

namespace TMDB.Data
{
    public class MovieSystemModel
    {
        public string ApiKey { get; set; }
        public string ApiToken { get; set; }
        public string SeesionId { get; set; }

        public string BaseUrl { get; set; }
        public string MovieDetailUrl { get; set; }
        public string PopulerMovieUrl { get; set; }
        public string RateMovieUrl { get; set; }
    }
}
