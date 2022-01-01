using System.Threading.Tasks;

namespace PracticalShooterApp.Helpers.Interfaces
{
    public interface IShareHelper
    {
        Task ShareText(string title, string text);

        Task ShareUri(string title, string uri);
    }
}