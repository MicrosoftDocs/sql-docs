---
title: "sys.destination_data_spaces (Transact-SQL)"
description: sys.destination_data_spaces (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/03/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.destination_data_spaces"
  - "destination_data_spaces_TSQL"
  - "destination_data_spaces"
  - "sys.destination_data_spaces_TSQL"
helpviewer_keywords:
  - "sys.destination_data_spaces catalog view"
dev_langs:
  - "TSQL"
---
# sys.destination_data_spaces (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains a row for each data space destination of a partition scheme.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**partition_scheme_id**|**int**|ID of the partition-scheme that is partitioning to the data space. For partitioned tables, this can be joined to **data_space_id** in `sys.partition_schemes`.|  
|**destination_id**|**int**|ID (1-based ordinal) of the destination-mapping, unique within the partition scheme.|  
|**data_space_id**|**int**|ID of the data space to which data for this scheme's destination is being mapped.|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Create Partitioned Tables and Indexes](../partitions/create-partitioned-tables-and-indexes.md#create-partitioned-tables-and-indexes)
- [sys.partition_schemes (Transact-SQL)](sys-partition-schemes-transact-sql.md)
