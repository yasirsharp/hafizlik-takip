using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStudentDal : EfEntityRepositoryBase<Student, HafizlikTakipContext>, IStudentDal { }
    public class EfClassDal : EfEntityRepositoryBase<Class, HafizlikTakipContext>, IClassDal { }
    public class EfLessonDal : EfEntityRepositoryBase<Lesson, HafizlikTakipContext>, ILessonDal { }
    public class EfMistakeDal : EfEntityRepositoryBase<Mistake, HafizlikTakipContext>, IMistakeDal { }
    public class EfHomeworkDal : EfEntityRepositoryBase<Homework, HafizlikTakipContext>, IHomeworkDal { }
    public class EfEtutNoteDal : EfEntityRepositoryBase<EtutNote, HafizlikTakipContext>, IEtutNoteDal { }
    public class EfAttendanceDal : EfEntityRepositoryBase<Attendance, HafizlikTakipContext>, IAttendanceDal { }
    public class EfDutyScheduleConstraintDal : EfEntityRepositoryBase<DutyScheduleConstraint, HafizlikTakipContext>, IDutyScheduleConstraintDal { }
    public class EfDutyScheduleWeekDal : EfEntityRepositoryBase<DutyScheduleWeek, HafizlikTakipContext>, IDutyScheduleWeekDal { }
    public class EfDutyScheduleEntryDal : EfEntityRepositoryBase<DutyScheduleEntry, HafizlikTakipContext>, IDutyScheduleEntryDal { }
    public class EfTeacherStudentAssignmentDal : EfEntityRepositoryBase<TeacherStudentAssignment, HafizlikTakipContext>, ITeacherStudentAssignmentDal { }
}
