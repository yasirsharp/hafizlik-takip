using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Business.Abstract;
using Core.Utilities.Security.Entities;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IOperationClaimDal _operationClaimDal;
        private IUserOperationClaimDal _userOperationClaimDal;

        public UserManager(IUserDal userDal, IOperationClaimDal operationClaimDal, IUserOperationClaimDal userOperationClaimDal)
        {
            _userDal = userDal;
            _operationClaimDal = operationClaimDal;
            _userOperationClaimDal = userOperationClaimDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public void AddClaim(User user, string claimName)
        {
            var claim = _operationClaimDal.Get(c => c.Name == claimName);
            if (claim != null)
            {
                _userOperationClaimDal.Add(new UserOperationClaim
                {
                    UserId = user.Id,
                    OperationClaimId = claim.Id
                });
            }
        }
    }
}
