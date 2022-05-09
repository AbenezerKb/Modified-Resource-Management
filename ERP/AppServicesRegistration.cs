
using ERP.Services.ReportServices;
using ERP.Services.AssetNumberServices;
using ERP.Services.BorrowServices;
using ERP.Services.BuyServices;
using ERP.Services.EquipmentCategoryServices;
using ERP.Services.IssueServices;
using ERP.Services.ItemServices;
using ERP.Services.MaintenanceServices;
using ERP.Services.NotificationServices;
using ERP.Services.PurchaseServices;
using ERP.Services.BulkPurchaseServices;
using ERP.Services.ReceiveServices;
using ERP.Services.ReturnServices;
using ERP.Services.SiteServices;
using ERP.Services.TransferServices;
using ERP.Services.ItemSiteQtyServices;
using ERP.Services.EquipmentAssetServices;
using ERP.Services.MiscServices;
using ERP.Services.FileServices;
using ERP.Services.DamageServices;

namespace ERP
{
    public static class AppServiceRegistration
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<IBorrowService, BorrowService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
            services.AddScoped<IReturnService, ReturnService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IEquipmentCategoryService, EquipmentCategoryService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAssetNumberService, AssetNumberService>();
            services.AddScoped<IItemSiteQtyService, ItemSiteQtyService>();
            services.AddScoped<IEquipmentAssetService, EquipmentAssetService>();
            services.AddScoped<IGeneralReportService, GeneralReportService>();
            services.AddScoped<IMiscService, MiscService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IBuyService, BuyService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IBulkPurchaseService, BulkPurchaseService>();
            services.AddScoped<IReceiveService, ReceiveService>();
            services.AddScoped<IDamageService, DamageService>();

        }
    }
}