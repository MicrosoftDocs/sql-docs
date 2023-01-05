---
title: "sys.change_tracking_tables (Transact-SQL)"
description: Change Tracking Catalog Views - sys.change_tracking_tables
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/08/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "change_tracking_tables_TSQL"
  - "sys.change_tracking_tables"
  - "change_tracking_tables"
  - "sys.change_tracking_tables_TSQL"
helpviewer_keywords:
  - "change tracking [SQL Server], sys.change_tracking_tables"
  - "sys.change_tracking_tables"
dev_langs:
  - "TSQL"
ms.assetid: 97ec69b6-0d49-4d98-82f0-d3e77ba1ad2b
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Change Tracking Catalog Views - sys.change_tracking_tables
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns one row for each table in the current database that has change tracking enabled.  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of a table that has a change journal. The table can have a change journal even if change tracking is currently off.<br /><br /> The table ID is unique within the database.|  
|is_track_columns_updated_on|**bit**|Current state of change tracking on the table:<br /><br /> 0 = OFF<br /><br /> 1 = ON|  
|begin_version|**bigint**|Version of the database when change tracking began for the table. This version is usually indicates when change tracking was enabled, but this value is reset if the table is truncated.|  
|cleanup_version|**bigint**|Version up to which cleanup might have removed change tracking information.|  
|min_valid_version|**bigint**|Minimum valid version of change tracking information that is available for the table.<br /><br /> When obtaining changes from the table that is associated with this row, the value of last_sync_version must be greater than or equal to the version reported by this column. For more information, see [CHANGE_TRACKING_MIN_VALID_VERSION &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-min-valid-version-transact-sql.md).|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [CHANGE_TRACKING_MIN_VALID_VERSION &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-min-valid-version-transact-sql.md)   
 [Change Tracking Catalog Views &#40;Transact-SQL&#41;](./catalog-views-transact-sql.md)   
 [Track Data Changes &#40;SQL Server&#41;](../../relational-databases/track-changes/track-data-changes-sql-server.md)  
  
