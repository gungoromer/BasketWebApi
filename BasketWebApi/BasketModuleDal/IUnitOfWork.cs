using BasketModuleDal.Repositories;

namespace BasketModuleDal
{
    public interface IUnitOfWork
    {
        BasketModuleRepository BasketModuleRepository { get; }
        
        
        void BeginTransaction();
        void SaveChanges();
        bool EndTransaction();
        void ManuelRollback();
    }
}
