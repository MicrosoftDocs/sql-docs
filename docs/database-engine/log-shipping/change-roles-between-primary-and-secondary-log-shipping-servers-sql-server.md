---
title: "Change primary & secondary log shipping roles"
description: Learn how to configure your secondary database to act as the primary for your SQL Server log shipping solution.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: log-shipping
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "log shipping [SQL Server], role changes"
  - "secondary data files [SQL Server], roles changed between"
  - "primary databases [SQL Server]"
  - "initial role changes [SQL Server]"
  - "log shipping [SQL Server], failover"
  - "failover [SQL Server], log shipping"
---
# Change Roles Between Primary and Secondary Log Shipping Servers (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  After you have failed over a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log shipping configuration to a secondary server, you can configure your secondary database to act as the primary database. Then, you will be able to swap primary and secondary databases as needed.  
  
## Performing the Initial Role Change  
 The first time you want to fail over to the secondary database and make it your new primary database, there is a series of steps you must take. After you have followed these initial steps, you will be able to swap roles between the primary database and the secondary database easily.  
  
1.  Manually fail over from the primary database to a secondary database. Be sure to back up the active transaction log on your primary server with NORECOVERY. For more information, see [Fail Over to a Log Shipping Secondary &#40;SQL Server&#41;](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md).  
  
2.  Disable the log shipping backup job on the original primary server, and the copy and restore jobs on the original secondary server.  
  
3.  On your secondary database (the database you want to be the new primary), configure log shipping using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Configure Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/configure-log-shipping-sql-server.md). Include the following steps:  
  
    1.  Use the same share for creating backups that you created for the original primary server.  
  
    2.  When adding the secondary database, in the **Secondary Database Settings** dialog box, enter the name of the original primary database in the **Secondary database** box.  
  
    3.  In the **Secondary Database Settings** dialog box, select **No, the secondary database is initialized**.  
  
4.  If log shipping monitoring was enabled on your former log shipping configuration, reconfigure log shipping monitoring to monitor the new log shipping configuration.  Setting the threshold_alert_enabled to 1 specifies that an alert will be raised when restore_threshold is exceeded. Execute the following commands, replacing *database_name* with the name of your database:  
  
    1.  **On the new primary server**  
  
         Execute the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements:  
  
        ```sql  
        -- Statement to execute on the new primary server  
        USE msdb  
        GO  
        EXEC master.dbo.sp_change_log_shipping_secondary_database @secondary_database = N'database_name', @threshold_alert_enabled = 1;  
        GO  
        ```  
  
    2.  **On the new secondary server**  
  
         Execute the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements:  
  
        ```sql  
        -- Statement to execute on the new secondary server  
        USE msdb  
        GO  
        EXEC master.dbo.sp_change_log_shipping_primary_database @database=N'database_name', @threshold_alert_enabled = 1;  
        GO  
        ```  
  
## Swapping Roles  
 After you have completed the steps above for the initial role change, you can change roles between the primary database and the secondary database by following the steps in this section. To perform a role change, follow these general steps:  
  
1.  Bring the secondary database online, backing up the transaction log on the primary server with NORECOVERY.  
  
2.  Disable the log shipping backup job on the original primary server, and the copy and restore jobs on the original secondary server.  
  
3.  Enable the log shipping backup job on the secondary server (the new primary server), and the copy and restore jobs on the primary server (the new secondary server).  
  
> [!IMPORTANT]  
>  When you change a secondary database to the primary database, to provide a consistent experience to users and applications, you might have to re-create some or all of the metadata for the database, such as logins and jobs, on the new primary server instance. For more information, see [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Fail Over to a Log Shipping Secondary &#40;SQL Server&#41;](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md)  
  
-   [Management of Logins and Jobs After Role Switching &#40;SQL Server&#41;](../../sql-server/failover-clusters/management-of-logins-and-jobs-after-role-switching-sql-server.md)  
  
## See Also  
 [Log Shipping Tables and Stored Procedures](../../database-engine/log-shipping/log-shipping-tables-and-stored-procedures.md)  
  
  
