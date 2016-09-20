---
title: "sys.objects (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c36fa71e-549a-4533-a6cd-1314d26f533f
caps.latest.revision: 13
author: BarbKess
---
# sys.objects (SQL Server PDW)
Contains a row for each user-defined object that is created within a database.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|name|**sysname**|Object name.||  
|object_id|int|Object identification number. Is unique within a database.||  
|principal_id|int|id of the individual owner, if different from the schema owner.|See principal_id in [sys.database_principals &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-database-principals-sql-server-pdw.md).|  
|schema_id|int|id of the schema that the object is contained in. Schema-scoped system objects are always contained in the sys or INFORMATION_SCHEMA schemas.||  
|parent_object_id|int|Id of the object to which this object belongs.<br /><br />0 = Not a child object.||  
|type|char(2)|Object type:<br /><br />ET = External Table<br /><br />FN = SQL scalar function<br /><br />IT = Internal table<br /><br />P = SQL_STORED_PROCEDURE<br /><br />S = System base table<br /><br />U = Table (user-defined)<br /><br />V = View||  
|type_desc|nvarchar(60)|SQL_SCALAR_FUNCTION<br /><br />INTERNAL_TABLE<br /><br />SYSTEM_TABLE<br /><br />USER_TABLE<br /><br />VIEW||  
|create_date|datetime|Date the object was created.||  
|modify_date|datetime|Date the object was last modified by using an ALTER statement. If the object is a table or a view, modify_date also changes when an index on the table or view is created or altered.||  
|is_ms_shipped|bit|Object is created by an internal SQL Server component.||  
|is_published|bit|Object is published.|Always 0.|  
|is_schema_published|bit|Only the schema of the object is published.|Always 0.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
[sys.all_objects &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-all-objects-sql-server-pdw.md)  
  
