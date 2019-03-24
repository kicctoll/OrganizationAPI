using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IExternalAuthenticationService<TReturnType>
    {
        Task<TReturnType> Login(string code, string redirectURI);
    }
}
