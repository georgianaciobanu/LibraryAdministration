using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Reflection.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimbioMed.Configuration;
using SimbioMed.Migrator;
using SimbioMed.Sale.Dto;
using SimbioMed.Sale.DtoDiscountSale;
using SimbioMed.Sale.DtoSaleDetail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Sale {
    public class SaleAppService : SimbioMedAppServiceBase, ISaleAppService {

        private readonly IRepository<Sale> _saleUnitRepository;
        private readonly IConfigurationRoot _appConfiguration;
        public readonly IRepository<SaleDetail> _saleDetailsRepository;
        public readonly IRepository<DiscountSale> _discountSaleRepository;


        public SaleAppService(IRepository<Sale> saleRepository, IRepository<SaleDetail> saleDetailsRepository, IRepository<DiscountSale> discountSaleRepository) {
            _saleUnitRepository = saleRepository;
            _saleDetailsRepository = saleDetailsRepository;
            _discountSaleRepository = discountSaleRepository;
            _appConfiguration = AppConfigurations.Get(typeof(SimbioMedMigratorModule).GetAssembly().GetDirectoryPathOrNull());


        }
        public async Task<int> CreateSale(CreateSaleInput input) {
            input.SaleDate = DateTime.Now;
            var sale = ObjectMapper.Map<Sale>(input);
            
           int id= await _saleUnitRepository.InsertAndGetIdAsync(sale);
            foreach (CreateSaleDetailInput elem in input.Details) {
                elem.SaleId = id;
                await CreateSaleDetail(elem);
            }


            return id;
        }

        public async Task CreateSaleDetail(CreateSaleDetailInput input) {
            var saleDetail = ObjectMapper.Map<SaleDetail>(input);
            int id = 0;
           id= await _saleDetailsRepository.InsertAndGetIdAsync(saleDetail);
     
            await InsertNewSale(saleDetail.SaleId.Value);
         }

        public async Task DeleteSale(GetSaleForEditInput input) {
            await _saleUnitRepository.DeleteAsync(input.Id);
            GetSaleForEditOutput sale = await GetSaleForEdit(input);
            foreach (SaleDetailListDto saleDetail in sale.Details) {
                DeleteSaleDetail(saleDetail.Id);
            }

            foreach (DiscountSaleListDto dis in sale.Discounts) {
                DeleteDiscountSale(dis.Id);
            }
        }

        public async Task DeleteSaleDetail(int id) {
            await _saleDetailsRepository.DeleteAsync(id);
        }

        public ListResultDto<SaleListDto> GetSale(GetSaleInput input) {
            var sale = _saleUnitRepository
                            .GetAll()
                            .Include(p => p.Customer)
                            .Include(p => p.Store)   
                            .Include(p=>p.Details)
                            .WhereIf(
                                !input.Filter.IsNullOrEmpty(),
                                p => p.SaleNumber.ToLower().Contains(input.Filter.ToLower()) ||
                                     p.Store.StoreName.ToLower().Contains(input.Filter.ToLower())                                  
                            )
                            .OrderBy(p => p.SaleDate)
                            .ToList();

            var sal = new  ListResultDto<SaleListDto>(ObjectMapper.Map<List<SaleListDto>>(sale));
            foreach (SaleListDto elem in sal.Items) {
                GetSaleInput inp = new GetSaleInput();
                inp.Filter = elem.Id.ToString();
                elem.Details = GetSaleDetail(inp);
                elem.Discounts = GetDiscountSale(inp);

            }
            return sal;
        }

        public Collection<SaleDetailListDto> GetSaleDetail(GetSaleInput input) {
            var ss = _saleDetailsRepository
                 .GetAll()
                 .Include(p => p.BookUnit)
                 .WhereIf(
                     int.TryParse(input.Filter, out var x),
                     p => p.SaleId.Equals(int.Parse(input.Filter))
                 )
                 .ToList();

            return new Collection<SaleDetailListDto>(ObjectMapper.Map<List<SaleDetailListDto>>(ss));
        }

        public async Task<GetSaleForEditOutput> GetSaleForEdit(GetSaleForEditInput input) {
            var sale = await _saleUnitRepository.GetAsync(input.Id);
            var sal= ObjectMapper.Map<GetSaleForEditOutput>(sale);
            GetSaleInput inp = new GetSaleInput();
            inp.Filter = sal.Id.ToString();
            sal.Details = GetSaleDetail(inp);
            sal.Discounts = GetDiscountSale(inp);
            return sal;
        }



        public async Task InsertNewSale(int saleId) {

            var ConnectionString = _appConfiguration.GetConnectionString(SimbioMedConsts.ConnectionStringName);
            using (var con = new SqlConnection(ConnectionString)) {
                con.Close();
                con.Open();
                //SqlTransaction transaction;
                //transaction = con.BeginTransaction("SampleTransaction");
                using (var command = new SqlCommand("", con)) {
                   // command.Transaction = transaction;
                   // command.CommandTimeout = 60;
                       var sql = string.Format("exec [dbo].[InsertNewSale]  '{0}' ", saleId);
                        try {
                            command.CommandText = sql;
                            await command.ExecuteNonQueryAsync();
                          //  transaction.Commit();
                            con.Close();
                        } catch (Exception ex) {
                            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                            Console.WriteLine("  Message: {0}", ex.Message);
                            try {
                               // transaction.Rollback();
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

        public Collection<DiscountSaleListDto> GetDiscountSale(GetSaleInput input) {
            var ds = _discountSaleRepository
                  .GetAll()
                  .Include(p => p.Discount)
                  .WhereIf(
                      int.TryParse(input.Filter, out var x),
                      p => p.SaleId.Equals(int.Parse(input.Filter))
                  )
                  .ToList();

            return new Collection<DiscountSaleListDto>(ObjectMapper.Map<List<DiscountSaleListDto>>(ds));
        }

        public async Task CreateDiscountSale(CreateDiscountSaleInput input) {
            var discountSale = ObjectMapper.Map<DiscountSale>(input);
            await _discountSaleRepository.InsertAsync(discountSale);
        }

        public async Task DeleteDiscountSale(int id) {
            await _discountSaleRepository.DeleteAsync(id);
        }
    }

    }

