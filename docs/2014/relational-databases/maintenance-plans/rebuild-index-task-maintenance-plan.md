---
title: "Rebuild Index Task (Maintenance Plan) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.maint.reindex.f1"
  - "reindex"
helpviewer_keywords: 
  - "Rebuild Index Task dialog box"
ms.assetid: 33e2940b-139f-4563-b0cb-5683f08bd879
author: MightyPen
ms.author: genemi
manager: craigg
---
# Rebuild Index Task (Maintenance Plan)
  Use the **Rebuild Index Task** dialog to re-create the indexes on the tables in the database with a new fill factor. The fill factor determines the amount of empty space on each page in the index, to accommodate future expansion. As data is added to the table, the free space fills because the fill factor is not maintained. Reorganizing data and index pages can re-establish the free space.  
  
 The **Rebuild Index Task** uses the ALTER INDEX statement.  
  
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
  
     Generate a maintenance plan that runs maintenance tasks against each of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system databases except tempdb. No maintenance tasks are run against user-created databases.  
  
-   **All user databases**  
  
     Generate a maintenance plan that runs maintenance tasks against all user-created databases. No maintenance tasks are run against the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system databases.  
  
-   **These specific databases**  
  
     Generate a maintenance plan that runs maintenance tasks against only those databases that are selected. At least one database in the list must be selected if this option is chosen.  
  
    > [!NOTE]  
    >  Maintenance plans only run against databases set to compatibility level 80 or higher. Databases set to compatibility level 70 or lower are not displayed.  
  
 **Object**  
 Limit the **Selection** grid to display tables, views, or both.  
  
 **Selection**  
 Specify the tables or indexes affected by this task. Not available when **Tables and Views** is selected in the Object box.  
  
 **Reorganize pages with the default amount of free space**  
 Drop the indexes on the tables in the database and re-create them with the fill factor that was specified when the indexes were created.  
  
 **Change free space per page percentage to**  
 Drop the indexes on the tables in the database and re-create them with a new, automatically calculated fill factor, thereby reserving the specified amount of free space on the index pages. The higher the percentage, the more free space is reserved on the index pages, and the larger the index grows. Valid values are from 0 through 100.  
  
 **Sort results in tempdb**  
 Use the `SORT_IN_TEMPDB`option, which determines where the intermediate sort results, generated during index creation, are temporarily stored. If a sort operation is not required, or if the sort can be performed in memory, the `SORT_IN_TEMPDB`option is ignored.  
  
 **Keep index online while reindexing**  
 Use the `ONLINE` option which allows users to access the underlying table or clustered index data and any associated nonclustered indexes during index operations.  
  
> [!NOTE]  
>  Online index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
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
 Connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] with Windows Authentication.  
  
 **Use a specific user name and password**  
 Connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. This option is not available.  
  
 **User name**  
 Provide a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to use when authenticating. This option is not available.  
  
 **Password**  
 Provide a password to use when authenticating. This option is not available.  
  
## See Also  
 [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql)   
 [DBCC DBREINDEX &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-dbreindex-transact-sql)   
 [CREATE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-index-transact-sql)   
 [SORT_IN_TEMPDB Option For Indexes](../indexes/indexes.md)   
 [Guidelines for Online Index Operations](../indexes/guidelines-for-online-index-operations.md)   
 [How Online Index Operations Work](../indexes/how-online-index-operations-work.md)   
 [Perform Index Operations Online](../indexes/perform-index-operations-online.md)  
  
  
