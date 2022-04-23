using BasketModuleDal;
using BasketModuleDal.Context;
using BasketModuleEntities.GeneralEntities;
using BasketModuleEntities.Models;
using BasketProductModuleBll.Concrete;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using TechTalk.SpecFlow;

namespace XUnitTests
{
    [Binding]
    public class BasketProductControllerStepDefinitions
    {
        private readonly BasketProductOperations _basketProductOperations;

        BasketProduct ResultBasketProduct;
        Action act;

        public BasketProductControllerStepDefinitions()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            var contextOptions = new DbContextOptionsBuilder<BasketModuleContext>()
                .UseSqlServer(@"Data Source=localhost\\SQLEXPRESS;Initial Catalog=BasketModuleDB;Integrated Security=False;Persist Security Info=True;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;")
                .Options;

            UnitOfWork unitOfWork = new UnitOfWork(new BasketModuleContext(contextOptions));
            _basketProductOperations = new BasketProductOperations(unitOfWork);
        }

        [When(@"Add method run with valid BasketProduct entity")]
        public void WhenAddMethodRunWithValidBasketProductEntity()
        {
            BasketProduct entity = new BasketProduct()
            {
                BasketID = 0,
                OperationDate = DateTime.Now,
                OperationType = OperationTypes.Added,
                ProductID = 1,
                Quantity = 2
            };
            ResultBasketProduct = _basketProductOperations.Add(entity);
        }

        [Then(@"Add method should add new BasketProduct entity and return to added BasketProduct item")]
        public void ThenAddMethodShouldAddNewBasketProductEntityAndReturnToAddedBasketProductItem()
        {
            ResultBasketProduct.GetType().Should().Be(typeof(BasketProduct));
            ResultBasketProduct.Should().NotBeNull();
        }

        [When(@"Null value sended to Add Method on BasketProductOperations")]
        public void WhenNullValueSendedToAddMethodOnBasketProductOperations()
        {
            act = () => _basketProductOperations.Add(null);
        }

        [Then(@"Add method should return null on sended null value on BasketProductOperations")]
        public void ThenAddMethodShouldReturnNullOnSendedNullValueOnBasketProductOperations()
        {
            act.Should().Throw<Exception>();
        }

        [When(@"BasketProduct entity with missing BasketProduct ProductID prop sended to Add Method")]
        public void WhenBasketProductEntityWithMissingBasketProductProductIDPropSendedToAddMethod()
        {
            BasketProduct entity = new BasketProduct()
            {
                BasketID = 0,
                OperationDate = DateTime.Now,
                OperationType = OperationTypes.Added,
                ProductID = 0,
                Quantity = 2
            };
            act = () => _basketProductOperations.Add(entity);
        }

        [Then(@"Add method should throw exception on BasketProduct entity with missing BasketProduct ProductID prop")]
        public void ThenAddMethodShouldThrowExceptionOnBasketProductEntityWithMissingBasketProductProductIDProp()
        {
            act.Should().Throw<Exception>();
        }
    }
}
