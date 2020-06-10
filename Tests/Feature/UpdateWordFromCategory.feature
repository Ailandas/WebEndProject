Feature: UpdateWordFromCategory
	I want to update a certain word from category

@UpdateWord
Scenario: I want to update a word in a category
	Given I have word I would like to update
	And I have a link to update the word
	When I press submit my request to update
	Then the server should return a response
	And the word is updated
