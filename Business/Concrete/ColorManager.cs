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
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId));
        }

        [SecuredOperation("product.add,admin,moderator")]
        [ValidationAspect(typeof(ColorValidator))]
        [TransactionScopeAspect]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect]
        public IResult Add(Color color)
        {
            var result = BusinessRules.RunBusinessRules(
                CheckColorNameIfExists(color.ColorName));

            if (result != null) return result;

            _colorDal.Add(color);
            return new SuccessResult(Messages.ProductSuccessfullyAdded);
        }

        [ValidationAspect(typeof(ColorValidator))]
        [SecuredOperation("product.add,admin,moderator")]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.ProductSuccessfullyUpdated);
        }

        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ProductSuccessfullyDeleted);
        }

        private IResult CheckColorNameIfExists(string colorName)
        {
            var result = _colorDal.GetAll(c => c.ColorName == colorName).Any();
            if (!result) return new SuccessResult();

            return new ErrorResult(Messages.ColorNameExists);
        }
    }
}