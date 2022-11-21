---
title: "sys.filegroups (Transact-SQL)"
description: sys.filegroups (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/24/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.filegroups_TSQL"
  - "filegroups"
  - "sys.filegroups"
  - "filegroups_TSQL"
helpviewer_keywords:
  - "sys.filegroups catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.filegroups (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Contains a row for each data space that is a filegroup.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**|--|For a list of columns that this view inherits, see [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md).|  
|**filegroup_guid**|**uniqueidentifier**|GUID for the filegroup.<br /><br /> NULL = PRIMARY filegroup|  
|**log_filegroup_id**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)] In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the value is NULL.|  
|**is_read_only**|**bit**|1 = Filegroup is read-only.<br /><br /> 0 = Filegroup is read/write.|  
|**is_autogrow_all_files**|**bit**|[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)].<br /><br /> 1 = When a file in the filegroup meets the autogrow threshold, all files in the filegroup grow.<br /><br /> 0 = When a file in the filegroup meets the autogrow threshold, only that file grows. This is the default.|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Data Spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-spaces-transact-sql.md)  
  
