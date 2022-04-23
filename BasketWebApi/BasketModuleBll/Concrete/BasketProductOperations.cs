using BasketModuleDal;
using BasketModuleDal.Repositories.Abstract;
using BasketModuleEntities.Models;
using System;
using System.Collections.Generic;

namespace BasketProductModuleBll.Concrete
{
    public class BasketProductOperations : IPrimitiveOperations<BasketProduct>
    {
        private readonly IUnitOfWork _unitOfWork;
        public BasketProductOperations(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Method should send new BasketProduct entity to DAL Layer
        /// </summary>
        /// <param name="entity">New BasketProduct Entity</param>
        /// <returns>Add BasketProduct Entity</returns>
        public BasketProduct Add(BasketProduct entity)
        {
            if (entity is null || !BasketProduct.Validator.Validate(entity).IsValid)
            {
                throw new ArgumentException("BasketProduct entity cannot pass in validation on Add method");
            }

            BasketProduct addedBasketProduct = _unitOfWork.BasketModuleRepository.BasketProductOperations.Add(entity);

            return addedBasketProduct;
        }

        /// <summary>
        /// Method should pass BasketProductId to DAL layer for delete in db
        /// </summary>
        /// <param name="entityID">BasketProductId</param>
        /// <returns>If Operations success return true, otherwise false</returns>
        public bool Delete(int entityID)
        {
            if (entityID <= 0)
            {
                return false;
            }

            return _unitOfWork.BasketModuleRepository.BasketProductOperations.Delete(entityID);
        }

        /// <summary>
        /// Method should pass Edited BasketProduct entity to DAL Layer
        /// </summary>
        /// <param name="entity">Editted BasketProduct Entity</param>
        /// <returns>Editted BasketProduct Entity</returns>
        public BasketProduct Edit(BasketProduct entity)
        {
            if (entity is null || !BasketProduct.Validator.Validate(entity).IsValid)
            {
                throw new ArgumentException("BasketProduct entity cannot pass in validation on Edit method");
            }

            BasketProduct editedEntity = _unitOfWork.BasketModuleRepository.BasketProductOperations.Edit(entity);

            return editedEntity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public BasketProduct GetWithId(int elementId)
        {
            if (elementId <= 0)
            {
                throw new ArgumentException("Sended BasketProduct entity Id cannot be equal or less than zero");
            }

            return _unitOfWork.BasketModuleRepository.BasketProductOperations.GetWithId(elementId);
        }

        /// <summary>
        /// Method should pass params to DAL layer and Getting in range List of BasketProducts
        /// </summary>
        /// <param name="page">Start range</param>
        /// <param name="pageSize">Range width</param>
        /// <returns>List of BasketProducts</returns>
        public List<BasketProduct> Take(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page and PageSize parameters cannot be equal or less than zero on BasketProduct Take method");
            }

            return _unitOfWork.BasketModuleRepository.BasketProductOperations.Take(page, pageSize);
        }
    }
}
