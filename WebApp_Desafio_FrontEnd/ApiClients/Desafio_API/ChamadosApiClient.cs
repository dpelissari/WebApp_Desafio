using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WebApp_Desafio_FrontEnd.ViewModels;

namespace WebApp_Desafio_FrontEnd.ApiClients.Desafio_API
{
    public class ChamadosApiClient : BaseClient
    {
        private readonly string _token;
        private readonly string _urlBase;

        public ChamadosApiClient() : base()
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

        public List<ChamadoViewModel> Listar()
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", _token }
            };

            var response = base.Get($"{_urlBase}Chamados/Listar", headers: headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<List<ChamadoViewModel>>(json);
        }

        public ChamadoViewModel Obter(int idChamado)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", _token }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idChamado", idChamado }
            };

            var response = base.Get($"{_urlBase}Chamados/Obter", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<ChamadoViewModel>(json);
        }

        public bool Gravar(ChamadoViewModel chamado)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", _token }
            };

            var response = base.Post($"{_urlBase}Chamados/Gravar", chamado, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

        public bool Excluir(int idChamado)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", _token }
            };

            var querys = new Dictionary<string, object>()
            {
                { "idChamado", idChamado }
            };

            var response = base.Delete($"{_urlBase}Chamados/Excluir", querys, headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }
    }
}
