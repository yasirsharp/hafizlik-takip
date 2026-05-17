using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class EtutNoteValidator : AbstractValidator<EtutNote>
    {
        public EtutNoteValidator()
        {
            RuleFor(e => e.StudentId).GreaterThan(0).WithMessage("Ogrenci secilmelidir");
            RuleFor(e => e.FromTeacherUserId).GreaterThan(0).WithMessage("Notu gonderen ogretici bilgisi zorunludur");
            RuleFor(e => e.Content).NotEmpty().WithMessage("Not icerigi bos olamaz");
        }
    }
}
