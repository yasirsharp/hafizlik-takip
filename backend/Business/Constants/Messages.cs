namespace Business.Constants
{
    public static class Messages
    {
        // Auth
        public static string UserRegistered = "Kullanici basariyla kaydedildi";
        public static string UserNotFound = "Kullanici bulunamadi";
        public static string PasswordError = "Sifre hatali";
        public static string SuccessfulLogin = "Sisteme giris basarili";
        public static string UserAlreadyExists = "Bu kullanici zaten mevcut";
        public static string AccessTokenCreated = "Access token basariyla olusturuldu";

        // Student
        public static string StudentAdded = "Ogrenci basariyla eklendi";
        public static string StudentUpdated = "Ogrenci basariyla guncellendi";
        public static string StudentDeleted = "Ogrenci basariyla silindi";
        public static string StudentsListed = "Ogrenciler basariyla listelendi";

        // Lesson
        public static string LessonAdded = "Ders kaydi basariyla eklendi";
        public static string LessonUpdated = "Ders kaydi basariyla guncellendi";
        public static string LessonsListed = "Ders kayitlari listelendi";

        // Mistake
        public static string MistakeAdded = "Hata kaydi eklendi";
        public static string MistakesListed = "Hata kayitlari listelendi";

        // Homework
        public static string HomeworkAdded = "Odev basariyla eklendi";
        public static string HomeworkUpdated = "Odev basariyla guncellendi";
        public static string HomeworksListed = "Odevler listelendi";

        // Attendance
        public static string AttendanceAdded = "Yoklama kaydi eklendi";
        public static string AttendancesListed = "Yoklama kayitlari listelendi";

        // Class
        public static string ClassAdded = "Sinif basariyla eklendi";
        public static string ClassUpdated = "Sinif basariyla guncellendi";
        public static string ClassesListed = "Siniflar listelendi";

        // DutySchedule
        public static string DutyScheduleGenerated = "Nobet programi basariyla olusturuldu";
        public static string DutyScheduleApproved = "Nobet programi onaylandi";
        public static string DutyScheduleListed = "Nobet programi listelendi";

        // General
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string ValidationError = "Dogrulama hatasi";
    }
}
