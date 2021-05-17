using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMDB.Data.DTOs
{
    public class MovieResultDTO
    {
        [JsonProperty("popularity")]
        public double Popularity { get; set; }
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        [JsonProperty("video")]
        public bool Video { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("adult")]
        public bool Adult { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> GenreIds { get; set; }

    }
}
