using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarRentalContext>, ICustomerDal
    {
        public List<CustomerDetailsDto> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from customer in filter is null ? context.Customers : context.Customers.Where(filter)
                    join user in context.Users
                        on customer.UserId equals user.Id
                    select new CustomerDetailsDto
                    {
                        CompanyName = customer.CompanyName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserId = user.Id,
                        CustomerId = customer.CustomerId,
                        Status = user.Status,
                        CustomerFindexPoint = (int) customer.CustomerFindexPoint
                    };
                return result.ToList();
            }
        }

        public CustomerDetailsDto GetCustomerDetailById(Expression<Func<Customer, bool>> filter)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from customer in filter is null ? context.Customers : context.Customers.Where(filter)
                    join user in context.Users
                        on customer.UserId equals user.Id
                    select new CustomerDetailsDto
                    {
                        CompanyName = customer.CompanyName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserId = user.Id,
                        CustomerId = customer.CustomerId,
                        Status = user.Status,
                        CustomerFindexPoint = (int) customer.CustomerFindexPoint
                    };
                return result.FirstOrDefault();
            }
        }

        public CustomerDetailsDto GetCustomerByEmail(Expression<Func<CustomerDetailsDto, bool>> filter)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from customer in context.Customers
                    join user in context.Users
                        on customer.UserId equals user.Id
                    select new CustomerDetailsDto
                    {
                        CustomerId = customer.CustomerId,
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        CompanyName = customer.CompanyName,
                        CustomerFindexPoint = (int) customer.CustomerFindexPoint
                    };
                return result.SingleOrDefault(filter);
            }
        }

        public CustomerDetailsDto GetCustomerByCompany(Expression<Func<CustomerDetailsDto, bool>> filter)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from customer in context.Customers
                    join user in context.Users
                        on customer.UserId equals user.Id
                    select new CustomerDetailsDto
                    {
                        CustomerId = customer.CustomerId,
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        CompanyName = customer.CompanyName,
                        CustomerFindexPoint = (int) customer.CustomerFindexPoint
                    };
                return result.SingleOrDefault(filter);
            }
        }
    }
}