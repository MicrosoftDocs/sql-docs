---
title: "sys.fulltext_index_catalog_usages (Transact-SQL)"
description: sys.fulltext_index_catalog_usages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.fulltext_index_catalog_usages"
  - "sys.fulltext_index_catalog_usages_TSQL"
  - "fulltext_index_catalog_usages"
  - "fulltext_index_catalog_usages_TSQL"
helpviewer_keywords:
  - "sys.fulltext_index_catalog_usages catalog view"
dev_langs:
  - "TSQL"
ms.assetid: d095ab62-270b-484b-a541-9f9e7c951cf0
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fulltext_index_catalog_usages (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a row for each full-text catalog to full-text index reference.    
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the full-text indexed table. Is unique within the database.|  
|**index_id**|**int**|ID of full-text index.|  
|**fulltext_catalog_id**|**int**|ID of full-text catalog.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)]  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Data Spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-spaces-transact-sql.md)  
  
  
