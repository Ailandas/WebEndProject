Feature: DeleteCategory
	Noriu istrinti kategorija

@deletion
Scenario: I want to delete a category
	Given I have a category i want to delete
	And I have a link to delete the category
	When I proceed to delete
	Then Server returns status OK
	And The record is deleted