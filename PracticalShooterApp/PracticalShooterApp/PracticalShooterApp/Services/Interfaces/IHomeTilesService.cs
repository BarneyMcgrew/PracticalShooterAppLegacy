using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Services.Interfaces
{
    public interface IHomeTilesService
    {
        Task<List<HomeTilesModel>> GetHomeTiles();
        Task<List<HomeTilesModel>> UpdateHomeTiles();
    }
}