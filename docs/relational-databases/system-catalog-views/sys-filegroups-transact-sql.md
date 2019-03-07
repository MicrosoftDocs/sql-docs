---
title: "sys.filegroups (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.filegroups_TSQL"
  - "filegroups"
  - "sys.filegroups"
  - "filegroups_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.filegroups catalog view"
ms.assetid: 9e851f72-1f8e-4515-a25d-152ebc12ed56
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.filegroups (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row for each data space that is a filegroup.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**|--|For a list of columns that this view inherits, see [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md).|  
|**filegroup_guid**|**uniqueidentifier**|GUID for the filegroup.<br /><br /> NULL = PRIMARY filegroup|  
|**log_filegroup_id**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)] In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the value is NULL.|  
|**is_read_only**|**bit**|1 = Filegroup is read-only.<br /><br /> 0 = Filegroup is read/write.|  
|**is_autogrow_all_files**|**bit**|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).<br /><br /> 1 = When a file in the filegroup meets the autogrow threshold, all files in the filegroup grow.<br /><br /> 0 = When a file in the filegroup meets the autogrow threshold, only that file grows. This is the default.|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Data Spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-spaces-transact-sql.md)  
  
  
