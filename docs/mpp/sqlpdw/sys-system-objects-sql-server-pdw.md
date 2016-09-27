---
title: "sys.system_objects (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ad341b90-e949-43f1-9c18-381b390d0923
caps.latest.revision: 11
author: BarbKess
---
# sys.system_objects (SQL Server PDW)
Returns system objects, including tables and views.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|name|**sysname**|Object name.||  
|object_id|**int**|Object identification number. Is unique within a database.||  
|principal_id|**int**|id of the individual owner, if different from the schema owner.|See principal_id in [sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md).|  
|schema_id|**int**|id of the schema that the object is contained in. Schema-scoped system objects are always contained in the sys or INFORMATION_SCHEMA schemas.||  
|parent_object_id|**int**|id of the object to which this object belongs.<br /><br />0 = Not a child object.||  
|type|**char(2)**|Object type:<br /><br />FN = SQL scalar function<br /><br />IT = Internal table<br /><br />S = System base table<br /><br />U = Table (user-defined)<br /><br />V = View|FN, IT, S, U, V|  
|type_desc|**nvarchar(60)**|SQL_SCALAR_FUNCTION<br /><br />INTERNAL_TABLE<br /><br />SYSTEM_TABLE<br /><br />USER_TABLE<br /><br />VIEW|SQL_SCALAR_FUNCTION,<br /><br />INTERNAL_TABLE, SYSTEM_TABLE,<br /><br />USER_TABLE, VIEW|  
|create_date|**datetime**|Date the object was created.||  
|modify_date|**datetime**|Date the object was last modified by using an ALTER statement. If the object is a table or a view, modify_date also changes when a clustered index on the table or view is created or altered.||  
|is_ms_shipped|**bit**|Object is created by an internal SQL Server component.||  
|is_published|**bit**|Object is published.|Always 0.|  
|is_schema_published|**bit**|Only the schema of the object is published.|Always 0.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
