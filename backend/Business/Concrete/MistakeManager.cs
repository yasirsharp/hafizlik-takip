using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MistakeManager : IMistakeService
    {
        private IMistakeDal _mistakeDal;

        public MistakeManager(IMistakeDal mistakeDal)
        {
            _mistakeDal = mistakeDal;
        }

        public IDataResult<List<Mistake>> GetAll()
        {
            return new SuccessDataResult<List<Mistake>>(_mistakeDal.GetAll(), Messages.MistakesListed);
        }

        public IDataResult<List<Mistake>> GetByLessonId(int lessonId)
        {
            return new SuccessDataResult<List<Mistake>>(_mistakeDal.GetAll(m => m.LessonId == lessonId));
        }

        [ValidationAspect(typeof(MistakeValidator))]
        public IResult Add(Mistake mistake)
        {
            _mistakeDal.Add(mistake);
            return new SuccessResult(Messages.MistakeAdded);
        }

        public IResult Delete(Mistake mistake)
        {
            _mistakeDal.Delete(mistake);
            return new SuccessResult("Hata kaydi silindi");
        }
    }
}
