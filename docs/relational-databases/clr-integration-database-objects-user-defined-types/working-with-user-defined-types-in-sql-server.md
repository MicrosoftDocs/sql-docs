---
title: "Working with User-Defined Types in SQL Server | Microsoft Docs"
description: You can access UDT functionality in SQL Server from the Transact-SQL language using regular query syntax. Define UDT tables and columns and manipulate UDT data.
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "user-defined types [CLR integration], queries"
  - "UDTs [CLR integration], queries"
  - "user-defined types [CLR integration], Transact-SQL"
  - "UDTs [CLR integration], Transact-SQL"
  - "queries [CLR integration]"
ms.assetid: 807376fb-1f1a-4f2a-8cf8-a622c5858634
author: "rothja"
ms.author: "jroth"
---
# Working with User-Defined Types in SQL Server
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  You can access user-defined type (UDT) functionality in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the [!INCLUDE[tsql](../../includes/tsql-md.md)] language by using regular query syntax. UDTs can be used in the definition of database objects, as variables in [!INCLUDE[tsql](../../includes/tsql-md.md)] batches, in functions and stored procedures, and as arguments in functions and stored procedures.  
  
## EXAMPLES

### Example 1: Creating a user-defined data type and then using it as input parameter for a stored procedure.
```
CREATE TYPE [IntListType] AS TABLE
   (   [T] INT  );
GO


CREATE PROCEDURE [myTest]
 (
       @IntListInput IntListType READONLY
 )
 AS
 BEGIN
    SET NOCOUNT ON;

    DECLARE @myInt INT;
    DECLARE intListCursor CURSOR LOCAL FAST_FORWARD
    FOR
    SELECT [T]
    FROM @IntListInput;

    OPEN intListCursor;

    FETCH NEXT FROM intListCursor INTO @myInt;

    WHILE @@FETCH_STATUS = 0
    BEGIN

       PRINT 'Int var is : ' + CONVERT(VARCHAR(max),@myInt);

       FETCH NEXT FROM intListCursor INTO @myInt;
    END;

    CLOSE intListCursor;
    DEALLOCATE intListCursor;
 END;
 GO
```

The above script creates a user-defined data type, then creates a stored procedure that uses the newly created data type.The stored procedure uses a cursor to display the data contained on the user-defined data type.
  
## In This Section  
 [Defining UDT Tables and Columns](../../relational-databases/clr-integration-database-objects-user-defined-types/working-with-user-defined-types-defining-udt-tables-and-columns.md)  
 Describes how to use [!INCLUDE[tsql](../../includes/tsql-md.md)] to create a UDT column in a table.  
  
 [Manipulating UDT Data](../../relational-databases/clr-integration-database-objects-user-defined-types/working-with-user-defined-types-manipulating-udt-data.md)  
 Describes how to use [!INCLUDE[tsql](../../includes/tsql-md.md)] to work with UDT data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## See Also  
 [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)  
  
  
