using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.DataModels;

namespace PracticalShooterApp.Services
{
    public interface IRulebookService
    {
        Task<Rulebook> GetCurrentRulebook();
        Task<List<Rulebook>> GetAllRulebooksByCurrentLanguage();
        Task<Rulebook> GetRulebookbyRulebookId(int rulebookId);
    }
}