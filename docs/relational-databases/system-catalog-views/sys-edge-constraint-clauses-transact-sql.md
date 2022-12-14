---
title: "sys.edge_constraint_clauses (Transact-SQL)"
description: sys.edge_constraint_clauses (Transact-SQL)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "09/17/2018"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.edge_constraint_clauses"
  - "edge_constraint_clauses"
  - "SQL Graph"
  - "edge_constraints_TSQL"
helpviewer_keywords:
  - "sys.edge_constraint_clauses catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 0f782d2f-7126-46ab-85b7-bcba44862231
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.edge_constraint_clauses (Transact-SQL)
[!INCLUDE[sqlserver2019](../../includes/applies-to-version/sqlserver2019.md)]

Contains one row per clause of an edge constraint.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|object_id of the edge constraint.|  
|**from_object_id**|**int**|object_id of the FROM node table.|  
|**to_object_id**|**int**|object_id of the TO node table.|  
|**clause_number**|**int**|Internally generated integer index of the clause.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)  
  
  
