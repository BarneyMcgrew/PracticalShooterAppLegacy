using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Services
{
    public interface ISearchService
    {
        Task<List<SearchModel>> SearchRules(string searchParameters);
    }
}