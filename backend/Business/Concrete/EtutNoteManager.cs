using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class EtutNoteManager : IEtutNoteService
    {
        private IEtutNoteDal _etutNoteDal;

        public EtutNoteManager(IEtutNoteDal etutNoteDal)
        {
            _etutNoteDal = etutNoteDal;
        }

        public IDataResult<List<EtutNote>> GetAll()
        {
            return new SuccessDataResult<List<EtutNote>>(_etutNoteDal.GetAll());
        }

        public IDataResult<List<EtutNote>> GetByStudentId(int studentId)
        {
            return new SuccessDataResult<List<EtutNote>>(_etutNoteDal.GetAll(e => e.StudentId == studentId));
        }

        public IDataResult<List<EtutNote>> GetUnreadByTeacherId(int teacherUserId)
        {
            return new SuccessDataResult<List<EtutNote>>(
                _etutNoteDal.GetAll(e => e.ToTeacherUserId == teacherUserId && !e.IsRead));
        }

        public IResult Add(EtutNote etutNote)
        {
            _etutNoteDal.Add(etutNote);
            return new SuccessResult("Etut notu eklendi");
        }

        public IResult MarkAsRead(int id)
        {
            var note = _etutNoteDal.Get(e => e.Id == id);
            if (note == null) return new ErrorResult("Etut notu bulunamadi");
            note.IsRead = true;
            _etutNoteDal.Update(note);
            return new SuccessResult("Etut notu okundu olarak isaretlendi");
        }
    }
}
