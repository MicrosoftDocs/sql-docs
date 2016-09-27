---
title: "RENAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 691eca95-5ada-4a74-947b-95daa3819f9a
caps.latest.revision: 32
author: BarbKess
---
# RENAME (SQL Server PDW)
Renames a user-created table or database in SQL Server PDW.  
  
## Syntax  
  
```  
RENAME <class_type> [ :: ] entity_name TO new_entity_name  
[;]  
  
<class_type> ::=  
{  
    DATABASE  
    | OBJECT  
}  
```  
  
## Arguments  
*entity_name*  
The name of the table or database to be renamed. For tables, you can specify *entity_name* as a three-part table name. The table can any type of user-created table including distributed, replicated, or external. You cannot rename a database to any of these [Reserved Database Names &#40;SQL Server PDW&#41;](../sqlpdw/reserved-database-names-sql-server-pdw.md).  
  
*new_entity_name*  
The new name of the table or database. For tables, you can specify *entity_name* as a two-part table name.  
  
DATABASE  
Specifies that the entity to be renamed is a database.  
  
OBJECT  
Specifies that the entity to be renamed is an object. Tables are the only objects supported in this release.  
  
## Permissions  
Requires the ALTER permission on the table.  
  
## General Remarks  
When renaming a table, all objects and properties associated with the table are updated to reference the new table name. For example, table definitions, indexes, constraints, and permissions are updated. Views are not updated. For more information, see Limitations and Restrictions.  
  
RENAME cannot be used to move a table to a different schema; use **ALTER SCHEMA … TRANSFER** instead.  
  
Renaming an external table changes the table name within SQL Server PDW. It does not effect the location of the external data for the table.  
  
## Limitations and Restrictions  
You cannot rename an entity that is in use by another user.  
  
You cannot rename indexes and views. To accomplish this task, you can drop the index or view and then recreate it with the new name.  
  
When renaming a database, views are not updated. Each view, inside or outside of the database, that uses the former database name will become invalid. For example, if the Sales database is renamed, a view that contains `SELECT * FROM Sales.dbo.table1` will become invalid. To resolve this, you can either avoid using three-part names in views, or update the views to reference the new database name.  
  
When renaming a table, views are not updated to reference the new table name. Each view, inside or outside of the database, that references the former table name will become invalid. To resolve this, you can update each view to reference the new table name.  
  
## Locking  
Takes a shared lock on the DATABASE object, and an exclusive lock on the object being renamed. Renaming an object takes a shared lock on the SCHEMA object.  
  
## Examples  
<pre>RENAME DATABASE::Sales TO SalesArchive;  
RENAME DATABASE Sales TO SalesArchive;  
RENAME OBJECT::Customer TO NewCustomer;  
RENAME OBJECT Customer TO NewCustomer;</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
