---
title: "TYPE_ID (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ce9e2da6-0e0e-464a-bdf2-a4f378e2cbb4
caps.latest.revision: 6
author: BarbKess
---
# TYPE_ID (SQL Server PDW)
Returns the ID for a specified data type name.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
TYPE_ID ( [ schema_name ] type_name )  
```  
  
## Arguments  
*type_name*  
The name of the data type. *type_name* is of type **nvarchar**.  
  
## Return Types  
**int**  
  
## Exceptions  
Returns NULL on error or if a caller does not have permission to view the object.  
  
In SQL Server PDW, a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as TYPE_ID may return NULL if the user does not have any permission on the object.  
  
## Remarks  
TYPE_ID returns NULL if the type name is not valid, or if the caller does not have sufficient permission to reference the type.  
  
## Examples  
The following example returns the `TYPE ID` for the `datetime` system data type.  
  
```  
SELECT TYPE_NAME(TYPE_ID('datetime')) AS typeName,   
    TYPE_ID('datetime') AS typeID FROM table1;  
```  
  
## See Also  
[TYPE_NAME &#40;SQL Server PDW&#41;](../sqlpdw/type-name-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
