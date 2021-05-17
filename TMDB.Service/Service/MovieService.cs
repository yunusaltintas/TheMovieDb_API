using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using TMDB.Data;
using TMDB.Data.DTOs;
using TMDB.Data.SystemModels;

namespace TMDB.Service
{
    public class MovieService : IMovieService
    {
        private readonly MovieSystemModel _movieSystemModel;
        private readonly SmtpSystemModel _smtpSystemModel;

        public MovieService(IOptions<MovieSystemModel> options, IOptions<SmtpSystemModel> options1)
        {
            _movieSystemModel = options.Value;
            _smtpSystemModel = options1.Value;
        }

        public MovieDetailDTO GetMovieDetail(int MovieId)
        {
            string Url = string.Format(_movieSystemModel.MovieDetailUrl, MovieId, _movieSystemModel.ApiKey);

            var client = new RestClient(_movieSystemModel.BaseUrl);
            var request = new RestRequest(Url, Method.GET);

            var query = client.Execute(request);
            var MovieDetail = JsonConvert.DeserializeObject<MovieDetailDTO>(query.Content);

            if (!query.IsSuccessful)
            {
                var queryError = client.Execute(request);
                var ErrorString = JsonConvert.DeserializeObject<ErrorDTO>(query.Content);
            }
            return MovieDetail;
        }

        public PopulerMovieListDTO GetMovieList(int PageNumber)
        {
            string Url = string.Format(_movieSystemModel.PopulerMovieUrl, _movieSystemModel.ApiKey, PageNumber);

            var client = new RestClient(_movieSystemModel.BaseUrl);
            var request = new RestRequest(Url, Method.GET);

            var query = client.Execute<PopulerMovieListDTO>(request);
            var MovieList = JsonConvert.DeserializeObject<PopulerMovieListDTO>(query.Content);

            if (!query.IsSuccessful)
            {
                var queryError = client.Execute(request);
                var ErrorString = JsonConvert.DeserializeObject<ErrorDTO>(query.Content);
            }
            return MovieList;
        }

        public RateDTO MovieRate(int MovieId, RateDTO Rate)
        {
            string Url = string.Format(_movieSystemModel.RateMovieUrl, MovieId, _movieSystemModel.ApiKey, _movieSystemModel.SeesionId);
            string Vote = "{\"value\":" + Rate.VoteRate + "}";
            var client = new RestClient(_movieSystemModel.BaseUrl);
            var request = new RestRequest(Url, Method.POST).AddJsonBody(Vote);


            var query = client.Post<RateDTO>(request);

            var Rates = JsonConvert.DeserializeObject<RateDTO>(query.Content);
            return Rates;
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

        public bool SendRecommendMail(int MovieId, RecommandDTO Recipient)
        {
            var MovieDetail = GetMovieDetail(MovieId);

            var mailContent = new StmpDTO
            {
                Email = Recipient.Email,
                Subject = "Film Tavsiyesi",
                IsBodyHtml = true,
                Body = $"<h4>Movie:{MovieDetail.Title}</h4></br><p>{MovieDetail.Overview}</p></br><p>Vote Average:{MovieDetail.VoteAverage}</p></br><a href=\"{ MovieDetail.HomePage }\">Watch Now...! </a>"
            };
            MailService(mailContent);
            return true;
        }

        private void MailService(StmpDTO stmpDTO)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential SenderInfo = new NetworkCredential(_smtpSystemModel.SenderMail, _smtpSystemModel.SenderPassword);
            smtp.Credentials = SenderInfo;

            MailAddress Sender = new MailAddress(_smtpSystemModel.SenderMail);
            MailAddress Recipient = new MailAddress(stmpDTO.Email);
            MailMessage mail = new MailMessage(Sender, Recipient);
            mail.Subject = stmpDTO.Subject;
            mail.Body = stmpDTO.Body;
            mail.IsBodyHtml = stmpDTO.IsBodyHtml;
            smtp.Send(mail);
        }
    }
}
