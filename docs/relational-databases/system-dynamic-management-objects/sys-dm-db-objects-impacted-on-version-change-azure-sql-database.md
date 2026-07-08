---
title: "sys.dm_db_objects_impacted_on_version_change"
titleSuffix: Azure SQL Database & SQL database in Fabric
description: The sys.dm_db_objects_impacted_on_version_change DMV provides an early warning system to determine objects that will be impacted by a major release upgrade.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf
ms.date: 09/11/2025
ms.service: azure-sql-database
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_db_objects_impacted_on_version_change_TSQL"
  - "dm_db_objects_impacted_on_version_change"
  - "dm_db_objects_impacted_on_version_change_TSQL"
  - "sys.dm_db_objects_impacted_on_version_change"
helpviewer_keywords:
  - "dm_db_objects_impacted_on_version_change"
  - "sys.dm_db_objects_impacted_on_version_change"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =fabric-sqldb"
---
# sys.dm_db_objects_impacted_on_version_change

[!INCLUDE[Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/asdb-asmi-fabricsqldb.md)]

  The `sys.dm_db_objects_impacted_on_version_change` DMV provides an early warning system to determine objects that will be impacted by a major release upgrade in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. You can use the view either before or after the upgrade to get a full enumeration of affected objects. You will need to query this view in each database to get a full accounting across the entire server.

| Column name |Data Type|Description|    
| ----------------- |---------------|-----------------|    
| `class` |**int** `NOT NULL`|The class of the object that will be affected:<br /><br /> **1** = constraint<br /><br /> **7** = Indexes and heaps|    
| `class_desc` |**nvarchar(60)** `NOT NULL`|Description of the class:<br /><br /> `OBJECT_OR_COLUMN`<br /><br /> `INDEX`|    
| `major_id` |**int** `NOT NULL`|Object ID of the constraint, or object ID of table that contains index or heap.|    
| `minor_id` |**int** `NULL`|`NULL` for constraints<br /><br /> `Index_id` for indexes and heaps|    
| `dependency` |**nvarchar(60)** `NOT NULL`|Description of dependency that is causing a constraint or index to be affected. The same value is also used for warnings generated during upgrade.<br /><br /> Examples:<br /><br /> **space** (for intrinsic)<br /><br /> **geometry** (for system UDT)<br /><br /> **geography::Parse** (for system UDT method)|    

## Permissions

 Requires the VIEW `DATABASE STATE` permission.    

## Examples

 The following example shows a query on `sys.dm_db_objects_impacted_on_version_change` to find the objects affected by an upgrade to the next major server version.

```sql
SELECT * FROM sys.dm_db_objects_disabled_on_version_change;  
GO
```

```output
class  class_desc        major_id    minor_id    dependency
------ ----------------- ----------- ----------- ----------   
1      OBJECT_OR_COLUMN  181575685   NULL        geometry
7      INDEX             37575172    1           geometry
7      INDEX             2121058592  1           geometry
1      OBJECT_OR_COLUMN  101575400   NULL        geometry
```

## Remarks

### How to update affected objects

 The following ordered steps describe the corrective action to take after the upcoming June service release upgrade.

| Order |Affected Object|Corrective Action|    
| ----------- |---------------------|-----------------------|
| `1` |**Indexes**|Rebuild any index identified by `sys.dm_db_objects_impacted_on_version_change` For example:  `ALTER INDEX ALL ON <table> REBUILD`<br />or<br />`ALTER TABLE <table> REBUILD`|    
| `2` |**Object**|All constraints identified by `sys.dm_db_objects_impacted_on_version_change` must be revalidated after the geometry and geography data in the underlying table is recomputed. For constraints, revalidate using `ALTER TABLE`. <br />For example: <br />`ALTER TABLE <tab> WITH CHECK CHECK CONSTRAINT <constraint name>`<br />or<br />`ALTER TABLE <tab> WITH CHECK CONSTRAINT ALL`|

## Related content

- [What's new in Azure SQL Database?](/azure/azure-sql/database/doc-changes-updates-release-notes-whats-new)
- [What's new in SQL database in Microsoft Fabric?](/fabric/fundamentals/whats-new#sql-database-in-microsoft-fabric)
