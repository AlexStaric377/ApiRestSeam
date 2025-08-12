using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRestSeam.Models;
using Microsoft.EntityFrameworkCore;


/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com

namespace AppRestSeam.Controllers
{
    public class StartLoadDb
    {
        DbContextSeam db;

        public void AddDb(DbContextSeam context)
        {
            db = context;
            if (!db.Complaints.Any())
            {
                // Тип жалобы, 
                db.Complaints.Add(new  Complaint { Name = "ніс" , KeyComplaint ="A.000", IdUser = "Admin"});
                db.Complaints.Add(new  Complaint { Name = "горло", KeyComplaint = "B.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "очі", KeyComplaint = "C.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "бронхи та легені", KeyComplaint = "D.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "шкіра", KeyComplaint = "E.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "серце", KeyComplaint = "F.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "біль в животі", KeyComplaint = "G.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "нирки та сечовивідна система", KeyComplaint = "H.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "статеві органи", KeyComplaint = "I.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "суглоби", KeyComplaint = "J.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "голова", KeyComplaint = "K.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "вуха", KeyComplaint = "L.000" ,IdUser="Admin"});
                db.Complaints.Add(new  Complaint { Name = "інші скарги", KeyComplaint = "M.000" ,IdUser="Admin"});
                db.SaveChanges();
            }

            if (!db.Features.Any())
            {
                // Особеность, характер жалобы, 
                // 1.	Проблемы с носом
                db.Features.Add(new  Feature { Name = "закладеність носа", KeyFeature = "A.000.001", KeyComplaint = "A.000" ,IdUser="Admin"});                              //1 : 1
                db.Features.Add(new  Feature { Name = "чихання", KeyFeature = "A.000.002", KeyComplaint = "A.000" ,IdUser="Admin"});                                         //2
                db.Features.Add(new  Feature { Name = "свербіння в носі", KeyFeature = "A.000.003", KeyComplaint = "A.000" ,IdUser="Admin"});                               //3
                db.Features.Add(new  Feature { Name = "виділення з носа", KeyFeature = "A.000.004", KeyComplaint = "A.000" ,IdUser="Admin"});                               //4
                db.Features.Add(new  Feature { Name = "порушення нюху", KeyFeature = "A.000.005", KeyComplaint = "A.000" ,IdUser="Admin"});                                 //5
                db.Features.Add(new  Feature { Name = "температура", KeyFeature = "A.000.006", KeyComplaint = "A.000" ,IdUser="Admin"});                                    //6
                db.Features.Add(new  Feature { Name = "біль у носі та/або приносових пазухах", KeyFeature = "A.000.007", KeyComplaint = "A.000" ,IdUser="Admin"});          //7
                db.Features.Add(new  Feature { Name = "тривалість проблеми", KeyFeature = "A.000.008", KeyComplaint = "A.000" ,IdUser="Admin"});                            //8
                db.Features.Add(new  Feature { Name = "сезонність", KeyFeature = "A.000.009", KeyComplaint = "A.000" ,IdUser="Admin"});                                     //9
                db.Features.Add(new  Feature { Name = "подібні проблеми у найближчих кровних родичів", KeyFeature = "A.000.010", KeyComplaint = "A.000" ,IdUser="Admin"});  //10
                db.Features.Add(new  Feature { Name = "поліпшення стану", KeyFeature = "A.000.011", KeyComplaint = "A.000" ,IdUser="Admin"});                               //11
                db.Features.Add(new  Feature { Name = "погіршення стану", KeyFeature = "A.000.012", KeyComplaint = "A.000" ,IdUser="Admin"});                               //12
                db.Features.Add(new  Feature { Name = "інші скарги", KeyFeature = "A.000.013", KeyComplaint = "A.000" ,IdUser="Admin"});                                    //13

                // 2.	Проблемы с горлом
                db.Features.Add(new  Feature { Name = "біль у горлі", KeyFeature = "B.000.001", KeyComplaint = "B.000" ,IdUser="Admin"});                                   //14
                db.Features.Add(new  Feature { Name = "кашель", KeyFeature = "B.000.002", KeyComplaint = "B.000" ,IdUser="Admin"});                                         //15
                db.Features.Add(new  Feature { Name = "свербіж у горлі та у роті", KeyFeature = "B.000.003", KeyComplaint = "B.000" ,IdUser="Admin"});                      //16
                db.Features.Add(new  Feature { Name = "почуття кома у горлі", KeyFeature = "B.000.004", KeyComplaint = "B.000" ,IdUser="Admin"});                           //17
                db.Features.Add(new  Feature { Name = "першіння у горлі", KeyFeature = "B.000.005", KeyComplaint = "B.000" ,IdUser="Admin"});                               //18
                db.Features.Add(new  Feature { Name = "стікання слизу з носа по задній стінці горла", KeyFeature = "B.000.006", KeyComplaint = "B.000" ,IdUser="Admin"});   //19
                db.Features.Add(new  Feature { Name = "температура", KeyFeature = "B.000.007", KeyComplaint = "B.000" ,IdUser="Admin"});                                    //20
                db.Features.Add(new  Feature { Name = "тривалість проблеми", KeyFeature = "B.000.008", KeyComplaint = "B.000" ,IdUser="Admin"});                            //21
                db.Features.Add(new  Feature { Name = "сезонність", KeyFeature = "B.000.009", KeyComplaint = "B.000" ,IdUser="Admin"});                                     //22
                db.Features.Add(new  Feature { Name = "подібні проблеми у найближчих кровних родичів", KeyFeature = "B.000.010", KeyComplaint = "B.000" ,IdUser="Admin"});  //23
                db.Features.Add(new  Feature { Name = "погіршення після", KeyFeature = "B.000.011", KeyComplaint = "B.000" ,IdUser="Admin"});                               //24                               //24
                db.Features.Add(new  Feature { Name = "поліпшення після", KeyFeature = "B.000.012", KeyComplaint = "B.000" ,IdUser="Admin"});                               //25
                db.Features.Add(new  Feature { Name = "інші скарги", KeyFeature = "B.000.013", KeyComplaint = "B.000" ,IdUser="Admin"});                                    //26

                //3.	Проблемы с глазами
                db.Features.Add(new  Feature { Name = "свербіж очей", KeyFeature = "C.000.001", KeyComplaint = "C.000" ,IdUser="Admin"});                                   //27
                db.Features.Add(new  Feature { Name = "виділення з очей ", KeyFeature = "C.000.002", KeyComplaint = "C.000" ,IdUser="Admin"});                              //28
                db.Features.Add(new  Feature { Name = "почервоніння слизової очей", KeyFeature = "C.000.003", KeyComplaint = "C.000" ,IdUser="Admin"});                     //29
                db.Features.Add(new  Feature { Name = "порушення зору", KeyFeature = "C.000.004", KeyComplaint = "C.000" ,IdUser="Admin"});                                 //30
                db.Features.Add(new  Feature { Name = "погіршення проблеми з очима", KeyFeature = "C.000.005", KeyComplaint = "C.000" ,IdUser="Admin"});                    //31

                // 4.	Проблемы с бронхами и легкими
                db.Features.Add(new  Feature { Name = "кашель", KeyFeature = "D.000.001", KeyComplaint = "D.000" ,IdUser="Admin"});                                         //32
                db.Features.Add(new  Feature { Name = "труднощі з диханням", KeyFeature = "D.000.002", KeyComplaint = "D.000" ,IdUser="Admin"});                            //33
                db.Features.Add(new  Feature { Name = "мокрота", KeyFeature = "D.000.003", KeyComplaint = "D.000" ,IdUser="Admin"});                                        //34
                db.Features.Add(new  Feature { Name = "біль за грудиною", KeyFeature = "D.000.004", KeyComplaint = "D.000" ,IdUser="Admin"});                               //35
                db.Features.Add(new  Feature { Name = "температура", KeyFeature = "D.000.005", KeyComplaint = "D.000" ,IdUser="Admin"});                                    //36
                db.Features.Add(new  Feature { Name = "погіршення проблеми із бронхами", KeyFeature = "D.000.006", KeyComplaint = "D.000" ,IdUser="Admin"});                //37
                db.Features.Add(new  Feature { Name = "поліпшення проблеми із бронхами", KeyFeature = "D.000.007", KeyComplaint = "D.000" ,IdUser="Admin"});                //38
                db.Features.Add(new  Feature { Name = "тривалість проблеми із бронхами", KeyFeature = "D.000.008", KeyComplaint = "D.000" ,IdUser="Admin"});                //39

                // 5.	Проблемы с кожей
                db.Features.Add(new  Feature { Name = "Висипання локалізація", KeyFeature = "E.000.001", KeyComplaint = "E.000" ,IdUser="Admin"});                          //40

                // 7. Біль у жовоті
                db.Features.Add(new Feature { Name = "біль між нижнім краєм грудної клітки та пахом", KeyFeature = "G.000.001", KeyComplaint = "G.000" ,IdUser="Admin"});
                db.Features.Add(new Feature { Name = "біль тільки над талією", KeyFeature = "G.000.002", KeyComplaint = "G.000" ,IdUser="Admin"});
                db.Features.Add(new Feature { Name = "епізодично виникаюча пекуча біль у центрі грудей, особливо при нахилу вперед", KeyFeature = "G.000.003", KeyComplaint = "G.000" ,IdUser="Admin"});
                db.Features.Add(new Feature { Name = "біль розповсюджується від підборіддя вправий бік", KeyFeature = "G.000.004", KeyComplaint = "G.000" ,IdUser="Admin"});
                db.Features.Add(new Feature { Name = "біль в основному нижче пупка", KeyFeature = "G.000.005", KeyComplaint = "G.000" ,IdUser="Admin"});
                db.Features.Add(new Feature { Name = "біль у попереку переміщується в пах", KeyFeature = "G.000.006", KeyComplaint = "G.000" ,IdUser="Admin"});
                db.Features.Add(new Feature { Name = "біль тільки нижче талії", KeyFeature = "G.000.007", KeyComplaint = "G.000" ,IdUser="Admin"});
                db.Features.Add(new Feature { Name = "біль у попереку з однієї із сторін над самою талією", KeyFeature = "G.000.008", KeyComplaint = "G.000" ,IdUser="Admin"});
                db.SaveChanges();

            }



            // Детализация особености жалобы 

            if (!db.Detailings.Any())
            {
                // Детализация особености  1.1.заложенность носа
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за часом", KeyGrDetailing = "YAA.000", KodDetailing= "A.000.001.001", KeyFeature = "A.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KeyGrDetailing = "YBB.000", KodDetailing = "A.000.001.002", KeyFeature = "A.000.001" ,IdUser="Admin"});

                // Детализация особености  1.2.чихание
                db.Detailings.Add(new  Detailing { NameDetailing = "епізодичне", KodDetailing = "A.000.002.001", KeyFeature = "A.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "нападоподібне", KodDetailing = "A.000.002.002", KeyFeature = "A.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за часом", KodDetailing = "A.000.002.003", KeyGrDetailing = "YAA.000", KeyFeature = "A.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "A.000.002.004", KeyGrDetailing = "YBB.000", KeyFeature = "A.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація кашель", KodDetailing = "A.000.002.005", KeyGrDetailing = "YEE.000", KeyFeature = "A.000.002" ,IdUser="Admin"});
                // Детализация особености  1.3.зуд в носу
                db.Detailings.Add(new  Detailing { NameDetailing = "епізодичний", KodDetailing = "A.000.003.001", KeyFeature = "A.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "постійний", KodDetailing = "A.000.003.002", KeyFeature = "A.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "A.000.003.003", KeyGrDetailing = "YBB.000", KeyFeature = "A.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація за часом", KodDetailing = "A.000.002.003", KeyGrDetailing = "YAA.000", KeyFeature = "A.000.002" ,IdUser="Admin"});

                // Детализация особености  1.4.выделения из носа
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація виділення", KodDetailing = "A.000.004.001", KeyGrDetailing = "YDD.000", KeyFeature = "A.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "с прожилками крови", KodDetailing = "A.000.004.002", KeyFeature = "A.000.004" ,IdUser="Admin"}); //9
                db.Detailings.Add(new  Detailing { NameDetailing = "без крови", KodDetailing = "A.000.004.003", KeyFeature = "A.000.004" ,IdUser="Admin"}); //10
                db.Detailings.Add(new  Detailing { NameDetailing = "відчуття стікання слизи на задній стінці глотки", KodDetailing = "A.000.004.004", KeyFeature = "A.000.004" ,IdUser="Admin"}); //11
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за часом", KodDetailing = "A.000.004.005", KeyGrDetailing = "YAA.000", KeyFeature = "A.000.004" ,IdUser="Admin"}); //12
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "A.000.004.006", KeyGrDetailing = "YBB.000", KeyFeature = "A.000.004" ,IdUser="Admin"}); //13

                // Детализация особености 1.5.нарушение обоняния
                db.Detailings.Add(new  Detailing { NameDetailing = "повне", KodDetailing = "A.000.005.001", KeyFeature = "A.000.005" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "часткове", KodDetailing = "A.000.005.002", KeyFeature = "A.000.005" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "у басейні", KodDetailing = "A.000.005.003", KeyFeature = "A.000.005" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "періодично покращується", KodDetailing = "A.000.005.004", KeyFeature = "A.000.005" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за часом", KodDetailing = "A.000.005.005", KeyGrDetailing = "YAA.000", KeyFeature = "A.000.005" ,IdUser="Admin"});

                // Детализация особености 1.6.температура
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "A.000.006.001", KeyGrDetailing = "YCC.000", KeyFeature = "A.000.006" ,IdUser="Admin"});

                //1.7.боль в носу и / или околоносовых пазухах (7)
                //1.8.длительность процесса (8)
                //1.9.сезонность (9)
                //1.10.подобные проблемы у ближайших кровных родственников (10)
                // Детализация особености 1.11.улучшение состояния
                db.Detailings.Add(new  Detailing { NameDetailing = "при прийомі протиалергічних пігулок", KodDetailing = "A.000.011.001", KeyFeature = "A.000.011" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "при прийомі парацетамолу", KodDetailing = "A.000.011.002", KeyFeature = "A.000.011" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "при застосуванні судинозвужувальних крапель у ніс", KodDetailing = "A.000.011.003", KeyFeature = "A.000.011" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "на вулиці", KodDetailing = "A.000.011.004", KeyFeature = "A.000.011" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "в приміщенні", KodDetailing = "A.000.011.005", KeyFeature = "A.000.011" ,IdUser="Admin"});

                // Детализация особености 1.12.ухудшение состояния
                db.Detailings.Add(new Detailing { NameDetailing = "при прийомі противоалергічних пігулок", KodDetailing = "A.000.012.001", KeyGrDetailing = "AA.000", KeyFeature = "B.000.012" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "при прийомі парацетомолу", KodDetailing = "A.000.012.002", KeyGrDetailing = "AA.000", KeyFeature = "B.000.012" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "при прийомі судинозвужуючих капель у ніс", KodDetailing = "A.000.012.003", KeyGrDetailing = "YBB.000", KeyFeature = "B.000.012" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за часом", KodDetailing = "A.000.012.004", KeyGrDetailing = "YAA.000", KeyFeature = "A.000.012" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "A.000.012.005", KeyGrDetailing = "YBB.000", KeyFeature = "A.000.012" ,IdUser="Admin"});

                // Детализация особености 1.13.другие жалобы
                db.Detailings.Add(new  Detailing { NameDetailing = "біль у вухах", KodDetailing = "A.000.013.001", KeyFeature = "A.000.013" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "біль в одному вусі", KodDetailing = "A.000.013.002", KeyFeature = "A.000.013" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "закладеність у вухах", KodDetailing = "A.000.013.003", KeyFeature = "A.000.013" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "закладеність в одному вусі", KodDetailing = "A.000.013.004", KeyFeature = "A.000.013" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "відчуття наявності рани в носі", KodDetailing = "A.000.013.005", KeyFeature = "A.000.013" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "головний біль у області чола", KodDetailing = "A.000.013.006", KeyFeature = "A.000.013" ,IdUser="Admin"});


                //   2.Проблемы с горлом
                // Детализация особености 2.1.боль в горле (14)
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація біль у горлі", KodDetailing = "B.000.001.001", KeyGrDetailing = "YFF.000", KeyFeature = "B.000.001" ,IdUser="Admin"});

                // Детализация особености 2.2.кашель (15)
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація кашель", KodDetailing = "B.000.002.001", KeyGrDetailing = "YEE.000", KeyFeature = "B.000.002" ,IdUser="Admin"});
  

                //2.3.зуд в горле и во рту (16)
                //2.4.чувство кома в горле (17)
                //2.5.першение в горле (18)
                //2.6.стекание слизи из носа по задней стенке горла (19)
                //2.7.Детализация температура
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "D.000.005.001", KeyGrDetailing = "YCC.000", KeyFeature = "D.000.005" ,IdUser="Admin"});

                //2.8.длительность проблемы (21)
                //2.9.сезонность (22)
                //2.10.подобные проблемы у ближайших кровных родственников (23)
                db.Detailings.Add(new Detailing { NameDetailing = "подібні проблеми у найближчих кровних родичів", KodDetailing = "B.000.010.001",  KeyFeature = "B.000.010" ,IdUser="Admin"});
                //2.11.Детализация ухудшение состояния  после (24)
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за часом", KodDetailing = "B.000.011.001", KeyGrDetailing = "YAA.000", KeyFeature = "B.000.011" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "B.000.011.002", KeyGrDetailing = "YBB.000", KeyFeature = "B.000.011" ,IdUser="Admin"});

                //2.10.Детализация улучшение состояния после (25)

                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за часом", KodDetailing = "B.000.012.004", KeyGrDetailing = "YAA.000", KeyFeature = "B.000.012" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "B.000.012.005", KeyGrDetailing = "YBB.000", KeyFeature = "B.000.012" ,IdUser="Admin"});

                //2.11.другие жалобы (26)


                //3.Проблемы с глазами
                //3.1.Зуд (27)
                // Детализация особености 3.2.Выделения из глаз (28)
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація виділення", KodDetailing = "C.000.002.001", KeyGrDetailing = "YDD.000", KeyFeature = "C.000.002" ,IdUser="Admin"});
              

                //3.3.Покраснение слизистой  глаз (29)
                // Детализация особености 3.4.Нарушение зрения (30)
                db.Detailings.Add(new  Detailing { NameDetailing = "1нед", KodDetailing = "C.000.004.001", KeyFeature = "C.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "1міс", KodDetailing = "C.000.004.002", KeyFeature = "C.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "6міс", KodDetailing = "C.000.004.003", KeyFeature = "C.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "більше 1 року", KodDetailing = "C.000.004.004", KeyFeature = "C.000.004" ,IdUser="Admin"});

                // Детализация особености 3.5.Ухудшение проблемы с глазами (31)
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "C.000.005.001", KeyGrDetailing = "YBB.000", KeyFeature = "C.000.005" ,IdUser="Admin"});


                //  4.Проблемы с бронхами и легкими 
                // Детализация особености 4.1.Кашель (32)
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація кашель", KodDetailing = "D.000.001.001", KeyGrDetailing = "YEE.000", KeyFeature = "D.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "D.000.001.002", KeyGrDetailing = "YBB.000", KeyFeature = "D.000.001" ,IdUser="Admin"});

                // Детализация особености 4.2.Трудности с дыханием (33)
                db.Detailings.Add(new  Detailing { NameDetailing = "важко вдихнути", KodDetailing = "D.000.002.001", KeyFeature = "D.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "важко видихнути", KodDetailing = "D.000.002.002", KeyFeature = "D.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "хочеться зайвий раз вдихнути", KodDetailing = "D.000.002.003", KeyFeature = "D.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "відчуття нестачі повітря", KodDetailing = "D.000.002.004", KeyFeature = "D.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "свист у грудях при диханні", KodDetailing = "D.000.002.005", KeyFeature = "D.000.002" ,IdUser="Admin"});

                // Детализация особености 4.3.Мокрота (34)
                db.Detailings.Add(new  Detailing { NameDetailing = "рідка", KodDetailing = "D.000.003.001", KeyFeature = "D.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "густа", KodDetailing = "D.000.003.002", KeyFeature = "D.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "в'язка", KodDetailing = "D.000.003.003", KeyFeature = "D.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "легко відходить", KodDetailing = "D.000.003.004", KeyFeature = "D.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "неможливо відкашлювати", KodDetailing = "D.000.003.005", KeyFeature = "D.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "світла", KodDetailing = "D.000.003.006", KeyFeature = "D.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "жовта", KodDetailing = "D.000.003.007", KeyFeature = "D.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "зелена", KodDetailing = "D.000.003.008", KeyFeature = "D.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "з неприємним запахом", KodDetailing = "D.000.003.009", KeyFeature = "D.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "без запаху", KodDetailing = "D.000.003.010", KeyFeature = "D.000.003" ,IdUser="Admin"});

                // Детализация особености  4.4.Боль за грудиной (35)
                db.Detailings.Add(new  Detailing { NameDetailing = "ниюча", KodDetailing = "D.000.004.001", KeyFeature = "D.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "гостра", KodDetailing = "D.000.004.002", KeyFeature = "D.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "давить", KodDetailing = "D.000.004.003", KeyFeature = "D.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "тупа", KodDetailing = "D.000.004.004", KeyFeature = "D.000.004" ,IdUser="Admin"});

                //4.5.Детализация особености Температура (36)
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "D.000.005.001", KeyGrDetailing = "YCC.000", KeyFeature = "D.000.005" ,IdUser="Admin"});

                // Детализация особености  4.6.Ухудшение проблемы с бронхами (37)
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за часом", KodDetailing = "D.000.006.001", KeyGrDetailing = "YAA.000", KeyFeature = "D.000.006" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "D.000.006.002", KeyGrDetailing = "YBB.000", KeyFeature = "D.000.006" ,IdUser="Admin"});

                // 4.7. Детализация особености Улучшение проблемы с бронхами (38)
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація за часом", KodDetailing = "D.000.007.001", KeyGrDetailing = "AA.000", KeyFeature = "D.000.007" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "Деталізація ситуативна", KodDetailing = "D.000.007.002", KeyGrDetailing = "YBB.000", KeyFeature = "D.000.007" ,IdUser="Admin"});

                //4.8.Длительность проблемы с бронхами (39)
                db.Detailings.Add(new  Detailing { NameDetailing = "1нед", KodDetailing = "D.000.008.001", KeyFeature = "D.000.008" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "1міс", KodDetailing = "D.000.008.002", KeyFeature = "D.000.008" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "6міс", KodDetailing = "D.000.008.003", KeyFeature = "D.000.008" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "більше 1 року", KodDetailing = "D.000.008.004", KeyFeature = "D.000.008" ,IdUser="Admin"});


                //5.Проблемы с кожей 
                // Детализация особености 5.1.Высыпания локализация (40)
                db.Detailings.Add(new  Detailing { NameDetailing = "у згинах рук", KodDetailing = "E.000.001.001", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "у згинах ніг", KodDetailing = "E.000.001.002", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "на шиї", KodDetailing = "E.000.001.003", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "на животі", KodDetailing = "E.000.001.004", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "на спині", KodDetailing = "E.000.001.005", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "на обличі", KodDetailing = "E.000.001.006", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "навколо рота", KodDetailing = "E.000.001.007", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "навколо вік", KodDetailing = "E.000.001.008", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "на волосистій частині голови", KodDetailing = "E.000.001.009", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "у паховій області", KodDetailing = "E.000.001.010", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "між пальцями", KodDetailing = "E.000.001.011", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "в іншій області", KodDetailing = "E.000.001.012", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "без сверблячки", KodDetailing = "E.000.001.013", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "сверблячі", KodDetailing = "E.000.001.014", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "погіршення навесні", KodDetailing = "E.000.001.015", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "погіршення влітку", KodDetailing = "E.000.001.016", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "погіршення восени", KodDetailing = "E.000.001.017", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "погіршення взимку", KodDetailing = "E.000.001.018", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "покращення навесні", KodDetailing = "E.000.001.019", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "покращення влітку", KodDetailing = "E.000.001.020", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "покращення восени", KodDetailing = "E.000.001.021", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "покращення взимку", KodDetailing = "E.000.001.022", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "після контакту з водою", KodDetailing = "E.000.001.023", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "після вживання солодкого", KodDetailing = "E.000.001.024", KeyFeature = "E.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new  Detailing { NameDetailing = "інше", KodDetailing = "E.000.001.025", KeyFeature = "E.000.001" ,IdUser="Admin"});


                // 7. Біль у жовоті
                // Детализация особености 7.1 біль між нижнім краєм грудної клітки та пахом KeyFeature = "G.000.001"
         
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація часу дії болі", KodDetailing = "G.000.001.001", KeyGrDetailing = "YGG.000", KeyFeature = "G.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація характеру зовнішніх проявів", KodDetailing = "G.000.001.002", KeyGrDetailing = "YHH.000", KeyFeature = "G.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при сечовипусканні", KodDetailing = "G.000.001.003", KeyGrDetailing = "YII.000", KeyFeature = "G.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при блювотині", KodDetailing = "G.000.001.004", KeyGrDetailing = "YJJ.000", KeyFeature = "G.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при проносі або запорі", KodDetailing = "G.000.001.005", KeyGrDetailing = "YKK.000", KeyFeature = "G.000.001" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "G.000.001.006", KeyGrDetailing = "YCC.000", KeyFeature = "G.000.001" ,IdUser="Admin"});
                // Детализация особености 7.2 біль тільки над талією KeyFeature = "G.000.002"

                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація часу дії болі", KodDetailing = "G.000.002.001", KeyGrDetailing = "YGG.000", KeyFeature = "G.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація характеру зовнішніх проявів", KodDetailing = "G.000.002.002", KeyGrDetailing = "YHH.000", KeyFeature = "G.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при сечовипусканні", KodDetailing = "G.000.002.003", KeyGrDetailing = "YII.000", KeyFeature = "G.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при блювотині", KodDetailing = "G.000.002.004", KeyGrDetailing = "YJJ.000", KeyFeature = "G.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при проносі або запорі", KodDetailing = "G.000.002.005", KeyGrDetailing = "YKK.000", KeyFeature = "G.000.002" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "G.000.002.006", KeyGrDetailing = "YCC.000", KeyFeature = "G.000.002" ,IdUser="Admin"});

                // Детализация особености 7.3 епізодично виникаюча пекуча біль у центрі грудей, особливо при нахилу вперед KeyFeature = "G.000.003" 

                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація часу дії болі", KodDetailing = "G.000.003.001", KeyGrDetailing = "YGG.000", KeyFeature = "G.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація характеру зовнішніх проявів", KodDetailing = "G.000.003.002", KeyGrDetailing = "YHH.000", KeyFeature = "G.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при сечовипусканні", KodDetailing = "G.000.003.003", KeyGrDetailing = "YII.000", KeyFeature = "G.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при блювотині", KodDetailing = "G.000.003.004", KeyGrDetailing = "YJJ.000", KeyFeature = "G.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при проносі або запорі", KodDetailing = "G.000.003.005", KeyGrDetailing = "YKK.000", KeyFeature = "G.000.003" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "G.000.003.006", KeyGrDetailing = "YCC.000", KeyFeature = "G.000.003" ,IdUser="Admin"});


                // Детализация особености 7.4 біль розповсюджується від підборіддя вправий бік KeyFeature = "G.000.004"

                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація часу дії болі", KodDetailing = "G.000.004.001", KeyGrDetailing = "YGG.000", KeyFeature = "G.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація характеру зовнішніх проявів", KodDetailing = "G.000.004.002", KeyGrDetailing = "YHH.000", KeyFeature = "G.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при сечовипусканні", KodDetailing = "G.000.004.003", KeyGrDetailing = "YII.000", KeyFeature = "G.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при блювотині", KodDetailing = "G.000.004.004", KeyGrDetailing = "YJJ.000", KeyFeature = "G.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при проносі або запорі", KodDetailing = "G.000.004.005", KeyGrDetailing = "YKK.000", KeyFeature = "G.000.004" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "G.000.004.006", KeyGrDetailing = "YCC.000", KeyFeature = "G.000.004" ,IdUser="Admin"});

                // Детализация особености 7.5 біль тільки нижче пупка KeyFeature = "G.000.005"

                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація часу дії болі", KodDetailing = "G.000.005.001", KeyGrDetailing = "YGG.000", KeyFeature = "G.000.005" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація характеру зовнішніх проявів", KodDetailing = "G.000.005.002", KeyGrDetailing = "YHH.000", KeyFeature = "G.000.005" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при сечовипусканні", KodDetailing = "G.000.005.003", KeyGrDetailing = "YII.000", KeyFeature = "G.000.005" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при блювотині", KodDetailing = "G.000.005.004", KeyGrDetailing = "YJJ.000", KeyFeature = "G.000.005" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при проносі або запорі", KodDetailing = "G.000.005.005", KeyGrDetailing = "YKK.000", KeyFeature = "G.000.005" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "G.000.005.006", KeyGrDetailing = "YCC.000", KeyFeature = "G.000.005" ,IdUser="Admin"});

                // Детализация особености 7.6 біль у попереку переміщується в пах KeyFeature = "G.000.006"

                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація часу дії болі", KodDetailing = "G.000.006.001", KeyGrDetailing = "YGG.000", KeyFeature = "G.000.006" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація характеру зовнішніх проявів", KodDetailing = "G.000.006.002", KeyGrDetailing = "YHH.000", KeyFeature = "G.000.006" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при сечовипусканні", KodDetailing = "G.000.006.003", KeyGrDetailing = "YII.000", KeyFeature = "G.000.006" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при блювотині", KodDetailing = "G.000.006.004", KeyGrDetailing = "YJJ.000", KeyFeature = "G.000.006" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при проносі або запорі", KodDetailing = "G.000.006.005", KeyGrDetailing = "YKK.000", KeyFeature = "G.000.006" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "G.000.006.006", KeyGrDetailing = "YCC.000", KeyFeature = "G.000.006" ,IdUser="Admin"});

                // Детализация особености 7.7 біль тільки нижче талії KeyFeature = "G.000.007"

                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація часу дії болі", KodDetailing = "G.000.007.001", KeyGrDetailing = "YGG.000", KeyFeature = "G.000.007" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація характеру зовнішніх проявів", KodDetailing = "G.000.007.002", KeyGrDetailing = "YHH.000", KeyFeature = "G.000.007" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при сечовипусканні", KodDetailing = "G.000.007.003", KeyGrDetailing = "YII.000", KeyFeature = "G.000.007" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при блювотині", KodDetailing = "G.000.007.004", KeyGrDetailing = "YJJ.000", KeyFeature = "G.000.007" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при проносі або запорі", KodDetailing = "G.000.007.005", KeyGrDetailing = "YKK.000", KeyFeature = "G.000.007" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "G.000.007.006", KeyGrDetailing = "YCC.000", KeyFeature = "G.000.007" ,IdUser="Admin"});

                // Детализация особености 7.8 біль у попереку з однієї із сторін над самою талією KeyFeature = "G.000.008"

                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація часу дії болі", KodDetailing = "G.000.008.001", KeyGrDetailing = "YGG.000", KeyFeature = "G.000.008" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація характеру зовнішніх проявів", KodDetailing = "G.000.008.002", KeyGrDetailing = "YHH.000", KeyFeature = "G.000.008" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при сечовипусканні", KodDetailing = "G.000.008.003", KeyGrDetailing = "YII.000", KeyFeature = "G.000.008" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при блювотині", KodDetailing = "G.000.008.004", KeyGrDetailing = "YJJ.000", KeyFeature = "G.000.008" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація при проносі або запорі", KodDetailing = "G.000.008.005", KeyGrDetailing = "YKK.000", KeyFeature = "G.000.008" ,IdUser="Admin"});
                db.Detailings.Add(new Detailing { NameDetailing = "Деталізація за температурою", KodDetailing = "G.000.008.006", KeyGrDetailing = "YCC.000", KeyFeature = "G.000.008" ,IdUser="Admin"});
                db.SaveChanges();
            }

            // Группы детализации особености (характера) жалобы 
            if (!db.GrDetailings.Any())
            {
                // Детализация группа  временная
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "цілий рік", KodDetailing = "YAA.000.001", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //1
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "навесні", KodDetailing = "YAA.000.002", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //2
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "влітку", KodDetailing = "YAA.000.003", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //3
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "восени", KodDetailing = "YAA.000.004", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //4
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "взимку", KodDetailing = "YAA.000.005", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //5
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "цілодобово", KodDetailing = "YAA.000.006", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "більше вранці після сну", KodDetailing = "YAA.000.007", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "вночі", KodDetailing = "YAA.000.008", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //8
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "вранці", KodDetailing = "YAA.000.009", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //9
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "вдень", KodDetailing = "YAA.000.010", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //10
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "увечері", KodDetailing = "YAA.000.011", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //11
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "незалежно від часу доби", KodDetailing = "YAA.000.012", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //12
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "протягом останнього 1 місяця", KodDetailing = "YAA.000.013", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //13
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "протягом 1 року", KodDetailing = "YAA.000.014", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //14
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "протягом 5 років", KodDetailing = "YAA.000.015", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //15
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "більше 5 років", KodDetailing = "YAA.000.016", KeyGrDetailing = "YAA.000" ,IdUser="Admin"}); //16


                //  Детализация группа  ситуативная
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "після ГРВІ", KodDetailing = "YBB.000.001", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //1
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "вдома", KodDetailing = "YBB.000.002", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //2
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "під час прибирання вдома", KodDetailing = "YBB.000.003", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //3
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "після контакту з тваринами (кітом, собакою, конем, хом'яком)", KodDetailing = "YBB.000.004", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //4
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "на вулиці", KeyGrDetailing = "YBB.000", KodDetailing = "YBB.000.005", KodGroupQualification = "YAAA.000",IdUser="Admin"}); //5
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "за містом", KodDetailing = "YBB.000.006", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "після перебування у сільському будинку", KodDetailing = "YBB.000.007", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "на работі", KodDetailing = "YBB.000.008", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //8
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "в полі", KodDetailing = "YBB.000.009", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //9
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "в лісі", KodDetailing = "YBB.000.010", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //10
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "в басейні", KodDetailing = "YBB.000.011", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //11
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "на річці", KodDetailing = "YBB.000.012", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //12
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "немає залежності від місця перебування", KodDetailing = "YBB.000.013", KeyGrDetailing = "YBB.000" ,IdUser="Admin"}); //13

                // Детализация группа Температура

                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "зазвичай нормальна", KodDetailing = "YCC.000.001", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //1
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "підвищується лише за застуді", KodDetailing = "YCC.000.002", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //2
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "зазвичай підвищується до вечора до 37 - 37.5", KodDetailing = "YCC.000.003", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //3
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "при застуді зазвичай підвищується до 37", KodDetailing = "YCC.000.004", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //4
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "при застуді зазвичай підвищується до 38", KodDetailing = "YCC.000.005", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //5
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "при застуді зазвичай підвищується до 39", KodDetailing = "YCC.000.006", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "при застуді не підвищується", KodDetailing = "YCC.000.007", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "36.6", KodDetailing = "YCC.000.008", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //8
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "37", KodDetailing = "YCC.000.009", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //9
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "37.5", KodDetailing = "YCC.000.010", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //10
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "38", KodDetailing = "YCC.000.012", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //11
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "38.5", KodDetailing = "YCC.000.013", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //12
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "39", KodDetailing = "YCC.000.014", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //13
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "Зазвичай занижена 36-36.5", KodDetailing = "YCC.000.015", KeyGrDetailing = "YCC.000" ,IdUser="Admin"}); //15
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "38 і вище", KodDetailing = "YCC.000.016", KeyGrDetailing = "YCC.000" ,IdUser="Admin"});
                // Детализация группа виділення

                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "епізодичні", KodDetailing = "YDD.000.001", KeyGrDetailing = "YDD.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "цілодобово", KodDetailing = "YDD.000.002", KeyGrDetailing = "YDD.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "прозорі", KodDetailing = "YDD.000.003", KeyGrDetailing = "YDD.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "світлі", KodDetailing = "YDD.000.004", KeyGrDetailing = "YDD.000" ,IdUser="Admin"}); //8
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "густі", KodDetailing = "YDD.000.005", KeyGrDetailing = "YDD.000" ,IdUser="Admin"}); //9
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "зелені", KodDetailing = "YDD.000.006", KeyGrDetailing = "YDD.000" ,IdUser="Admin"}); //10
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "жовті", KodDetailing = "YDD.000.007", KeyGrDetailing = "YDD.000" ,IdUser="Admin"}); //11
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "рясні", KodDetailing = "YDD.000.008", KeyGrDetailing = "YDD.000" ,IdUser="Admin"}); //12
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "мізерні", KodDetailing = "YDD.000.009", KeyGrDetailing = "YDD.000" ,IdUser="Admin"}); //13

                // Детализация группа кашель

                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "сухий", KodDetailing = "YEE.000.001", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "вологий", KodDetailing = "YEE.000.002", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "нав'язливий", KodDetailing = "YEE.000.003", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //8
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "нападоподібний", KodDetailing = "YEE.000.004", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //9
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "більше вранці після сну", KodDetailing = "YEE.000.005", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //10
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "більше ввечері, коли лягаю в ліжко", KodDetailing = "YEE.000.006", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //11
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "вночі", KodDetailing = "YEE.000.007", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //12
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "вранці", KodDetailing = "YEE.000.008", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //13
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "після фізичного навантаження (підйом сходами або бігом)", KodDetailing = "YEE.000.009", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //11
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "з відходженням мокротиння", KodDetailing = "YEE.000.010", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //12
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "без відходження мокротиння", KodDetailing = "YEE.000.011", KeyGrDetailing = "YEE.000" ,IdUser="Admin"}); //13

                // Детализация группа біль у горлі

                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "перхотить", KodDetailing = "YFF.000.001", KeyGrDetailing = "YFF.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "здавлюча", KodDetailing = "YFF.000.002", KeyGrDetailing = "YFF.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "ріжача", KodDetailing = "YFF.000.003", KeyGrDetailing = "YFF.000" ,IdUser="Admin"}); //8           


                // Деталізація часу дії болі

                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "подібні напади відбувалися на протязі декількох днів за останню неділю", KodDetailing = "YGG.000.001", KeyGrDetailing = "YGG.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "сильна біль, продовжується більше години", KodDetailing = "YGG.000.002", KeyGrDetailing = "YGG.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "періодичні, нападоподібні ", KodDetailing = "YGG.000.003", KeyGrDetailing = "YGG.000" ,IdUser="Admin"}); //8           

                //  Деталізація характеру зовнішніх проявів

                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "хворобливо здутий живіт", KodDetailing = "YHH.000.001", KeyGrDetailing = "YHH.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "нема апетиту", KodDetailing = "YHH.000.002", KeyGrDetailing = "YHH.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "схудність на 5кг і більше за останні 10 неділь", KodDetailing = "YHH.000.003", KeyGrDetailing = "YHH.000" ,IdUser="Admin"}); //8           
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "опухлість у паху", KodDetailing = "YHH.000.004", KeyGrDetailing = "YHH.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "стиск у паху при кашлі", KodDetailing = "YHH.000.005", KeyGrDetailing = "YHH.000" ,IdUser="Admin"}); //8           

                // Деталізація при сечовипусканні

                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "печіння при сечовипусканні", KodDetailing = "YII.000.001", KeyGrDetailing = "YII.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "сечовипускання частіше звичайного", KodDetailing = "YII.000.002", KeyGrDetailing = "YII.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "біль у паху при сечовипусканні", KodDetailing = "YII.000.003", KeyGrDetailing = "YII.000" ,IdUser="Admin"}); //8           

                // Деталізація при  блювотині

                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "раптове блювотиння", KodDetailing = "YJJ.000.001", KeyGrDetailing = "YJJ.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "епізодичне блювотиння", KodDetailing = "YJJ.000.002", KeyGrDetailing = "YJJ.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "постійне блювотиння на протязі кількох днів за останню неділю", KodDetailing = "YJJ.000.003", KeyGrDetailing = "YJJ.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "головокружіння перед блювотинням", KodDetailing = "YJJ.000.004", KeyGrDetailing = "YJJ.000" ,IdUser="Admin"}); //8           

                // Деталізація при проносі або запорі

                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "епізодичні проноси", KodDetailing = "YKK.000.001", KeyGrDetailing = "YKK.000" ,IdUser="Admin"}); //6
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "приступи проносу за останні неділі", KodDetailing = "YKK.000.002", KeyGrDetailing = "YKK.000" ,IdUser="Admin"}); //7
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "нудота під час проносу", KodDetailing = "YKK.000.003", KeyGrDetailing = "YKK.000" ,IdUser="Admin"}); //8           
                db.GrDetailings.Add(new GrDetailing { NameGrDetailing = "періодичні запори", KodDetailing = "YKK.000.004", KeyGrDetailing = "YKK.000" ,IdUser="Admin"}); //7
                db.SaveChanges();
            }

            // Справочник груп детализации
            if (!db.ListGrDetailings.Any())
            {
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YAA.000", NameGrup = "Деталізація за часом" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YBB.000", NameGrup = "Деталізація ситуативна" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YCC.000", NameGrup = "Деталізація за температурою" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YDD.000", NameGrup = "Деталізація виділення" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YEE.000", NameGrup = "Деталізація кашель" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YFF.000", NameGrup = "Деталізація біль у горлі" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YGG.000", NameGrup = "Деталізація часу дії болі" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YHH.000", NameGrup = "Деталізація характеру зовнішніх проявів" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YII.000", NameGrup = "Деталізація при сечовипусканні" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YJJ.000", NameGrup = "Деталізація при блювотині" ,IdUser="Admin"});
                db.ListGrDetailings.Add(new ListGrDetailing { KeyGrDetailing = "YKK.000", NameGrup = "Деталізація при проносі або запорі" ,IdUser="Admin"});
                db.SaveChanges();
            }
            // Справочник груп уточнений (классификации) детализации
            if (!db.ListGroupQualifications.Any())
            {
                db.ListGroupQualifications.Add(new ListGroupQualification { KodGroupQualification = "YAAA.000", NameGroupQualification = "Кваліфікація погоди" ,IdUser="Admin"});
                db.SaveChanges();
            }


                // Справочник уточнений (классификации) детализации
            if (!db.Qualifications.Any())
            {

                db.Qualifications.Add(new Qualification { KodGroupQualification = "YAAA.000", KodQualification = "YAAA.000.001", NameQualification = "в ясну погоду" ,IdUser="Admin"});
                db.Qualifications.Add(new Qualification { KodGroupQualification = "YAAA.000", KodQualification = "YAAA.000.002", NameQualification = "в дощ" ,IdUser="Admin"});
                db.Qualifications.Add(new Qualification { KodGroupQualification = "YAAA.000", KodQualification = "YAAA.000.003", NameQualification = "в мокрий сніг" ,IdUser="Admin"});
                db.Qualifications.Add(new Qualification { KodGroupQualification = "YAAA.000", KodQualification = "YAAA.000.004", NameQualification = "при сильному вітру" ,IdUser="Admin"});
                db.Qualifications.Add(new Qualification { KodGroupQualification = "YAAA.000", KodQualification = "YAAA.000.005", NameQualification = "в дощ з поривчастим вітром" ,IdUser="Admin"});
                db.SaveChanges();
            } 

            // Справочник диагнозов

            if (!db.Diagnozs.Any())
            {
                db.Diagnozs.Add(new  Diagnoz { KodDiagnoza= "DIA.000000001", KeyIcd= "J00-J99.J30-J39.J30.J30.30", NameDiagnoza = "Попередній діагноз - Алергічний риніт цілорічний" , IcdGrDiagnoz ="",IdUser="Admin"});
                db.Diagnozs.Add(new Diagnoz { KodDiagnoza = "DIA.000000002", KeyIcd = "J00-J99.J30-J39.J30.J30.20", NameDiagnoza = "Попередній діагноз - Алергічний сезонний риніт (поліноз)", IcdGrDiagnoz = "" ,IdUser="Admin"});
                db.Diagnozs.Add(new Diagnoz { KodDiagnoza = "DIA.000000003", KeyIcd = "J00-J99.J30-J39.J30.J30.40", NameDiagnoza = "Попередній діагноз-рініт неалергічний (ймовірно пов'язаний із застудою – ГРВІ, грип)", IcdGrDiagnoz = "" ,IdUser="Admin"});
                db.Diagnozs.Add(new Diagnoz { KodDiagnoza = "DIA.000000004", KeyIcd = "J00-J99.J30-J39.J30.J30.40", NameDiagnoza = "Попередній діагноз-рініт неалергічний (ймовірно пов'язаний із загостренням хронічної інфекції в носі )", IcdGrDiagnoz = "" ,IdUser="Admin"});
                db.Diagnozs.Add(new Diagnoz { KodDiagnoza = "DIA.000000005", KeyIcd = "J00-J99.J30-J39.J30.J30.00", NameDiagnoza = "Попередній діагноз-рініт вазомоторний", IcdGrDiagnoz = "" ,IdUser="Admin"});
                db.Diagnozs.Add(new Diagnoz { KodDiagnoza = "DIA.000000006", KeyIcd = "N00-N99.N40-N54.N41.N41.00", NameDiagnoza = "Попередній діагноз- гострий простатит", IcdGrDiagnoz = "" ,IdUser="Admin"});
                db.Diagnozs.Add(new Diagnoz { KodDiagnoza = "DIA.000000007", KeyIcd = "K00-K99.K35-K38.K35.K35.00", NameDiagnoza = "Попередній діагноз- гострий апендецит або прободна виразка дванадцятипалої кішки", IcdGrDiagnoz = "" ,IdUser="Admin"});
                db.Diagnozs.Add(new Diagnoz { KodDiagnoza = "DIA.000000008", KeyIcd = "K00-K99.K50-K52.K51.K51.00", NameDiagnoza = "Попередній діагноз- спастичний коліт", IcdGrDiagnoz = ""  ,IdUser="Admin"});
                db.SaveChanges();
            }

            // Справочник рекомендаций для пациентов

            if (!db.Recommendations.Any())
            {
                db.Recommendations.Add(new Recommendation { KodRecommendation = "REC.000000001", ContentRecommendation = "Рекомендується консультація лікаря  алерголога" });
                db.Recommendations.Add(new Recommendation { KodRecommendation = "REC.000000002", ContentRecommendation = "Рекомендується консультація лікаря алерголога" });
                db.Recommendations.Add(new Recommendation { KodRecommendation = "REC.000000003", ContentRecommendation = "Рекомендується консультація сімейного лікаря" });
                db.Recommendations.Add(new Recommendation { KodRecommendation = "REC.000000004", ContentRecommendation = "Рекомендується консультація лікаря отоларинголога" });
                db.Recommendations.Add(new Recommendation { KodRecommendation = "REC.000000005", ContentRecommendation = "Рекомендується консультація лікаря отоларинголога, алерголога" });
                db.Recommendations.Add(new Recommendation { KodRecommendation = "REC.000000006", ContentRecommendation = "Рекомендується звернутися до лікаря уролога" });
                db.Recommendations.Add(new Recommendation { KodRecommendation = "REC.000000007", ContentRecommendation = "Рекомендується негайно визвати швидку допомогу. Нічого не їсти і не пити." });
                db.Recommendations.Add(new Recommendation { KodRecommendation = "REC.000000008", ContentRecommendation = "Рекомендується негайно звернутися до гастроентеролога" });
                db.SaveChanges();
            }


            // Справочник интервью

            if (!db.DependencyDiagnozs.Any())
            {
                db.DependencyDiagnozs.Add(new DependencyDiagnoz { KodDiagnoz = "DIA.000000001", KodRecommend = "REC.000000001", KodProtokola = "PRT.000000001"});
                db.DependencyDiagnozs.Add(new DependencyDiagnoz { KodDiagnoz = "DIA.000000002", KodRecommend = "REC.000000002", KodProtokola = "PRT.000000002"});
                db.DependencyDiagnozs.Add(new DependencyDiagnoz { KodDiagnoz = "DIA.000000003", KodRecommend = "REC.000000003", KodProtokola = "PRT.000000003"});
                db.DependencyDiagnozs.Add(new DependencyDiagnoz { KodDiagnoz = "DIA.000000004", KodRecommend = "REC.000000004", KodProtokola = "PRT.000000004"});
                db.DependencyDiagnozs.Add(new DependencyDiagnoz { KodDiagnoz = "DIA.000000005", KodRecommend = "REC.000000005", KodProtokola = "PRT.000000005"});
                db.DependencyDiagnozs.Add(new DependencyDiagnoz { KodDiagnoz = "DIA.000000006", KodRecommend = "REC.000000006", KodProtokola = "PRT.000000006" });
                db.DependencyDiagnozs.Add(new DependencyDiagnoz { KodDiagnoz = "DIA.000000007", KodRecommend = "REC.000000007", KodProtokola = "PRT.000000007" });
                db.DependencyDiagnozs.Add(new DependencyDiagnoz { KodDiagnoz = "DIA.000000008", KodRecommend = "REC.000000008", KodProtokola = "PRT.000000008" });
                db.SaveChanges();
            }

            // Справочник интервью

            if (!db.Icds.Any())
            {
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J30.000.00", Name = "J30. Вазомоторний та алергічний риніт" });
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J30.J30.00", Name = "Вазомоторний риніт" });
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J30.J30.10", Name = "Алергічний риніт, зумовлений пилком рослин" });
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J30.J30.20", Name = "Iнший сезонний алергічний риніт" });
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J30.J30.30", Name = "Iнший алергічний риніт" });
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J30.J30.40", Name = "Алергічний риніт, неуточнений" });
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J31.000.00", Name = "J31. Хронічний риніт, назофарингіт та фарингіт" });
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J31.J31.00", Name = "Хронічний риніт" });
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J31.J31.10", Name = "Хронічний назофарингіт" });
                db.Icds.Add(new Icd { KeyIcd = "J00-J99.J30-J39.J31.J31.20", Name = "Хронічний фарингіт" });
                db.Icds.Add(new Icd { KeyIcd = "N00-J99.N40-N54.N41.N41.00", Name = "Гострий простатит" });
                db.Icds.Add(new Icd { KeyIcd = "K00-K99.K35-K38.K35.K35.00", Name = "Гострий апендецит або прободна виразка дванадцятипалої кішки" });
                db.Icds.Add(new Icd { KeyIcd = "K00-K99.K20-K31.K26.K26.00", Name = "Виразка дванадцятипалої кішки" });
                db.Icds.Add(new Icd { KeyIcd = "K00-K99.K50-K52.K51.K51.00", Name = "Виразковий коліт" });
                db.SaveChanges();
            }

            // Справочник протоколов интервью
            if (!db.Interviews.Any())
            {
                db.Interviews.Add(new Interview 
                {   KodProtokola = "PRT.000000001",
                    DetailsInterview="A.000;A.000.001;AA.000.001;YAA.000.006;YAA.000.013;YAA.000.014;YAA.000.015;" +
                    "A.000.002;A.000.002.002;YAA.000.001;YEE.000.005;YBB.000.004;YBB.000.007;YAA.000.014;YAA.000.015;" +
                    "A.000.003;A.000.003.002;YBB.000.004;YBB.000.007;YAA.000.014;YAA.000.015;" +
                    "A.000.004;YDD.000.002;YDD.000.003;YDD.000.008;A.000.004.004;YBB.000.004;YBB.000.007;YAA.000.014;YAA.000.015;" +
                    "A.000.006;YCC.000.001;YCC.000.002;YCC.000.004;" +
                    "A.000.012;A.000.012.001;A.000.012.003;YBB.000.005;" +
                    "A.000.011;AA.000.008;AA.000.009;YBB.000.002;YBB.000.003;YBB.000.004;" +
                    "A.000.013;A.000.013.003;",
                    NametInterview = " Алергічний риніт цілорічний",
                    OpistInterview = "Алергічний риніт — інтермітуюче або постійне запалення слизової оболонки носа і його пазух, викликане алергеном."+
                    " Алергічний риніт є дуже поширеним захворюванням.  Алергічний риніт поділяють на сезонний і цілорічний",
                    UriInterview = "https://empendium.com/ua/chapter/B27.YII.17.3."
                    ,IdUser="Admin", GrDetail = "A.000;YAA.000;YEE.000;YBB.000;YCC.000"  });
                db.Interviews.Add(new Interview
                {
                    KodProtokola = "PRT.000000006",
                    DetailsInterview = "G.000;G.000.001;G.000.007;YCC.000.016;YII.000.001;YII.000.002;YII.000.003;",
                    NametInterview = " Простатит",
                    OpistInterview = "Признаки простатита у мужчин :Частое мочеиспускание; Болезненное мочеиспускание;"+
                    " Слабый напор мочи и рези; Болезненная дефекация;Нарушение эрекции; Жжение в уретре; Ускоренное семяизвержение или длительная эрекция; ",
                    UriInterview = "https://www.dobrobut.com/med/c-simptomy-prostatita-cto-nuzno-znat-kazdomu-muzcine"
                    ,IdUser="Admin", GrDetail = "A.000;YAA.000;YEE.000;YBB.000;YCC.000" }); 
                db.Interviews.Add(new Interview
                {
                    KodProtokola = "PRT.000000007",
                    DetailsInterview = "G.000;G.000.001;YGG.000.002;YHH.000.001;YJJ.000.001;",
                    NametInterview = " Гострий апендецит або прободна виразка дванадцятипалої кішки" ,
                    OpistInterview = "Острый аппендицит — это острый воспалительно-некротический процесс придатка слепой кишки, который создает угрозу здоровью и жизни пациента.",
                    UriInterview = "https://empendium.com/ua/chapter/B27.YII.4.25.3."
                    ,IdUser="Admin", GrDetail = "A.000;YAA.000;YEE.000;YBB.000;YCC.000" });
                db.Interviews.Add(new Interview
                {
                    KodProtokola = "PRT.000000008",
                    DetailsInterview = "G.000;G.000.001;G.000.002;G.000.005;YGG.000.001;YKK.000.001;YKK.000.004;",
                    NametInterview = " Виразковий коліт",
                    OpistInterview = "Виразковий коліт (ВК) є дифузним неспецифічним запаленням слизової оболонки прямої кишки або прямої"+
                    " та ободової кишок, що у тяжчих випадках призводить до утворення виразок. ",
                    UriInterview = "https://empendium.com/ua/chapter/B27.YII.4.16."
                    ,IdUser="Admin", GrDetail = "A.000;YAA.000;YEE.000;YBB.000;YCC.000"  });
                db.SaveChanges();
            }



            if (!db.ContentIntervs.Any())
            {
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 1, KodDetailing = "A.000", DetailsInterview = "ніс" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 2, KodDetailing = "A.000.001", DetailsInterview = "  закладеність носа" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 3, KodDetailing = "YAA.000.001", DetailsInterview = "   цілий рік" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 4, KodDetailing = "YAA.000.006", DetailsInterview = "   цілодобово" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 5, KodDetailing = "YAA.000.013", DetailsInterview = "   протягом останнього 1 місяця" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 6, KodDetailing = "YAA.000.014", DetailsInterview = "   протягом 1 року" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 7, KodDetailing = "YAA.000.015", DetailsInterview = "   протягом 5 років" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 8, KodDetailing = "A.000.002", DetailsInterview = "  чихання" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 9, KodDetailing = "A.000.002.002", DetailsInterview = "   нападоподібне" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 10, KodDetailing = "YAA.000.001", DetailsInterview = "   цілий рік" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 11, KodDetailing = "YEE.000.005", DetailsInterview = "   більше вранці після сну" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 12, KodDetailing = "YBB.000.004", DetailsInterview = "   після контакту з тваринами (кітом, собакою, конем, хом'яком)" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 13, KodDetailing = "YBB.000.007", DetailsInterview = "   після перебування у сільському будинку" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 14, KodDetailing = "YAA.000.014", DetailsInterview = "   протягом 1 року" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 15, KodDetailing = "YAA.000.015", DetailsInterview = "   протягом 5 років" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 16, KodDetailing = "A.000.003", DetailsInterview = "  свербіння в носі" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 17, KodDetailing = "A.000.003.002", DetailsInterview = "   постійний" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 18, KodDetailing = "YBB.000.004", DetailsInterview = "   після контакту з тваринами (кітом, собакою, конем, хом'яком)" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 19, KodDetailing = "YBB.000.007", DetailsInterview = "   після перебування у сільському будинку" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 20, KodDetailing = "YAA.000.014", DetailsInterview = "   протягом 1 року" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 21, KodDetailing = "YAA.000.015", DetailsInterview = "   протягом 5 років" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 22, KodDetailing = "A.000.004", DetailsInterview = "  виділення з носа" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 23, KodDetailing = "YDD.000.002", DetailsInterview = "   цілодобово" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 24, KodDetailing = "YDD.000.003", DetailsInterview = "   прозорі" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 25, KodDetailing = "YDD.000.008", DetailsInterview = "   рясні" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 26, KodDetailing = "A.000.004.004", DetailsInterview = "   відчуття стікання слизи на задній стінці глотки" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 27, KodDetailing = "YBB.000.004", DetailsInterview = "   після контакту з тваринами (кітом, собакою, конем, хом'яком)" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 28, KodDetailing = "YBB.000.007", DetailsInterview = "   після перебування у сільському будинку" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 29, KodDetailing = "YAA.000.014", DetailsInterview = "   протягом 1 року" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 30, KodDetailing = "YAA.000.015", DetailsInterview = "   протягом 5 років" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 31, KodDetailing = "A.000.006", DetailsInterview = "  температура" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 32, KodDetailing = "YCC.000.001", DetailsInterview = "   зазвичай нормальна" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 33, KodDetailing = "YCC.000.002", DetailsInterview = "   підвищується лише за застуді" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 34, KodDetailing = "YCC.000.004", DetailsInterview = "   при застуді зазвичай підвищується до 37" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 35, KodDetailing = "B.000.010.001", DetailsInterview = "  подібні проблеми у найближчих кровних родичів" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 36, KodDetailing = "A.000.011", DetailsInterview = "  поліпшення стану" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 37, KodDetailing = "A.000.012.001", DetailsInterview = "   при прийомі противоалергічних пігулок" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 38, KodDetailing = "A.000.012.003", DetailsInterview = "   при прийомі судинозвужуючих капель у ніс" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 39, KodDetailing = "YBB.000.005", DetailsInterview = "   на вулиці" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 40, KodDetailing = "A.000.012", DetailsInterview = "  погіршення стану" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 41, KodDetailing = "YAA.000.008", DetailsInterview = "   вночі" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 42, KodDetailing = "YAA.000.009", DetailsInterview = "   вранці" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 43, KodDetailing = "YBB.000.002", DetailsInterview = "   вдома" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 44, KodDetailing = "YBB.000.003", DetailsInterview = "   під час прибирання вдома" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 45, KodDetailing = "YBB.000.004", DetailsInterview = "   після контакту з тваринами (кітом, собакою, конем, хом'яком)" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 46, KodDetailing = "A.000.013", DetailsInterview = "  інші скарги" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000001", Numberstr = 47, KodDetailing = "A.000.013.003", DetailsInterview = "   закладеність у вухах" ,IdUser="Admin"});

                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000006", Numberstr = 1, KodDetailing = "G.000", DetailsInterview = "біль в животі" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000006", Numberstr = 2, KodDetailing = "G.000.001", DetailsInterview = "  біль між нижнім краєм грудної клітки та пахом" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000006", Numberstr = 3, KodDetailing = "G.000.007", DetailsInterview = "  біль тільки нижче талії" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000006", Numberstr = 4, KodDetailing = "YII.000.001", DetailsInterview = "   печіння при сечовипусканні" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000006", Numberstr = 5, KodDetailing = "YII.000.002", DetailsInterview = "   сечовипускання частіше звичайного" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000006", Numberstr = 6, KodDetailing = "YII.000.003", DetailsInterview = "   біль у паху при сечовипусканні" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000006", Numberstr = 7, KodDetailing = "YCC.000.016", DetailsInterview = "   38 і вище" ,IdUser="Admin"});

                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000007", Numberstr = 1, KodDetailing = "G.000", DetailsInterview = "біль в животі" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000007", Numberstr = 2, KodDetailing = "G.000.001", DetailsInterview = "  біль між нижнім краєм грудної клітки та пахом" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000007", Numberstr = 3, KodDetailing = "YGG.000.002", DetailsInterview = "  сильна біль, продовжується більше години" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000007", Numberstr = 4, KodDetailing = "YHH.000.001", DetailsInterview = "  хворобливо здутий живіт" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000007", Numberstr = 5, KodDetailing = "YJJ.000.001", DetailsInterview = "  раптове блювотиння" ,IdUser="Admin"});


                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000008", Numberstr = 1, KodDetailing = "G.000", DetailsInterview = "біль в животі" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000008", Numberstr = 2, KodDetailing = "G.000.001", DetailsInterview = "  біль між нижнім краєм грудної клітки та пахом" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000008", Numberstr = 3, KodDetailing = "G.000.002", DetailsInterview = "  біль тільки над талією" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000008", Numberstr = 4, KodDetailing = "G.000.005", DetailsInterview = "  біль в основному нижче пупка" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000008", Numberstr = 5, KodDetailing = "YGG.000.001", DetailsInterview = "   подібні напади відбувалися на протязі декількох днів за останню неділю" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000008", Numberstr = 6, KodDetailing = "YKK.000.001", DetailsInterview = "   епізодичні проноси" ,IdUser="Admin"});
                db.ContentIntervs.Add(new ContentInterv { KodProtokola = "PRT.000000008", Numberstr = 7, KodDetailing = "YKK.000.004", DetailsInterview = "   періодичні запори" ,IdUser="Admin"});
                db.SaveChanges();

            }
            if (!db.MedicalInstitutions.Any())
            { 
                db.MedicalInstitutions.Add(new MedicalInstitution
                {
                    Edrpou = "31256789",
                    Telefon = "044 502 22 62",
                    Name = "Клініка імунології та алергології 'Форпост' Лабораторія 'Форпост'",
                    PostIndex = "03150",
                    Adres = "03150,Україна м.Київ, Ежи Гедройца,2, офіс 262",
                    Email = "info@forpost",
                    KodObl = "2703150"
                });
                db.SaveChanges();
            }

            if (!db.AccountUsers.Any())
            { 
                db.AccountUsers.Add(new AccountUser { IdUser = "CNT.0000000001", IdStatus = "1", Login = "Admin", Password = "7" });
                db.AccountUsers.Add(new AccountUser { IdUser = "PCN.0000000001", IdStatus = "2", Login = "+380675540132", Password = "2" });
                db.AccountUsers.Add(new AccountUser { IdUser = "DTR.0000000001", IdStatus = "3", Login = "+380662904827", Password = "3" });
                db.SaveChanges();
            }
            if (!db.AccountUsers.Any())
            { 
                db.Pacients.Add(new Pacient { KodPacient = "PCN.0000000001", Age = 78, Weight = 89, Growth = 170, Gender = "чол", Tel = "+380675540132", Name = "Микола", Surname = "Павлюк", Pind = "01234", Profession = "шофер" });
                db.SaveChanges();
            }
            if (!db.Doctors.Any())
            { 
                db.Doctors.Add(new Doctor
                {
                    KodDoctor = "DTR.0000000001",
                    Telefon = "+380662904827",
                    Name = "Петро",
                    Surname = "Козак",
                    Edrpou = "31256789",
                    Specialnoct = "фізіотерапевт"
                });
                db.SaveChanges();
            }
            // Довідник розкладу прийомів лікаря 
            if (!db.VisitingDayss.Any())
            { 
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Понеділок", DateVizita = "10.07.2023", TimeVizita = "10:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Понеділок", DateVizita = "10.07.2023", TimeVizita = "11:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Понеділок", DateVizita = "10.07.2023", TimeVizita = "12:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Понеділок", DateVizita = "10.07.2023", TimeVizita = "13:00", OnOff = "Так" });

                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Вівторок", DateVizita = "11.07.2023", TimeVizita = "14:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Вівторок", DateVizita = "11.07.2023", TimeVizita = "15:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Вівторок", DateVizita = "11.07.2023", TimeVizita = "16:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Вівторок", DateVizita = "11.07.2023", TimeVizita = "17:00", OnOff = "Так" });

                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Середа", DateVizita = "12.07.2023", TimeVizita = "10:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Середа", DateVizita = "12.07.2023", TimeVizita = "11:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Середа", DateVizita = "12.07.2023", TimeVizita = "12:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Середа", DateVizita = "12.07.2023", TimeVizita = "13:00", OnOff = "Так" });
 
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Четверг", DateVizita = "13.07.2023", TimeVizita = "14:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Четверг", DateVizita = "13.07.2023", TimeVizita = "15:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Четверг", DateVizita = "13.07.2023", TimeVizita = "16:00", OnOff = "Так" });
                db.VisitingDayss.Add(new VisitingDays { KodDoctor = "DTR.0000000001", DaysOfTheWeek = "Четверг", DateVizita = "13.07.2023", TimeVizita = "17:00", OnOff = "Так" });
                db.SaveChanges();
            }



            // показники аналізу крові пацієнта
            if (!db.PacientAnalizKrovis.Any())
            { 
                db.PacientAnalizKrovis.Add(new PacientAnalizKrovi 
                { KodPacient = "PCN.0000000001",
                    DateAnaliza = "10.07.2023",
                    Gender = "чол",
                    Rbc = "4,8",
                    Hgb = "140",
                    Wbc = "5,0",
                    Cp = "0,9",
                    Hct = "41",
                    Ret = "0,8",
                    Plt = "220,0",
                    Esr = "5",
                    Bas = "0,4",
                    Eo = "2,5",
                    Mot = "",
                    Mtmot = "",
                    Neutp = "3",
                    Neuts = "57",
                    Lym = "28",
                    Mon = "7"
                });
                db.SaveChanges();
            }

            // показники аналізу мочи пацієнта
            if (!db.PacientAnalizKrovis.Any())
            { 
                db.PacientAnalizUrines.Add(new PacientAnalizUrine
                {
                    KodPacient = "PCN.0000000001",
                    DateAnaliza = "10.07.2023",
                    Color = "солом'яний",
                    Ph = "5",
                    Sg = "1,015",
                    Pro = "0,012 г/л",
                    Glu = "0,12 ммоль/л",
                    Bil = "7,8 мкмоль/л",
                    Uro = "25 мкмоль/л",
                    Ket = "0,15 ммоль/л",
                    Bld = "1,2 в п/з",
                    Leu = "1,5 в п/з",
                    Nit = "нема"

                });
                db.SaveChanges();
            }

            if (!db.PacientAnalizKrovis.Any())
            { 
                db.PacientMapAnalizs.Add(new PacientMapAnaliz { KodPacient= "PCN.0000000001", DateAnaliza = "10.07.2023", Pulse ="67", Pressure ="120/80", Temperature ="36.7", ResultAnaliza ="Стан здоров'я задовільний"});
                db.SaveChanges();
            }

            // Справочник уточнений (классификации) детализации
            if (!db.NsiStatusUsers.Any())
            {
                db.NsiStatusUsers.Add(new NsiStatusUser { IdStatus = "1", StatusUser = "on", NameStatus = "Адміністратор", KodDostupa = "RWED" });
                db.NsiStatusUsers.Add(new NsiStatusUser { IdStatus = "2", StatusUser = "on", NameStatus = "Паціент", KodDostupa = "RWE" });
                db.NsiStatusUsers.Add(new NsiStatusUser { IdStatus = "3", StatusUser = "on", NameStatus = "Лікар", KodDostupa = "RWE" });
                db.SaveChanges();

            }
            

            if (!db.Prices.Any())
            {
                db.Prices.Add(new Price { KeyPrice = "1", QuantityDays = 30, PriceQuantity = 200m, NamePrice = "Послуги з діагностики нездужання  протягом місяця" });
                db.SaveChanges();
            }


            if (!db.StatusMedZaklads.Any())
            {
                db.StatusMedZaklads.Add(new StatusMedZaklad { IdStatus = "1", NameStatus = "Лікарняний заклад", TypeStatus="1"});
                db.StatusMedZaklads.Add(new StatusMedZaklad { IdStatus = "2", NameStatus = "Амбулаторно-поліклінічний заклад", TypeStatus = "1" });
                db.StatusMedZaklads.Add(new StatusMedZaklad { IdStatus = "3", NameStatus = "Заклад переливання крові, швидкої та екстреної медичної допомоги", TypeStatus = "1" });
                db.StatusMedZaklads.Add(new StatusMedZaklad { IdStatus = "4", NameStatus = "Санаторно-курортний заклад", TypeStatus = "1" });
                db.StatusMedZaklads.Add(new StatusMedZaklad { IdStatus = "5", NameStatus = "Установа з надання спеціалізованої медичної допомоги", TypeStatus = "1" });
                db.SaveChanges();
            }

        }
                                                             
    }
}
