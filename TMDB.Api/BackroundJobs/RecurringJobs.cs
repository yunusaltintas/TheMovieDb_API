using Hangfire;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDB.Data;
using TMDB.Data.DTOs;
using TMDB.Data.SystemModels;

namespace TMDB.Api.BackroundJobs
{
    public class RecurringJobs
    {

        private readonly MovieSystemModel _movieSystemModel;

        public RecurringJobs(IOptions<MovieSystemModel> options)
        {
            _movieSystemModel = options.Value;
        }

        public void ReportingJob()
        {

            RecurringJob.AddOrUpdate("reportjob1", () => GetAllMovieList(),  Cron.Daily());
        }

        public List<MovieResultDTO> GetAllMovieList()
        {
            var AllMovieList = new List<MovieResultDTO>();

            for (int PageNumber = 1; PageNumber < 501; PageNumber++)
            {
                string Url = string.Format(_movieSystemModel.PopulerMovieUrl, _movieSystemModel.ApiKey, PageNumber);

                var client = new RestClient(_movieSystemModel.BaseUrl);
                var request = new RestRequest(Url, Method.GET);

                var query = client.Execute(request);
                var MovieList = JsonConvert.DeserializeObject<PopulerMovieListDTO>(query.Content);

                AllMovieList.AddRange(MovieList.Results);
                if (AllMovieList.Count >= 100)
                {
                    return AllMovieList;
                }

                if (!query.IsSuccessful)
                {
                    var queryError = client.Execute<ErrorDTO>(request);
                    var ErrorString = JsonConvert.DeserializeObject<ErrorDTO>(query.Content);
                }
            }
            return AllMovieList;
        }
    }
}
