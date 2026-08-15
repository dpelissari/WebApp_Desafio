using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WebApp_Desafio_FrontEnd.ViewModels;

namespace WebApp_Desafio_FrontEnd.ApiClients.Desafio_API
{
    public class DepartamentosApiClient : BaseClient
    {
        private readonly string token;
        private readonly string urlBase;

        public DepartamentosApiClient() : base()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            token = config["DesafioApi:TokenAutenticacao"];
            urlBase = config["DesafioApi:UrlBase"];

            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidOperationException("Token não configurado no appsettings.");

            if (string.IsNullOrWhiteSpace(urlBase))
                throw new InvalidOperationException("UrlBase não configurada no appsettings.");
        }

        public List<DepartamentoViewModel> Listar()
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", token }
            };

            var response = base.Get($"{urlBase}Listar", headers: headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<List<DepartamentoViewModel>>(json);
        }

        public DepartamentoViewModel Obter(int idDepartamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", token }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idDepartamento", idDepartamento }
            };

            var response = base.Get($"{urlBase}Obter", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<DepartamentoViewModel>(json);
        }

        public bool Gravar(DepartamentoViewModel departamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", token }
            };

            var response = base.Post($"{urlBase}Gravar", departamento, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

        public bool Excluir(int idDepartamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", token }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idDepartamento", idDepartamento }
            };

            var response = base.Delete($"{urlBase}Excluir", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }
    }
}