using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRestSeam.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Windows;

/// "Диференційна діагностика стану нездужання людини-SEAM" 
/// Розробник Стариченко Олександр Павлович тел.+380674012840, mail staric377@gmail.com
/// Контролер обробки запитів до БД з боку front та  Back користувачів

namespace AppRestSeam
{

    public class Startup 
    {

  
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            
            //// устанавливаем контекст данных
            //string connection = "Server=(localdb)\\mssqllocaldb;Database=d:\\dbseam;Trusted_Connection=True;";
            services.AddDbContext<DbContextSeam>(options => options.UseSqlServer(Program.connection));
            //services.AddControllersWithViews();
            services.AddControllers();
            //    .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling =
            //    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ApiControllerDoctor}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ApiControllerComplaint}/{action}/{id?}/{*catchall}"); 
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ControllerListGroupDetail }/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = FeatureController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = DetailingController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = GrDetalingController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = GroupQualificationController }/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = QualificationController }/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = DiagnozController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = InterviewController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ContentInterviewController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = CompletedInterviewController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = RecommendationController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = DependencyDiagnozController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = PacientController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ColectionInterviewController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = MedicalInstitutionController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = IcdController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = LanguageUIController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = PacientMapAnalizController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = LifePacientController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = LifeDoctorController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = AccountUserController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = NsiStatusUserController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ControllerAdmissionPatients}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = RegistrationAppointmentController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = PacientAnalizKroviController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = PacientAnalizUrineController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = VisitingDaysController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = UnLoadController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ApiControllerVisitingDays}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = SobController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = GrupDiagnozController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = MedGrupDiagnozController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = LikarGrupDiagnozController}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ControllerPayment}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ControllerPrice}/{action}/{id?}/{*catchall}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller = ControllerStatusMedZaklad}/{action}/{id?}/{*catchall}");
            });
        }
    }

    //    Эффективное использование кодов ответов HTTP

    //HTTP определяет различные коды ответов для указания клиенту различной информации об операциях.Ваше REST приложение могло бы эффективно использовать все доступные HTTP-коды, чтобы помочь клиенту правильно настроить ответ.Далее представлен список кодов ответов HTTP:

    //200 OK — это ответ на успешные GET, PUT, PATCH или DELETE.Этот код также используется для POST, который не приводит к созданию.
    //201 Created — этот код состояния является ответом на POST, который приводит к созданию.
    //204 Нет содержимого. Это ответ на успешный запрос, который не будет возвращать тело (например, запрос DELETE)
    //304 Not Modified — используйте этот код состояния, когда заголовки HTTP-кеширования находятся в работе
    //400 Bad Request — этот код состояния указывает, что запрос искажен, например, если тело не может быть проанализировано
    //401 Unauthorized — Если не указаны или недействительны данные аутентификации.Также полезно активировать всплывающее окно auth, если приложение используется из браузера
    //403 Forbidden — когда аутентификация прошла успешно, но аутентифицированный пользователь не имеет доступа к ресурсу
    //404 Not found — если запрашивается несуществующий ресурс
    //405 Method Not Allowed — когда запрашивается HTTP-метод, который не разрешен для аутентифицированного пользователя
    //410 Gone — этот код состояния указывает, что ресурс в этой конечной точке больше не доступен.Полезно в качестве защитного ответа для старых версий API
    //415 Unsupported Media Type.Если в качестве части запроса был указан неправильный тип содержимого
    //422 Unprocessable Entity — используется для проверки ошибок
    //429 Too Many Requests — когда запрос отклоняется из-за ограничения скорости
//    В стандарте HTTP описано более 70 статус кодов.Хорошим тоном является использование хотя бы основных.

//200 – OK – успешный запрос. Если клиентом были запрошены какие-либо данные, то они находятся в заголовке и/или теле сообщения.
//    201 – OK – в результате успешного выполнения запроса был создан новый ресурс.
//    204 – OK – ресурс успешно удалён.
//    304 – Not Modified – клиент может использовать данные из кэша.
//400 – Bad Request – запрос невалидный или не может быть обработан.
//    401 – Unauthorized – запрос требует аутентификации пользователя.
//403 – Forbidden – сервер понял запрос, но отказывается его обработать или доступ запрещён.
//    404 – Not found – ресурс не найден.
//    500 – Internal Server Error – разработчики API должны стараться избежать таких ошибок.
}
