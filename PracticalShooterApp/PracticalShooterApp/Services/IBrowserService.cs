using System;
using System.Threading.Tasks;

namespace PracticalShooterApp.Services
{
    public interface IBrowserService
    {
        Task<bool> GoToLink(Uri uri);
    }
}