---
title: "sys.assembly_references (Transact-SQL)"
description: sys.assembly_references (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "assembly_references"
  - "sys.assembly_references_TSQL"
  - "assembly_references_TSQL"
  - "sys.assembly_references"
helpviewer_keywords:
  - "sys.assembly_references catalog view"
dev_langs:
  - "TSQL"
---
# sys.assembly_references (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains a row for each pair of assemblies where one is directly referencing another.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**assembly_id**|**int**|ID of the assembly to which this reference belongs.|  
|**referenced_assembly_id**|**int**|ID of the assembly being referenced.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Related content

- [CLR Assembly Catalog Views (Transact-SQL)](clr-assembly-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [ASSEMBLYPROPERTY (Transact-SQL)](../../t-sql/functions/assemblyproperty-transact-sql.md)
