---
title: "ALTER PROCEDURE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d64b49f4-7e0f-43e9-a69d-7b82f8185313
caps.latest.revision: 13
author: BarbKess
---
# ALTER PROCEDURE (SQL Server PDW)
Changes the definition of a stored procedure on a SQL Data Warehouse database.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```Transact-SQL  
--Transact-SQL Stored Procedure Syntax  
ALTER { PROC | PROCEDURE } [schema_name.] procedure_name  
    [ { @parameterdata_type } [= ] ] [ ,...n ]  
AS { [ BEGIN ] sql_statement [ ; ] [ ,...n ] [ END ] }  
[;]  
```  
  
## Arguments  
*schema_name*  
The name of the schema to which the procedure belongs.  
  
*procedure_name*  
The name of the procedure to change. Procedure names must comply with the rules for identifiers.  
  
**@***parameter*  
A parameter in the procedure. Up to 2,100 parameters can be specified.  
  
{ [ BEGIN ] *sql_statement* [;] [ ...*n* ] [ END ] }  
One or more Transact\-SQL statements comprising the body of the procedure. You can use the optional BEGIN and END keywords to enclose the statements. For more information, see the Remarks section in [CREATE PROCEDURE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-procedure-sql-server-pdw.md).  
  
## General Remarks  
For a general description of stored procedures, see the SQL Server documentation at [CREATE PROCEDURE (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187926.aspx).  
  
## Permissions  
Requires **ALTER** permission on the stored procedure, or membership in the **db_ddladmin** fixed database role.  
  
The *sql_statement* section of the stored procedure may require additional permissions.  
  
## Locking  
Takes an exclusive lock on the procedure. Takes a shared lock on the DATABASE, SCHEMA, and SCHEMARESOLUTION object.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE PROCEDURE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-procedure-sql-server-pdw.md)  
[DROP PROCEDURE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-procedure-sql-server-pdw.md)  
  
