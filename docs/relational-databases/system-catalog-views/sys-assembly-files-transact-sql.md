---
title: "sys.assembly_files (Transact-SQL)"
description: sys.assembly_files (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.assembly_files"
  - "assembly_files_TSQL"
  - "assembly_files"
  - "sys.assembly_files_TSQL"
helpviewer_keywords:
  - "sys.assembly_files catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 1a384a2c-5556-4d12-a2ba-4da781363143
---
# sys.assembly_files (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains a row for each file that makes up an assembly.  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**assembly_id**|**int**|ID of the assembly to which this file belongs.|  
|**name**|**nvarchar(260)**|Name of the assembly file.|  
|**file_id**|**int**|ID of the file. Is unique within an assembly. The file ID numbered 1 represents the assembly DLL.|  
|**content**|**varbinary(max)**|Content of file.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [CLR Assembly Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/clr-assembly-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [ASSEMBLYPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/assemblyproperty-transact-sql.md)  
  
  
