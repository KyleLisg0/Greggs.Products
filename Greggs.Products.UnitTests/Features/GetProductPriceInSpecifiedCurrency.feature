Feature: GetProductPriceInSpecifiedCurrency

As a Greggs Entrepreneur
I want to get the price of the products returned to me in Euros
So that I can set up a shop in Europe as part of our expansion

Scenario: Successfully get the latest products with prices
	Given an exchange rate of 1GBP to 1.11 'EUR'
	When I hit a specified endpoint to get a list of products
	Then I will get the products and their prices returned
