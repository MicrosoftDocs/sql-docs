---
title: "CREATE PROCEDURE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3703323f-d9ab-4795-8ff7-dd971c2c47e3
caps.latest.revision: 32
author: BarbKess
---
# CREATE PROCEDURE (SQL Server PDW)
Creates a stored procedure for SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
-- Create a stored procedure   
CREATE { PROC | PROCEDURE } [ schema_name.] procedure_name  
    [ { @parameterdata_type } [ OUT | OUTPUT ] ] [ ,...n ]  
AS { [ BEGIN ] sql_statement [;][ ,...n ] [ END ] }  
[;]  
```  
  
## Arguments  
*schema_name*  
The schema of the procedure.  
  
*procedure_name*  
The name of the procedure. Procedure names must comply with the rules for identifiers and must be unique within the schema.  
  
Avoid the use of the **sp_** prefix when naming procedures. This prefix is used by SQL Server to designate system procedures. Using the prefix can cause application code to break if there is a system procedure with the same name.  
  
**@***parameter*  
A parameter declared in the procedure. Specify a parameter name by using the at sign (@) as the first character. The parameter name must comply with the rules for identifiers. Parameters are local to the procedure; the same parameter names can be used in other procedures.  
  
OUT | OUTPUT  
Indicates that the parameter is an output parameter. Use OUTPUT parameters to return values to the caller of the procedure. **text**, **ntext**, and **image** parameters cannot be used as OUTPUT parameters. An output parameter can be a cursor placeholder. A table-value data type cannot be specified as an OUTPUT parameter of a procedure.  
  
{ [ BEGIN ] *sql_statement* [;] [ ...*n* ] [ END ] }  
One or more SQL statements comprising the body of the procedure. You can use the optional **BEGIN** and **END** keywords to enclose the statements.  
  
## General Remarks  
For a general description of stored procedures, see the SQL Server documentation at [CREATE PROCEDURE (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187926.aspx).  
  
Stored procedures can call stored procedures, and have up to 8 nested levels.  
  
The following formats for calling stored procedures are permitted.  
  
-   Calling a stored procedure with name determined at runtime: `EXEC ('EXEC ' + @var);`  
  
-   Calling a stored procedure from within a stored procedure: `CREATE sp_first AS EXEC sp_second; EXEC sp_third;`  
  
-   Executing a SQL string: `EXEC (''SELECT * FROM sys.types'');`  
  
-   Executing a nested string: `EXEC ('EXEC (''SELECT * FROM sys.types'')');`  
  
-   Calling a stored procedure by name without the EXEC or EXECUTE keyword if the statement is the first statement in the batch.  
  
## Limitations and Restrictions  
When running CREATE PROCEDURE in a batch, CREATE PROCEDURE must be the first statement in the batch. This behavior is the same as SQL Server.  
  
The following stored procedure features are not supported in SQL Server PDW.  
  
-   Temporary stored procedures.  
  
-   Numbered stored procedures.  
  
-   Table valued parameters for stored procedures.  
  
-   Extended stored procedures.  
  
-   CLR stored procedures.  
  
-   Read-only parameters for stored procedures.  
  
-   Encrypted stored procedures.  
  
-   **INSERT … EXECUTE** statements.  
  
-   Execution of stored procedures under a different user context (the **EXECUTE AS** clause).  
  
-   **RAISERROR** statements in stored procedures.  
  
-   Default parameters.  
  
-   The **RETURN** statement.  
  
## Permissions  
Requires **CREATE PROCEDURE** permission in the database and **ALTER** permission on the schema in which the procedure is being created (dbo), or requires membership in the **db_ddladmin** fixed database role.  
  
The *sql_statement* section of the stored procedure may require additional permissions.  
  
## Locking  
Takes an exclusive lock on the procedure. Takes a shared lock on the DATABASE, SCHEMA, and SCHEMARESOLUTION object.  
  
## Examples  
  
### A. Create a Stored Procedure that runs a SELECT statement  
This example shows the basic syntax for creating and running a procedure. When running a batch, CREATE PROCEDURE must be the first statement. For example, to create the following stored procedure in **AdventureWorksPDW2012**, set the database context first, and then run the CREATE PROCEDURE statement.  
  
```  
--Run this statement first to set the database context.  
USE AdventureWorksPDW2012;  
  
--Run CREATE PROCEDURE as the first statement in a batch.  
CREATE PROCEDURE Get10TopResellers   
AS   
BEGIN  
    SELECT TOP (10) r.ResellerName, r.AnnualSales  
    FROM DimReseller AS r  
    ORDER BY AnnualSales DESC, ResellerName ASC;  
END  
;  
  
--Show 10 Top Resellers  
EXEC Get10TopResellers;  
  
--DROP the procedure if you need to re-run the example.  
DROP PROCEDURE Get10TopResellers;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[ALTER PROCEDURE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-procedure-sql-server-pdw.md)  
[DROP PROCEDURE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-procedure-sql-server-pdw.md)  
  
