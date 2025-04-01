using Application.Services.Interfaces;

namespace Application.Services;

public class UFEService : IUFEService
{
    private decimal _UFERandom = 0;
    private decimal _initialFee = 100;
    private DateTime _lastUpdate = DateTime.MinValue;

    public Task<decimal> GetUFE()
    {
        var now = DateTime.Now;
        
        if (_lastUpdate.Hour != now.Hour || _lastUpdate.Date != now.Date)
        {
            UpdateUFERate();
        }

        
        return Task.FromResult(_initialFee);
    }

    private void UpdateUFERate()
    {
        Random random = new Random();
        _UFERandom = (decimal)(random.NextDouble() * 2 + double.Epsilon);
        _lastUpdate = DateTime.Now;

        _initialFee = _UFERandom * _initialFee;
    }
}