using Application.Services.Interfaces;

namespace Application.Services;

public class UFEService : IUFEService
{
    private decimal _UFERate = 0;
    private DateTime _lastUpdate = DateTime.MinValue;

    public async Task<decimal> GetUFE()
    {
        var now = DateTime.Now;
        
        if (_lastUpdate.Hour != now.Hour || _lastUpdate.Date != now.Date)
        {
            UpdateUFERate();
        }

        
        return _UFERate;
    }

    private void UpdateUFERate()
    {
        Random random = new Random();
        _UFERate = (decimal)(random.NextDouble() * 2 + double.Epsilon) / 100;
        _lastUpdate = DateTime.Now;
    }
}