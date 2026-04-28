using System.Security.Claims;
using PersonalFinanceTracker.Data;

namespace PersonalFinanceTracker.Interfaces;

public interface ITokenService
{
    string CreateToken(ApplicationUser user, IList<Claim> claims);
}