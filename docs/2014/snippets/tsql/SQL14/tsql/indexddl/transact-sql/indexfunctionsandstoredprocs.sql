-- Index function examples
--<snippetIndexFunction1>  
USE AdventureWorks2012;
GO
SELECT 
    INDEXPROPERTY(OBJECT_ID('HumanResources.Employee'),
        'PK_Employee_BusinessEntityID','IsClustered')AS [Is Clustered],
    INDEXPROPERTY(OBJECT_ID('HumanResources.Employee'),
        'PK_Employee_BusinessEntityID','IndexDepth') AS [Index Depth],
    INDEXPROPERTY(OBJECT_ID('HumanResources.Employee'),
        'PK_Employee_BusinessEntityID','IndexFillFactor') AS [Fill Factor];
GO
--</snippetIndexFunction1> 
--<snippetIndexFunction2>  
USE AdventureWorks2012;
GO
SELECT 
    INDEXKEY_PROPERTY(OBJECT_ID('Production.Location', 'U'),
        1,1,'ColumnId') AS [Column ID],
    INDEXKEY_PROPERTY(OBJECT_ID('Production.Location', 'U'),
        1,1,'IsDescending') AS [Asc or Desc order];
--</snippetIndexFunction2> 
--<snippetIndexFunction3>  
USE AdventureWorks2012;
GO
SELECT 
    INDEX_COL (N'AdventureWorks2012.Sales.SalesOrderDetail', 1,1) AS
        [Index Column 1], 
    INDEX_COL (N'AdventureWorks2012.Sales.SalesOrderDetail', 1,2) AS
        [Index Column 2]
;
GO
--</snippetIndexFunction3> 
--<snippetIndexFunction4>  
DECLARE @db_id SMALLINT;
DECLARE @object_id INT;

SET @db_id = DB_ID(N'AdventureWorks2012');
SET @object_id = OBJECT_ID(N'AdventureWorks2012.Person.Address');

IF @db_id IS NULL
BEGIN;
    PRINT N'Invalid database';
END;
ELSE IF @object_id IS NULL
BEGIN;
    PRINT N'Invalid object';
END;
ELSE
BEGIN;
    SELECT * FROM sys.dm_db_index_physical_stats(@db_id, @object_id, NULL, NULL , 'LIMITED');
END;
GO
--</snippetIndexFunction4> 
--<snippetIndexFunction5>  
DECLARE @db_id SMALLINT;
DECLARE @object_id INT;
SET @db_id = DB_ID(N'AdventureWorks2012');
SET @object_id = OBJECT_ID(N'AdventureWorks2012.dbo.DatabaseLog');
IF @object_id IS NULL 
BEGIN;
    PRINT N'Invalid object';
END;
ELSE
BEGIN;
    SELECT * FROM sys.dm_db_index_physical_stats(@db_id, @object_id, 0, NULL , 'DETAILED');
END;
GO
--</snippetIndexFunction5> 
--<snippetIndexFunction6>  
SELECT * FROM sys.dm_db_index_physical_stats (NULL, NULL, NULL, NULL, NULL);
GO
--</snippetIndexFunction6> 
--<snippetIndexFunction7>  
-- Ensure a USE <databasename> statement has been executed first.
SET NOCOUNT ON;
DECLARE @objectid int;
DECLARE @indexid int;
DECLARE @partitioncount bigint;
DECLARE @schemaname nvarchar(130); 
DECLARE @objectname nvarchar(130); 
DECLARE @indexname nvarchar(130); 
DECLARE @partitionnum bigint;
DECLARE @partitions bigint;
DECLARE @frag float;
DECLARE @command nvarchar(4000); 
-- Conditionally select tables and indexes from the sys.dm_db_index_physical_stats function 
-- and convert object and index IDs to names.
SELECT
    object_id AS objectid,
    index_id AS indexid,
    partition_number AS partitionnum,
    avg_fragmentation_in_percent AS frag
INTO #work_to_do
FROM sys.dm_db_index_physical_stats (DB_ID(), NULL, NULL , NULL, 'LIMITED')
WHERE avg_fragmentation_in_percent > 10.0 AND index_id > 0;

-- Declare the cursor for the list of partitions to be processed.
DECLARE partitions CURSOR FOR SELECT * FROM #work_to_do;

-- Open the cursor.
OPEN partitions;

-- Loop through the partitions.
WHILE (1=1)
    BEGIN;
        FETCH NEXT
           FROM partitions
           INTO @objectid, @indexid, @partitionnum, @frag;
        IF @@FETCH_STATUS < 0 BREAK;
        SELECT @objectname = QUOTENAME(o.name), @schemaname = QUOTENAME(s.name)
        FROM sys.objects AS o
        JOIN sys.schemas as s ON s.schema_id = o.schema_id
        WHERE o.object_id = @objectid;
        SELECT @indexname = QUOTENAME(name)
        FROM sys.indexes
        WHERE  object_id = @objectid AND index_id = @indexid;
        SELECT @partitioncount = count (*)
        FROM sys.partitions
        WHERE object_id = @objectid AND index_id = @indexid;

-- 30 is an arbitrary decision point at which to switch between reorganizing and rebuilding.
        IF @frag < 30.0
            SET @command = N'ALTER INDEX ' + @indexname + N' ON ' + @schemaname + N'.' + @objectname + N' REORGANIZE';
        IF @frag >= 30.0
            SET @command = N'ALTER INDEX ' + @indexname + N' ON ' + @schemaname + N'.' + @objectname + N' REBUILD';
        IF @partitioncount > 1
            SET @command = @command + N' PARTITION=' + CAST(@partitionnum AS nvarchar(10));
        EXEC (@command);
        PRINT N'Executed: ' + @command;
    END;

-- Close and deallocate the cursor.
CLOSE partitions;
DEALLOCATE partitions;

-- Drop the temporary table.
DROP TABLE #work_to_do;
GO
--</snippetIndexFunction7> 
--<snippetIndexFunction8>  
DECLARE @db_id int;
DECLARE @object_id int;
SET @db_id = DB_ID(N'AdventureWorks2012');
SET @object_id = OBJECT_ID(N'AdventureWorks2012.Person.Address');
IF @db_id IS NULL 
  BEGIN;
    PRINT N'Invalid database';
  END;
ELSE IF @object_id IS NULL
  BEGIN;
    PRINT N'Invalid object';
  END;
ELSE
  BEGIN;
    SELECT * FROM sys.dm_db_index_operational_stats(@db_id, @object_id, NULL, NULL);
  END;
GO
--</snippetIndexFunction8> 
--<snippetIndexFunction9>  
SELECT * FROM sys.dm_db_index_operational_stats(NULL, NULL, NULL, NULL);
GO 
--</snippetIndexFunction9> 

--<snippetIndexFunction10>  

USE AdventureWorks2012;
GO
EXEC sp_indexoption N'Sales.Customer.IX_Customer_TerritoryID',
    N'disallowpagelocks', TRUE;
--</snippetIndexFunction10> 

--<snippetIndexFunction11> 
USE AdventureWorks2012;
GO
--Display the current row and page lock options for all indexes on the table.
SELECT name, type_desc, allow_row_locks, allow_page_locks 
FROM sys.indexes
WHERE object_id = OBJECT_ID(N'Production.Product');
GO
-- Set the disallowrowlocks option on the Product table. 
EXEC sp_indexoption N'Production.Product',
    N'disallowrowlocks', TRUE;
GO
--Verify the row and page lock options for all indexes on the table.
SELECT name, type_desc, allow_row_locks, allow_page_locks 
FROM sys.indexes
WHERE object_id = OBJECT_ID(N'Production.Product');
GO
--</snippetIndexFunction11> 

--<snippetIndexFunction12> 
USE AdventureWorks2012;
GO
--Display the current row and page lock options of the table. 
SELECT OBJECT_NAME (object_id) AS [Table], type_desc, allow_row_locks, allow_page_locks 
FROM sys.indexes
WHERE OBJECT_NAME (object_id) = N'DatabaseLog';
GO
-- Set the disallowpagelocks option on the table. 
EXEC sp_indexoption DatabaseLog,
    N'disallowpagelocks', TRUE;
GO
--Verify the row and page lock settings of the table.
SELECT OBJECT_NAME (object_id) AS [Table], allow_row_locks, allow_page_locks 
FROM sys.indexes
WHERE OBJECT_NAME (object_id) = N'DatabaseLog';
GO
--</snippetIndexFunction12> 

--<snippetIndexFunction13>
USE AdventureWorks2012;
GO
EXEC sp_helpindex N'Sales.Customer';
GO
--</snippetIndexFunction13>

