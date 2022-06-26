using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Services
{
    public interface IInAppMessagesService
    {
        Task<List<InAppMessageModel>> GetInAppMessages();

        Task<List<InAppMessageModel>> UpdateInAppMessages();
    }
}