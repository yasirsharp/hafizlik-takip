using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class DutyScheduleConstraintValidator : AbstractValidator<DutyScheduleConstraint>
    {
        public DutyScheduleConstraintValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Kural adi bos olamaz");
            RuleFor(c => c.TeachersPerDay).GreaterThanOrEqualTo(0).WithMessage("Gunluk ogretici sayisi 0'dan kucuk olamaz");
            RuleFor(c => c.AssistantsPerDay).GreaterThanOrEqualTo(0).WithMessage("Gunluk yardimci ogretici sayisi 0'dan kucuk olamaz");
            RuleFor(c => c.MaxConsecutiveDays).GreaterThan(0).WithMessage("Maksimum ardisik nobet gunu 0'dan buyuk olmalidir");
        }
    }
}
