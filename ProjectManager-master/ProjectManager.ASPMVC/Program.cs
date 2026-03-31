using Microsoft.Data.SqlClient;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.Common.Repositories;

namespace ProjectManager.ASPMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // CONNECTION STRING
            string connectionString = builder.Configuration.GetConnectionString("ProjectManager.Database")!;
            string sessionConnectionString = builder.Configuration.GetConnectionString("ProjectManager.Session")!;

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //SESSIONS
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<UserSessionManager>();

            // Cookies de session
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "ProjectManager.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(2);
            });
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
            });


            //AJOUT DES SERVICES
            builder.Services.AddScoped<SqlConnection>(serviceProdiver =>
        new SqlConnection(connectionString));

            //DEPENDANCES DE NOS SERVICES

            builder.Services.AddScoped<IUserRepository<BLL.Entities.User>, BLL.Services.UserService>();
            builder.Services.AddScoped<IUserRepository<DAL.Entities.User>, DAL.Services.UserService>();
            builder.Services.AddScoped<IEmployeeRepository<BLL.Entities.Employee>, BLL.Services.EmployeeService>();
            builder.Services.AddScoped<IEmployeeRepository<DAL.Entities.Employee>, DAL.Services.EmployeeService>();
            builder.Services.AddScoped<IProjectRepository<BLL.Entities.Project>, BLL.Services.ProjectService>();
            builder.Services.AddScoped<IProjectRepository<DAL.Entities.Project>, DAL.Services.ProjectService>();
            builder.Services.AddScoped<IPostRepository<BLL.Entities.Post>, BLL.Services.PostService>();
            builder.Services.AddScoped<IPostRepository<DAL.Entities.Post>, DAL.Services.PostService>();
            builder.Services.AddScoped<ITakePartRepository<BLL.Entities.TakePart>, BLL.Services.TakePartService>();
            builder.Services.AddScoped<ITakePartRepository<DAL.Entities.TakePart>, DAL.Services.TakePartService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //App SESSION ET COOKIES
            app.UseSession();
            app.UseCookiePolicy();


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
