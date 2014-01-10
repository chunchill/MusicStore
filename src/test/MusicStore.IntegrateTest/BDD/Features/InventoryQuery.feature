











Feature: InventoryQuery
	This story is set to test the Query functionality for Album、Artists、Genre
	If the User input some criterias
	we could provide the results 

@mytag
Scenario: Getting all the albums for the providing Genre and artist
	Given I have entered 'Pop' as the genre's name
	And I have entered 'Michael Jackson' as the artist's name
	When I excute getAllAlbums
	Then the result should be 'Beat It'
