---
title: "PARSENAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 98093526-7656-4f03-9128-74f7aca5d835
caps.latest.revision: 6
author: BarbKess
---
# PARSENAME (SQL Server PDW)
Returns the specified part of an object name. The parts of an object that can be retrieved are the object name, owner name, database name, and server name.  
  
> [!NOTE]  
> The PARSENAME function does not indicate whether an object by the specified name exists. PARSENAME just returns the specified part of the specified object name.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
PARSENAME ( 'object_name' , object_piece )  
```  
  
## Arguments  
'*object_name*'  
The name of the object for which to retrieve the specified object part. *object_name* is **sysname**. This parameter is an optionally-qualified object name. If all parts of the object name are qualified, this name can have four parts: the server name, the database name, the owner name, and the object name.  
  
*object_piece*  
The object part to return. *object_piece* is of type **int**, and can have these values:  
  
1 = Object name  
  
2 = Schema name  
  
3 = Database name  
  
4 = Server name  
  
## Return Types  
**nchar**  
  
## Remarks  
PARSENAME returns NULL if one of the following conditions is true:  
  
-   Either *object_name* or *object_piece* is NULL.  
  
-   A syntax error occurs.  
  
-   The requested object part has a length of 0 and is not a valid Microsoft SQL Server PDW identifier.  
  
## Examples  
The following example uses `PARSENAME` to return information about the `Person` table in the `AdventureWorks2012` database.  
  
```  
USE AdventureWorksPDW2012;  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 1) AS 'Object Name';  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 2) AS 'Schema Name';  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 3) AS 'Database Name';  
SELECT PARSENAME('AdventureWorksPDW2012.dbo.DimCustomer', 4) AS 'Server Name';  
GO  
```  
  
Here is the result set.  
  
<pre>Object Name  
------------------------------  
DimCustomer  
(1 row(s) affected)  
Schema Name  
------------------------------  
dbo  
(1 row(s) affected)  
Database Name  
------------------------------  
AdventureWorksPDW2012  
(1 row(s) affected)  
Server Name  
------------------------------  
(null)  
(1 row(s) affected)</pre>  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
