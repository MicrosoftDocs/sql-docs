--<snippetDBCCCleanTable1>
DBCC CLEANTABLE (AdventureWorks2012,"Production.Document", 0)
WITH NO_INFOMSGS;
GO
--</snippetDBCCCleanTable1>
--<snippetDBCCCleanTable2>
USE AdventureWorks2012;
GO
IF OBJECT_ID ('dbo.CleanTableTest', 'U') IS NOT NULL
    DROP TABLE dbo.CleanTableTest;
GO
CREATE TABLE dbo.CleanTableTest
    (FileName nvarchar(4000), 
    DocumentSummary nvarchar(max),
    Document varbinary(max)
    );
GO
-- Populate the table with data from the Production.Document table.
INSERT INTO dbo.CleanTableTest
    SELECT REPLICATE(FileName, 1000), 
           DocumentSummary, 
           Document
    FROM Production.Document;
GO
-- Verify the current page counts and average space used in the dbo.CleanTableTest table.
DECLARE @db_id SMALLINT;
DECLARE @object_id INT;
SET @db_id = DB_ID(N'AdventureWorks2012');
SET @object_id = OBJECT_ID(N'AdventureWorks2012.dbo.CleanTableTest');
SELECT alloc_unit_type_desc, 
       page_count, 
       avg_page_space_used_in_percent, 
       record_count
FROM sys.dm_db_index_physical_stats(@db_id, @object_id, NULL, NULL , 'Detailed');
GO
-- Drop two variable-length columns from the table.
ALTER TABLE dbo.CleanTableTest
DROP COLUMN FileName, Document;
GO
-- Verify the page counts and average space used in the dbo.CleanTableTest table
-- Notice that the values have not changed.
DECLARE @db_id SMALLINT;
DECLARE @object_id INT;
SET @db_id = DB_ID(N'AdventureWorks2012');
SET @object_id = OBJECT_ID(N'AdventureWorks2012.dbo.CleanTableTest');
SELECT alloc_unit_type_desc, 
       page_count, 
       avg_page_space_used_in_percent, 
       record_count
FROM sys.dm_db_index_physical_stats(@db_id, @object_id, NULL, NULL , 'Detailed');
GO
-- Run DBCC CLEANTABLE.
DBCC CLEANTABLE (AdventureWorks2012,"dbo.CleanTableTest");
GO
-- Verify the values in the dbo.CleanTableTest table after the DBCC CLEANTABLE command.
DECLARE @db_id SMALLINT;
DECLARE @object_id INT;
SET @db_id = DB_ID(N'AdventureWorks2012');
SET @object_id = OBJECT_ID(N'AdventureWorks2012.dbo.CleanTableTest');
SELECT alloc_unit_type_desc, 
       page_count, 
       avg_page_space_used_in_percent, 
       record_count
FROM sys.dm_db_index_physical_stats(@db_id, @object_id, NULL, NULL , 'Detailed');
GO
--</snippetDBCCCleanTable2>
--<snippetDBCCDDLName>
DBCC xp_sample (FREE);
--</snippetDBCCDDLName>
--<snippetDBCC_DBREINDEX1>
USE AdventureWorks2012; 
GO
DBCC DBREINDEX ("HumanResources.Employee", PK_Employee_BusinessEntityID,80);
GO
--</snippetDBCC_DBREINDEX1>
--<snippetDBCC_DBREINDEX2>
USE AdventureWorks2012; 
GO
DBCC DBREINDEX ("HumanResources.Employee", " ", 70);
GO
--</snippetDBCC_DBREINDEX2>
--<snippetDBCC_HELP1>
DECLARE @dbcc_stmt sysname;
SET @dbcc_stmt = 'CHECKDB';
DBCC HELP (@dbcc_stmt);
GO
--</snippetDBCC_HELP1>
--<snippetDBCC_HELP2>
DBCC HELP ('?');
GO
--</snippetDBCC_HELP2>
--<snippetDBCC_INDEXDEFRAG>
DBCC INDEXDEFRAG (AdventureWorks2012, "Production.Product", PK_Product_ProductID)
GO
--</snippetDBCC_INDEXDEFRAG>
--<snippetDBCC_OPENTRAN>
CREATE TABLE T1(Col1 int, Col2 char(3));
GO
BEGIN TRAN
INSERT INTO T1 VALUES (101, 'abc');
GO
DBCC OPENTRAN;
ROLLBACK TRAN;
GO
DROP TABLE T1;
GO
--</snippetDBCC_OPENTRAN>

--<snippetDBCC_OPENTRAN2>
-- Create the temporary table to accept the results.
CREATE TABLE #OpenTranStatus (
   ActiveTransaction varchar(25),
   Details sql_variant 
   )
-- Execute the command, putting the results in the table.
INSERT INTO #OpenTranStatus 
   EXEC ('DBCC OPENTRAN WITH TABLERESULTS, NO_INFOMSGS');

-- Display the results.
SELECT * FROM #OpenTranStatus;
GO
--</snippetDBCC_OPENTRAN2>
--<snippetDBCC_SHOWSTATS>
USE AdventureWorks2012;
GO
DBCC SHOW_STATISTICS ("Person.Address", AK_Address_rowguid);
GO
--</snippetDBCC_SHOWSTATS>
--<snippetDBCC_SHOWSTATS1>
USE AdventureWorks2012;
GO
DBCC SHOW_STATISTICS ("Person.Address", AK_Address_rowguid) WITH HISTOGRAM;
GO
--</snippetDBCC_SHOWSTATS1>
--<snippetDBCC_SHOWCONTIG1>
USE AdventureWorks2012;
GO
DBCC SHOWCONTIG ("HumanResources.Employee");
GO
--</snippetDBCC_SHOWCONTIG1>
--<snippetDBCC_SHOWCONTIG2>
USE AdventureWorks2012;
GO
DECLARE @id int, @indid int
SET @id = OBJECT_ID('Production.Product')
SELECT @indid = index_id 
FROM sys.indexes
WHERE object_id = @id 
   AND name = 'AK_Product_Name'
DBCC SHOWCONTIG (@id, @indid);
GO
--</snippetDBCC_SHOWCONTIG2>
--<snippetDBCC_SHOWCONTIG3>
USE AdventureWorks2012;
GO
DBCC SHOWCONTIG ("Production.Product", 1) WITH FAST;
GO
--</snippetDBCC_SHOWCONTIG3>
--<snippetDBCC_SHOWCONTIG4>
USE AdventureWorks2012;
GO
DBCC SHOWCONTIG WITH TABLERESULTS, ALL_INDEXES;
GO
--</snippetDBCC_SHOWCONTIG4>
--<snippetDBCC_SHOWCONTIG5>
/*Perform a 'USE <database name>' to select the database in which to run the script.*/
-- Declare variables
SET NOCOUNT ON;
DECLARE @tablename varchar(255);
DECLARE @execstr   varchar(400);
DECLARE @objectid  int;
DECLARE @indexid   int;
DECLARE @frag      decimal;
DECLARE @maxfrag   decimal;

-- Decide on the maximum fragmentation to allow for.
SELECT @maxfrag = 30.0;

-- Declare a cursor.
DECLARE tables CURSOR FOR
   SELECT TABLE_SCHEMA + '.' + TABLE_NAME
   FROM INFORMATION_SCHEMA.TABLES
   WHERE TABLE_TYPE = 'BASE TABLE';

-- Create the table.
CREATE TABLE #fraglist (
   ObjectName char(255),
   ObjectId int,
   IndexName char(255),
   IndexId int,
   Lvl int,
   CountPages int,
   CountRows int,
   MinRecSize int,
   MaxRecSize int,
   AvgRecSize int,
   ForRecCount int,
   Extents int,
   ExtentSwitches int,
   AvgFreeBytes int,
   AvgPageDensity int,
   ScanDensity decimal,
   BestCount int,
   ActualCount int,
   LogicalFrag decimal,
   ExtentFrag decimal);

-- Open the cursor.
OPEN tables;

-- Loop through all the tables in the database.
FETCH NEXT
   FROM tables
   INTO @tablename;

WHILE @@FETCH_STATUS = 0
BEGIN
-- Do the showcontig of all indexes of the table
   INSERT INTO #fraglist 
   EXEC ('DBCC SHOWCONTIG (''' + @tablename + ''') 
      WITH FAST, TABLERESULTS, ALL_INDEXES, NO_INFOMSGS');
   FETCH NEXT
      FROM tables
      INTO @tablename;
END;

-- Close and deallocate the cursor.
CLOSE tables;
DEALLOCATE tables;

-- Declare the cursor for the list of indexes to be defragged.
DECLARE indexes CURSOR FOR
   SELECT ObjectName, ObjectId, IndexId, LogicalFrag
   FROM #fraglist
   WHERE LogicalFrag >= @maxfrag
      AND INDEXPROPERTY (ObjectId, IndexName, 'IndexDepth') > 0;

-- Open the cursor.
OPEN indexes;

-- Loop through the indexes.
FETCH NEXT
   FROM indexes
   INTO @tablename, @objectid, @indexid, @frag;

WHILE @@FETCH_STATUS = 0
BEGIN
   PRINT 'Executing DBCC INDEXDEFRAG (0, ' + RTRIM(@tablename) + ',
      ' + RTRIM(@indexid) + ') - fragmentation currently '
       + RTRIM(CONVERT(varchar(15),@frag)) + '%';
   SELECT @execstr = 'DBCC INDEXDEFRAG (0, ' + RTRIM(@objectid) + ',
       ' + RTRIM(@indexid) + ')';
   EXEC (@execstr);

   FETCH NEXT
      FROM indexes
      INTO @tablename, @objectid, @indexid, @frag;
END;

-- Close and deallocate the cursor.
CLOSE indexes;
DEALLOCATE indexes;

-- Delete the temporary table.
DROP TABLE #fraglist;
GO
--</snippetDBCC_SHOWCONTIG5>
--<snippetDBCC_SHRINKDB1>
DBCC SHRINKDATABASE (UserDB, 10);
GO
--</snippetDBCC_SHRINKDB1>
--<snippetDBCC_SHRINKDB2>
DBCC SHRINKDATABASE (AdventureWorks2012, TRUNCATEONLY);
--</snippetDBCC_SHRINKDB2>
--<snippetDBCC_SHRINKFILE1>
USE UserDB;
GO
DBCC SHRINKFILE (DataFile1, 7);
GO
--</snippetDBCC_SHRINKFILE1>
--<snippetDBCC_SHRINKFILE2>
USE AdventureWorks2012;
GO
-- Truncate the log by changing the database recovery model to SIMPLE.
ALTER DATABASE AdventureWorks2012
SET RECOVERY SIMPLE;
GO
-- Shrink the truncated log file to 1 MB.
DBCC SHRINKFILE (AdventureWorks2012_Log, 1);
GO
-- Reset the database recovery model.
ALTER DATABASE AdventureWorks2012
SET RECOVERY FULL;
GO
--</snippetDBCC_SHRINKFILE2>
--<snippetDBCC_SHRINKFILE3>
USE AdventureWorks2012;
GO
SELECT file_id, name
FROM sys.database_files;
GO
DBCC SHRINKFILE (1, TRUNCATEONLY);
--</snippetDBCC_SHRINKFILE3>
--<snippetDBCC_SHRINKFILE4>
USE AdventureWorks2012;
GO
-- Create a data file and assume it contains data.
ALTER DATABASE AdventureWorks2012 
ADD FILE (
    NAME = Test1data,
    FILENAME = 'C:\t1data.ndf',
    SIZE = 5MB
    );
GO
-- Empty the data file.
DBCC SHRINKFILE (Test1data, EMPTYFILE);
GO
-- Remove the data file from the database.
ALTER DATABASE AdventureWorks2012
REMOVE FILE Test1data;
GO
--</snippetDBCC_SHRINKFILE4>
--<snippetDBCC_SQLPERF1>
DBCC SQLPERF(LOGSPACE);
GO
--</snippetDBCC_SQLPERF1>
--<snippetDBCC_SQLPERF2>
DBCC SQLPERF("sys.dm_os_wait_stats",CLEAR);
--</snippetDBCC_SQLPERF2>
--<snippetDBCC_TRACEON1>
DBCC TRACEON (3205);
GO
--</snippetDBCC_TRACEON1>
--<snippetDBCC_TRACEON2>
DBCC TRACEON (3205, -1);
GO
--</snippetDBCC_TRACEON2>
--<snippetDBCC_TRACEON3>
DBCC TRACEON (3205, 260, -1);
GO
--</snippetDBCC_TRACEON3>
--<snippetDBCC_TRACEOFF1>
DBCC TRACEOFF (3205);
GO
--</snippetDBCC_TRACEOFF1>
--<snippetDBCC_TRACEOFF2>
DBCC TRACEOFF (3205, -1);
GO
--</snippetDBCC_TRACEOFF2>
--<snippetDBCC_TRACEOFF3>
DBCC TRACEOFF (3205, 260, -1);
GO
--</snippetDBCC_TRACEOFF3>
--<snippetDBCC_TRACESTATUS1>
DBCC TRACESTATUS(-1);
GO
--</snippetDBCC_TRACESTATUS1>
--<snippetDBCC_TRACESTATUS2>
DBCC TRACESTATUS (2528, 3205);
GO
--</snippetDBCC_TRACESTATUS2>
--<snippetDBCC_TRACESTATUS3>
DBCC TRACESTATUS (3205, -1);
GO
--</snippetDBCC_TRACESTATUS3>
--<snippetDBCC_TRACESTATUS4>
DBCC TRACESTATUS();
GO
--</snippetDBCC_TRACESTATUS4>

--<snippetDBCC_UPDATEUSAGE1>
DBCC UPDATEUSAGE (0);
GO
--</snippetDBCC_UPDATEUSAGE1>
--<snippetDBCC_UPDATEUSAGE2>
USE AdventureWorks2012;
GO
DBCC UPDATEUSAGE (AdventureWorks2012) WITH NO_INFOMSGS; 
GO
--</snippetDBCC_UPDATEUSAGE2>
--<snippetDBCC_UPDATEUSAGE3>
USE AdventureWorks2012;
GO
DBCC UPDATEUSAGE (AdventureWorks2012,"HumanResources.Employee");
GO
--</snippetDBCC_UPDATEUSAGE3>
--<snippetDBCC_UPDATEUSAGE4>
USE AdventureWorks2012;
GO
DBCC UPDATEUSAGE (AdventureWorks2012, "HumanResources.Employee", IX_Employee_OrganizationLevel_OrganizationNode);
GO
--</snippetDBCC_UPDATEUSAGE4>
--<snippetDBCC_USEROPTIONS>
DBCC USEROPTIONS;
--</snippetDBCC_USEROPTIONS>

--<snippetDBCC_FREEPROCCACHE1>
USE AdventureWorks2012;
GO
SELECT * FROM Person.Address;
GO
SELECT plan_handle, st.text
FROM sys.dm_exec_cached_plans 
CROSS APPLY sys.dm_exec_sql_text(plan_handle) AS st
WHERE text LIKE N'SELECT * FROM Person.Address%';
GO
--</snippetDBCC_FREEPROCCACHE1>

--<snippetDBCC_FREEPROCCACHE2>
-- Remove the specific plan from the cache.
DBCC FREEPROCCACHE (0x060006001ECA270EC0215D05000000000000000000000000);
GO
--</snippetDBCC_FREEPROCCACHE2>

--<snippetDBCC_FREEPROCCACHE3>
DBCC FREEPROCCACHE WITH NO_INFOMSGS;
--</snippetDBCC_FREEPROCCACHE3>

--<snippetDBCC_FREEPROCCACHE4>
SELECT * FROM sys.dm_resource_governor_resource_pools;
GO
DBCC FREEPROCCACHE ('default');
GO
--</snippetDBCC_FREEPROCCACHE4>