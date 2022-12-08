---
title: "Shrink Database Task (Maintenance Plan)"
description: Learn how to create a task that attempts to reduce the size of selected SQL Server databases by using the Shrink Database Task.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "Shrink Database Task"
  - "Shrink Database(s) Task"
  - "sql13.swb.maint.shrink.f1"
helpviewer_keywords:
  - "Shrink Database Task dialog box"
ms.assetid: a9874cac-cded-4145-9c38-8aafd267dbee
---
# Shrink Database Task (Maintenance Plan)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **Shrink Database Task** dialog to create a task that attempts to reduce the size of the selected databases. Use the options below to determine the amount of unused space to remain in the database after the database is shrunk (the larger the percentage, the less the database can shrink). The value is based on the percentage of the actual data in the database. For example, a 100-MB database containing 60 MB of data and 40 MB of free space, with a free space percentage of 50 percent, would result in 60 MB of data and 30 MB of free space (because 50 percent of 60 MB is 30 MB). Only excess space in the database is eliminated. Valid values are from 0 through 100.  
  
 Shrinking data files recovers space by moving pages of data from the end of the file to unoccupied space closer to the front of the file. When enough free space is created at the end of the file, data pages at end of the file can deallocated and returned to the file system.  
  
> [!WARNING]  
>  Data that is moved to shrink a file can be scattered to any available location in the file. This causes index fragmentation and can slow the performance of queries that search a range of the index. To eliminate the fragmentation, consider rebuilding the indexes on the file after shrinking.  
  
 This task executes the DBCC SHRINKDATABASE statement.  
  
## Options  
 **Connection**  
 Select the server connection to use when performing this task.  
  
 **New**  
 Create a new server connection to use when performing this task. The **New Connection** dialog box is described below.  
  
 **Databases**  
 Specify the databases affected by this task.  
  
-   **All databases**  
  
     Generate a maintenance plan that runs maintenance tasks against all [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases except tempdb.  
  
-   **All system databases**  
  
     Generate a maintenance plan that runs maintenance tasks against each of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system databases except tempdb. No maintenance tasks are run against user-created databases.  
  
-   **All user databases**  
  
     Generate a maintenance plan that runs maintenance tasks against all user-created databases. No maintenance tasks are run against the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system databases.  
  
-   **These databases**  
  
     Generate a maintenance plan that runs maintenance tasks against only those databases that are selected. At least one database in the list must be selected if this option is chosen.  
  
    > [!NOTE]  
    >  Maintenance plans only run against databases set to compatibility level 80 or higher. Databases set to compatibility level 70 or lower are not displayed.  
  
 **Shrink database when it grows beyond**  
 Specify the size in megabytes that causes the task to execute.  
  
 **Amount of free space to remain after shrink**  
 Stop shrinking when free space in database files reaches this size.  
  
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
  
 **Use Windows NT Integrated security**  
 Connect to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] with [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.  
  
 **Use a specific user name and password**  
 Connect to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. This option is not available.  
  
 **User name**  
 Provide a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to use when authenticating. This option is not available.  
  
 **Password**  
 Provide a password to use when authenticating. This option is not available.  
  
## See Also  
 [DBCC SHRINKDATABASE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md)  
  
  
