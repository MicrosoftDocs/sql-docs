:setvar Publisher N'glengatest2'

--<snippetsp_enable_xactsetjob>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @publisher AS sysname;
SET @publisher = $(Publisher);

-- Enable the creation of transaction sets
-- at the Oracle Publisher.
EXEC sp_publisherproperty 
  @publisher = @publisher, 
  @propertyname = N'xactsetbatching', 
  @propertyvalue = N'enabled';

-- Set the job interval before enabling
-- the job, otherwise the job must be restarted.
EXEC sp_publisherproperty 
  @publisher = @publisher, 
  @propertyname = N'xactsetjobinterval', 
  @propertyvalue = N'3';

-- Enable the transaction set job.
EXEC sp_publisherproperty 
  @publisher = @publisher, 
  @propertyname = N'xactsetjob', 
  @propertyvalue = N'enabled';
GO
--</snippetsp_enable_xactsetjob>