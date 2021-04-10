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
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [CacheAspect]
        [LogAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
        }
        
        public IDataResult<List<Car>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.CarId == categoryId));
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [TransactionScopeAspect]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect]
        public IResult Add(Car car)
        {
            var result = BusinessRules.RunBusinessRules(
                CheckCarNameIfExists(car.CarName));

            if (result != null) return result;
            
            _carDal.Add(car);
            return new SuccessResult(Messages.ProductSuccessfullyAdded);
        }


        [ValidationAspect(typeof(CarValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            var result = BusinessRules.RunBusinessRules(
                CheckCarNameIfExists(car.CarName));

            if (result != null) return result;

            _carDal.Update(car);
            return new SuccessResult(Messages.ProductSuccessfullyUpdated);
        }

        [SecuredOperation("product.add,admin,moderator")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.ProductSuccessfullyDeleted);
        }

   

        //Business Rules
        private IResult CheckCarNameIfExists(string carName)
        {
            var result = _carDal.GetAll(c => c.CarName == carName).Any();
            if (result) return new ErrorResult(Messages.CarNameExists);

            return new SuccessResult();
        }
    }
}