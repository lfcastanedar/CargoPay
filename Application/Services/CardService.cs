using Domain.Services.Interfaces.Interfaces;
using Infraestructure.Core.DTO.Card;
using Infraestructure.Core.Entities;

namespace Domain.Services.Interfaces;

public class CardService: ICardService
{
    public async Task<object> CreateCardAsync(CreateCardRequest model, int userId)
    {
        var card = new CardEntity
        {
            CardNumber = model.CardNumber,
            Balance = model.InitialBalance ?? 0,
        };
        
        throw new NotImplementedException();
    }

    public Task<object> GetCardBalanceAsync(string cardNumber, int userId)
    {
        throw new NotImplementedException();
    }
}