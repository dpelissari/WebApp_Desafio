using System;
using WebApp_Desafio_API.ViewModels.Enums;

namespace WebApp_Desafio_API.ViewModels
{
    public class ErrorViewModel
    {
        public string Message { get; set; }
        public string Validation { get; set; }
        public string PropertyName { get; set; }
        public int StatusCode { get; set; }
        public AlertTypes Type { get; set; }
    }
}