using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class DutyScheduleManager : IDutyScheduleService
    {
        private IDutyScheduleConstraintDal _constraintDal;
        private IDutyScheduleWeekDal _weekDal;
        private IDutyScheduleEntryDal _entryDal;

        public DutyScheduleManager(
            IDutyScheduleConstraintDal constraintDal,
            IDutyScheduleWeekDal weekDal,
            IDutyScheduleEntryDal entryDal)
        {
            _constraintDal = constraintDal;
            _weekDal = weekDal;
            _entryDal = entryDal;
        }

        // Constraint
        public IDataResult<List<DutyScheduleConstraint>> GetConstraints()
        {
            return new SuccessDataResult<List<DutyScheduleConstraint>>(_constraintDal.GetAll());
        }

        public IDataResult<DutyScheduleConstraint> GetConstraintById(int id)
        {
            return new SuccessDataResult<DutyScheduleConstraint>(_constraintDal.Get(c => c.Id == id));
        }

        [SecuredOperation("admin,yonetici")]
        [ValidationAspect(typeof(DutyScheduleConstraintValidator))]
        public IResult AddConstraint(DutyScheduleConstraint constraint)
        {
            _constraintDal.Add(constraint);
            return new SuccessResult("Nobet kisitlari eklendi");
        }

        [SecuredOperation("admin,yonetici")]
        [ValidationAspect(typeof(DutyScheduleConstraintValidator))]
        public IResult UpdateConstraint(DutyScheduleConstraint constraint)
        {
            _constraintDal.Update(constraint);
            return new SuccessResult("Nobet kisitlari guncellendi");
        }

        // Week
        public IDataResult<DutyScheduleWeek> GetWeekById(int id)
        {
            return new SuccessDataResult<DutyScheduleWeek>(_weekDal.Get(w => w.Id == id));
        }

        public IDataResult<List<DutyScheduleWeek>> GetWeeksByDateRange(DateTime startDate, DateTime endDate)
        {
            return new SuccessDataResult<List<DutyScheduleWeek>>(
                _weekDal.GetAll(w => w.WeekStartDate >= startDate && w.WeekStartDate <= endDate));
        }

        public IResult GenerateWeek(int constraintId, DateTime weekStartDate, int createdByUserId)
        {
            // TODO (ALGORITMA): Yonetici tarafindan secilen kisitlamalara (Constraint) ve haftalik 
            // kurallara gore nobet tablosunu otomatik (random veya ardışıklık kısıtlarıyla) dolduracak 
            // is mantigi buraya eklenecek. Ileride entegre edilecek.
            var week = new DutyScheduleWeek
            {
                ConstraintId = constraintId,
                WeekStartDate = weekStartDate,
                Status = "Draft",
                CreatedByUserId = createdByUserId,
                CreatedAt = DateTime.Now
            };
            _weekDal.Add(week);
            return new SuccessResult(Messages.DutyScheduleGenerated);
        }

        public IResult ApproveWeek(int weekId)
        {
            var week = _weekDal.Get(w => w.Id == weekId);
            if (week == null) return new ErrorResult("Haftalik program bulunamadi");
            week.Status = "Published";
            week.ApprovedAt = DateTime.Now;
            _weekDal.Update(week);
            return new SuccessResult(Messages.DutyScheduleApproved);
        }

        // Entry
        public IDataResult<List<DutyScheduleEntry>> GetEntriesByWeekId(int weekId)
        {
            return new SuccessDataResult<List<DutyScheduleEntry>>(
                _entryDal.GetAll(e => e.WeekId == weekId), Messages.DutyScheduleListed);
        }

        public IResult AddEntry(DutyScheduleEntry entry)
        {
            _entryDal.Add(entry);
            return new SuccessResult("Nobet girisi eklendi");
        }

        public IResult DeleteEntry(int entryId)
        {
            var entry = _entryDal.Get(e => e.Id == entryId);
            if (entry == null) return new ErrorResult("Nobet girisi bulunamadi");
            _entryDal.Delete(entry);
            return new SuccessResult("Nobet girisi silindi");
        }
    }
}
