@DatabaseTests
Feature: DatabaseTests
	User wants to access the database
	and retreive data
	
Scenario Outline: Get chapters from a rulebook
	Given the language is '<Language>'
	And the discipline is '<Discipline>'
	When the retrieving the chapters from the database
	Then the chapters should be populated
Examples: 
| Language | Discipline |
| English  | Handgun    |
| English  | PCC        |
| English  | Rifle      |
| English  | MiniRifle  |
| English  | ActionAir  |

