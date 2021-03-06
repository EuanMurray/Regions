/****** Object:  StoredProcedure [dbo].[sp_create_regions]    Script Date: 11/17/2021 7:01:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_create_regions]
    -- Parameter for number of created regions
	@create_number	int = 5000

AS
BEGIN
    -- Loop for creating a region
	
	DECLARE @count INT;
	SET @count = 1;

	WHILE @count <= @create_number
	BEGIN
		-- Criteria
		-- First two digits = 00
		-- Random
		-- Unique

		DECLARE @region_name varchar(10);
		DECLARE @region_code nvarchar(300);
		DECLARE @max_int_value int = 2147483647;
		DECLARE @codevalue int = RAND()*@max_int_value;

		SET @region_name = 'Region' + CAST(@count as nvarchar(300));
		SET @region_code = '00' + CAST(@codevalue as nvarchar(300));

		-- Create row
		INSERT INTO dbo.Regions
			(
				Region_Name,
				Region_Code
			)
		VALUES
			(
				@region_name,
				@region_code
			)


		SET @count = @count + 1;
	END
END
