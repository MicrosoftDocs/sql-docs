-- Database functions and stored proc examples

--<snippetDatabaseFunction1>
USE master;
GO
SELECT DATABASEPROPERTY('master', 'IsTruncLog');
--</snippetDatabaseFunction1>

--<snippetDatabaseFunction2>
SELECT DB_NAME() AS [Current Database];
GO
--</snippetDatabaseFunction2>
--<snippetDatabaseFunction3>
USE master;
GO
SELECT DB_NAME(3)AS [Database Name];
GO
--</snippetDatabaseFunction3>
--<snippetDatabaseFunction4>
SELECT DATABASEPROPERTYEX('AdventureWorks2012', 'IsAutoShrink');
--</snippetDatabaseFunction4>
--<snippetDatabaseFunction5>
SELECT DATABASEPROPERTYEX('AdventureWorks2012', 'Collation');
--</snippetDatabaseFunction5>
--<snippetDatabaseFunction6>
SELECT DB_ID() AS [Database ID];
GO
--</snippetDatabaseFunction6>
--<snippetDatabaseFunction7>
SELECT DB_ID(N'AdventureWorks2012') AS [Database ID];
GO
--</snippetDatabaseFunction7>

--<snippetDatabaseStoredProc1>
EXEC sp_helpdb N'AdventureWorks2012';
--</snippetDatabaseStoredProc1>
--<snippetDatabaseStoredProc2>
EXEC sp_helpdb;
GO
--</snippetDatabaseStoredProc2>

--<snippetDatabaseStoredProc3>
USE master;
GO
EXEC sp_databases;

--</snippetDatabaseStoredProc3>

--<snippetDatabaseStoredProc4>
USE master;
GO
EXEC sp_dboption 'AdventureWorks2012', 'read only', 'TRUE';
--</snippetDatabaseStoredProc4>

--<snippetDatabaseStoredProc5>
USE master;
GO
EXEC sp_dboption 'AdventureWorks2012', 'read only', 'FALSE';
--</snippetDatabaseStoredProc5>