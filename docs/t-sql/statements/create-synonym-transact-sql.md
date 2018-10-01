---
title: "CREATE SYNONYM (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/11/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE_SYNONYM_TSQL"
  - "SYNONYM_TSQL"
  - "SYNONYM"
  - "CREATE SYNONYM"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "alternate names [SQL Server]"
  - "names [SQL Server], synonyms"
  - "CREATE SYNONYM statement"
  - "synonyms [SQL Server], creating"
ms.assetid: 41313809-e970-449c-bc35-85da2ef96e48
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE SYNONYM (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Creates a new synonym.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- SQL Server Syntax  
  
CREATE SYNONYM [ schema_name_1. ] synonym_name FOR <object>  
  
<object> :: =  
{  
    [ server_name.[ database_name ] . [ schema_name_2 ]. object_name   
  | database_name . [ schema_name_2 ].| schema_name_2. ] object_name  
}  
```  
  
```  
-- Azure SQL Database Syntax  
  
CREATE SYNONYM [ schema_name_1. ] synonym_name FOR < object >  
  
< object > :: =  
{  
    [database_name. [ schema_name_2 ].| schema_name_2. ] object_name  
}  
```  
  
## Arguments  
 *schema_name_1*  
 Specifies the schema in which the synonym is created. If *schema* is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the default schema of the current user.  
  
 *synonym_name*  
 Is the name of the new synonym.  
  
 *server_name*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Is the name of the server on which base object is located.  
  
 *database_name*  
 Is the name of the database in which the base object is located. If *database_name* is not specified, the name of the current database is used.  
  
 *schema_name_2*  
 Is the name of the schema of the base object. If *schema_name* is not specified the default schema of the current user is used.  
  
 *object_name*  
 Is the name of the base object that the synonym references.  
  
 Windows Azure SQL Database supports the three-part name format database_name.[schema_name].object_name when the database_name is the current database or the database_name is tempdb and the object_name starts with #.  
  
## Remarks  
 The base object need not exist at synonym create time. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] checks for the existence of the base object at run time.  
  
 Synonyms can be created for the following types of objects:  
  
|||  
|-|-|  
|Assembly (CLR) Stored Procedure|Assembly (CLR) Table-valued Function|  
|Assembly (CLR) Scalar Function|Assembly Aggregate (CLR) Aggregate Functions|  
|Replication-filter-procedure|Extended Stored Procedure|  
|SQL Scalar Function|SQL Table-valued Function|  
|SQL Inline-table-valued Function|SQL Stored Procedure|  
|View|Table<sup>1</sup> (User-defined)|  
  
 <sup>1 Includes local and global temporary tables</sup>  
  
 Four-part names for function base objects are not supported.  
  
 Synonyms can be created, dropped and referenced in dynamic SQL.  
  
## Permissions  
 To create a synonym in a given schema, a user must have CREATE SYNONYM permission and either own the schema or have ALTER SCHEMA permission.  
  
 The CREATE SYNONYM permission is a grantable permission.  
  
> [!NOTE]  
>  You do not need permission on the base object to successfully compile the CREATE SYNONYM statement, because all permission checking on the base object is deferred until run time.  
  
## Examples  
  
### A. Creating a synonym for a local object  
 The following example first creates a synonym for the base object, `Product` in the `AdventureWorks2012` database, and then queries the synonym.  
  
```  
-- Create a synonym for the Product table in AdventureWorks2012.  
CREATE SYNONYM MyProduct  
FOR AdventureWorks2012.Production.Product;  
GO  
  
-- Query the Product table by using the synonym.  
SELECT ProductID, Name   
FROM MyProduct  
WHERE ProductID < 5;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 ----------------------- 
 ProductID   Name 
 ----------- -------------------------- 
 1           Adjustable Race 
 2           Bearing Ball 
 3           BB Ball Bearing 
 4           Headset Ball Bearings 

 (4 row(s) affected)
``` 
  
### B. Creating a synonym to remote object  
 In the following example, the base object, `Contact`, resides on a remote server named `Server_Remote`.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
EXEC sp_addlinkedserver Server_Remote;  
GO  
USE tempdb;  
GO  
CREATE SYNONYM MyEmployee FOR Server_Remote.AdventureWorks2012.HumanResources.Employee;  
GO  
```  
  
### C. Creating a synonym for a user-defined function  
 The following example creates a function named `dbo.OrderDozen` that increases order amounts to an even dozen units. The example then creates the synonym `dbo.CorrectOrder` for the `dbo.OrderDozen` function.  
  
```  
-- Creating the dbo.OrderDozen function  
CREATE FUNCTION dbo.OrderDozen (@OrderAmt int)  
RETURNS int  
WITH EXECUTE AS CALLER  
AS  
BEGIN  
IF @OrderAmt % 12 <> 0  
BEGIN  
    SET @OrderAmt +=  12 - (@OrderAmt % 12)  
END  
RETURN(@OrderAmt);  
END;  
GO  
  
-- Using the dbo.OrderDozen function  
DECLARE @Amt int;  
SET @Amt = 15;  
SELECT @Amt AS OriginalOrder, dbo.OrderDozen(@Amt) AS ModifiedOrder;  
  
-- Create a synonym dbo.CorrectOrder for the dbo.OrderDozen function.  
CREATE SYNONYM dbo.CorrectOrder  
FOR dbo.OrderDozen;  
GO  
  
-- Using the dbo.CorrectOrder synonym.  
DECLARE @Amt int;  
SET @Amt = 15;  
SELECT @Amt AS OriginalOrder, dbo.CorrectOrder(@Amt) AS ModifiedOrder;  
```  
  
## See Also  
 [DROP SYNONYM &#40;Transact-SQL&#41;](../../t-sql/statements/drop-synonym-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
