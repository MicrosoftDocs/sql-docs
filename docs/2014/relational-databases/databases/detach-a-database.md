---
title: "Detach a Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.detachdatabase.f1"
helpviewer_keywords: 
  - "database detaching [SQL Server]"
  - "detaching databases [SQL Server]"
ms.assetid: f63d4107-13e4-4bfe-922d-5e4f712e472d
author: stevestein
ms.author: sstein
manager: craigg
---
# Detach a Database
  This topic describes how to detach a database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The detached files remain and can be reattached by using CREATE DATABASE with the FOR ATTACH or FOR ATTACH_REBUILD_LOG option. The files can be moved to another server and attached there.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To detach a database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 For a list of limitations and restrictions, see [Database Detach and Attach &#40;SQL Server&#41;](database-detach-and-attach-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the db_owner fixed database role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To detach a database  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer, connect to the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand the instance.  
  
2.  Expand **Databases**, and select the name of the user database you want to detach.  
  
3.  Right-click the database name, point to **Tasks**, and then click **Detach**. The **Detach Database** dialog box appears.  
  
     **Databases to detach**  
     Lists the databases to detach.  
  
     **Database Name**  
     Displays the name of the database to be detached.  
  
     **Drop Connections**  
     Disconnect connections to the specified database.  
  
    > [!NOTE]  
    >  You cannot detach a database with active connections.  
  
     **Update Statistics**  
     By default, the detach operation retains any out-of-date optimization statistics when detaching the database; to update the existing optimization statistics, click this check box.  
  
     **Keep Full-Text Catalogs**  
     By default, the detach operation keeps any full-text catalogs that are associated with the database. To remove them, clear the **Keep Full-Text Catalogs** check box. This option appears only when you are upgrading a database from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
     **Status**  
     Displays one of the following states: **Ready** or **Not ready**.  
  
     **Message**  
     The **Message** column may display information about the database, as follows:  
  
    -   When a database is involved with replication, the **Status** is **Not ready** and the **Message** column displays **Database replicated**.  
  
    -   When a database has one or more active connections, the **Status** is **Not ready** and the **Message** column displays _<number_of_active_connections>_**Active connection(s)** - for example: **1 Active connection(s)**. Before you can detach the database, you need to disconnect any active connections by selecting **Drop Connections**.  
  
     To obtain more information about a message, click the hyperlinked text to open Activity Monitor.  
  
4.  When you are ready to detach the database, click **OK**.  
  
> [!NOTE]  
>  The newly detached database will remain visible in the **Databases** node of Object Explorer until the view is refreshed. You can refresh the view at any time: Click in the Object Explorer pane, and from the menu bar select **View** and then **Refresh**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To detach a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example detaches the AdventureWorks2012 database with skipchecks set to true.  
  
```  
EXEC sp_detach_db 'AdventureWorks2012', 'true';  
```  
  
## See Also  
 [Database Detach and Attach &#40;SQL Server&#41;](database-detach-and-attach-sql-server.md)   
 [sp_detach_db &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-detach-db-transact-sql)  
  
  
