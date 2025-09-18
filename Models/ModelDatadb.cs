using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com

namespace AppRestSeam.Models
{

    // Тип жалобы, 
    public class Complaint
    {

        public int Id { get; set; }
        public string KeyComplaint { get; set; }
        public string Name { get; set; }
        public string IdUser { get; set; } // код пациента или доктора
        //public ICollection<Feature> Features { get; set; }

    }

    // характер,Особеность жалобы 
    public class Feature
    {
        public int Id { get; set; }
        public string KeyComplaint { get; set; }
        public string KeyFeature { get; set; }
        public string Name { get; set; }
        public string IdUser { get; set; } // код пациента или доктора

        //public ICollection<Detailing> Detailings { get; set; }


    }


    // Детализация характера или особености жалобы 
    public class Detailing
    {
        public int Id { get; set; }
        public string KeyFeature { get; set; }
        public string KeyGrDetailing { get; set; }
        public string KodDetailing { get; set; }
        public string NameDetailing { get; set; }
        public string IdUser { get; set; } // код пациента или доктора


    }

    // Список групп детализации 
    public class ListGrDetailing
    {
        public int Id { get; set; }
        public string KeyGrDetailing { get; set; }
        public string NameGrup { get; set; }
        public string IdUser { get; set; } // код пациента или доктора

    }

    // Группы содержащие детализации характеров жалоб 
    public class GrDetailing
    {
        public int Id { get; set; }
        public string KeyGrDetailing { get; set; }
        public string KodDetailing { get; set; }
        public string KodGroupQualification { get; set; }
        public string NameGrDetailing { get; set; }
        public string IdUser { get; set; } // код пациента или доктора

    }

    // Список групп детализации 
    public class ListGroupQualification
    {
        public int Id { get; set; }
        public string KodGroupQualification { get; set; }
        public string NameGroupQualification { get; set; }
        public string IdUser { get; set; } // код пациента или доктора

    }


    // Уточнение класификация детализации данного характера или особености жалобы 
    public class Qualification
    {
        public int Id { get; set; }
        public string KodGroupQualification { get; set; }
        public string KodQualification { get; set; }
        public string NameQualification { get; set; }
        public string IdUser { get; set; } // код пациента или доктора

    }


    // словарь диагонозов 
    public class Diagnoz
    {
        public int Id { get; set; }
        public string KodDiagnoza { get; set; }
        public string NameDiagnoza { get; set; }
        public string OpisDiagnoza { get; set; }
        public string UriDiagnoza { get; set; }
        public string KeyIcd { get; set; } // код Международная классификация болезней 11 пересмотра 
        public string IcdGrDiagnoz { get; set; } // код группы МКХ в которую входит диагноз
        public string IdUser { get; set; } // код пациента или доктора

    }


    // словарь груп активных диагонозов в системе в разрезе МКХ
    public class GrupDiagnoz
    {
        public int Id { get; set; }
        public string IcdGrDiagnoz { get; set; }
        public string NameGrDiagnoz { get; set; }
        public string OpisDiagnoza { get; set; }
        public string UriDiagnoza { get; set; }
        public string IdUser { get; set; } // код пациента или доктора

    }
    // Рекомендации о дальнейших действиях, в том числе, и по адресному обращению для оказания медицинской помощи к врачам - специалистам медицинских учреждений.
    public class Recommendation
    {
        public int Id { get; set; }
        public string KodRecommendation { get; set; }
        public string ContentRecommendation { get; set; }

    }

    // словарь протоколов интревью

    public class Interview
    {
        public int Id { get; set; }
        public string KodProtokola { get; set; }
        public string DetailsInterview { get; set; }
        public string NametInterview { get; set; }
        public string OpistInterview { get; set; }
        public string UriInterview { get; set; }
        public string IdUser { get; set; } // код пациента или доктора
        public string GrDetail { get; set; }

    }
    // Контент  создаваемого или просматриваемого  интревью из справочника
    public class ContentInterv
    {
        public int Id { get; set; }
        public string KodProtokola { get; set; }
        public int Numberstr { get; set; }
        public string KodDetailing { get; set; }
        public string DetailsInterview { get; set; }
        public string IdUser { get; set; } // код пациента или доктора


    }

    // Взаимосвязь между докторами и пациентами 
    //один доктор обслуживает много пациентов
    //один пациент может обслуживаться многоми докторами.
    // Коллекция выполненных диагнозов пациентами или врачами

    // Коллекция проведеных интервью и Взаємозв'язки Пацієнт Лікарь
    public class ColectionInterview
    {
        public int Id { get; set; }
        public string KodDoctor { get; set; }
        public string KodPacient { get; set; }
        public string DateInterview { get; set; }
        public string DateDoctor { get; set; }
        public string KodProtokola { get; set; }
        public string DetailsInterview { get; set; }
        public string ResultDiagnoz { get; set; }
        public string KodComplInterv { get; set; }
        public string NameInterview { get; set; }



    }

    // ПРоведенные   интревью
    public class CompletedInterview
    {
        public int Id { get; set; }
        public string KodComplInterv { get; set; }
        public int Numberstr { get; set; }
        public string KodDetailing { get; set; }
        public string DetailsInterview { get; set; }

    }

    // словарь взаимосвязи диагонозов рекомендаций и протокола формирования диагноза по факту ответов пациента.  
    public class DependencyDiagnoz
    {
        public int Id { get; set; }
        public string KodDiagnoz { get; set; }
        public string KodRecommend { get; set; }
        public string KodProtokola { get; set; }


    }



    // Международная классификация болезней 11 пересмотра МКБ11 
    public class Icd
    {
        public int Id { get; set; }
        public string KeyIcd { get; set; }
        public string Name { get; set; }

    }
    // Пациент
    public class Pacient
    {
        public int Id { get; set; }
        public string KodPacient { get; set; }
        public string KodKabinet { get; set; }
        public int Age { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Weight { get; set; }
        public int Growth { get; set; }
        public string Gender { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pind { get; set; }
        public string Profession { get; set; }

    }


    // Карта различных анализов на заданныю дату
    public class PacientMapAnaliz
    {
        public int Id { get; set; }
        public string KodPacient { get; set; }
        public string DateAnaliza { get; set; }
        public string Pulse { get; set; }   // пульс
        public string Pressure { get; set; } // давление
        public string Temperature { get; set; } // температура
        public string ResultAnaliza { get; set; }

    }

    //  анализ крови на заданныю дату
    public class PacientAnalizKrovi
    {
        public int Id { get; set; }
        public string KodPacient { get; set; }
        public string DateAnaliza { get; set; }
        public string Gender { get; set; }
        public string Rbc { get; set; }   // Эритроциты
        public string Hgb { get; set; } // Гемоглобин
        public string Wbc { get; set; } // Лейкоциты
        public string Cp { get; set; }   // Цветовой показатель
        public string Hct { get; set; } // Гематокрит
        public string Ret { get; set; } // Ретикулоциты
        public string Plt { get; set; }   // Тромбоциты
        public string Esr { get; set; } // СОЭ
        public string Bas { get; set; } // Базофилы
        public string Eo { get; set; }   // Эозинофилы
        public string Mot { get; set; } // Миелоциты
        public string Mtmot { get; set; } // Метамиелоциты
        public string Neutp { get; set; }   // Нейтрофилы палочкоядерные
        public string Neuts { get; set; } // Нейтрофилы сегментоядерные
        public string Lym { get; set; } // Лимфоциты
        public string Mon { get; set; }   // Моноциты


    }



    //  анализ мочи на заданныю дату
    public class PacientAnalizUrine
    {
        public int Id { get; set; }
        public string KodPacient { get; set; }
        public string DateAnaliza { get; set; }
        public string Color { get; set; }   // цвет
        public string Ph { get; set; } // кислотность
        public string Sg { get; set; } // плотность
        public string Pro { get; set; }   // белок
        public string Glu { get; set; } //глюкоза
        public string Bil { get; set; } // билирубин
        public string Uro { get; set; }   // уробилиноген
        public string Ket { get; set; } // кетоновые тела
        public string Bld { get; set; } // эритроциты
        public string Leu { get; set; }   // лейкоциты
        public string Nit { get; set; } // соли

    }

    // Жизнь пациента и взаимодействие с врачами
    // 
    public class LifePacient
    {
        public int Id { get; set; }
        public string KodPacient { get; set; }
        public string KodDoctor { get; set; }
        public string DateInterview { get; set; }
        public string KodComplInterv { get; set; }
        public string KodProtokola { get; set; }
        public string TopictVizita { get; set; }
    }
    // Список пациентов записавшихся на прием средством СЕАМ
    public class RegistrationAppointment
    {
        public int Id { get; set; }
        public string KodPacient { get; set; }
        public string KodDoctor { get; set; }
        public string DateDoctor { get; set; }
        public string DateInterview { get; set; }
        public string KodComplInterv { get; set; }
        public string KodProtokola { get; set; }
        public string TopictVizita { get; set; }
        public string KodDiagnoz { get; set; }
    }

    // Врачи
    public class Doctor
    {
        public int Id { get; set; }
        public string KodDoctor { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Edrpou { get; set; }
        public string Specialnoct { get; set; }
        public string Napryamok { get; set; }
        public string UriwebDoctor { get; set; }

    }

    // справочник груп диагнозов которые обслуживает доктор
    public class DoctorGrDiagnoz
    {
        public int Id { get; set; }
        public string KodDoctor { get; set; }
        public string IcdGrDiagnoz { get; set; } // код группы МКХ в которую входит диагноз
    }


    // Статистика список  проведенных  приемов пациентов
    public class LifeDoctor
    {
        public int Id { get; set; }
        public string KodDoctor { get; set; }
        public string KodPacient { get; set; }
        public string DateVizita { get; set; }
        public string DateInterview { get; set; }
        public string KodComplInterv { get; set; }
        public string KodProtokola { get; set; }
        public string ResultVizita { get; set; }

    }
    // список приемов пациентов записавшихся на прием средством СЕАМ 
    public class AdmissionPatients
    {
        public int Id { get; set; }
        public string KodDoctor { get; set; }
        public string KodPacient { get; set; }
        public string DateVizita { get; set; }
        public string DateInterview { get; set; }
        public string KodComplInterv { get; set; }
        public string KodProtokola { get; set; }
        public string TopictVizita { get; set; }
        public string KodDiagnoz { get; set; }

    }

    // График приемных дней недели и времени доктора

    public class VisitingDays
    {
        public int Id { get; set; }
        public string KodDoctor { get; set; }
        public string DaysOfTheWeek { get; set; }
        public string DateVizita { get; set; }
        public string TimeVizita { get; set; }
        public string OnOff { get; set; }
        public DateTime DateWork { get; set; }
    }

    // Медицинское учреждение содержит смылку на таблицу докторов работающих в данном учрежлении

    public class MedicalInstitution
    {

        public int Id { get; set; }
        public string Edrpou { get; set; }
        public string Name { get; set; }
        public string PostIndex { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string UriwebZaklad { get; set; }
        public string KodObl { get; set; }
        public string IdStatus { get; set; }
        public string KodZaklad { get; set; }

    }
    // справочник груп диагнозов которые обслуживает мед учреждение
    public class MedicalGrDiagnoz
    {
        public int Id { get; set; }
        public string Edrpou { get; set; }
        public string IcdGrDiagnoz { get; set; } // код группы МКХ в которую входит диагноз
        public string IcdKey { get; set; } // код группы МКХ в которую входит диагноз
        public string KodZaklad { get; set; }
    }

    // Язык интерфейса

    public class LanguageUI
    {
        public int Id { get; set; }
        public string KeyLang { get; set; }
        public string Name { get; set; }

    }

    // Справочник областей
    public class Sob
    {
        public int Id { get; set; }
        public string KodObl { get; set; }
        public string NameObl { get; set; }
        public string NameRajon { get; set; }
        public string Namepunkt { get; set; }
        public int Piple  { get; set; }
        public string Pind { get; set; }

    }

    // справочник пользователей стстемы
    public class AccountUser
    {
        public int Id { get; set; }
        public string IdUser { get; set; } // код пациента или доктора
        public string IdStatus { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string AccountCreatDate { get; set; }
        public string Subscription { get; set; }

    }

    // справочник статуса пользователей стстемы
    public class NsiStatusUser
    {

        public int Id { get; set; }
        public string IdStatus { get; set; }
        public string StatusUser { get; set; }
        public string NameStatus { get; set; }
        public string KodDostupa { get; set; }

    }

    // таблица текущих оплат по подписке в соответствии с прайсом на указанное количество дней
    public class Payment
    {
        public int Id { get; set; }
        public string KeyClient { get; set; } // код пациента или доктора
        public string DatePayment { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        public decimal Suma { get; set; }
        public string KeyPrice { get; set; }
        public string Telefon { get; set; }
    }

    // справочник тарифов на предоставляемые услуги по диагностике в личных кабинетах
    public class Price
    {
        public int Id { get; set; }
        public string KeyPrice { get; set; }
        public int QuantityDays { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        public decimal PriceQuantity { get; set; }
        public string NamePrice { get; set; }
    }

    // справочник статуса пользователей стстемы
    public class StatusMedZaklad
    {

        public int Id { get; set; }
        public string IdStatus { get; set; }
        public string NameStatus { get; set; }
        public string TypeStatus { get; set; }

    }

}
