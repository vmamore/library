Feature: Librarian

Background: 
	Given a Librarian user

Scenario: Create Librarian
	When creating a librarian with
	| Key       | Value				  |
	| FirstName | Vinicius			  |
	| LastName	| Mamoré			  |
	| CPF		| 00012422233		  |
	| Street	| Alegrete  		  |
	| City		| Rio Grande do Sul   |
	| Number	| 157				  |
	| District	| Tiradentes		  |
	Then should create with success
