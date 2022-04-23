using BasketModuleDal.Context;
using BasketModuleDal.Repositories.Abstract;
using BasketModuleDal.Repositories.Concrete;
using BasketModuleEntities.GeneralEntities;
using BasketModuleEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketProductModuleDal.Repositories.Concrete
{
    public class BasketProductOperations : IPrimitiveOperations<BasketProduct>
    {
        private readonly BasketModuleContext _context;
        private readonly BasketOperations _basketOperations;

        public BasketProductOperations(BasketModuleContext context)
        {
            _context = context;
            _basketOperations = new BasketOperations(context);
        }

        /// <summary>
        /// Method Add BasketProduct to DB
        /// </summary>
        /// <param name="addEntity">BasketProduct Entity</param>
        /// <returns>Add BasketProduct</returns>
        public BasketProduct Add(BasketProduct addEntity)
        {
            if (addEntity is null)
            {
                throw new ArgumentException("BasketProduct entity cannot pass as null in Add method on BasketProductOperations");
            }

            if (!BasketProduct.Validator.Validate(addEntity).IsValid)
            {
                throw new ArgumentException("BasketProduct entity cannot validate in Add method on BasketProductOperations");
            }

            //Aynı sepette aynı üründen varsa miktarını güncelliyoruz.
            BasketProduct ExistEntity = _context.BasketProduct.FirstOrDefault(q => q.BasketID == addEntity.BasketID && q.ProductID == addEntity.ProductID);
            if (ExistEntity != null)
            {
                ExistEntity.Quantity += addEntity.Quantity;
                return this.Edit(ExistEntity);
            }

            int UserID = 1;//Normalde bir token alınıp kullanıcının kim olduğunun bilinmesi lazım. Örnek olması açısından bu şekilde kullanıyorum.


            //BasketID gönderilmediyse yeni sepet oluşturma işlemi ile birllikte çalışmalıdır.
            if (addEntity.BasketID == 0 && addEntity.Basket == null)
            {
                Basket basket = _basketOperations.Add(new Basket()
                {
                    UserID = UserID,
                    IsActive = true
                });

                addEntity.BasketID = basket.BasketID;
            }

            _context.BasketProduct.Add(addEntity);
            _context.SaveChanges();
            _context.Entry(addEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return addEntity;
        }

        /// <summary>
        /// Method Edit existing BasketProduct entity
        /// </summary>
        /// <param name="editEntity"></param>
        /// <returns></returns>
        public BasketProduct Edit(BasketProduct editEntity)
        {
            if (editEntity is null)
            {
                throw new ArgumentException("BasketProduct entity cannot pass as null in Edit method on BasketProductsOperations");
            }

            if (BasketProduct.Validator.Validate(editEntity).IsValid)
            {
                throw new ArgumentException("BasketProduct entity cannot validate in Edit method on BasketProductsOperations");
            }


            _context.BasketProduct.Update(editEntity);
            _context.SaveChanges();
            _context.Entry(editEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return editEntity;
        }

        /// <summary>
        /// Method delete BasketProduct entity in Db and it's relations and then sync other resource if avaliable
        /// </summary>
        /// <param name="entityId">Entity Primary Key</param>
        /// <returns>If Operation success return true, otherwise return false</returns>
        public bool Delete(int entityId)
        {
            if (entityId <= 0)
            {
                return false;
            }

            List<BasketProduct> deleteBasketProducts = _context.BasketProduct.Where(g => g.BasketProductID == entityId).ToList();
            deleteBasketProducts.ForEach(g =>
            {
                g.OperationDate = DateTime.Now;
                g.OperationType = OperationTypes.Deleted;
            });

            _context.BasketProduct.UpdateRange(deleteBasketProducts);
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// This function return user in db with using BasketProduct entity Id(Primary Key)
        /// </summary>
        /// <param name="id">This param should be valid BasketProduct Id</param>
        /// <returns>BasketProduct entity</returns>
        public BasketProduct GetWithId(int entityId)
        {
            if (entityId <= 0)
            {
                throw new ArgumentException("Passed BasketProductId cannot be equal or less than zero");
            }

            BasketProduct selectedBasketProduct = _context.BasketProduct
               .Where(
                       q => q.BasketProductID == entityId &&
                       (q.OperationType != OperationTypes.Deleted && q.OperationType != OperationTypes.Unknown)
                     )
               .AsNoTracking()
               .FirstOrDefault();

            if (selectedBasketProduct is null)
            {
                throw new ArgumentException("BasketProduct cannot find in db");
            }


            return selectedBasketProduct;
        }

        /// <summary>
        /// Method take Page and PageSize return List of BasketProduct in spesific range 
        /// </summary>
        /// <param name="page">This param is start point of range</param>
        /// <param name="pageSize">This param is range of entites</param>
        /// <returns>List of BasketProducts without relations</returns>
        public List<BasketProduct> Take(int page, int pageSize)
        {
            int skipSize = (page - 1) * pageSize;

            if ((page > 0) && (pageSize > 0) && (skipSize > -1))
            {
                List<BasketProduct> entityList = _context.BasketProduct
                .Where(
                        q => (q.OperationType != OperationTypes.Deleted && q.OperationType != OperationTypes.Unknown)
                      )
                .AsNoTracking()
                .Skip(skipSize)
                .Take(pageSize)
                .ToList();
                return entityList;
            }
            else
            {
                throw new InvalidOperationException("SkipSize:" + skipSize + " Page:" + page + " PageSize:" + pageSize + " skip size or Page and PageSize values not valid in TakeWithRelations method on BasketProductOperations");
            }
        }
    }
}