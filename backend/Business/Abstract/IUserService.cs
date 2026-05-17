using Core.Utilities.Results;
using Core.Utilities.Security.Entities;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
        void AddClaim(User user, string claimName);
    }
}
