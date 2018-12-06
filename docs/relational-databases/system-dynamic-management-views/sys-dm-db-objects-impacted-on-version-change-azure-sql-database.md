---
title: "sys.dm_db_objects_impacted_on_version_change (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: ""
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_objects_impacted_on_version_change_TSQL"
  - "dm_db_objects_impacted_on_version_change"
  - "dm_db_objects_impacted_on_version_change_TSQL"
  - "sys.dm_db_objects_impacted_on_version_change"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dm_db_objects_impacted_on_version_change"
  - "sys.dm_db_objects_impacted_on_version_change"
ms.assetid: b94af834-c4f6-4a27-80a6-e8e71fa8793a
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.dm_db_objects_impacted_on_version_change (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  This database-scoped system view is designed to provide an early warning system to determine objects that will be impacted by a major release upgrade in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. You can use the view either before or after the upgrade to get a full enumeration of impacted objects. You will need to query this view in each database to get a full accounting across the entire server.  
  
|Column name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|class|**int** NOT NULL|The class of the object which will be impacted:<br /><br /> **1** = constraint<br /><br /> **7** = Indexes and heaps|  
|class_desc|**nvarchar(60)** NOT NULL|Description of the class:<br /><br /> **OBJECT_OR_COLUMN**<br /><br /> **INDEX**|  
|major_id|**int** NOT NULL|object id of the constraint, or object id of table that contains index or heap.|  
|minor_id|**int** NULL|**NULL** for constraints<br /><br /> Index_id for indexes and heaps|  
|dependency|**nvarchar(60)** NOT NULL|Description of dependency that is causing a constraint or index to be impacted. The same value is also used for warnings generated during upgrade.<br /><br /> Examples:<br /><br /> **space** (for intrinsic)<br /><br /> **geometry** (for system UDT)<br /><br /> **geography::Parse** (for system UDT method)|  
  
## Permissions  
 Requires the VIEW DATABASE STATE permission.  
  
## Example  
 The following example shows a query on **sys.dm_db_objects_impacted_on_version_change** to find the objects impacted by an upgrade to the next major server version  
  
```  
SELECT * FROM sys.dm_db_objects_disabled_on_version_change;  
GO  
```  
  
```  
class  class_desc        major_id    minor_id    dependency                       
------ ----------------- ----------- ----------- ----------   
1      OBJECT_OR_COLUMN  181575685   NULL        geometry                        
7      INDEX             37575172    1           geometry                        
7      INDEX             2121058592  1           geometry                        
1      OBJECT_OR_COLUMN  101575400   NULL        geometry     
```  
  
## Remarks  
  
### How to Update Impacted Objects  
 The following ordered steps describe the corrective action to take after the upcoming June service release upgrade.  
  
|Order|Impacted Object|Corrective Action|  
|-----------|---------------------|-----------------------|  
|1|**Indexes**|Rebuild any index identified by **sys.dm_db_objects_impacted_on_version_change** For example:  `ALTER INDEX ALL ON <table> REBUILD`<br />or<br />`ALTER TABLE <table> REBUILD`|  
|2|**Object**|All constraints identified by **sys.dm_db_objects_impacted_on_version_change** must be revalidated after the geometry and geography data in the underlying table is recomputed. For constraints, revalidate using ALTER TABLE. <br />For example: <br />`ALTER TABLE <tab> WITH CHECK CHECK CONSTRAINT <constraint name>`<br />or<br />`ALTER TABLE <tab> WITH CHECK CONSTRAINT ALL`|  
  
  
