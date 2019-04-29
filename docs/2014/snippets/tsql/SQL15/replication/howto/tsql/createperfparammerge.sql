--<snippetsp_addagentprofileparam>
DECLARE @profilename AS sysname;
DECLARE @profileid AS int;
SET @profilename = N'custom_merge';

-- Create a temporary table to hold the returned 
-- Merge Agent profiles.
CREATE TABLE #profiles (
	profile_id int, 
	profile_name sysname,
	agent_type int,
	[type] int,
	description varchar(3000),
	def_profile bit)

INSERT INTO #profiles (profile_id, profile_name, 
	agent_type, [type],description, def_profile)
	EXEC sp_help_agent_profile @agent_type = 4;

SET @profileid = (SELECT profile_id FROM #profiles 
    WHERE profile_name = @profilename);

IF (@profileid IS NOT NULL)
BEGIN
    EXEC sp_drop_agent_profile @profileid;
END
DROP TABLE #profiles

-- Add a new merge agent profile. 
EXEC sp_add_agent_profile @profile_id = @profileid OUTPUT, 
@profile_name = @profilename, @agent_type = 4, 
@description = N'custom merge profile';

-- Change the value of uploadreadchangesperbatch in the profile.
EXEC sp_change_agent_parameter @profile_id = @profileid, 
    @parameter_name = N'-UploadReadChangesPerBatch', @parameter_value = 50;

-- Add a new parameter ExchangeType the profile. 
EXEC sp_add_agent_parameter @profile_id = @profileid, 
    @parameter_name = N'-ExchangeType', @parameter_value = 1;

-- Verify the new profile. 
EXEC sp_help_agent_parameter @profileid;
GO
--</snippetsp_addagentprofileparam>


