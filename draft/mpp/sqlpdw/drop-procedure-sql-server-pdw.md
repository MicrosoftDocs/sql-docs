---
title: "DROP PROCEDURE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 69297d41-81e0-4034-bc6d-bd539782f17d
caps.latest.revision: 10
author: BarbKess
---
# DROP PROCEDURE (SQL Server PDW)
Drops a stored procedure on a SQL Server PDW database.  
  
## Syntax  
  
```vb  
DROP { PROC | PROCEDURE } { [ schema_name. ] procedure_name }  
```  
  
## Arguments  
*schema_name*  
Is the name of the schema to which the procedure belongs. A server name or database name cannot be specified.  
  
*procedure_name*  
Is name of the stored procedure to be removed. Procedure names must follow the rules for [identifiers](assetId:///171291bb-f57f-4ad1-8cea-0b092d5d150c).  
  
## Permissions  
Requires **CONTROL** permission on the procedure, or **ALTER** permission on the schema to which the procedure belongs, or membership in the **db_ddladmin** fixed server role.  
  
## Locking  
Takes an exclusive lock on the procedure. Takes a shared lock on the DATABASE, SCHEMA, and SCHEMARESOLUTION object.  
  
## Examples  
The following example removes the `dbo.uspMyProc` stored procedure in the current database.  
  
```  
DROP PROCEDURE dbo.uspMyProc;  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/create-procedure-sql-server-pdw.md)  
[ALTER PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/alter-procedure-sql-server-pdw.md)  
  
