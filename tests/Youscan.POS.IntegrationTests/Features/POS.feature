Feature: POS Terminal
	
Scenario Outline: Scan existing products
	Given I have products:
	| ProductName | SinglePrice | VolumeCount | VolumePrice |
	| A           | 1.25        | 3           | 3           |
	| B           | 4.25        |             |             |
	| C           | 1           | 6           | 5           |
	| D           | 0.75        |             |             |
	When I scan products <scannedProducts>
	Then Total price will be <totalPrice>

Examples: 
| scannedProducts | totalPrice |
| ABCDABA         | 13.25      |
| CCCCCCC         | 6          |
| ABCD            | 7.25       |
	

Scenario Outline: Scan existing products with discount card
	Given I have products:
	| ProductName | SinglePrice | VolumeCount | VolumePrice |
	| A           | 1.25        | 3           | 3           |
	| B           | 4.25        |             |             |
	| C           | 1           | 6           | 5           |
	| D           | 0.75        |             |             |
	When I scan products <scannedProducts>
	And I use discount card with 50% of discount
	Then Total price will be <totalPrice>
	And There will be <fullPrice> on discount card

Examples: 
| scannedProducts | totalPrice | fullPrice |
| ABCDABA         | 8.13       | 14        |
| CCCCCCC         | 5.5        | 7         |
| ABCD            | 3.64       | 7.25      |


Scenario Outline: Scan non-existing products
	Given I have products:
	| ProductName | SinglePrice | VolumeCount | VolumePrice |
	| A           | 1.25        | 3           | 3           |
	| B           | 4.25        |             |             |
	| C           | 1           | 6           | 5           |
	| D           | 0.75        |             |             |
	When I try to scan products <scannedProducts>
	Then I will get exception that product <notFound> doesn't exist

Examples: 
| scannedProducts | notFound |
| ABFR            | F        |
| G               | G        |
| HGK             | H        |