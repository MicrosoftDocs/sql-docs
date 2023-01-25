---
title: "sys.data_spaces (Transact-SQL)"
description: sys.data_spaces (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "data_spaces"
  - "sys.data_spaces_TSQL"
  - "sys.data_spaces"
  - "data_spaces_TSQL"
helpviewer_keywords:
  - "sys.data_spaces catalog view"
dev_langs:
  - "TSQL"
ms.assetid: f39d55fe-2c0f-472d-a77f-cebc6fea95b5
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.data_spaces (Transact-SQL)
[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

  Contains a row for each data space. This can be a filegroup, partition scheme, or FILESTREAM data filegroup.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|**sysname**|Name of data space, unique within the database.|  
|data_space_id|**int**|Data space ID number, unique within the database.|  
|type|**char(2)**|Data space type:<br /><br /> FG = Filegroup<br /><br /> FD = FILESTREAM data filegroup<br /><br /> FX = Memory-optimized tables filegroup<br /><br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> PS = Partition scheme|  
|type_desc|**nvarchar(60)**|Description of data space type:<br /><br /> FILESTREAM_DATA_FILEGROUP<br /><br /> MEMORY_OPTIMIZED_DATA_FILEGROUP<br /><br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> PARTITION_SCHEME<br /><br /> ROWS_FILEGROUP|  
|is_default|**bit**|1 = This is the default data space. The default data space is used when a filegroup or partition scheme is not specified in a CREATE TABLE or CREATE INDEX statement.<br /><br /> 0 = This is not the default data space.|  
|is_system|**bit**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> 1 = Data space is used for full-text index fragments.<br /><br /> 0 = Data space is not used for full-text index fragments.|  
  
## Permissions  
 Requires membership in the public role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Data Spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-spaces-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [sys.destination_data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-destination-data-spaces-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)   
 [sys.partition_schemes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-schemes-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)   
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md)  
  
