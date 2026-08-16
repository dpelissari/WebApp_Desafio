using Microsoft.AspNetCore.Mvc;
using System;
using WebApp_Desafio_API.ViewModels;
using WebApp_Desafio_API.ViewModels.Enums;

namespace WebApp_Desafio_API.Extensions
{
    public static class ControllerExtensions
    {
        public static ObjectResult ExceptionProcess(this ControllerBase controllerBase, Exception ex)
        {
            return ExceptionProcess(controllerBase, ex, AlertTypes.warning);
        }

        public static ObjectResult ExceptionProcess(this ControllerBase controllerBase, Exception ex, AlertTypes type)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel() { Message = ex.Message, StatusCode = 500 };

            if (ex is ArgumentException aex)
            {
                errorViewModel.StatusCode = 400;
            }
            else if (ex is ApplicationException apex)
            {
                errorViewModel.StatusCode = 422;
            }

            errorViewModel.Type = type;

            return controllerBase.StatusCode(errorViewModel.StatusCode, errorViewModel);
        }
    }
}