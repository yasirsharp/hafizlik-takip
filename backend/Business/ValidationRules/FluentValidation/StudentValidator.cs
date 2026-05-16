using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(s => s.FirstName).NotEmpty().WithMessage("Ogrenci adi bos olamaz");
            RuleFor(s => s.FirstName).MinimumLength(2).WithMessage("Ogrenci adi en az 2 karakter olmali");
            RuleFor(s => s.LastName).NotEmpty().WithMessage("Ogrenci soyadi bos olamaz");
            RuleFor(s => s.TenantId).GreaterThan(0).WithMessage("Kurum bilgisi zorunludur");
            RuleFor(s => s.CurrentPage).GreaterThanOrEqualTo(1).WithMessage("Sayfa numarasi 1'den kucuk olamaz");
            RuleFor(s => s.CurrentPage).LessThanOrEqualTo(604).WithMessage("Sayfa numarasi 604'ten buyuk olamaz");
        }
    }
}
