using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<Rental> GetByRentId(int id);
        IDataResult<List<Rental>> GetAllRentals();
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
        
        IDataResult<List<Rental>> GetRentalByCarId(int carId);
    }
}