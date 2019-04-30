---
title: "sys.registered_search_property_lists (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "registered_search_property_lists_TSQL"
  - "sys.registered_search_property_lists"
  - "registered_search_property_lists"
  - "sys.registered_search_property_lists_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full-text search [SQL Server], search property lists"
  - "sys.registered_search_property_lists catalog view"
  - "search property lists [SQL Server], viewing"
ms.assetid: 630d4caa-9bea-4cd3-a5b1-01098b0855fc
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
---
# sys.registered_search_property_lists (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains a row for each search property list on the current database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**property_list_id**|**int**|ID of the property list.|  
|**name**|**sysname**|Name of the property list.|  
|**create_date**|**datetime**|Date the property list was created.|  
|**modify_date**|**datetime**|Date the property list was last modified by any ALTER statement.|  
|**principal_id**|**int**|Owner of the property list.|  
  
## Remarks  
 For more information, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
## Permissions  
 Visibility of the metadata in search property lists is limited to those that you either own or on which you have been granted some REFERENCE permission.  
  
> [!NOTE]  
>  The search property list owner can grant REFERENCE or CONTROL permissions on the list. Users with CONTROL permission can also grant REFERENCE permission to other users.  
  
## Examples  
 The following example displays the ID and name of the search property lists in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT property_list_id, name FROM sys.registered_search_property_lists;  
GO  
```  
  
## See Also  
 [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)   
 [sys.fulltext_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md)  
  
  
