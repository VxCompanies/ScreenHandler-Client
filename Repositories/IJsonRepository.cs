using System.Linq.Expressions;
using Client.Entities;
using ScreenHandler.Settings;

namespace Client.Repositories;

public interface IJsonRepository<TEntity>
 where TEntity : EntityBase
{
    void Create(TEntity entity);
    TEntity Get(int id);
    TEntity Get(Func<TEntity, bool> filter);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(Func<TEntity, bool> filter);
    void Update(int id, TEntity newEntity);
    void Update(Func<TEntity, bool> filter, TEntity newEntity);
    void Delete(int id);
    void Delete(Func<TEntity, bool> filter);
}
