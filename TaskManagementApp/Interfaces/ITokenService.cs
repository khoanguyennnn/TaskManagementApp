using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
