Feature: Locator

Background: 
	Given a Librarian user

Scenario: Create Locator
	When creating a locator with
	| Key       | Value				  |
	| FirstName | Vinicius			  |
	| LastName	| Mamoré			  |
	| CPF		| 00011122233		  |
	| Street	| Alegrete  		  |
	| City		| Rio Grande do Sul   |
	| Number	| 157				  |
	| District	| Tiradentes		  |
	Then should create with success
