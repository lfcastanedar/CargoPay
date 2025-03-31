using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository;

public class CardRepository: ICardRepository
{
    private bool disposed = false;
    private readonly DataContext _dataContext;
    
    public CardRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<IEnumerable<CardEntity>> GetAll()
    {
        return await _dataContext.CardEntity.ToListAsync();
    }

    public async Task<CardEntity?> GetByNumberCard(string numberCard, int userId)
    {
        var result = await _dataContext.CardEntity
            .FirstOrDefaultAsync(x => x.CardNumber == numberCard && x.UserId == userId);

        return result;
    }

    public async Task<CardEntity?> GetByNumberCard(string numberCard)
    {
        var result = await _dataContext.CardEntity
            .FirstOrDefaultAsync(x => x.CardNumber == numberCard);
        
        return result;
    }

    public void Insert(CardEntity card)
    {
        _dataContext.CardEntity.Add(card);
    }

    public void Update(CardEntity card)
    {
        _dataContext.Entry(card).State = EntityState.Modified;
    }


    public async Task Save()
    {
        await _dataContext.SaveChangesAsync();
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}