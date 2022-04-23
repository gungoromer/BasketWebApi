using System.Collections.Generic;

namespace BasketModuleDal.Repositories.Abstract
{
    public interface IPrimitiveOperations<TEntity> where TEntity : class
    {
        TEntity Add(TEntity addEntity);
        TEntity Edit(TEntity editEntity);
        bool Delete(int entityId);
        TEntity GetWithId(int entityId);
        List<TEntity> Take(int page, int pageSize);
    }
}
