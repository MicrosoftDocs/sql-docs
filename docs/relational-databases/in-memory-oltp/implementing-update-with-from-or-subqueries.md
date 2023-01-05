---
title: "Implementing UPDATE with FROM or Subqueries"
description: Learn which syntax elements are supported on the Transact-SQL UPDATE statement in a natively compiled T-SQL module.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "11/17/2016"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: 138f5b0e-f8a4-400f-b581-8062aebc62b6
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Implementing UPDATE with FROM or Subqueries

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]



On the Transact-SQL UPDATE statement, in a natively compiled T-SQL module, the following syntax elements are *not* supported:

- The FROM clause
- Subqueries

In contrast, the preceding elements *are* supported in natively compiled modules on the SELECT statement.

UPDATE statements with a FROM clause are often used to update information in a table based on a table-valued parameter (TVP), or to update columns in a table in an AFTER trigger.

For the scenario of update based on a TVP, see [Implementing MERGE Functionality in a Natively Compiled Stored Procedure](../../relational-databases/in-memory-oltp/implementing-merge-functionality-in-a-natively-compiled-stored-procedure.md). 

The following sample illustrates an update performed in a trigger. In the table, the column named LastUpdated is set to the current date-time AFTER updates. The workaround performs individual updates by using the following items:

- A table variable that has an IDENTITY column.
- A WHILE loop to iterate of the rows in the table variable.

Here is the original T-SQL UPDATE statement:

   ```sql
    UPDATE dbo.Table1  
        SET LastUpdated = SysDateTime()  
        FROM  
            dbo.Table1 t  
            JOIN Inserted i ON t.Id = i.Id;  
   ```

The sample T-SQL code in the following block demonstrates a workaround that provides good performance. The workaround is implemented in a natively compiled trigger. Crucial to notice in the code are:  
  
- The type named dbo.Type1, which is a memory-optimized table type.  
- The WHILE loop in the trigger.  
  - The loop retrieves the rows from Inserted one at a time.  
  
  
  
 ```sql
    DROP TABLE IF EXISTS dbo.Table1;  
    go  
    DROP TYPE IF EXISTS dbo.Type1;  
    go  
    -----------------------------
    -- Table and table type.
    -----------------------------
  
    CREATE TABLE dbo.Table1  
    (  
        Id           INT        NOT NULL  PRIMARY KEY NONCLUSTERED,  
        Column2      INT        NOT NULL,  
        LastUpdated  DATETIME2  NOT NULL  DEFAULT (SYSDATETIME())  
    )  
        WITH (MEMORY_OPTIMIZED = ON);  
    go  
  
  
    CREATE TYPE dbo.Type1 AS TABLE  
    (  
        Id       INT NOT  NULL,  
        
        RowID    INT NOT  NULL  IDENTITY,  
        INDEX ix_RowID HASH (RowID) WITH (BUCKET_COUNT=1024)
    )   
        WITH (MEMORY_OPTIMIZED = ON);  
    go  
    ----------------------------------------
    -- Trigger that contains the workaround
    -- for UPDATE with FROM.
    ----------------------------------------
  
    CREATE TRIGGER dbo.tr_a_u_Table1  
        ON dbo.Table1  
        WITH NATIVE_COMPILATION, SCHEMABINDING  
        AFTER UPDATE  
    AS 
    BEGIN ATOMIC WITH  
        (  
        TRANSACTION ISOLATION LEVEL = SNAPSHOT,  
        LANGUAGE = N'us_english'  
        )  
        
      DECLARE @tabvar1 dbo.Type1;  
    
      INSERT @tabvar1 (Id)   
          SELECT Id FROM Inserted;  
    
      DECLARE  
          @i INT = 1,  @Id INT,  
          @max INT = SCOPE_IDENTITY();  
    
      ---- Loop as a workaround to simulate a cursor.
      ---- Iterate over the rows in the memory-optimized table  
      ----   variable and perform an update for each row.  
    
      WHILE @i <= @max  
      BEGIN  
          SELECT @Id = Id  
              FROM @tabvar1  
              WHERE RowID = @i;  
    
          UPDATE dbo.Table1  
              SET LastUpdated = SysDateTime()  
              WHERE Id = @Id;  
    
          SET @i += 1;  
      END  
    END  
    go  
    ---------------------------------
    -- Test to verify functionality.
    ---------------------------------
  
    SET NOCOUNT ON;  
  
    INSERT dbo.Table1 (Id, Column2)  
        VALUES (1,9), (2,9), (3,600);  
    
    SELECT N'BEFORE-Update' AS [BEFORE-Update], *  
        FROM dbo.Table1  
        ORDER BY Id;  
  
    WAITFOR DELAY '00:00:01';  

    UPDATE dbo.Table1  
        SET   Column2 += 1  
        WHERE Column2 <= 99;  
  
    SELECT N'AFTER--Update' AS [AFTER--Update], *  
        FROM dbo.Table1  
        ORDER BY Id;  
    go  
    -----------------------------  
  
    /**** Actual output:  
  
    BEFORE-Update   Id   Column2   LastUpdated  
    BEFORE-Update   1       9      2016-04-20 21:18:42.8394659  
    BEFORE-Update   2       9      2016-04-20 21:18:42.8394659  
    BEFORE-Update   3     600      2016-04-20 21:18:42.8394659  
  
    AFTER--Update   Id   Column2   LastUpdated  
    AFTER--Update   1      10      2016-04-20 21:18:43.8529692  
    AFTER--Update   2      10      2016-04-20 21:18:43.8529692  
    AFTER--Update   3     600      2016-04-20 21:18:42.8394659  
    ****/  
 ```
