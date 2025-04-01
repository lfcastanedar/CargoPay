using Application.Services;
using Application.Services.Interfaces;
using Domain.DTO.Payment;
using Domain.Entities;
using Domain.Resources;
using Infraestructure.Repository.Interfaces;
using Moq;

namespace Application.UnitTests.Services;

public class PaymentServiceTests
{
    private readonly Mock<ICardRepository> _cardRepository;
    private readonly Mock<IUFEService> _ufeService;
    private readonly IPaymentService _paymentService;

    public PaymentServiceTests()
    {
        _cardRepository = new Mock<ICardRepository>();
        _ufeService = new Mock<IUFEService>();
        _paymentService = new PaymentService(_ufeService.Object, _cardRepository.Object);
    }
    
    [Fact]
    public async Task PaymentAsync_ValidPayment_SuccessfullyProcessesPayment()
    {
        // Arrange
        var userId = 1;
        var model = new PaymentAsyncRequest
        {
            CardNumber = "123456789",
            Amount = 30
        };

        var card = new CardEntity
        {
            CardNumber = "123456789",
            Balance = 1000
        };

        _cardRepository
            .Setup(r => r.GetByNumberCard("123456789", userId))
            .ReturnsAsync(card);

        _ufeService
            .Setup(s => s.GetUFE())
            .ReturnsAsync(100);

        // Act
        var result = await _paymentService.PaymentAsync(model, userId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Contains("Payment completed", result.Message);

        _cardRepository.Verify(r => r.Update(It.Is<CardEntity>(c => c.Balance == 870 && c.Transactions.Count == 1)), Times.Once);
        _cardRepository.Verify(r => r.Save(), Times.Once);
        _ufeService.Verify(s => s.GetUFE(), Times.Once);
    }
    
    [Fact]
    public async Task PaymentAsync_ZeroPaymentAmount_ReturnsError()
    {
        // Arrange
        var userId = 1;
        var model = new PaymentAsyncRequest
        {
            CardNumber = "123456789",
            Amount = 0
        };

        // Act
        var result = await _paymentService.PaymentAsync(model, userId);

        // Assert
        Assert.False(result.IsSuccess);
    }
}