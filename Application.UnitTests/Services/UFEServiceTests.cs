using Application.Services;
using Application.Services.Interfaces;

namespace Application.UnitTests.Services;

public class UFEServiceTests
{
    private readonly IUFEService _ufeService;

    public UFEServiceTests()
    {
        _ufeService = new UFEService();
    }
    
    [Fact]
    public async Task GetUFE_MultipleCallsInSameHour_ReturnsSameFee()
    {
        // Act
        var firstCall = await _ufeService.GetUFE();
        var secondCall = await _ufeService.GetUFE();

        // Assert
        Assert.Equal(firstCall, secondCall);
    }
    
    [Fact]
    public async Task GetUFE_DifferentHours_UpdatesFee()
    {
        // Arrange
        var initialUFE = await _ufeService.GetUFE();
        
        var updateUFERateMethod = typeof(UFEService).GetMethod("UpdateUFERate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        updateUFERateMethod.Invoke(_ufeService, null);

        // Act
        var updatedUFE = await _ufeService.GetUFE();

        // Assert
        Assert.NotEqual(initialUFE, updatedUFE);
    }
}