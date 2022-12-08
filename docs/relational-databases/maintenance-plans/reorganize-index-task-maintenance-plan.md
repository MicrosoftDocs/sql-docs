---
title: "Reorganize Index Task (Maintenance Plan)"
description: Reorganize Index Task (Maintenance Plan)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/21/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.maint.defrag.f1"
helpviewer_keywords:
  - "Reorganize Index Task dialog box"
ms.assetid: e9cbebbd-f36f-4176-9832-382a46ac946c
---
# Reorganize Index Task (Maintenance Plan)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **ReorganizeIndex Task** dialog to move index pages into a more efficient search order. This task uses the `ALTER INDEX REORGANIZE` statement with [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] databases.  
  
## Options  
 **Connection**  
 Select the server connection to use when performing this task.  
  
 **New**  
 Create a new server connection to use when performing this task. The **New Connection** dialog box is described below.  
  
 **Databases**  
 Specify the databases affected by this task.  
  
-   **All databases**  
  
     Generate a maintenance plan that runs maintenance tasks against all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases except tempdb.  
  
-   **All system databases**  
  
     Generate a maintenance plan that runs maintenance tasks against each of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system databases except **tempdb**. No maintenance tasks are run against user-created databases.  
  
-   **All user databases**  
  
     Generate a maintenance plan that runs maintenance tasks against all user-created databases. No maintenance tasks are run against the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system databases.  
  
-   **These specific databases**  
  
     Generate a maintenance plan that runs maintenance tasks against only those databases that are selected. At least one database in the list must be selected if this option is chosen.  
  
 **Object**  
 Limit the **Selection** grid to display tables, views, or both.  
  
 **Selection**  
 Specify the tables or indexes affected by this task. Not available when **Tables and Views** is selected in the **Object** box.  
  
 **Compact large objects**  
 Deallocate space for tables and views when possible. This option uses `ALTER INDEX LOB_COMPACTION = ON`.  


[!INCLUDE[index-stats-options-reorg-5589131-2999104](../../includes/paragraph-content/index-stats-options-reorganize-maintenance-plan-include.md)]

  
 **View T-SQL**  
 View the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements performed against the server for this task, based on the selected options.  
  
> [!NOTE]  
>  When the number of objects affected is large, this display can take a considerable amount of time.  

  
## New Connection Dialog Box  
 **Connection name**  
 Enter a name for the new connection.  
  
 **Select or enter a server name**  
 Select a server to connect to when performing this task.  
  
 **Refresh**  
 Refresh the list of available servers.  
  
 **Enter information to log on to the server**  
 Specify how to authenticate against the server.  
  
 **Use Windows integrated security**  
 Connect to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] with [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.  
  
 **Use a specific user name and password**  
 Connect to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. This option is not available.  
  
 **User name**  
 Provide a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to use when authenticating. This option is not available.  
  
 **Password**  
 Provide a password to use when authenticating. This option is not available.  
  
## See Also  
 [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)   
 [DBCC INDEXDEFRAG &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-indexdefrag-transact-sql.md)  
  
  
