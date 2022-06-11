using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.DataModels;

namespace PracticalShooterApp.Services
{
    public interface IGlossaryService
    {
        Task<List<Glossary>> GetAllGlossaries();
        Task<List<Glossary>> GetGlossariesFromCurrentRulebook();
    }
}