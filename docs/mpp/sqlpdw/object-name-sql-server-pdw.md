---
title: "OBJECT_NAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: bc295f05-c32c-49c4-83a0-564249df9788
caps.latest.revision: 11
author: BarbKess
---
# OBJECT_NAME (SQL Server PDW)
Returns the database object name for schema-scoped objects.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
OBJECT_NAME ( object_id [, database_id ] )  
```  
  
## Arguments  
*object_id*  
The ID of the object to be used. *object_id* is **int** and is assumed to be a schema-scoped object in the specified database, or in the current database context.  
  
*database_id*  
The ID of the database where the object is to be looked up. *database_id* is **int**.  
  
## Return Types  
**sysname**  
  
## Error Handling  
Returns NULL on error or if a caller does not have permission to view the object.  
  
A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as OBJECT_NAME may return NULL if the user does not have any permission on the object.  
  
## Permissions  
Requires ANY permission on the object.  
  
## Remarks  
System functions can be used in most places where expressions are allowed, including a **SELECT** list or a **WHERE** clause. In situations where a system function is not supported, the statement will return an error.  
  
By default, the SQL Server PDW assumes that *object_id* is in the context of the current database. A query that references an *object_id* in another database returns NULL or incorrect results.  
  
You can resolve object names in the context of another database by specifying a database ID. The following example specifies the database ID for the `master` database in the `OBJECT_SCHEMA_NAME` function and returns the correct results.  
  
## Examples  
  
### A. Using OBJECT_NAME in a WHERE clause  
The following example returns columns from the `sys.objects` catalog view for the object specified by `OBJECT_NAME` in the `WHERE` clause of the `SELECT` statement. (Your object number (274100017 in the example below) will be different.  To test this example, look up a valid object number by executing `SELECT name, object_id FROM sys.objects;` in your database.)  
  
```  
SELECT name, object_id, type_desc  
FROM sys.objects  
WHERE name = OBJECT_NAME(274100017);  
```  
  
## See Also  
[OBJECT_ID &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/object-id-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
