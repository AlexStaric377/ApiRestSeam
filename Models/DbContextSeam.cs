using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppRestSeam;

/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com

namespace AppRestSeam.Models
{
    public class DbContextSeam : DbContext
    {

        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Detailing> Detailings { get; set; }
        public DbSet<ListGrDetailing> ListGrDetailings { get; set; }
        public DbSet<GrDetailing> GrDetailings { get; set; }
        public DbSet<ListGroupQualification> ListGroupQualifications { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Diagnoz> Diagnozs { get; set; }
        public DbSet<GrupDiagnoz> GrupDiagnozs { get; set; }    
        public DbSet<DependencyDiagnoz> DependencyDiagnozs { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<ContentInterv> ContentIntervs { get; set; }
        public DbSet<Icd> Icds { get; set; }
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorGrDiagnoz> DoctorGrDiagnozs { get; set; }

        public DbSet<ColectionInterview> ColectionInterviews { get; set; }
        public DbSet<CompletedInterview> CompletedInterviews { get; set; }
        public DbSet<MedicalInstitution> MedicalInstitutions { get; set; }
        public DbSet<MedicalGrDiagnoz> MedicalGrDiagnozs { get; set; }
        public DbSet<LanguageUI> LanguageUIs { get; set; }
 
        public DbSet<PacientMapAnaliz> PacientMapAnalizs { get; set; }
        public DbSet<LifePacient> LifePacients { get; set; }
        public DbSet<RegistrationAppointment> RegistrationAppointments { get; set; }
        public DbSet<LifeDoctor> LifeDoctors { get; set; }
        public DbSet<AdmissionPatients> AdmissionPatientss { get; set; }
        public DbSet<VisitingDays> VisitingDayss { get; set; }
        public DbSet<Sob> Sobs { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<NsiStatusUser> NsiStatusUsers { get; set; }
        public DbSet<PacientAnalizKrovi> PacientAnalizKrovis { get; set; }

        public DbSet<PacientAnalizUrine> PacientAnalizUrines { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Price> Prices { get; set; }

        public DbSet<StatusMedZaklad> StatusMedZaklads { get; set; }

        public DbContextSeam(DbContextOptions<DbContextSeam> options)
            : base(options)
        {
            if (AppRestSeam.Program.Upload == "true")Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }

}
