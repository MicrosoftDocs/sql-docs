---
title: "sp_replmonitorhelpmergesessiondetail (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_replmonitorhelpmergesessiondetail"
  - "sp_replmonitorhelpmergesessiondetail_TSQL"
helpviewer_keywords: 
  - "sp_replmonitorhelpmergesessiondetail"
ms.assetid: 805c92fc-3169-410c-984d-f37e063b791d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_replmonitorhelpmergesessiondetail (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns detailed, article-level information about a specific replication Merge Agent session, which is used to monitor merge replication. The result set includes a detail row for each article that was synchronized during the session. It also includes a row that represents the session initialization and rows that summarize both the upload and download phases of the session. This stored procedure is executed at the Distributor on the distribution database, or at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_replmonitorhelpmergesessiondetail [ @session_id = ] session_id  
```  
  
## Arguments  
 [ **@session_id** = ] *session_id*  
 Specifies an agent session. *session_id* is **int** with no default.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**PhaseID**|**int**|Is the phase of the synchronization session, which can be one of the following:<br /><br /> **0** = Initialization or summary row<br /><br /> **1** = Upload<br /><br /> **2** = Download|  
|**ArticleName**|**sysname**|Is the name of the article being synchronized. **ArticleName** also contains summary information for rows in the result set that do not represent article details.|  
|**PercentComplete**|**decimal**|Indicates the percent of the total changes applied in a given article detail row for currently running or failed sessions.|  
|**RelativeCost**|**decimal**|Indicates the time spent synchronizing the article as a percentage of the total synchronization time for the session.|  
|**Duration**|**int**|Length of the agent session.|  
|**Inserts**|**int**|Number of inserts in a session.|  
|**Updates**|**int**|Number of updates in a session.|  
|**Deletes**|**int**|Number of deletes in a session.|  
|**Conflicts**|**int**|Number of conflicts that occurred in a session.|  
|**ErrorID**|**int**|ID of a session error.|  
|**SeqNo**|**int**|Order of sessions in the result set.|  
|**RowType**|**int**|Indicates what type of information each row in the result set represents.<br /><br /> **0** = initialization<br /><br /> **1** = upload summary<br /><br /> **2** = article upload detail<br /><br /> **3** = download summary<br /><br /> **4** = article download detail|  
|**SchemaChanges**|**int**|Number of schema changes in a session.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_replmonitorhelpmergesessiondetail** is used to monitor merge replication.  
  
 When executed on the Subscriber, **sp_replmonitorhelpmergesessiondetail** only returns detailed information about the last 5 Merge Agent sessions.  
  
## Permissions  
 Only members of the **db_owner** or **replmonitor** fixed database role on the distribution database at the Distributor or on the subscription database at the Subscriber can execute **sp_replmonitorhelpmergesessiondetail**.  
  
## See Also  
 [Programmatically Monitor Replication](../../relational-databases/replication/monitor/programmatically-monitor-replication.md)  
  
  
