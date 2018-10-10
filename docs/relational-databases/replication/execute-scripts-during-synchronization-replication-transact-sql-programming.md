---
title: "Execute Scripts During Synchronization (Replication Transact-SQL Programming) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "synchronization [SQL Server replication], scripts"
  - "scripts [SQL Server replication], synchronization and"
  - "sp_addscriptexec"
ms.assetid: b58a0877-4e43-4fab-a281-24e6022d3fb1
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Execute Scripts During Synchronization (Replication Transact-SQL Programming)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Replication supports on demand script execution for Subscribers to transactional and merge publications. This functionality copies the script to the replication working directory and then uses **sqlcmd** to apply the script at the Subscriber. By default, if there is a failure when applying the script for a subscription to a transactional publication, the Distribution Agent will stop. You can specify a [!INCLUDE[tsql](../../includes/tsql-md.md)] script to execute programmatically using replication stored procedures.  
  
### To specify a script to run for all Subscribers to a snapshot, transactional or merge publication  
  
1.  Compose and test the [!INCLUDE[tsql](../../includes/tsql-md.md)] script that will be executed on demand.  
  
2.  Save the script file to a location where it can be accessed by the Snapshot Agent for the publication.  
  
3.  At the Publisher on the publication database, execute [sp_addscriptexec &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addscriptexec-transact-sql.md). Specify **@publication**, the name of the script file with full UNC path created in step 2 for **@scriptfile**, and one of the following values for **@skiperror**:  
  
    -   **0** - the agent will stop executing the script if an error is encountered.  
  
    -   **1** - the agent will log errors and continue executing the script when errors are encountered.  
  
4.  The specified script will be executed at each Subscriber when the agent next runs to synchronize the subscription.  
  
## See Also  
 [Synchronize Data](../../relational-databases/replication/synchronize-data.md)  
  
  
