---
title: "MSmerge_conflict_&lt;publication&gt;_&lt;article&gt; (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSmerge_conflict_publication_article_TSQL"
  - "MSmerge_conflict_publication_article"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSmerge_conflict_publication_article system table"
ms.assetid: dc4490b4-02d8-4dfc-98f5-0cf8de8e11be
author: stevestein
ms.author: sstein
manager: craigg
---
# MSmerge_conflict_&lt;publication&gt;_&lt;article&gt; (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSmerge_conflict_*publication*_*article*** table contains information on rows that conflicted or for row changes that were undone to achieve data convergence. A conflict table exists for each replicated table in a publication, where the name of the conflict table is appended with the publication and article name. These article-specific conflict tables exist in the database used for conflict logging, usually the publication database but can be the subscription database if there is decentralized conflict logging.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|***article_column_name***|**variable**|Represents a column in a replicated table. This system table contains one column for each column in the table article.|  
|**rowguid**|**uniqueidentifier**|The row identifier for the conflict row.|  
|**ModifiedDate**|**datetime**|The time when the conflict occurred.|  
|**origin_datasource_id**|**uniqueidentifier**|The subscription for which the row change was undone or that lost the conflict.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
