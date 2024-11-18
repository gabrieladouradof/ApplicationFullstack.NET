using Dimadev.Core.Requests.Account;
using Dimadev.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimadev.Core.Handlers
{
    public interface IAccountHandler
    {
        Task<Response<string>> LoginAsync(LoginRequest request);
        Task<Response<string>> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
    }
}
