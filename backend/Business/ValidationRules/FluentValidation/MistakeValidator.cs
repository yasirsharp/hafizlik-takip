using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class MistakeValidator : AbstractValidator<Mistake>
    {
        public MistakeValidator()
        {
            RuleFor(m => m.LessonId).GreaterThan(0).WithMessage("Gecerli bir ders secilmelidir");
            RuleFor(m => m.MistakeType).NotEmpty().WithMessage("Hata tipi bos olamaz");
            RuleFor(m => m.Page).GreaterThan(0).WithMessage("Sayfa numarasi 0'dan buyuk olmalidir");
        }
    }
}
