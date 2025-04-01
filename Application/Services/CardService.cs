using Application.Services.Interfaces;
using Domain.DTO.Card;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Resources;
using Infraestructure.Repository.Interfaces;

namespace Application.Services;

public class CardService: ICardService
{
    private readonly ICardRepository _cardRepository;

    public CardService(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<CardResponse> CreateCardAsync(CreateCardRequest model, int userId)
    {
        var isCardExist = await _cardRepository.GetByNumberCard(model.CardNumber);

        if (isCardExist is not null)
            throw new BusinessException(GeneralMessages.CreditCardExist);

        var card = new CardEntity
        {
            CardNumber = model.CardNumber,
            Balance = model.InitialBalance ?? 0,
            UserId = userId
        };
        
        _cardRepository.Insert(card);
        
        await _cardRepository.Save();

        return new CardResponse
        {
            CardNumber = card.CardNumber,
            Balance = card.Balance
        };
    }

    public async Task<CardBalanceResponse?> GetCardBalanceAsync(string cardNumber, int userId)
    {
        var userCard = await _cardRepository.GetByNumberCard(cardNumber, userId);
        if (userCard is null)
            return null;

        return new CardBalanceResponse
        {
            Balance = userCard.Balance,
            CardNumber = userCard.CardNumber,
        };
    }
}