Region Finder

I was not too sure if Countries and Regions were the same thing. I assumed in this case that they were. Region codes = Country code.




Region Generator
This is a tool which creates N regions.
Will also generate M phone numbers

This is a stored procedure. This is stored in data folder

Region Code Criteria:
	First two digits = 00
	Random
	Unique
	
Phone Number Criteria
	Start with region code
	Unique
	

SQL Tables

Regions
	ID
	Region_Code
	Region_Name
PhoneNumbers
	Phone_Number
	Region_ID
	Line_Number
	
Database stored in data folder






Region Finder API

.net core api

Controller with endpoint
	HttpGet("DetectCountryFor")]
	IActionResult DetectCountryFor([FromQuery]string phone)
		
		
Service
	Algorithm which finds Region from phone number
	
		Retreieves Regions from DB
		Caches if not yet cached
		Loops through regions and check if quered input begins with region code
		If match, returns Region Name
	
	
Tests

