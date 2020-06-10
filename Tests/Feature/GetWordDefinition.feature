Feature: GetWordDefinition
	Noriu gauti zodzio aprasyma

@WordDefinition
Scenario: Get word definition
	Given I have a link to get a word
	When I press enter button
	Then Response is returned
	And I get word with its definition
