Feature: GetAllProducts

As a Greggs Fanatic
I want to be able to get the latest menu of products rather than the random static products it returns now
So that I get the most recently available products.

Scenario: Successfully get the latest products
	Given a previously implemented data access layer
	When I hit a specified endpoint to get a list of products
	Then a list of products is returned that uses the data access implementation rather than the static list it current utilises

Scenario: Data Access Fails
	Given a faulty data access layer
	When I hit a specified endpoint to get a list of products
	Then an exception is thrown
