using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AttendanceManager : IAttendanceService
    {
        private IAttendanceDal _attendanceDal;

        public AttendanceManager(IAttendanceDal attendanceDal)
        {
            _attendanceDal = attendanceDal;
        }

        public IDataResult<List<Attendance>> GetAll()
        {
            return new SuccessDataResult<List<Attendance>>(_attendanceDal.GetAll(), Messages.AttendancesListed);
        }

        public IDataResult<List<Attendance>> GetByStudentId(int studentId)
        {
            return new SuccessDataResult<List<Attendance>>(_attendanceDal.GetAll(a => a.StudentId == studentId));
        }

        public IDataResult<List<Attendance>> GetByDate(DateTime date)
        {
            return new SuccessDataResult<List<Attendance>>(_attendanceDal.GetAll(a => a.AttendanceDate.Date == date.Date));
        }

        public IResult Add(Attendance attendance)
        {
            _attendanceDal.Add(attendance);
            return new SuccessResult(Messages.AttendanceAdded);
        }

        public IResult Update(Attendance attendance)
        {
            _attendanceDal.Update(attendance);
            return new SuccessResult("Yoklama guncellendi");
        }
    }
}
