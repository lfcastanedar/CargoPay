using Application.Services;
using Application.Services.Interfaces;
using Domain.DTO.Card;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Resources;
using Infraestructure.Repository.Interfaces;
using Moq;

namespace Application.UnitTests.Services;

public class CardServiceTests
{
    private readonly Mock<ICardRepository> _cardRepository;
    private readonly ICardService _cardService;

    public CardServiceTests()
    {
        _cardRepository = new Mock<ICardRepository>();
        _cardService = new CardService(_cardRepository.Object);
    }
    
    [Fact]
    public async Task CreateCardAsync_ValidModel_CreatesCardSuccessfully()
    {
        // Arrange
        var userId = 1;
        var model = new CreateCardRequest
        {
            CardNumber = "123456789",
            InitialBalance = 1000
        };

        _cardRepository
            .Setup(r => r.GetByNumberCard(model.CardNumber))
            .ReturnsAsync((CardEntity?)null); 

        // Act
        var result = await _cardService.CreateCardAsync(model, userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.CardNumber, result.CardNumber);
        Assert.Equal(model.InitialBalance.GetValueOrDefault(), result.Balance);

        _cardRepository.Verify(r => r.GetByNumberCard(model.CardNumber), Times.Once);
        _cardRepository.Verify(r => r.Insert(It.Is<CardEntity>(c =>
            c.CardNumber == model.CardNumber &&
            c.Balance == model.InitialBalance &&
            c.UserId == userId
        )), Times.Once);
        _cardRepository.Verify(r => r.Save(), Times.Once);
    }
    
    [Fact]
    public async Task CreateCardAsync_DuplicateCard_ThrowsBusinessException()
    {
        // Arrange
        var userId = 1;
        var model = new CreateCardRequest
        {
            CardNumber = "123456789",
            InitialBalance = 500
        };

        _cardRepository
            .Setup(r => r.GetByNumberCard(model.CardNumber))
            .ReturnsAsync(new CardEntity
            {
                CardNumber = model.CardNumber,
                Balance = 1000,
                UserId = userId
            }); 

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(
            () => _cardService.CreateCardAsync(model, userId)
        );

        Assert.Equal(GeneralMessages.CreditCardExist, exception.Message);
        _cardRepository.Verify(r => r.GetByNumberCard(model.CardNumber), Times.Once);
        _cardRepository.Verify(r => r.Insert(It.IsAny<CardEntity>()), Times.Never);
        _cardRepository.Verify(r => r.Save(), Times.Never);
    }
}