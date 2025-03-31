using Domain.Entities;

namespace Infraestructure.Repository.Interfaces;

public interface ICardRepository
{
    public Task<IEnumerable<CardEntity>> GetAll();
    public Task<CardEntity?> GetByNumberCard(string numberCard);
    public Task<CardEntity?> GetByNumberCard(string numberCard, int userId);
    public void Insert(CardEntity card);
    public void Update(CardEntity card);
    public Task Save();

}