using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using SimbioMed.Authorization;
using SimbioMed.Customer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Customer {



    public class CustomerAppService: SimbioMedAppServiceBase, ICustomerAppService {

        private readonly IRepository<Customer> _customerRepository;


        public CustomerAppService(IRepository<Customer> customerRepository) {
            _customerRepository = customerRepository;

        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_EditCustomer)]

        public async Task CreateCustomer(CreateCustomerInput input) {
            var customer = ObjectMapper.Map<Customer>(input);

            await _customerRepository.InsertAsync(customer);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_EditCustomer)]

        public async Task DeleteCustomer(EntityDto input) {
            await _customerRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer)]
        [AbpAuthorize(AppPermissions.Pages_Tenant_City)]
        [AbpAuthorize(AppPermissions.Pages_Tenant_Discount)]
        public ListResultDto<CustomerListDto> GetCustomer(GetCustomerInput input) {
            var customer = _customerRepository
                            .GetAll()
                            .Include(p => p.City)
                            .WhereIf(
                                !input.Filter.IsNullOrEmpty(),
                                p => p.FirstName.ToLower().Contains(input.Filter.ToLower()) ||
                                     p.LastName.ToLower().Contains(input.Filter.ToLower())

                            )
                            .OrderBy(p => p.LastName)
                            .ToList();

            return new ListResultDto<CustomerListDto>(ObjectMapper.Map<List<CustomerListDto>>(customer));
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_EditCustomer)]

        public async Task EditCustomer(EditCustomerInput input) {
            var customer = await _customerRepository.GetAsync(input.Id);

            customer.FirstName = input.FirstName;
            customer.LastName = input.LastName;
            customer.CityId = input.CityId;
            customer.DateOfBirth = input.DateOfBirth;
            customer.Email = input.Email;
            customer.Phone = input.Phone;



            await _customerRepository.UpdateAsync(customer);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_EditCustomer)]

        public async Task<GetCustomerForEditOutput> GetCustomerForEdit(GetCustomerForEditInput input) {
            var customer = await _customerRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetCustomerForEditOutput>(customer);
        }
    }

}
