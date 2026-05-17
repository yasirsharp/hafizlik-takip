using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AttendanceValidator : AbstractValidator<Attendance>
    {
        public AttendanceValidator()
        {
            RuleFor(a => a.StudentId).GreaterThan(0).WithMessage("Ogrenci secilmelidir");
            RuleFor(a => a.TeacherUserId).GreaterThan(0).WithMessage("Ogretmen bilgisi zorunludur");
            RuleFor(a => a.AttendanceDate).NotEmpty().WithMessage("Tarih bos olamaz");
            RuleFor(a => a.Reason).MaximumLength(500).WithMessage("Mazeret aciklamasi 500 karakteri gecemez");
        }
    }
}
