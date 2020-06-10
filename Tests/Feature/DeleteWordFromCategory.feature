Feature: DeleteWordFromCategory
	I want to delete a single word 
	from a certain category

@DeleteWordFromCategory
Scenario: I want to delete a word from a category
	Given I have a word I want to delete
	And I have a link to delete the word
	When I press delete
	Then The server informs me that the code is OK
	And The word is deleted from the database
