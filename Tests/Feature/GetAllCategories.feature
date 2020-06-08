Feature: GetCategories
	Noriu gauti kategorijas

@GetAllCategories
Scenario: I want to get a list of all available categories
Given I have a link to all categories
When I press enter
Then Result should be returned
And I get a list of categories
