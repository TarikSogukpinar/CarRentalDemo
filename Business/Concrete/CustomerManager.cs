using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Log;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == customerId));
        }

        [SecuredOperation("product.add,admin,moderator")]
        [ValidationAspect(typeof(CustomerValidator))]
        [TransactionScopeAspect]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult();
        }

        public IResult Update(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult();
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult();
        }

        public IDataResult<List<CustomerDetailsDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailsDto>>(_customerDal.GetCustomerDetails());
        }

        public IDataResult<CustomerDetailsDto> GetCustomerDetailById(int customerId)
        {
            return new SuccessDataResult<CustomerDetailsDto>(
                _customerDal.GetCustomerDetailById(c => c.CustomerId == customerId));
        }

        public IDataResult<CustomerDetailsDto> GetCustomerByEmail(string email)
        {
            return new SuccessDataResult<CustomerDetailsDto>(_customerDal.GetCustomerByEmail(c => c.Email == email));
        }

        public IDataResult<CustomerDetailsDto> GetCustomerByCompanyName(string companyName)
        {
            return new SuccessDataResult<CustomerDetailsDto>(_customerDal.GetCustomerByCompany(c => c.CompanyName == companyName));
        }
    }
}