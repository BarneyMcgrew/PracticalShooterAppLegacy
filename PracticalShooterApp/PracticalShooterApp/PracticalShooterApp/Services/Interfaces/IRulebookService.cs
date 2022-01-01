using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.DataModels;
using PracticalShooterApp.Enums;

namespace PracticalShooterApp.Services.Interfaces
{
    public interface IRulebookService
    {
        Task<List<Rulebook>> GetAllRulebooksByLanguage(Language language);
    }
}