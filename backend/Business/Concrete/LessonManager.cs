using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class LessonManager : ILessonService
    {
        private ILessonDal _lessonDal;

        public LessonManager(ILessonDal lessonDal)
        {
            _lessonDal = lessonDal;
        }

        public IDataResult<List<Lesson>> GetAll()
        {
            return new SuccessDataResult<List<Lesson>>(_lessonDal.GetAll(), Messages.LessonsListed);
        }

        public IDataResult<List<Lesson>> GetByStudentId(int studentId)
        {
            return new SuccessDataResult<List<Lesson>>(_lessonDal.GetAll(l => l.StudentId == studentId));
        }

        public IDataResult<Lesson> GetById(int id)
        {
            return new SuccessDataResult<Lesson>(_lessonDal.Get(l => l.Id == id));
        }

        [ValidationAspect(typeof(LessonValidator))]
        public IResult Add(Lesson lesson)
        {
            _lessonDal.Add(lesson);
            return new SuccessResult(Messages.LessonAdded);
        }

        [ValidationAspect(typeof(LessonValidator))]
        public IResult Update(Lesson lesson)
        {
            _lessonDal.Update(lesson);
            return new SuccessResult(Messages.LessonUpdated);
        }

        public IResult Delete(Lesson lesson)
        {
            _lessonDal.Delete(lesson);
            return new SuccessResult("Ders kaydi silindi");
        }
    }
}
