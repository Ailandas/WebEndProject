Feature: GetWordsFromCategory
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@CategoryEntries
Scenario: I wanna get all words from a certain category
	Given I have a link
	When When I enter a category
	And Press enter
	Then the result should be visible
