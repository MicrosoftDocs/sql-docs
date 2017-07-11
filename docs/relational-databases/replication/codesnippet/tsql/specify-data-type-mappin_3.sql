EXEC sp_helpdatatypemap 
	@source_dbms = N'ORACLE', 
	@source_version = 9,
	@source_type = N'NUMBER',
	@defaults_only = 1;
GO