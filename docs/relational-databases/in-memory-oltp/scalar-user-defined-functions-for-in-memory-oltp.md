---
title: "Scalar User-Defined Functions for In-Memory OLTP"
description: Learn how to create and drop natively compiled, scalar user-defined functions for In-Memory OLTP in SQL Server. Native compilation improves performance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/20/2017"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: d2546e40-fdfc-414b-8196-76ed1f124bf5
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Scalar User-Defined Functions for In-Memory OLTP
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], you can create and drop natively compiled, scalar user-defined functions. You can also alter these user-defined functions. Native compilation improves performance of the evaluation of user-defined functions in Transact-SQL.  
  
 When you alter a natively compiled, scalar user-defined function, the application remains available while the operation is being run and the new version of the function is being compiled.  
  
 For supported T-SQL constructs, see [Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md).  
  
## Creating, Dropping, and Altering User-Defined Functions  
 You use the CREATE FUNCTION to create the natively compiled, scalar user-defined function, the DROP FUNCTION to remove the user-defined function, and the ALTER FUNCTION to change the function. BEGIN ATOMIC WITH is required for the user-defined functions.  
  
 For information about the supported syntax and any restrictions, see the following topics.  
  
-   [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md)  
  
-   [ALTER FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-function-transact-sql.md)  
  
-   [DROP FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-function-transact-sql.md)  
  
     The DROP FUNCTION syntax for natively compiled, scalar user-defined functions is the same as for interpreted user-defined functions.  
  
-   [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)  
  
 The [sp_recompile &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-recompile-transact-sql.md)stored procedure can be used with the natively compiled, scalar user-defined function. It will result in the function being recompiled using the definition that exists in metadata.  
  
 The following sample shows a scalar UDF from the [AdventureWorks2016CTP3](https://github.com/microsoft/sql-server-samples/releases/tag/adventureworks) sample database.  
  
```sql  
CREATE FUNCTION [dbo].[ufnLeadingZeros_native](@Value int)   
RETURNS varchar(8)   
WITH NATIVE_COMPILATION, SCHEMABINDING  
AS   
BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'English')  
  
    DECLARE @ReturnValue varchar(8);  
    SET @ReturnValue = CONVERT(varchar(8), @Value);  
       DECLARE @i int = 0, @count int = 8 - LEN(@ReturnValue)  
  
    WHILE @i < @count  
       BEGIN  
            SET @ReturnValue = '0' + @ReturnValue;  
            SET @i += 1  
       END  
  
    RETURN (@ReturnValue);  
  
END  
```  
  
## Calling User-Defined Functions  
 Natively compiled, scalar user-defined functions can be used in expressions, in the same place as built-in scalar functions and interpreted scalar user-defined functions. Natively compiled, scalar user-defined functions can also be used with the EXECUTE statement, in a Transact-SQL statement and in a natively compiled stored procedure.  
  
 You can use these scalar user-defined functions in natively compiled store procedures and natively compiled user-defined functions, and wherever built-in functions are permitted. You can also use natively compiled, scalar user-defined functions in traditional Transact-SQL modules.  
  
 You can use these scalar user-defined functions in interop mode, wherever an interpreted scalar user-defined function can be used. This use is subject to cross-container transaction limitations, as described in **Supported Isolation Levels for Cross-Container Transactions** section in [Transactions with Memory-Optimized Tables](../../relational-databases/in-memory-oltp/transactions-with-memory-optimized-tables.md). For more information about interop mode, see [Accessing Memory-Optimized Tables Using Interpreted Transact-SQL](../../relational-databases/in-memory-oltp/accessing-memory-optimized-tables-using-interpreted-transact-sql.md).  
  
 Natively compiled, scalar user-defined functions do require an explicit execution context. For more information, see [EXECUTE AS Clause &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-clause-transact-sql.md). EXECUTE AS CALLER is not supported. For more information, see [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md).  
  
 For the supported syntax for Transact-SQL Execute statements, for natively compiled, scalar user-defined functions, see [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md). For the supported syntax for executing the user-defined functions in a natively compiled stored procedure, see [Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md).  
  
## Hints and Parameters  
 Support for table, join, and query hints inside natively compiled, scalar user-defined functions is equal to support for these hints for natively compiled stored procedures. As with interpreted scalar user-defined functions, the query hints included with a Transact-SQL query that reference a natively compiled, scalar user-defined function do not impact the query plan for this user-defined function.  
  
 The parameters supported for the natively compiled, scalar user-defined functions are all the parameters supported for natively compiled stored procedures, as long as the parameters are allowed for scalar user-defined functions. An example of a supported parameter is the table-valued parameter.  
  
## Schema-Bound  
 The following apply to natively compiled, scalar user-defined functions.  
  
-   Must be schema-bound, by using the WITH SCHEMABINDING argument in the CREATE FUNCTION and ALTER FUNCTION.  
  
-   Cannot be dropped or altered when referenced by a schema-bound stored procedure or user-defined function.  
  
## SHOWPLAN_XML  
 Natively compiled, scalar user-defined functions support SHOWPLAN_XML. It conforms to the general SHOWPLAN_XML schema, as with natively compiled stored procedures. The base element for the user-defined functions is `<UDF>`.  
  
 STATISTICS XML is not supported for natively compiled, scalar user-defined functions. When you run a query referencing the user-defined function, with STATISTICS XML enabled, the XML content is returned without the part for the user-defined function.  
  
## Permissions  
 As with natively compiled stored procedures, the permissions for objects referenced from a natively compiled, scalar user-defined function are checked when the function is created. The CREATE FUNCTION fails if the impersonated user does not have the correct permissions. If permission changes result in the impersonated user no longer having the correct permissions, subsequent executions of the user-defined function fail.  
  
 When you use a natively compiled, scalar user-defined function inside a natively compiled stored procedure, the permissions for executing the user-defined function are checked when the outer procedure is created. If the user impersonated by the outer procedure does not have EXEC permissions for the user-defined function, the creation of the stored procedure fails. If permission changes result in the user no longer having the EXEC permissions, the execution of the outer procedure fails.  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Save an Execution Plan in XML Format](../../relational-databases/performance/save-an-execution-plan-in-xml-format.md)  
  
  
