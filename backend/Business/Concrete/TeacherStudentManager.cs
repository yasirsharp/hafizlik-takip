using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class TeacherStudentManager : ITeacherStudentService
    {
        private ITeacherStudentAssignmentDal _assignmentDal;
        private IStudentDal _studentDal;

        public TeacherStudentManager(ITeacherStudentAssignmentDal assignmentDal, IStudentDal studentDal)
        {
            _assignmentDal = assignmentDal;
            _studentDal = studentDal;
        }

        public IDataResult<List<TeacherStudentAssignment>> GetByTeacherId(int teacherUserId)
        {
            return new SuccessDataResult<List<TeacherStudentAssignment>>(
                _assignmentDal.GetAll(a => a.TeacherUserId == teacherUserId && a.IsActive));
        }

        public IDataResult<List<Student>> GetOfficialStudents(int teacherUserId)
        {
            var assignments = _assignmentDal.GetAll(a =>
                a.TeacherUserId == teacherUserId &&
                a.AssignmentType == "Official" &&
                a.IsActive);

            var studentIds = assignments.Select(a => a.StudentId).ToList();
            var students = _studentDal.GetAll(s => studentIds.Contains(s.Id));

            return new SuccessDataResult<List<Student>>(students);
        }

        [ValidationAspect(typeof(TeacherStudentAssignmentValidator))]
        public IResult Add(TeacherStudentAssignment assignment)
        {
            _assignmentDal.Add(assignment);
            return new SuccessResult("Ogrenci atamasi eklendi");
        }

        [ValidationAspect(typeof(TeacherStudentAssignmentValidator))]
        public IResult Update(TeacherStudentAssignment assignment)
        {
            _assignmentDal.Update(assignment);
            return new SuccessResult("Ogrenci atamasi guncellendi");
        }

        public IResult Delete(TeacherStudentAssignment assignment)
        {
            _assignmentDal.Delete(assignment);
            return new SuccessResult("Ogrenci atamasi silindi");
        }
    }
}
