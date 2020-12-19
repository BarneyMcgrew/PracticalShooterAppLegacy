using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooter.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {
        private string _errorMessage;

        public ErrorViewModel() { }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public void SetErrorMessage(Exception ex)
        {
            ErrorMessage = JsonConvert.SerializeObject(ex);
        }
    }
}
