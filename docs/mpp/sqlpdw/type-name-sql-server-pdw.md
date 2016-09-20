---
title: "TYPE_NAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7cc98b7e-8eed-4b47-b263-dd764cc0d875
caps.latest.revision: 7
author: BarbKess
---
# TYPE_NAME (SQL Server PDW)
Returns the unqualified type name of a specified type ID.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
TYPE_NAME ( type_id )  
```  
  
## Arguments  
*type_id*  
The ID of the type that will be used. *type_id* is an **int**, and it can refer to a type in any schema that the caller has permission to access.  
  
## Return Types  
**sysname**  
  
## Error Handling  
Returns NULL on error or if a caller does not have permission to view the object.  
  
In SQL Server PDW, a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as TYPE_NAME may return NULL if the user does not have any permission on the object.  
  
## Remarks  
TYPE_NAME will return NULL when *type_id* is not valid or when the caller does not have sufficient permission to reference the type.  
  
TYPE_NAME works for system data types and also for user-defined data types. The type can be contained in any schema, but an unqualified type name is always returned. This means the name does not have the *schema***.** prefix.  
  
System functions can be used in the select list, in the WHERE clause, and anywhere an expression is allowed.  
  
## Examples  
The following example returns the `TYPE ID` for the data type with id `1`.  
  
```  
SELECT TYPE_NAME(36) AS Type36, TYPE_NAME(239) AS Type239;  
GO  
```  
  
For a list of types, query sys.types.  
  
```  
SELECT * FROM sys.types;  
GO  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
