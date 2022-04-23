using System.Collections.Generic;

namespace BasketModuleBll.Abstract
{
    public interface IBllPrimitiveOperations<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Edit(TEntity entity);
        bool Delete(int entityID);
        TEntity GetWithId(int elementId);
        List<TEntity> Take(int page, int pageSize);
    }
}
