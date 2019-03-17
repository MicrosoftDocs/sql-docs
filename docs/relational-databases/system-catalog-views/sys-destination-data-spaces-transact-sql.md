---
title: "sys.destination_data_spaces (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.destination_data_spaces"
  - "destination_data_spaces_TSQL"
  - "destination_data_spaces"
  - "sys.destination_data_spaces_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.destination_data_spaces catalog view"
ms.assetid: 92df932b-ad5c-43f8-81f4-b158823ab189
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.destination_data_spaces (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains a row for each data space destination of a partition scheme.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**partition_scheme_id**|**int**|ID of the partition-scheme that is partitioning to the data space.|  
|**destination_id**|**int**|ID (1-based ordinal) of the destination-mapping, unique within the partition scheme.|  
|**data_space_id**|**int**|ID of the data space to which data for this scheme's destination is being mapped.|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
