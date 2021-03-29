using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Log;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.FileSystem;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [CacheAspect]
        [LogAspect]
        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }

        [CacheAspect]
        [LogAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        [CacheAspect]
        [LogAspect]
        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
        }

        [SecuredOperation("product.add,admin,moderator")]
        [TransactionScopeAspect]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect]
        public IResult Add(CarImage carImage, IFormFile file)
        {
            var result = BusinessRules.RunBusinessRules(
                CheckImageMaxLimit(carImage.CarId),
                CheckIfCarImageIdExists(carImage.CarId));

            if (result != null) return result;

            carImage.ImagePath = new FileManagerOnDisk().Add(file, CreateNewImagePath(file));
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.ImageSuccessfullyAdded);
        }

        [SecuredOperation("product.add,admin,moderator")]
        [TransactionScopeAspect]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect]
        public IResult Update(CarImage carImage, IFormFile file)
        {
            var result = BusinessRules.RunBusinessRules(
                CheckImageMaxLimit(carImage.CarId),
                CheckIfCarImageIdExists(carImage.CarId));

            if (result != null) return result;

            var carImageToUpdate = _carImageDal.Get(c => c.Id == carImage.Id);
            carImage.CarId = carImageToUpdate.CarId;
            carImage.ImagePath =
                new FileManagerOnDisk().Update(carImageToUpdate.ImagePath, file, CreateNewImagePath(file));
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.ImageSuccessfullyUpdated);
        }

        [SecuredOperation("product.add,admin,moderator")]
        [TransactionScopeAspect]
        [CacheAspect]
        [PerformanceAspect(5)]
        [LogAspect]
        public IResult Delete(CarImage carImage)
        {
            new FileManagerOnDisk().Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.ImageSuccessfullyDeleted);
        }

        private string CreateNewImagePath(IFormFile file)
        {
            var fileInfo = new FileInfo(file.FileName);
            var newPath =
                $@"{Environment.CurrentDirectory}\Images\CarImage\UploadImages\{Guid.NewGuid()}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Year}{fileInfo.Extension}";

            return newPath;
        }

        private IResult CheckImageMaxLimit(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5) return new ErrorResult(Messages.CarImageMaxLimit);

            return new SuccessResult();
        }

        private IResult CheckIfCarImageIdExists(int ımageId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == ımageId).Any();
            if (result) return new ErrorResult(Messages.CarImageIfExists);

            return new SuccessResult();
        }
    }
}