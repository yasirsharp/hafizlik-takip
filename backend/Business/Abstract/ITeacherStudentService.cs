using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITeacherStudentService
    {
        IDataResult<List<TeacherStudentAssignment>> GetByTeacherId(int teacherUserId);
        IDataResult<List<Student>> GetOfficialStudents(int teacherUserId);
        IResult Add(TeacherStudentAssignment assignment);
        IResult Update(TeacherStudentAssignment assignment);
        IResult Delete(TeacherStudentAssignment assignment);
    }
}
