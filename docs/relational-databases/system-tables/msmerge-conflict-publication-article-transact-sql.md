---
title: "MSmerge_conflict_publication_article (T-SQL)"
description: Describes the MSmerge_conflict_publication_article stored procedure which contains information on rows that conflicted or row changes that were undone to achieve data convergence.
author: VanMSFT
ms.author: vanto
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
ms.custom: seo-lt-2019
f1_keywords:
  - "MSmerge_conflict_publication_article_TSQL"
  - "MSmerge_conflict_publication_article"
helpviewer_keywords:
  - "MSmerge_conflict_publication_article system table"
dev_langs:
  - "TSQL"
ms.assetid: dc4490b4-02d8-4dfc-98f5-0cf8de8e11be
---
# MSmerge_conflict_publication_article (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_conflict_publication_article** table contains information on rows that conflicted or row changes that were undone to achieve data convergence. A conflict table exists for each replicated table in a publication, where the name of the conflict table is appended with the publication and article name. These article-specific conflict tables exist in the database used for conflict logging, usually the publication database but can be the subscription database if there is decentralized conflict logging.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**_article\_column\_name_**|**variable**|Represents a column in a replicated table. This system table contains one column for each column in the table article.|  
|**rowguid**|**uniqueidentifier**|The row identifier for the conflict row.|  
|**ModifiedDate**|**datetime**|The time when the conflict occurred.|  
|**origin\_datasource\_id**|**uniqueidentifier**|The subscription for which the row change was undone or that lost the conflict.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
