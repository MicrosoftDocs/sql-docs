--:setvar Password Secure$Password

-----------------------------------------------------------------
-- View and change distpub properties.
-----------------------------------------------------------------
--<snippetsp_helpdistributor>
-- View information about the Distributor, distribution database, 
-- working directory, and SQL Server Agent user account. 
USE master
EXEC sp_helpdistributor;
GO
--</snippetsp_helpdistributor>

--<snippetsp_helpdistributiondb>
-- View information about the specified distribution database. 
USE distribution
EXEC sp_helpdistributiondb;
GO
--</snippetsp_helpdistributiondb>

--<snippetsp_changedistributor_property>

-- Change the heartbeat interval at the Distributor to 5 minutes. 
USE master 
exec sp_changedistributor_property 
    @property = N'heartbeat_interval', 
    @value = 5;
GO
--</snippetsp_changedistributor_property>

--<snippetsp_changedistributiondb>
DECLARE @distributionDB AS sysname;
SET @distributionDB = N'distribution';

-- Change the history retention period to 24 hours and the
-- maximum retention period to 48 hours.  
USE distribution
EXEC sp_changedistributiondb @distributionDB, N'history_retention', 24
EXEC sp_changedistributiondb @distributionDB, N'max_distretention', 48
GO 
--</snippetsp_changedistributiondb>

--<snippetsp_changedistributor_password>
-- Change the password on the Distributor. 
-- To avoid storing the password in the script file, the value is passed 
-- into SQLCMD as a scripting variable. For information about how to use 
-- scripting variables on the command line and in SQL Server Management
-- Studio, see the "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".
USE master
EXEC sp_changedistributor_password $(Password)
GO
--</snippetsp_changedistributor_password>
