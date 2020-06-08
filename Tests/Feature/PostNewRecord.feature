Feature: PostNewRecord
	Noriu iterpti nauja zodi,kategorija bei laika i duomenu baze

@mytag
Scenario: I want to add a new record to the database
	Given I have a word, its category and time
	And I have a link to post
	When I submit the post
	Then I get response from the server
	And The record is found in database
