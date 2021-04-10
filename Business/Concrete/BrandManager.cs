using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Log;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == brandId));
        }

        [SecuredOperation("product.add,admin,moderator")]
        [ValidationAspect(typeof(BrandValidator))]
        [TransactionScopeAspect]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect]
        public IResult Add(Brand brand)
        {
            var result = BusinessRules.RunBusinessRules(
                CheckBrandNameIfExists(brand.BrandName));

            if (result != null) return result;

            _brandDal.Add(brand);
            return new SuccessResult(Messages.ProductSuccessfullyAdded);
        }

        [SecuredOperation("product.add,admin,moderator")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            var result = BusinessRules.RunBusinessRules(
                CheckBrandNameIfExists(brand.BrandName));

            if (result != null) return result;

            _brandDal.Update(brand);
            return new SuccessResult(Messages.ProductSuccessfullyUpdated);
        }

        [SecuredOperation("product.add,admin,moderator")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.ProductSuccessfullyDeleted);
        }

        private IResult CheckBrandNameIfExists(string brandName)
        {
            var result = _brandDal.GetAll(c => c.BrandName == brandName).Any();
            if (!result) return new SuccessResult();

            return new ErrorResult(Messages.BrandNameExists);
        }
    }
}