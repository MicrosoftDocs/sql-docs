---
title: "OBJECT_ID (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7265b61d-1452-4563-8ffa-4eec2f560993
caps.latest.revision: 12
author: BarbKess
---
# OBJECT_ID (SQL Server PDW)
Returns the database object identification number of a schema-scoped object.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
OBJECT_ID ('[ database_name . [ schema_name ] . | schema_name . ]   
  object_name' [ ,'object_type' ] )  
```  
  
## Arguments  
**'***object_name***'**  
The object to be used. *object_name* is either **varchar** or **nvarchar**. If *object_name* is **varchar**, it is implicitly converted to **nvarchar**. Specifying the database and schema names is optional.  
  
**'***object_type***'**  
The schema-scoped object type. *object_type* is either **varchar** or **nvarchar**. If *object_type* is **varchar**, it is implicitly converted to **nvarchar**.  
  
## Return Types  
**int**  
  
## Exceptions  
Returns NULL on error.  
  
A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as OBJECT_ID may return NULL if the user does not have any permission on the object.  
  
## Remarks  
When the parameter to a system function is optional, the current database, host computer, server user, or database user is assumed. Built-in functions must always be followed by parentheses.  
  
System functions can be used in most places where expressions are allowed, including a **SELECT** list or a **WHERE** clause. In situations where a system function is not supported, the statement will return an error.  
  
## Examples  
The following example returns the object ID for the  `FactFinance` table in the **AdventureWorksPDW2012** database.  
  
```  
SELECT OBJECT_ID(AdventureWorksPDW2012.dbo.FactFinance') AS 'Object ID';  
```  
  
## See Also  
[OBJECT_NAME &#40;SQL Server PDW&#41;](../sqlpdw/object-name-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
