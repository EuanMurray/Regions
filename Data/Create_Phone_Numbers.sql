/****** Object:  StoredProcedure [dbo].[sp_create_phone_numbers]    Script Date: 11/17/2021 7:00:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_create_phone_numbers]

   	@create_number	int = 20000
AS
BEGIN
	
	DECLARE @count INT;
	SET @count = 1;

	-- Get all Regions from table into new variable

	DECLARE @regions TABLE(
	[ID] [int] NOT NULL,
	[Region_Code] [nvarchar](300) NOT NULL
	);

	INSERT INTO @regions (ID, Region_Code)
	SELECT ID, Region_Code FROM Regions

	
    -- Loop for creating a phone number
	WHILE @count <= @create_number
	BEGIN

		DECLARE @phone_number nvarchar(450);
		DECLARE @region_id int;
		DECLARE @region_code nvarchar(300);

		DECLARE @max_int_value int = 2147483647;
		DECLARE @line_number int = RAND()*@max_int_value;


		SELECT TOP 1 
		@region_id = ID,
		@region_code = Region_Code
		FROM @regions
		ORDER BY NEWID()

		SET @phone_number = @region_code + CAST(@line_number as nvarchar(300));

		-- Create row
		INSERT INTO dbo.Phone_Numbers
			(
				Phone_Number,
				Region_ID,
				Line_Number
			)
		VALUES
			(
				@phone_number,
				@region_id,
				@line_number
			)

		SET @count = @count + 1;
	END
END
