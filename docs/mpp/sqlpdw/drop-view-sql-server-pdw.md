---
title: "DROP VIEW (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 82f14dc1-b584-4cde-9768-3d71d37f7114
caps.latest.revision: 31
author: BarbKess
---
# DROP VIEW (SQL Server PDW)
Deletes a view from the current SQL Server PDW database.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP VIEW [ schema_name . ] view_name   
[;]  
```  
  
## Arguments  
[ *schema_name* . ] *view_name*  
The two-part name for the view to be removed.  
  
## Permissions  
Requires CONTROL permission on the view, ALTER permission on the schema containing the view, or membership in the db_ddladmin fixed server role.  
  
## General Remarks  
When you drop a view, the view is removed from the system catalog. All permissions and all dependent views are dropped.  
  
## Metadata  
To see the Data Definition Language (DDL) for a view, you can use the [sys.views](../../mpp/sqlpdw/sys-views-sql-server-pdw.md) catalog view.  
  
## Locking  
Takes an exclusive lock on the VIEW. Takes a shared lock on the DATABASE, SCHEMA, and SCHEMARESOLUTION objects.  
  
## Examples  
  
### A. Drop a view  
The following example removes the `Reorder` view from the local database.  
  
```  
DROP VIEW Reorder;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE VIEW &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-view-sql-server-pdw.md)  
  
