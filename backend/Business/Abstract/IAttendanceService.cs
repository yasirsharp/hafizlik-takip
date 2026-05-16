using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAttendanceService
    {
        IDataResult<List<Attendance>> GetAll();
        IDataResult<List<Attendance>> GetByStudentId(int studentId);
        IDataResult<List<Attendance>> GetByDate(DateTime date);
        IResult Add(Attendance attendance);
        IResult Update(Attendance attendance);
    }
}
