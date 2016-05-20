DECLARE @CodeValue VARCHAR(40);
DECLARE @EntityTypeKey INT;
DECLARE @error VARCHAR(MAX);

-- Begin insert into [EntityTypes] table
	-- Begin insert of INDIVIDUAL record into [EntityTypes] table
		SET @CodeValue = 'INDIVIDUAL';
		IF NOT EXISTS
		(SELECT 1 FROM [EntityTypes]
			WHERE Upper(Code)=@CodeValue)
		BEGIN
			INSERT INTO [dbo].[EntityTypes]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Individual'
					   ,'Individual'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of INDIVIDUAL record into [EntityTypes] table

	-- Begin insert of RESOURCE record into [EntityTypes] table
		SET @CodeValue = 'RESOURCE'
		IF NOT EXISTS
		(SELECT 1 FROM [EntityTypes]
			WHERE Upper(Code)=@CodeValue)
		BEGIN
			INSERT INTO [dbo].[EntityTypes]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Resource'
					   ,'Resource'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of RESOURCE record into [EntityTypes] table
-- End insert into [EntityTypes] table

-- Begin insert into [EntitySubtypes] table
	-- Begin insert of EMPLOYEE record into [EntitySubtypes] table
		SET @CodeValue = 'EMPLOYEE'
		SET @EntityTypeKey = (SELECT EntityTypeKey FROM EntityTypes WHERE Upper(Code)=Upper('INDIVIDUAL'));

		IF NOT EXISTS
		(SELECT 1 FROM [EntitySubtypes]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[EntitySubtypes]
						([EntityTypeKey]
						,[Code]
						,[DescShort]
						,[DescLong]
						,[ActiveFlag]
						,[CreatedBy]
						,[CreatedDt]
						,[UpdatedBy]
						,[UpdatedDt])
					VALUES
						(@EntityTypeKey
						,@CodeValue
						,'Employee'
						,'Employee'
						,1
						,0
						,getdate()
						,0
						,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of EMPLOYEE record into [EntitySubtypes] table

	-- Begin insert of MANAGER record into [EntitySubtypes] table
		SET @CodeValue = 'MANAGER'
		SET @EntityTypeKey = (SELECT EntityTypeKey FROM EntityTypes WHERE Upper(Code)=Upper('INDIVIDUAL'));

		IF NOT EXISTS
		(SELECT 1 FROM [EntitySubtypes]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[EntitySubtypes]
						([EntityTypeKey]
						,[Code]
						,[DescShort]
						,[DescLong]
						,[ActiveFlag]
						,[CreatedBy]
						,[CreatedDt]
						,[UpdatedBy]
						,[UpdatedDt])
					VALUES
						(@EntityTypeKey
						,@CodeValue
						,'Manager'
						,'Manager'
						,1
						,0
						,getdate()
						,0
						,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of MANAGER record into [EntitySubtypes] table

	-- Begin insert of Conference Room record into [EntitySubtypes] table
		SET @CodeValue = 'CONFROOM'
		SET @EntityTypeKey = (SELECT EntityTypeKey FROM EntityTypes WHERE Upper(Code)=Upper('RESOURCE'));

		IF NOT EXISTS
		(SELECT 1 FROM [EntitySubtypes]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[EntitySubtypes]
						([EntityTypeKey]
						,[Code]
						,[DescShort]
						,[DescLong]
						,[ActiveFlag]
						,[CreatedBy]
						,[CreatedDt]
						,[UpdatedBy]
						,[UpdatedDt])
					VALUES
						(@EntityTypeKey
						,@CodeValue
						,'Conference Room'
						,'Conference Room'
						,1
						,0
						,getdate()
						,0
						,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of Conference Room record into [EntitySubtypes] table

	-- Begin insert of Restroom record into [EntitySubtypes] table
		SET @CodeValue = 'RESTROOM'
		SET @EntityTypeKey = (SELECT EntityTypeKey FROM EntityTypes WHERE Upper(Code)=Upper('RESOURCE'));

		IF NOT EXISTS
		(SELECT 1 FROM [EntitySubtypes]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[EntitySubtypes]
						([EntityTypeKey]
						,[Code]
						,[DescShort]
						,[DescLong]
						,[ActiveFlag]
						,[CreatedBy]
						,[CreatedDt]
						,[UpdatedBy]
						,[UpdatedDt])
					VALUES
						(@EntityTypeKey
						,@CodeValue
						,'Restroom'
						,'Restroom'
						,1
						,0
						,getdate()
						,0
						,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of Restroom record into [EntitySubtypes] table
-- End insert into [EntitySubtypes] table

-- Begin insert into [Buildings] table
	-- Begin insert of LPN record into [Buildings] table
		SET @CodeValue = 'LPN'

		IF NOT EXISTS
		(SELECT 1 FROM [Buildings]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[Buildings]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Lincoln Plaza North'
					   ,'Lincoln Plaza North'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())

		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of LPN record into [Buildings] table

	-- Begin insert of LPW record into [Buildings] table
		SET @CodeValue = 'LPW'

		IF NOT EXISTS
		(SELECT 1 FROM [Buildings]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[Buildings]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Lincoln Plaza West'
					   ,'Lincoln Plaza West'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())

		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of LPW record into [Buildings] table

	-- Begin insert of LPE record into [Buildings] table
		SET @CodeValue = 'LPE'

		IF NOT EXISTS
		(SELECT 1 FROM [Buildings]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[Buildings]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Lincoln Plaza East'
					   ,'Lincoln Plaza East'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())

		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of LPE record into [Buildings] table

	-- Begin insert of LPS record into [Buildings] table
		SET @CodeValue = 'LPS'

		IF NOT EXISTS
		(SELECT 1 FROM [Buildings]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[Buildings]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Lincoln Plaza South'
					   ,'Lincoln Plaza South'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())

		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of LPS record into [Buildings] table

-- End insert into [Buildings] table

-- Begin insert into [AccessDoors] table
	-- Begin insert of Access Door 1 record into [AccessDoors] table
		SET @CodeValue = 'AD1'

		IF NOT EXISTS
		(SELECT 1 FROM [AccessDoors]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[AccessDoors]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Access Door 1'
					   ,'Access Door 1'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of Access Door 1 record into [AccessDoors] table
	
	-- Begin insert of Access Door 2 record into [AccessDoors] table
		SET @CodeValue = 'AD2'

		IF NOT EXISTS
		(SELECT 1 FROM [AccessDoors]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[AccessDoors]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Access Door 2'
					   ,'Access Door 2'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of Access Door 2 record into [AccessDoors] table

	-- Begin insert of Access Door 3 record into [AccessDoors] table
		SET @CodeValue = 'AD3'

		IF NOT EXISTS
		(SELECT 1 FROM [AccessDoors]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[AccessDoors]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Access Door 3'
					   ,'Access Door 3'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of Access Door 3 record into [AccessDoors] table
-- End insert into [AccessDoors] table

-- Begin insert into [EmailTypes] table
	-- Begin insert of PERSONAL record into [EmailTypes] table
		SET @CodeValue = 'PERSONAL'

		IF NOT EXISTS
		(SELECT 1 FROM [EmailTypes]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[AccessDoors]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Personal'
					   ,'Personal'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of PERSONAL record into [EmailTypes] table

	-- Begin insert of BUSINESS record into [EmailTypes] table
		SET @CodeValue = 'BUSINESS'

		IF NOT EXISTS
		(SELECT 1 FROM [EmailTypes]
			WHERE Upper(Code)=Upper(@CodeValue))
		BEGIN
			INSERT INTO [dbo].[AccessDoors]
					   ([Code]
					   ,[DescShort]
					   ,[DescLong]
					   ,[ActiveFlag]
					   ,[CreatedBy]
					   ,[CreatedDt]
					   ,[UpdatedBy]
					   ,[UpdatedDt])
				 VALUES
					   (@CodeValue
					   ,'Business'
					   ,'Business'
					   ,1
					   ,0
					   ,getdate()
					   ,0
					   ,getdate())
		END
			ELSE
				BEGIN
					SET @error = 'Code value: ' + @CodeValue + ' already exists.'
					RAISERROR ( @error,16,2 ) WITH SETERROR;
				END
	-- End insert of BUSINESS record into [EmailTypes] table
-- End insert into [EmailTypes] table

