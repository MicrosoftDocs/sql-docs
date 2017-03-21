USE AdventureWorks2012;
GO
-- Check the state of the snapshot_isolation_framework
-- in the database.
SELECT name, snapshot_isolation_state,
     snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'AdventureWorks2012';
GO
USE master;
GO
ALTER DATABASE AdventureWorks2012
    SET ALLOW_SNAPSHOT_ISOLATION ON;
GO
-- Check again.
SELECT name, snapshot_isolation_state,
     snapshot_isolation_state_desc AS description
FROM sys.databases
WHERE name = N'AdventureWorks2012';
GO