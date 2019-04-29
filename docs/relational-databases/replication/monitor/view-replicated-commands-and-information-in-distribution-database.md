---
title: "View Replicated Commands and Information in Distribution Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_browsereplcmds"
  - "transactional replication, monitoring"
  - "distribution databases [SQL Server replication], viewing replicated commands"
  - "viewing replicated commands"
ms.assetid: 9c20acec-8fab-4483-b9c1-dfe3768f85dd
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# View Replicated Commands and Information in Distribution Database
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  When using transactional replication, transaction commands are stored in the distribution database until the Distribution Agent propagates them to all Subscribers or a Distribution Agent at the Subscriber pulls the changes. These pending commands in the distribution database can be viewed programmatically using replication stored procedures. For more information, see [Replication Stored Procedures &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).  
  
### To view replicated commands from all transactional publications in the distribution database  
  
1.  At the Distributor on the distribution database, execute [sp_browsereplcmds](../../../relational-databases/system-stored-procedures/sp-browsereplcmds-transact-sql.md).  
  
### To view replicated commands in the distribution database from a specific article or from a specific database published using transactional replication  
  
1.  (Optional) At the Publisher on the publication database, execute [sp_helparticle](../../../relational-databases/system-stored-procedures/sp-helparticle-transact-sql.md). Specify **@publication** and **@article**. Note the value of **article id** in the result set.  
  
2.  At the Distributor on the distribution database, execute [sp_browsereplcmds](../../../relational-databases/system-stored-procedures/sp-browsereplcmds-transact-sql.md). (Optional) Specify the article ID from step 2 for **@article_id**. (Optional) Specify the ID of the publication database for **@publisher_database_id**, which can be obtained from the **database_id** column in the [sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
## See Also  
 [Programmatically Monitor Replication](../../../relational-databases/replication/monitor/programmatically-monitor-replication.md)  
  
  
