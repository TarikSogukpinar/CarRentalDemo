using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {
        List<CustomerDetailsDto> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null);
        CustomerDetailsDto GetCustomerDetailById(Expression<Func<Customer, bool>> filter);

        CustomerDetailsDto GetCustomerByEmail(Expression<Func<CustomerDetailsDto, bool>> filter);
        
        CustomerDetailsDto GetCustomerByCompany(Expression<Func<CustomerDetailsDto, bool>> filter);
    }
}