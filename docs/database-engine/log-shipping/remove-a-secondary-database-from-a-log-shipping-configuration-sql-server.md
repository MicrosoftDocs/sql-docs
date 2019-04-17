---
title: "Remove a Secondary Database from a Log Shipping Configuration (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "deleting secondary databases"
  - "secondary databases [SQL Server], in log shipping"
  - "removing secondary databases"
  - "secondary data files [SQL Server], removing"
  - "log shipping [SQL Server], secondary databases"
ms.assetid: ebe368a4-ca1c-45d0-9a71-3ddbd5b26a8e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Remove a Secondary Database from a Log Shipping Configuration (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to remove a log shipping secondary database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To remove a log shipping secondary database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 The log-shipping stored procedures require membership in the **sysadmin** fixed server role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To remove a log shipping secondary database  
  
1.  Connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is currently the log shipping primary server and expand that instance.  
  
2.  Expand **Databases**, right-click the log shipping primary database, and then click **Properties**.  
  
3.  Under **Select a page**, click **Transaction Log Shipping**.  
  
4.  Under **Secondary server instances and databases**, click the database you want to remove.  
  
5.  Click **Remove**.  
  
6.  Click **OK** to update the configuration.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To Remove a Secondary Database  
  
1.  On the primary server, execute [sp_delete_log_shipping_primary_secondary](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-primary-secondary-transact-sql.md) to delete the information about the secondary database from the primary server.  
  
2.  On the secondary server, execute [sp_delete_log_shipping_secondary_database](../../relational-databases/system-stored-procedures/sp-delete-log-shipping-secondary-database-transact-sql.md) to delete the secondary database.  
  
    > [!NOTE]  
    >  If there are no other secondary databases with the same secondary ID, **sp_delete_log_shipping_secondary_primary** is invoked from **sp_delete_log_shipping_secondary_database** and deletes the entry for the secondary ID and the copy and restore jobs.  
  
3.  On the secondary server, disable the copy and restore jobs. For more information, see [Disable or Enable a Job](../../ssms/agent/disable-or-enable-a-job.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Upgrading Log Shipping to SQL Server 2016 &#40;Transact-SQL&#41;](../../database-engine/log-shipping/upgrading-log-shipping-to-sql-server-2016-transact-sql.md)  
  
-   [Configure Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/configure-log-shipping-sql-server.md)  
  
-   [Add a Secondary Database to a Log Shipping Configuration &#40;SQL Server&#41;](../../database-engine/log-shipping/add-a-secondary-database-to-a-log-shipping-configuration-sql-server.md)  
  
-   [Remove Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/remove-log-shipping-sql-server.md)  
  
-   [View the Log Shipping Report &#40;SQL Server Management Studio&#41;](../../database-engine/log-shipping/view-the-log-shipping-report-sql-server-management-studio.md)  
  
-   [Monitor Log Shipping &#40;Transact-SQL&#41;](../../database-engine/log-shipping/monitor-log-shipping-transact-sql.md)  
  
-   [Fail Over to a Log Shipping Secondary &#40;SQL Server&#41;](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md)  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [Log Shipping Tables and Stored Procedures](../../database-engine/log-shipping/log-shipping-tables-and-stored-procedures.md)  
  
  
