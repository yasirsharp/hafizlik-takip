using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class HomeworkValidator : AbstractValidator<Homework>
    {
        public HomeworkValidator()
        {
            RuleFor(h => h.StudentId).GreaterThan(0).WithMessage("Ogrenci secilmelidir");
            RuleFor(h => h.Title).NotEmpty().WithMessage("Odev basligi bos olamaz");
            RuleFor(h => h.StartPage).GreaterThanOrEqualTo(1).WithMessage("Baslangic sayfasi 1'den kucuk olamaz");
            RuleFor(h => h.EndPage).GreaterThanOrEqualTo(h => h.StartPage).WithMessage("Bitis sayfasi baslangictan kucuk olamaz");
        }
    }
}
