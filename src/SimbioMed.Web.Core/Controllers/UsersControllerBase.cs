using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using SimbioMed.Authorization.Users.Dto;
using SimbioMed.Storage;
using Abp.BackgroundJobs;
using SimbioMed.Authorization;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Session;
using SimbioMed.Authorization.Users.Importing;
using SimbioMed.BookUnit;
using Abp.Domain.Repositories;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SimbioMed.Migrator;
using SimbioMed.Configuration;
using Abp.Reflection.Extensions;

namespace SimbioMed.Web.Controllers
{
    public abstract class UsersControllerBase : SimbioMedControllerBase
    {
        protected readonly IBinaryObjectManager BinaryObjectManager;
        protected readonly IBackgroundJobManager BackgroundJobManager;
        private readonly IConfigurationRoot _appConfiguration;


        protected UsersControllerBase(
            IBinaryObjectManager binaryObjectManager,
            IBackgroundJobManager backgroundJobManager)
        {
            BinaryObjectManager = binaryObjectManager;
            BackgroundJobManager = backgroundJobManager;
            _appConfiguration = AppConfigurations.Get(typeof(SimbioMedMigratorModule).GetAssembly().GetDirectoryPathOrNull());
        }

        [HttpPost]
        [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users_Create)]
        public async Task<JsonResult> ImportFromExcel()
        {
           // try
           // {
                var file = Request.Form.Files.First();

                if (file == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                if (file.Length > 1048576 * 100) //100 MB
                {
                    throw new UserFriendlyException(L("File_SizeLimit_Error"));
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var tenantId = AbpSession.TenantId;
                var fileObject = new BinaryObject(tenantId, fileBytes, $"{DateTime.UtcNow} import from excel file.");

                await BinaryObjectManager.SaveAsync(fileObject);
               await GetUserListFromExcelOrNullAsync(fileObject);

                //await BackgroundJobManager.EnqueueAsync<ImportUsersToExcelJob, ImportUsersFromExcelJobArgs>(new ImportUsersFromExcelJobArgs
                //{
                //    TenantId = tenantId,
                //    BinaryObjectId = fileObject.Id,
                //    User = AbpSession.ToUserIdentifier()
                //});



               return Json(new AjaxResponse(new { }));
            //}
            //catch (UserFriendlyException ex)
            //{
            //    return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            //}
        }

        public async Task GetUserListFromExcelOrNullAsync(BinaryObject args) {
            var file = await BinaryObjectManager.GetOrNullAsync(args.Id);
            string filePath = "C:\\Users\\Quokka\\Desktop\\Emp.xlsx";
            System.IO.File.WriteAllBytes(filePath, args.Bytes);
            await ImportBooksFromExcel();

        }

        public async Task ImportBooksFromExcel() {

            var ConnectionString = _appConfiguration.GetConnectionString(SimbioMedConsts.ConnectionStringName);
            using (var con = new SqlConnection(ConnectionString)) {
                con.Close();
                con.Open();
                SqlTransaction transaction;
                transaction = con.BeginTransaction("SampleTransaction");
                using (var command = new SqlCommand("", con)) {
                    command.Transaction = transaction;
                    var sql = string.Format("exec [dbo].[ImportBooksFromExcel] ");
                    try {
                        command.CommandText = sql;
                        await command.ExecuteNonQueryAsync();
                        transaction.Commit();
                        con.Close();
                    } catch (Exception ex) {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);
                        try {
                            transaction.Rollback();
                            con.Close();
                        } catch (Exception ex2) {
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            Console.WriteLine("  Message: {0}", ex2.Message);
                            con.Close();
                        }
                    }
                }

            }
        }
    }
}
