using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PRDenaCo.Application.Interfaces.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using PRDenaCo.Application.Interfaces.FacadPatterns;
using PRDenaCo.Persistence.Contexts;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Options;
using PRDenaCo.Common.Roles;
using PRDenaCo.Application.Services.Users.Commands.UserLogin;
using PRDenaCo.Application.Services.Users.Commands.UserRegister;
using PRDenaCo.Application.Services.Users.Commands.UserActiveAccount;
using PRDenaCo.Infrastracture.EmailSender;
using PRDenaCo.Application.Services.Companies.FacadPattern;
using PRDenaCo.Application.Services.Users.Commands.UserChange;

using PRDenaCo.Application.Services.Users.Queries.GetUserAccesses;
using PRDenaCo.Application.Services.Users.Commands.UserAccess;
using PRDenaCo.Application.Services.Account.Queries.GetAccountGroup;
using PRDenaCo.Application.Services.Account.Commands.AddNewAccountGroup;
using PRDenaCo.Application.Services.Account.Commands.DeleteAccountGroup;
using PRDenaCo.Application.Services.Account.Queries.GetNature;
using PRDenaCo.Application.Services.Account.Queries.GetAccess;
using PRDenaCo.Application.Services.Account.Queries.GetAccount;
using PRDenaCo.Application.Services.Account.Commands.AddNewAccountLedger;
using PRDenaCo.Application.Services.CostCenter.Commands.IAddNewCostCenterService;

//using PRDenaCo.Application.Services.CostCenter.Queries.ListCostCenter;
//using PRDenaCo.Application.Services.Project.Queries.GetProject;

using PRDenaCo.Application.Services.Project.Commands.AddNewProject;
//using PRDenaCo.Application.Services.Project.Queries.ListProject;
using PRDenaCo.Application.Services.Common.Queries.GetListItem;
using PRDenaCo.Application.Services.MemoryCash;
using PRDenaCo.Application.Services.Users.Queries.GetUserBranchAccess;
using PRDenaCo.Application.Services.Users.Commands.UserBranchAccess;
using PRDenaCo.Application.Services.Common.Commands.UserProfile;
using PRDenaCo.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using PRDenaCo.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using PRDenaCo.Application.Services.Vouchers.JournalVoucher.Queries.GetJournalVoucher;
using PRDenaCo.Application.Services.Vouchers.PaymentVoucher.Queries.LoadPaymentlVoucher;
using PRDenaCo.Application.Services.Vouchers.ReceiptVoucher.Queries.LoadReceiptVoucher;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using PRDenaCo.Application.Services.SuffixPrefix.Commands.AddNewSuffixPrefix;
using PRDenaCo.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix;
using PRDenaCo.Application.Services.SuffixPrefix.Queries.LoadSuffixPrefix;
using PRDenaCo.Application.Services.Vouchers.JournalVoucher.Queries.NavigateJornalVoucher;
using PRDenaCo.Application.Services.ExchangeRate.Queries.GetExchangeRate;
using PRDenaCo.Application.Services.Common.Queries.GetCurrentDate;
using PRDenaCo.Application.Services.Beneficiary.Commands.AddNewBenefeciary;
using PRDenaCo.Application.Services.Beneficiary.Commands.DeleteBeneficiary;
using PRDenaCo.Application.Services.Beneficiary.Queries.GetBenefeciaries;

using PRDenaCo.Application.Services.Currencies.Commands.AddNewCurrency;
using PRDenaCo.Application.Services.Currencies.Commands.DeleteCurrency;
using PRDenaCo.Application.Services.Currencies.Queries.GetCurrencies;
using PRDenaCo.Application.Services.ExchangeRate.Commands.AddNewExchangeRate;
using PRDenaCo.Application.Services.ExchangeRate.Commands.DeleteExchangeRate;
using PRDenaCo.Application.Services.Project.Queries.GetProjects;
using PRDenaCo.Application.Services.Project.Commands.DeleteProject;
using PRDenaCo.Application.Services.CostCenter.Queries.GetCostCenters;
using PRDenaCo.Application.Services.CostCenter.DeleteCostCenter;
using PRDenaCo.Application.Services.Vouchers.Remittance.Queries.LoadRemittance;
using PRDenaCo.Application.Services.Vouchers.Remittance.Queries.GetRemittanceForInsert;
using PRDenaCo.Application.Services.Account.Commands.DeleteAccountLedger;
using PRDenaCo.Application.Services.Users.Commands.UserForgot;

namespace PRDenaCo.Web
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

            services.AddMemoryCache();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new CustomMetadataProvider()));


            services.AddMemoryCache();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new List<CultureInfo> {
        new CultureInfo("en"),
        new CultureInfo("fr")
                };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRoles.Creator, policy => policy.RequireRole(UserRoles.Creator));
                options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Auth/Login");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20.0);
                options.AccessDeniedPath = new PathString("/Auth/Login");

            });
            
            services.AddScoped<IDatabaseContext, DatabaseContext>();

            //فساد تعریف گردد
            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddScoped<IUserRegisterService, UserRegisterService>();
            services.AddScoped<IUserChangeService, UserChangeService>();
            services.AddScoped<IUserActiveAccountService, UserActiveAccountService>();
            services.AddScoped<IUserForgotService, UserForgotService>();
            services.AddScoped<IUserUpdatePassService, UserUpdatePassService>();

            services.AddScoped<IMessageSender, MessageSender>();
            services.AddScoped<IUserAccessService, UserAccessService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            

            //فساد تعریف گردد

            services.AddScoped<IGetAccountGroupService, GetAccountGroupService>();
            services.AddScoped<IAddNewAccountGroupService, AddNewAccountGroupService>();
            services.AddScoped<IGetAccountGroupInfoService, GetAccountGroupInfoService>();
            services.AddScoped<IDeleteAccountGroupService, DeleteAccountGroupService>();
            services.AddScoped<IGetNatureService, GetNatureService>();
            services.AddScoped<IGetAccessService, GetAccessService>();



            services.AddScoped<IGetListItemService, GetListItemService>();
            services.AddScoped<IFlushableMemoryCache, FlushableMemoryCache>();
            

            services.AddScoped<IGetAccountService, GetAccountService>();
            services.AddScoped<IGetAccountInfoService, GetAccountInfoService>();
            services.AddScoped<IAddNewAccountLedgerService, AddNewAccountLedgerService>();
            //Cost center
            services.AddScoped<IGetCostCenterService, GetCostCenterService>();
            services.AddScoped<IAddNewCostCenterService, AddNewCostCenterService>();
            //services.AddScoped<IListCostCenterService, ListCostCenterService>();
            services.AddScoped<IGetCostCenterInfoService, GetCostCenterInfoService>();
            services.AddScoped<IDeleteCostCenterService, DeleteCostCenterService>();



        //Cost center
            services.AddScoped<IGetProjectService, GetProjectService>();
            services.AddScoped<IAddNewProjectService, AddNewProjectService>();
            services.AddScoped<IGetProjectInfoService, GetProjectInfoService>();
            services.AddScoped<IDeleteProjectService, DeleteProjectService>();

            

            //Facade Inject
            services.AddScoped<ICompanyFacad, CompanyFacad>();

            //فساد دسترسی منوها
            services.AddScoped<IGetUserAccessesService, GetUserAccessesService>();
            
            services.AddScoped<IGetUserBranchAccessesService, GetUserBranchAccessesService>();
            services.AddScoped<IUserAccessBranchService, UserAccessBranchService>();

            
            services.AddScoped<ILoadJournalVoucherService, LoadJournalVoucherService>();
            services.AddScoped<IAddNewJournalVoucherSevice, AddNewJournalVoucherSevice>();
            
            services.AddScoped<IGetJournalVoucher, GetJournalVoucher>();

            
            services.AddScoped<ILoadPaymentVoucherService, LoadPaymentVoucherService>();
            
            services.AddScoped<ILoadReceiptVoucherService, LoadReceiptVoucherService>();

            //SuffixPrefix
            services.AddScoped<IAddNewSuffixPrefixService, AddNewSuffixPrefixService>();
            services.AddScoped<IGetSuffixPrefixInfoService, GetSuffixPrefixInfoService>();
            services.AddScoped<IGetSuffixPrefixService, GetSuffixPrefixService>();
            services.AddScoped<ILoadSuffixPrefixService, LoadSuffixPrefixService>();

            //Exchange Rate
            services.AddScoped<IGetExchangeRateService, GetExchangeRateService>();
            services.AddScoped<IGetExchangeRateInfoService, GetExchangeRateInfoService>();
            services.AddScoped<IAddNewExchangeRateService, AddNewExchangeRateService>();
            services.AddScoped<IDeleteExchangeRateService, DeleteExchangeRateService>();
            


            services.AddScoped<INavigateJornalVoucherService, NavigateJornalVoucherService>();

            //Benefeciary
            services.AddScoped<IAddNewBenefeciaryService, AddNewBenefeciaryService>();
            services.AddScoped<IDeleteBeneficiaryService, DeleteBeneficiaryService>();
            services.AddScoped<IGetBenefeciaryInfoService, GetBenefeciaryInfoService>();
            services.AddScoped<IGetBenefeciaryService, GetBenefeciaryService>();

            //Currency
            services.AddScoped<IAddNewCurrencyService, AddNewCurrencyService>();
            services.AddScoped<IDeleteCurrencyService, DeleteCurrencyService>();
            services.AddScoped<IGetCurrencyInfoService, GetCurrencyInfoService>();
            services.AddScoped<IGetCurrencyService, GetCurrencyService>();

            services.AddScoped<IGetCurrentDateService, GetCurrentDateService>();


            //Remittance
            services.AddScoped<ILoadRemittanceService, LoadRemittanceService>();
            services.AddScoped<IGetRemittanceForInsertService, GetRemittanceForInsertService>();

            
            services.AddScoped<IDeleteAccountLedgerService, DeleteAccountLedgerService>();


            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
           
            services.AddControllersWithViews();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class CustomMetadataProvider : IMetadataDetailsProvider, IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {

            if (context.Key.MetadataKind == ModelMetadataKind.Property)
            {

                context.DisplayMetadata.ConvertEmptyStringToNull = false;
            }
        }
    }
}
