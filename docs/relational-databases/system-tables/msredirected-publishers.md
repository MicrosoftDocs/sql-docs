---
description: "MSRedirected_publishers (Transact-SQL)"
title: "MSRedirected_publishers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/06/2021"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "reference"
f1_keywords: 
  - "MSRedirected_publishers_TSQL"
  - "MSRedirected_publishers"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSRedirected_publishers system table"
ms.assetid: ea3dd634-28e1-4676-befc-d9bda87e6c1d
author: briancarrig
ms.author: brcarrig
---
# MSRedirected_publishers (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSRedirected_publishers** table maintains a list of redirected publisher servers in the Distribution database. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**original_publisher**|**sysname**|The name of the original publisher server.|  
|**publisher_db**|**sysname**|The name of the published database.|  
|**redirected_publisher**|**sysname**|The name of the redirected publisher server.|
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
