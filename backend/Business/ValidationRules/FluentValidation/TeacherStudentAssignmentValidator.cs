using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TeacherStudentAssignmentValidator : AbstractValidator<TeacherStudentAssignment>
    {
        public TeacherStudentAssignmentValidator()
        {
            RuleFor(a => a.TeacherUserId).GreaterThan(0).WithMessage("Ogretici secilmelidir");
            RuleFor(a => a.StudentId).GreaterThan(0).WithMessage("Ogrenci secilmelidir");
            RuleFor(a => a.AssignmentType).NotEmpty().WithMessage("Atama tipi bos olamaz");
            RuleFor(a => a.StartDate).NotEmpty().WithMessage("Baslangic tarihi bos olamaz");
        }
    }
}
