using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class LessonValidator : AbstractValidator<Lesson>
    {
        public LessonValidator()
        {
            RuleFor(l => l.StudentId).GreaterThan(0).WithMessage("Ogrenci secilmelidir");
            RuleFor(l => l.TeacherUserId).GreaterThan(0).WithMessage("Ogretici secilmelidir");
            RuleFor(l => l.LessonType).NotEmpty().WithMessage("Ders tipi bos olamaz");
            RuleFor(l => l.StartPage).GreaterThanOrEqualTo(1).WithMessage("Baslangic sayfasi 1'den kucuk olamaz");
            RuleFor(l => l.EndPage).GreaterThanOrEqualTo(l => l.StartPage).WithMessage("Bitis sayfasi baslangictan kucuk olamaz");
            RuleFor(l => l.Performance).InclusiveBetween(1, 10).WithMessage("Performans 1-10 arasi olmali");
        }
    }
}
