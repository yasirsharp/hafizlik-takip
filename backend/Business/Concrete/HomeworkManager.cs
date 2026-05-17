using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class HomeworkManager : IHomeworkService
    {
        private IHomeworkDal _homeworkDal;

        public HomeworkManager(IHomeworkDal homeworkDal)
        {
            _homeworkDal = homeworkDal;
        }

        public IDataResult<List<Homework>> GetAll()
        {
            return new SuccessDataResult<List<Homework>>(_homeworkDal.GetAll(), Messages.HomeworksListed);
        }

        public IDataResult<List<Homework>> GetByStudentId(int studentId)
        {
            return new SuccessDataResult<List<Homework>>(_homeworkDal.GetAll(h => h.StudentId == studentId));
        }

        public IDataResult<Homework> GetById(int id)
        {
            return new SuccessDataResult<Homework>(_homeworkDal.Get(h => h.Id == id));
        }

        [ValidationAspect(typeof(HomeworkValidator))]
        public IResult Add(Homework homework)
        {
            _homeworkDal.Add(homework);
            return new SuccessResult(Messages.HomeworkAdded);
        }

        [ValidationAspect(typeof(HomeworkValidator))]
        public IResult Update(Homework homework)
        {
            _homeworkDal.Update(homework);
            return new SuccessResult(Messages.HomeworkUpdated);
        }

        public IResult Delete(Homework homework)
        {
            _homeworkDal.Delete(homework);
            return new SuccessResult("Odev silindi");
        }
    }
}
