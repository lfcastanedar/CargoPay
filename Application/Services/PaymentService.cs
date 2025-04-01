using System.Collections.Concurrent;
using Application.Services.Interfaces;
using Domain.DTO;
using Domain.DTO.Payment;
using Domain.Entities;
using Domain.Resources;
using Infraestructure.Repository.Interfaces;

namespace Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IUFEService _UFEService;
    private readonly ICardRepository _cardRepository;
    
    public PaymentService(IUFEService UFEService, ICardRepository cardRepository)
    {
        _UFEService = UFEService;
        _cardRepository = cardRepository;
    }

    public async Task<ResponseDto> PaymentAsync(PaymentAsyncRequest model, int userId)
    {
        if (model.Amount == 0)
        {
            return new ResponseDto{ IsSuccess = false, Message = GeneralMessages.PaymentAmountLessThanZero };
        }
        
        var card = await _cardRepository.GetByNumberCard(model.CardNumber, userId);
        var UFEFee = await _UFEService.GetUFE();
        var amountToPay = model.Amount + UFEFee;

        if (card.Balance < amountToPay)
        {
            return new ResponseDto{ IsSuccess = false, Message = string.Format(GeneralMessages.PaymentAmountExcede, amountToPay, card.Balance)};
        }
        
        card.Transactions.Add(new TransactionEntity
        {
            Balance = card.Balance,
            Amount = model.Amount,
            CreatedAt = DateTime.Now,
            UFE = UFEFee,
            PaymentFee = amountToPay,
            Total = card.Balance - amountToPay
        });
        
        card.Balance -= amountToPay;
        
        _cardRepository.Update(card);
        await _cardRepository.Save();
        


        return new ResponseDto{ IsSuccess = true, Message = string.Format(GeneralMessages.PaymentCompleted, amountToPay, card.Balance) };
    }
}