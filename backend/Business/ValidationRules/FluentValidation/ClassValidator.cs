using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ClassValidator : AbstractValidator<Class>
    {
        public ClassValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Sinif adi bos olamaz");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Sinif adi en az 2 karakter olmalidir");
            RuleFor(c => c.Capacity).GreaterThan(0).WithMessage("Kapasite 0'dan buyuk olmalidir");
        }
    }
}
