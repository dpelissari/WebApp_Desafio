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
        private readonly string _token;
        private readonly string _urlBase;

        public DepartamentosApiClient() : base()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            _token = config["DesafioApi:TokenAutenticacao"];
            _urlBase = config["DesafioApi:UrlBase"];

            if (string.IsNullOrWhiteSpace(_token))
                throw new InvalidOperationException("Token não configurado no appsettings.");

            if (string.IsNullOrWhiteSpace(_urlBase))
                throw new InvalidOperationException("UrlBase não configurada no appsettings.");
        }

        public List<DepartamentoViewModel> Listar()
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", _token }
            };

            var response = base.Get($"{_urlBase}Departamentos/Listar", headers: headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<List<DepartamentoViewModel>>(json);
        }

        public DepartamentoViewModel Obter(int idDepartamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", _token }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idDepartamento", idDepartamento }
            };

            var response = base.Get($"{_urlBase}Departamentos/Obter", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<DepartamentoViewModel>(json);
        }

        public bool Gravar(DepartamentoViewModel departamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", _token }
            };

            var response = base.Post($"{_urlBase}Departamentos/Gravar", departamento, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

        public bool Excluir(int idDepartamento)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", _token }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idDepartamento", idDepartamento }
            };

            var response = base.Delete($"{_urlBase}Departamentos/Excluir", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }
    }
}