-- ============================================
-- Hafizlik Takip - Veritabani Olusturma Scripti
-- PostgreSQL 16+
-- ============================================

-- Veritabani olustur (psql ile calistir)
-- CREATE DATABASE "HafizlikTakipDb";

-- ============================================
-- CORE TABLOLARI
-- ============================================

CREATE TABLE IF NOT EXISTS "Users" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL DEFAULT 0,
    "Role" VARCHAR(50) NOT NULL DEFAULT 'ogrenci',
    "FirstName" VARCHAR(100) NOT NULL,
    "LastName" VARCHAR(100) NOT NULL,
    "Email" VARCHAR(200) NOT NULL UNIQUE,
    "PasswordSalt" BYTEA NOT NULL,
    "PasswordHash" BYTEA NOT NULL,
    "Status" BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS "OperationClaims" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS "UserOperationClaims" (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL REFERENCES "Users"("Id"),
    "OperationClaimId" INT NOT NULL REFERENCES "OperationClaims"("Id")
);

-- ============================================
-- TENANT
-- ============================================

CREATE TABLE IF NOT EXISTS "Tenants" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL,
    "Code" VARCHAR(50) NOT NULL UNIQUE,
    "Address" TEXT,
    "Phone" VARCHAR(20),
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

-- ============================================
-- DOMAIN TABLOLARI
-- ============================================

CREATE TABLE IF NOT EXISTS "Classes" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "Name" VARCHAR(200) NOT NULL,
    "TeacherUserId" INT REFERENCES "Users"("Id"),
    "Capacity" INT NOT NULL DEFAULT 30,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "Students" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "FirstName" VARCHAR(100) NOT NULL,
    "LastName" VARCHAR(100) NOT NULL,
    "Phone" VARCHAR(20),
    "BirthDate" DATE,
    "ParentName" VARCHAR(200),
    "ParentPhone" VARCHAR(20),
    "CurrentPage" INT NOT NULL DEFAULT 1,
    "CurrentJuz" INT NOT NULL DEFAULT 1,
    "ClassId" INT REFERENCES "Classes"("Id"),
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "ArchivedAt" TIMESTAMP
);

CREATE TABLE IF NOT EXISTS "Lessons" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "StudentId" INT NOT NULL REFERENCES "Students"("Id"),
    "TeacherUserId" INT NOT NULL REFERENCES "Users"("Id"),
    "LessonType" VARCHAR(50) NOT NULL,
    "StartPage" INT NOT NULL,
    "EndPage" INT NOT NULL,
    "Performance" INT NOT NULL DEFAULT 5,
    "Notes" TEXT,
    "LessonDate" DATE NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "Mistakes" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "LessonId" INT NOT NULL REFERENCES "Lessons"("Id"),
    "MistakeType" VARCHAR(50) NOT NULL,
    "Page" INT NOT NULL,
    "AyahNumber" INT,
    "Word" VARCHAR(200),
    "Description" TEXT,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "Homeworks" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "StudentId" INT NOT NULL REFERENCES "Students"("Id"),
    "TeacherUserId" INT NOT NULL REFERENCES "Users"("Id"),
    "Title" VARCHAR(200) NOT NULL,
    "Description" TEXT,
    "StartPage" INT NOT NULL,
    "EndPage" INT NOT NULL,
    "Status" VARCHAR(50) NOT NULL DEFAULT 'Bekliyor',
    "DueDate" DATE NOT NULL,
    "CompletedAt" TIMESTAMP,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "EtutNotes" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "StudentId" INT NOT NULL REFERENCES "Students"("Id"),
    "FromTeacherUserId" INT NOT NULL REFERENCES "Users"("Id"),
    "ToTeacherUserId" INT REFERENCES "Users"("Id"),
    "Content" TEXT NOT NULL,
    "IsRead" BOOLEAN NOT NULL DEFAULT FALSE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "Attendances" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "StudentId" INT NOT NULL REFERENCES "Students"("Id"),
    "TeacherUserId" INT NOT NULL REFERENCES "Users"("Id"),
    "AttendanceDate" DATE NOT NULL,
    "IsPresent" BOOLEAN NOT NULL DEFAULT TRUE,
    "Reason" VARCHAR(500),
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "VideoRecords" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "StudentId" INT NOT NULL REFERENCES "Students"("Id"),
    "TeacherUserId" INT NOT NULL REFERENCES "Users"("Id"),
    "FilePath" VARCHAR(500) NOT NULL,
    "Title" VARCHAR(200),
    "DurationSeconds" INT NOT NULL DEFAULT 0,
    "FileSizeBytes" BIGINT NOT NULL DEFAULT 0,
    "RecordedAt" TIMESTAMP NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

-- ============================================
-- KURAN REFERANS TABLOLARI
-- ============================================

CREATE TABLE IF NOT EXISTS "Surahs" (
    "Id" SERIAL PRIMARY KEY,
    "SurahNumber" INT NOT NULL UNIQUE,
    "NameArabic" VARCHAR(100) NOT NULL,
    "NameTurkish" VARCHAR(100) NOT NULL,
    "NameEnglish" VARCHAR(100) NOT NULL,
    "AyahCount" INT NOT NULL,
    "StartPage" INT NOT NULL,
    "JuzNumber" INT NOT NULL
);

CREATE TABLE IF NOT EXISTS "Juzs" (
    "Id" SERIAL PRIMARY KEY,
    "JuzNumber" INT NOT NULL UNIQUE,
    "StartPage" INT NOT NULL,
    "EndPage" INT NOT NULL
);

-- ============================================
-- NOBET PROGRAMI TABLOLARI
-- ============================================

CREATE TABLE IF NOT EXISTS "DutyScheduleConstraints" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "Name" VARCHAR(200) NOT NULL,
    "AssistantWeeklyPattern" TEXT,
    "TeachersPerDay" INT NOT NULL DEFAULT 3,
    "AssistantsPerDay" INT NOT NULL DEFAULT 3,
    "MaxConsecutiveDays" INT NOT NULL DEFAULT 3,
    "EveningShiftUserIds" TEXT,
    "FixedDayAssignments" TEXT,
    "ExcludedDates" TEXT,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "DutyScheduleWeeks" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "ConstraintId" INT NOT NULL REFERENCES "DutyScheduleConstraints"("Id"),
    "WeekStartDate" DATE NOT NULL,
    "Status" VARCHAR(50) NOT NULL DEFAULT 'Draft',
    "CreatedByUserId" INT NOT NULL REFERENCES "Users"("Id"),
    "ApprovedAt" TIMESTAMP,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "DutyScheduleEntries" (
    "Id" SERIAL PRIMARY KEY,
    "WeekId" INT NOT NULL REFERENCES "DutyScheduleWeeks"("Id"),
    "DayOfWeek" INT NOT NULL,
    "UserId" INT NOT NULL REFERENCES "Users"("Id"),
    "UserType" VARCHAR(50) NOT NULL,
    "SlotOrder" INT NOT NULL DEFAULT 1,
    "ClassId" INT REFERENCES "Classes"("Id")
);

CREATE TABLE IF NOT EXISTS "TeacherStudentAssignments" (
    "Id" SERIAL PRIMARY KEY,
    "TenantId" INT NOT NULL REFERENCES "Tenants"("Id"),
    "TeacherUserId" INT NOT NULL REFERENCES "Users"("Id"),
    "StudentId" INT NOT NULL REFERENCES "Students"("Id"),
    "AssignmentType" VARCHAR(50) NOT NULL DEFAULT 'Official',
    "StartDate" DATE NOT NULL,
    "EndDate" DATE,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE
);

-- ============================================
-- INDEXLER
-- ============================================

CREATE INDEX IF NOT EXISTS "IX_Students_TenantId" ON "Students"("TenantId");
CREATE INDEX IF NOT EXISTS "IX_Lessons_TenantId" ON "Lessons"("TenantId");
CREATE INDEX IF NOT EXISTS "IX_Lessons_StudentId" ON "Lessons"("StudentId");
CREATE INDEX IF NOT EXISTS "IX_Mistakes_LessonId" ON "Mistakes"("LessonId");
CREATE INDEX IF NOT EXISTS "IX_Homeworks_StudentId" ON "Homeworks"("StudentId");
CREATE INDEX IF NOT EXISTS "IX_Attendances_StudentId" ON "Attendances"("StudentId");
CREATE INDEX IF NOT EXISTS "IX_DutyScheduleEntries_WeekId" ON "DutyScheduleEntries"("WeekId");
CREATE INDEX IF NOT EXISTS "IX_TeacherStudentAssignments_TeacherUserId" ON "TeacherStudentAssignments"("TeacherUserId");

-- ============================================
-- SEED DATA: Roller
-- ============================================

INSERT INTO "OperationClaims" ("Name") VALUES
    ('developer'),
    ('admin'),
    ('yonetici'),
    ('ogretici'),
    ('yardimci_ogretici'),
    ('veli'),
    ('ogrenci')
ON CONFLICT ("Name") DO NOTHING;

-- ============================================
-- SEED DATA: Cuzler (30 cuz)
-- ============================================

INSERT INTO "Juzs" ("JuzNumber", "StartPage", "EndPage") VALUES
    (1, 1, 21), (2, 22, 41), (3, 42, 61), (4, 62, 81), (5, 82, 101),
    (6, 102, 121), (7, 122, 141), (8, 142, 161), (9, 162, 181), (10, 182, 201),
    (11, 202, 221), (12, 222, 241), (13, 242, 261), (14, 262, 281), (15, 282, 301),
    (16, 302, 321), (17, 322, 341), (18, 342, 361), (19, 362, 381), (20, 382, 401),
    (21, 402, 421), (22, 422, 441), (23, 442, 461), (24, 462, 481), (25, 482, 501),
    (26, 502, 521), (27, 522, 541), (28, 542, 561), (29, 562, 581), (30, 582, 604)
ON CONFLICT ("JuzNumber") DO NOTHING;
