---
title: "sys.partition_schemes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "partition_schemes_TSQL"
  - "partition_schemes"
  - "sys.partition_schemes_TSQL"
  - "sys.partition_schemes"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.partition_schemes catalog view"
ms.assetid: ed557fd5-12b0-4cef-9e4f-440b02e99d1f
caps.latest.revision: 37
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.partition_schemes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Contains a row for each Data Space that is a partition scheme, with **type** = PS.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**||Inherits columns from [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md).|  
|**function_id**|**int**|ID of partition function used in the scheme.|  
  
 For a list of columns that this view inherits, see [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)  
  
  