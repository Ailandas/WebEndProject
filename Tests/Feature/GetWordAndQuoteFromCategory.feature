Feature: GetWordAndQuoteFromCategory
	noriu gauti zodi ir quote is kategorijos (pagal dienos laika)

@GetWord
Scenario: I want to get a word and a quote
	Given I have a link with category
	When I proceed
	Then Result of the word should be returned
	And I get a word and a quote
