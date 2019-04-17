---
title: "Use Table-Valued Parameters (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "table-valued parameters"
  - "table-valued parameters, about table-valued parameters"
  - "parameters [SQL Server], table-valued"
  - "TVP See table-valued parameters"
ms.assetid: 5e95a382-1e01-4c74-81f5-055612c2ad99
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use Table-Valued Parameters (Database Engine)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Table-valued parameters are declared by using user-defined table types. You can use table-valued parameters to send multiple rows of data to a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or a routine, such as a stored procedure or function, without creating a temporary table or many parameters.  
  
 Table-valued parameters are like parameter arrays in OLE DB and ODBC, but offer more flexibility and closer integration with [!INCLUDE[tsql](../../includes/tsql-md.md)]. Table-valued parameters also have the benefit of being able to participate in set-based operations.  
  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] passes table-valued parameters to routines by reference to avoid making a copy of the input data. You can create and execute [!INCLUDE[tsql](../../includes/tsql-md.md)] routines with table-valued parameters, and call them from [!INCLUDE[tsql](../../includes/tsql-md.md)] code, managed and native clients in any managed language.  
  
 **In This Topic:**  
  
 [Benefits](#Benefits)  
  
 [Restrictions](#Restrictions)  
  
 [Table-Valued Parameters vs. BULK INSERT Operations](#BulkInsert)  
  
 [Example](#Example)  
  
##  <a name="Benefits"></a> Benefits  
 A table-valued parameter is scoped to the stored procedure, function, or dynamic [!INCLUDE[tsql](../../includes/tsql-md.md)] text, exactly like other parameters. Similarly, a variable of table type has scope like any other local variable that is created by using a DECLARE statement. You can declare table-valued variables within dynamic [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and pass these variables as table-valued parameters to stored procedures and functions.  
  
 Table-valued parameters offer more flexibility and in some cases better performance than temporary tables or other ways to pass a list of parameters. Table-valued parameters offer the following benefits:  
  
-   Do not acquire locks for the initial population of data from a client.  
  
-   Provide a simple programming model.  
  
-   Enable you to include complex business logic in a single routine.  
  
-   Reduce round trips to the server.  
  
-   Can have a table structure of different cardinality.  
  
-   Are strongly typed.  
  
-   Enable the client to specify sort order and unique keys.  
  
-   Are cached like a temp table when used in a stored procedure. Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], table-valued parameters are also cached for parameterized queries.  
  
##  <a name="Restrictions"></a> Restrictions  
 Table-valued parameters have the following restrictions:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not maintain statistics on columns of table-valued parameters.  
  
-   Table-valued parameters must be passed as input READONLY parameters to [!INCLUDE[tsql](../../includes/tsql-md.md)] routines. You cannot perform DML operations such as UPDATE, DELETE, or INSERT on a table-valued parameter in the body of a routine.  
  
-   You cannot use a table-valued parameter as target of a SELECT INTO or INSERT EXEC statement. A table-valued parameter can be in the FROM clause of SELECT INTO or in the INSERT EXEC string or stored procedure.  
  
##  <a name="BulkInsert"></a> Table-Valued Parameters vs. BULK INSERT Operations  
 Using table-valued parameters is comparable to other ways of using set-based variables; however, using table-valued parameters frequently can be faster for large data sets. Compared to bulk operations that have a greater startup cost than table-valued parameters, table-valued parameters perform well for inserting less than 1000 rows.  
  
 Table-valued parameters that are reused benefit from temporary table caching. This table caching enables better scalability than equivalent BULK INSERT operations. By using small row-insert operations a small performance benefit might be gained by using parameter lists or batched statements instead of BULK INSERT operations or table-valued parameters. However, these methods are less convenient to program, and performance decreases quickly as rows increase.  
  
 Table-valued parameters perform equally well or better than an equivalent parameter array implementation.  
  
##  <a name="Example"></a> Example  
 The following example uses [!INCLUDE[tsql](../../includes/tsql-md.md)] and shows you how to create a table-valued parameter type, declare a variable to reference it, fill the parameter list, and then pass the values to a stored procedure.  
  
```  
USE AdventureWorks2012;  
GO  
  
/* Create a table type. */  
CREATE TYPE LocationTableType AS TABLE   
( LocationName VARCHAR(50)  
, CostRate INT );  
GO  
  
/* Create a procedure to receive data for the table-valued parameter. */  
CREATE PROCEDURE dbo. usp_InsertProductionLocation  
    @TVP LocationTableType READONLY  
    AS   
    SET NOCOUNT ON  
    INSERT INTO AdventureWorks2012.Production.Location  
           (Name  
           ,CostRate  
           ,Availability  
           ,ModifiedDate)  
        SELECT *, 0, GETDATE()  
        FROM  @TVP;  
        GO  
  
/* Declare a variable that references the type. */  
DECLARE @LocationTVP AS LocationTableType;  
  
/* Add data to the table variable. */  
INSERT INTO @LocationTVP (LocationName, CostRate)  
    SELECT Name, 0.00  
    FROM AdventureWorks2012.Person.StateProvince;  
  
/* Pass the table variable data to a stored procedure. */  
EXEC usp_InsertProductionLocation @LocationTVP;  
GO  
```  
  
## See Also  
 [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md)   
 [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [sys.types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)   
 [sys.parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md)   
 [sys.parameter_type_usages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-parameter-type-usages-transact-sql.md)   
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md)  
  
  
