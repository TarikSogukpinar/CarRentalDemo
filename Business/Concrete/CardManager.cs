using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CardManager : ICardService
    {
        private readonly ICardDal _cardDal;

        public CardManager(ICardDal cardDal)
        {
            _cardDal = cardDal;
        }

        public IDataResult<List<Card>> GetAll()
        {
            var getAll = _cardDal.GetAll();
            return new SuccessDataResult<List<Card>>(getAll);
        }

        public IDataResult<List<Card>> GetByCustomerId(int customerId)
        {
            var getByCustomerId = _cardDal.GetAll(card => card.CustomerId == customerId);
            return new SuccessDataResult<List<Card>>(getByCustomerId);
        }

        public IResult Add(Card card)
        {
            if (CheckCard(card))
            {
                return new SuccessResult();
            }

            _cardDal.Add(card);
            return new SuccessResult();
        }

        public IResult Update(Card card)
        {
            _cardDal.Update(card);
            return new SuccessResult();
        }

        public IResult Delete(Card card)
        {
            _cardDal.Delete(card);
            return new SuccessResult();
        }

        private bool CheckCard(Card checkCard)
        {
            var card = _cardDal.Get(c => c.CustomerId == checkCard.CustomerId);

            if (card == null)
            {
                return false;
            }


            if (card.CardNumber == checkCard.CardNumber)
            {
                return true;
            }

            return false;
        }
    }
}