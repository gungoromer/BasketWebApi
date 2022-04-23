using BasketModuleDal;
using BasketModuleDal.Repositories.Abstract;
using BasketModuleEntities.Models;
using System;
using System.Collections.Generic;

namespace BasketModuleBll.Concrete
{
    public class BasketOperations : IPrimitiveOperations<Basket>
    {
        private readonly IUnitOfWork _unitOfWork;
        public BasketOperations(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Method should send new Basket entity to DAL Layer
        /// </summary>
        /// <param name="entity">New Basket Entity</param>
        /// <returns>Add Basket Entity</returns>
        public Basket Add(Basket entity)
        {
            if (entity is null || !Basket.Validator.Validate(entity).IsValid)
            {
                throw new ArgumentException("Basket entity cannot pass in validation on Add method");
            }

            Basket addedBasket = _unitOfWork.BasketModuleRepository.BasketOperations.Add(entity);

            return addedBasket;
        }

        /// <summary>
        /// Method should pass BasketId to DAL layer for delete in db
        /// </summary>
        /// <param name="entityID">BasketId</param>
        /// <returns>If Operations success return true, otherwise false</returns>
        public bool Delete(int entityID)
        {
            if (entityID <= 0)
            {
                return false;
            }

            return _unitOfWork.BasketModuleRepository.BasketOperations.Delete(entityID);
        }

        /// <summary>
        /// Method should pass Edited Basket entity to DAL Layer
        /// </summary>
        /// <param name="entity">Editted Basket Entity</param>
        /// <returns>Editted Basket Entity</returns>
        public Basket Edit(Basket entity)
        {
            if (entity is null || !Basket.Validator.Validate(entity).IsValid)
            {
                throw new ArgumentException("Basket entity cannot pass in validation on Edit method");
            }

            Basket editedEntity = _unitOfWork.BasketModuleRepository.BasketOperations.Edit(entity);

            return editedEntity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Basket GetWithId(int elementId)
        {
            if (elementId <= 0)
            {
                throw new ArgumentException("Sended Basket entity Id cannot be equal or less than zero");
            }

            return _unitOfWork.BasketModuleRepository.BasketOperations.GetWithId(elementId);
        }

        /// <summary>
        /// Method should pass params to DAL layer and Getting in range List of Baskets
        /// </summary>
        /// <param name="page">Start range</param>
        /// <param name="pageSize">Range width</param>
        /// <returns>List of Baskets</returns>
        public List<Basket> Take(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page and PageSize parameters cannot be equal or less than zero on Basket Take method");
            }

            return _unitOfWork.BasketModuleRepository.BasketOperations.Take(page, pageSize);
        }
    }
}
