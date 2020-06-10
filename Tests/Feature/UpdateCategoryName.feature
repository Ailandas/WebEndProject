Feature: UpdateCategoryName
	I want to update a category name

@UpdateCategory
Scenario: I want to update all categories with a same name
	Given I have a new category name 
	And I have a link to update category
	When I submit my update
	Then The server informs me that the status is ok
	And The category name has changed
