---
title: "DROP SCHEMA (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 41f6b983-84e4-4106-b560-7234598874b9
caps.latest.revision: 7
author: BarbKess
---
# DROP SCHEMA (SQL Server PDW)
Removes a schema from the database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP SCHEMA schema_name  
```  
  
## Arguments  
*schema_name*  
Is the name by which the schema is known within the database.  
  
## Remarks  
The schema that is being dropped must not contain any objects. If the schema contains objects, the DROP statement fails.  
  
Information about schemas is visible in the sys.schemas catalog view.  
  
## Permissions  
Requires CONTROL permission on the schema or ALTER ANY SCHEMA permission on the database.  
  
## Locking  
Takes an exclusive lock on the SCHEMA and on the SCHEMARESOLUTION object. Takes a shared lock on the DATABASE object.  
  
## Examples  
The following example creates the schema `Sprockets` and a table `Sprockets.NineProngs`.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE SCHEMA Sprockets;  
CREATE TABLE NineProngs (source int, cost int, partnumber int);  
GO  
```  
  
The following statements drop the schema. Note that you must first drop the table that is contained by the schema.  
  
```  
DROP TABLE Sprockets.NineProngs;  
DROP SCHEMA Sprockets;  
GO  
```  
  
## See Also  
[CREATE SCHEMA &#40;SQL Server PDW&#41;](../sqlpdw/create-schema-sql-server-pdw.md)  
[ALTER SCHEMA &#40;SQL Server PDW&#41;](../sqlpdw/alter-schema-sql-server-pdw.md)  
[sys.schemas &#40;SQL Server PDW&#41;](../sqlpdw/sys-schemas-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
