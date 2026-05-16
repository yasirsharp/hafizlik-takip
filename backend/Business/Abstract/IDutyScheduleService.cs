using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDutyScheduleService
    {
        IDataResult<List<DutyScheduleConstraint>> GetConstraints();
        IDataResult<DutyScheduleConstraint> GetConstraintById(int id);
        IResult AddConstraint(DutyScheduleConstraint constraint);
        IResult UpdateConstraint(DutyScheduleConstraint constraint);

        IDataResult<DutyScheduleWeek> GetWeekById(int id);
        IDataResult<List<DutyScheduleWeek>> GetWeeksByDateRange(DateTime startDate, DateTime endDate);
        IResult GenerateWeek(int constraintId, DateTime weekStartDate, int createdByUserId);
        IResult ApproveWeek(int weekId);

        IDataResult<List<DutyScheduleEntry>> GetEntriesByWeekId(int weekId);
        IResult AddEntry(DutyScheduleEntry entry);
        IResult DeleteEntry(int entryId);
    }
}
