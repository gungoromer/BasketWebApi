using BasketModuleDal;
using BasketModuleEntities.GeneralEntities;
using BasketModuleEntities.Models;
using BasketProductModuleBll.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BasketProductWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketProductController : ControllerBase
    {
        private BasketModuleResponse basketModuleResponse;

        private readonly BasketProductOperations _basketOperations;
        public BasketProductController(IUnitOfWork unitOfWork)
        {
            _basketOperations = new BasketProductOperations(unitOfWork);
        }

        // GET: api/BasketProduct
        [HttpGet]
        public IActionResult GetBasketProduct(int page, int pageSize)
        {
            List<BasketProduct> list = _basketOperations.Take(page, pageSize);

            if (list == null)
            {
                basketModuleResponse = new BasketModuleResponse()
                {
                    Status = StatusType.Error,
                    Message = "Unexpected Error"
                };
                return new BadRequestObjectResult(basketModuleResponse);
            }

            basketModuleResponse = new BasketModuleResponse()
            {
                Status = StatusType.Success,
                SuccessObject = list
            };
            return new OkObjectResult(basketModuleResponse);
        }

        // GET: api/BasketProduct/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            BasketProduct entity = _basketOperations.GetWithId(id);
            if (entity == null)
            {
                basketModuleResponse = new BasketModuleResponse()
                {
                    Status = StatusType.Error,
                    Message = "Unexpected Error"
                };
                return new BadRequestObjectResult(basketModuleResponse);
            }

            basketModuleResponse = new BasketModuleResponse()
            {
                Status = StatusType.Success,
                SuccessObject = entity
            };
            return new OkObjectResult(basketModuleResponse);
        }

        // POST: api/BasketProduct
        [HttpPost("add")]
        public IActionResult AddBasketProduct([FromBody] BasketProduct sendedBasketProduct)
        {
            if (!BasketProduct.Validator.Validate(sendedBasketProduct).IsValid)
            {
                basketModuleResponse = new BasketModuleResponse()
                {
                    Status = StatusType.Error,
                    Message = BasketProduct.Validator.Validate(sendedBasketProduct).ToString(",")
                };
                return new BadRequestObjectResult(basketModuleResponse);
            }

            BasketProduct entity = _basketOperations.Add(sendedBasketProduct);
            if (entity == null)
            {
                basketModuleResponse = new BasketModuleResponse()
                {
                    Status = StatusType.Error,
                    Message = "Unexpected Error"
                };
                return new BadRequestObjectResult(basketModuleResponse);
            }

            basketModuleResponse = new BasketModuleResponse()
            {
                Status = StatusType.Success,
                SuccessObject = entity
            };
            return new OkObjectResult(basketModuleResponse);
        }

        // POST: api/BasketProduct
        [HttpPost("edit")]
        public IActionResult EditBasketProduct([FromBody] BasketProduct sendedBasketProduct)
        {
            if (!BasketProduct.Validator.Validate(sendedBasketProduct).IsValid)
            {
                basketModuleResponse = new BasketModuleResponse()
                {
                    Status = StatusType.Error,
                    Message = BasketProduct.Validator.Validate(sendedBasketProduct).Errors.ToString()
                };
                return new BadRequestObjectResult(basketModuleResponse);
            }

            BasketProduct entity = _basketOperations.Edit(sendedBasketProduct);
            if (entity == null)
            {
                basketModuleResponse = new BasketModuleResponse()
                {
                    Status = StatusType.Error,
                    Message = "Unexpected Error"
                };
                return new BadRequestObjectResult(basketModuleResponse);
            }

            basketModuleResponse = new BasketModuleResponse()
            {
                Status = StatusType.Success,
                SuccessObject = entity
            };
            return new OkObjectResult(basketModuleResponse);
        }

        // DELETE: api/BasketProduct/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = _basketOperations.Delete(id);

            if (result)
            {
                BasketModuleResponse basketModuleResponse = new BasketModuleResponse()
                {
                    Status = StatusType.Success,
                    SuccessObject = result
                };
                return new OkObjectResult(basketModuleResponse);
            }
            else
            {
                basketModuleResponse = new BasketModuleResponse()
                {
                    Status = StatusType.Error,
                    Message = "",
                };
                return new BadRequestObjectResult(basketModuleResponse);
            }
        }
    }
}
