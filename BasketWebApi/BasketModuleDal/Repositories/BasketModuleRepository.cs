using BasketModuleDal.Context;
using BasketModuleDal.Repositories.Abstract;
using BasketModuleDal.Repositories.Concrete;
using BasketModuleEntities.Models;
using BasketProductModuleDal.Repositories.Concrete;

namespace BasketModuleDal.Repositories
{
    public class BasketModuleRepository
    {
        public BasketModuleRepository(BasketModuleContext context)
        {
            BasketOperations = new BasketOperations(context);
            BasketProductOperations = new BasketProductOperations(context);
        }

        public IPrimitiveOperations<Basket> BasketOperations { get; set; }
        public IPrimitiveOperations<BasketProduct> BasketProductOperations { get; set; }
    }
}
