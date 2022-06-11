using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.DataModels;

namespace PracticalShooterApp.Services
{
    public interface IAppendixService
    {
        Task<List<Appendix>> GetAllAppendices();
        Task<List<Appendix>> GetAppendicesFromCurrentRulebook();
    }
}