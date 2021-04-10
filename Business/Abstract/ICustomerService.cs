using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetById(int customerId);
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);
        IDataResult<List<CustomerDetailsDto>> GetCustomerDetails();

        IDataResult<CustomerDetailsDto> GetCustomerDetailById(int customerId);
        IDataResult<CustomerDetailsDto> GetCustomerByEmail(string email);
        
        IDataResult<CustomerDetailsDto> GetCustomerByCompanyName(string companyName);
    }
}