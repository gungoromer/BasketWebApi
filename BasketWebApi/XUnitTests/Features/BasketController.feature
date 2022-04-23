Feature: BasketProductController

Normal senaryo

@NormalCase-Add
Scenario: [Add method can take valid new BasketProduct entity and method will add this BasketProduct to db]	
	When Add method run with valid BasketProduct entity
	Then Add method should add new BasketProduct entity and return to added BasketProduct item
@ExceptionalCase-Add
Scenario: Add method cannot take null as method property on BasketProductOperations
	When Null value sended to Add Method on BasketProductOperations
	Then Add method should return null on sended null value on BasketProductOperations
@ExceptionalCase-Add
Scenario: Add method cannot take BasketProduct entity with missing ProductID
	When BasketProduct entity with missing BasketProduct ProductID prop sended to Add Method
	Then Add method should throw exception on BasketProduct entity with missing BasketProduct ProductID prop
