using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalShooterApp.DataModels;

namespace PracticalShooterApp.Services.Interfaces
{
    public interface ISectionService
    {
        Task<List<Section>> GetSectionsByChapterId(int chapterId);
    }
}