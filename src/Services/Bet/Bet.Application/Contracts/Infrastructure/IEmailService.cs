using Bet.Application.Models;
using System.Threading.Tasks;

namespace Bet.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
