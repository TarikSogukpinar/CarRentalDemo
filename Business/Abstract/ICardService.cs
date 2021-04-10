using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICardService
    {
        IDataResult<List<Card>> GetAll();
        IDataResult<List<Card>> GetByCustomerId(int id);
        IResult Add(Card card);
        IResult Update(Card card);
        IResult Delete(Card card);
    }
}