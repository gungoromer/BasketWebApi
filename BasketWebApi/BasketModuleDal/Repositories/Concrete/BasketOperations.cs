using BasketModuleDal.Context;
using BasketModuleDal.Repositories.Abstract;
using BasketModuleEntities.GeneralEntities;
using BasketModuleEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketModuleDal.Repositories.Concrete
{
    public class BasketOperations : IPrimitiveOperations<Basket>
    {
        private readonly BasketModuleContext _context;

        public BasketOperations(BasketModuleContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method Add Basket to DB
        /// </summary>
        /// <param name="addEntity">Basket Entity</param>
        /// <returns>Add Basket</returns>
        public Basket Add(Basket addEntity)
        {
            if (addEntity is null)
            {
                throw new ArgumentException("Basket entity cannot pass as null in Add method on BasketOperations");
            }

            if (!Basket.Validator.Validate(addEntity).IsValid)
            {
                throw new ArgumentException("Basket entity cannot validate in Add method on BasketOperations");
            }

            //Zaten oluşturulmak istenen kullanıcıya ait aktif bir sepet var ise onu dönüyoruz.
            Basket basket = _context.Basket.FirstOrDefault(f => f.UserID == addEntity.UserID && f.IsActive);
            if (basket != null)
            {
                return basket;
            }

            _context.Basket.Add(addEntity);
            _context.SaveChanges();
            _context.Entry(addEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return addEntity;
        }

        /// <summary>
        /// Method Edit existing Basket entity
        /// </summary>
        /// <param name="editEntity"></param>
        /// <returns></returns>
        public Basket Edit(Basket editEntity)
        {
            if (editEntity is null)
            {
                throw new ArgumentException("Basket entity cannot pass as null in Edit method on BasketsOperations");
            }

            if (Basket.Validator.Validate(editEntity).IsValid)
            {
                throw new ArgumentException("Basket entity cannot validate in Edit method on BasketsOperations");
            }


            _context.Basket.Update(editEntity);
            _context.SaveChanges();
            _context.Entry(editEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return editEntity;
        }

        /// <summary>
        /// Method delete Basket entity in Db and it's relations and then sync other resource if avaliable
        /// </summary>
        /// <param name="entityId">Entity Primary Key</param>
        /// <returns>If Operation success return true, otherwise return false</returns>
        public bool Delete(int entityId)
        {
            if (entityId <= 0)
            {
                return false;
            }

            List<Basket> deleteBaskets = _context.Basket.Where(g => g.BasketID == entityId).ToList();
            deleteBaskets.ForEach(g =>
            {
                g.OperationDate = DateTime.Now;
                g.OperationType = OperationTypes.Deleted;
            });


            List<BasketProduct> deletedBasketProduct = _context.BasketProduct.Where(gp => gp.BasketID == entityId).ToList();
            deletedBasketProduct.ForEach(g =>
            {
                g.OperationDate = DateTime.Now;
                g.OperationType = OperationTypes.Deleted;
            });


            _context.BasketProduct.UpdateRange(deletedBasketProduct);
            _context.Basket.UpdateRange(deleteBaskets);
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// This function return user in db with using Basket entity Id(Primary Key)
        /// </summary>
        /// <param name="id">This param should be valid Basket Id</param>
        /// <returns>Basket entity</returns>
        public Basket GetWithId(int entityId)
        {
            if (entityId <= 0)
            {
                throw new ArgumentException("Passed BasketId cannot be equal or less than zero");
            }

            Basket selectedBasket = _context.Basket
               .Where(
                       q => q.BasketID == entityId &&
                       (q.OperationType != OperationTypes.Deleted && q.OperationType != OperationTypes.Unknown)
                     )
               .AsNoTracking()
               .FirstOrDefault();

            if (selectedBasket is null)
            {
                throw new ArgumentException("Basket cannot find in db");
            }


            return selectedBasket;
        }

        /// <summary>
        /// Method take Page and PageSize return List of Basket in spesific range 
        /// </summary>
        /// <param name="page">This param is start point of range</param>
        /// <param name="pageSize">This param is range of entites</param>
        /// <returns>List of Baskets without relations</returns>
        public List<Basket> Take(int page, int pageSize)
        {
            int skipSize = (page - 1) * pageSize;

            if ((page > 0) && (pageSize > 0) && (skipSize > -1))
            {
                List<Basket> entityList = _context.Basket
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
                throw new InvalidOperationException("SkipSize:" + skipSize + " Page:" + page + " PageSize:" + pageSize + " skip size or Page and PageSize values not valid in TakeWithRelations method on BasketOperations");
            }
        }
    }
}