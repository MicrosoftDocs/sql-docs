---
title: "Remove a primary database from an availability group"
description: "Steps to remove a primary database from an Always On availability group using Transact-SQL (T-SQL), PowerShell, or SQL Server Management Studio."
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.availabilitygroup.removeprimarydb.f1"
helpviewer_keywords: 
  - "primary databases [SQL Server], in availability group"
  - "Availability Groups [SQL Server], removing"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], databases"
ms.assetid: 6d4ca31e-ddf0-44bf-be5e-a5da060bf096
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Remove a primary database from an Always On availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to remove both the primary database and the corresponding secondary database(s) from an Always On availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
##  <a name="Prerequisites"></a> Prerequisites and Restrictions  
  
-   This task is supported only on primary replicas. You must be connected to the server instance that hosts the primary replica.  
  
 
##  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To remove an availability database**  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica of the database or databases to be removed, and expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  Select the availability group, and expand the **Availability Databases** node.  
  
4.  This step depends on whether you want to remove multiple databases groups or only one database, as follows:  
  
    -   To remove multiple databases, use the **Object Explorer Details** pane to view and select all the databases that you want to remove. For more information, see [Use the Object Explorer Details to Monitor Availability Groups &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-object-explorer-details-to-monitor-availability-groups.md).  
  
    -   To remove a single database, select it in either the **Object Explorer** pane or the **Object Explorer Details** pane.  
  
5.  Right-click the selected database or databases, and select **Remove Database from Availability Group** in the command menu.  
  
6.  In the **Remove Databases from Availability Group** dialog box, to remove all the listed databases, click **OK**. If you do not want to remove all them, click **Cancel**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To remove an availability database**  
  
1.  Connect to the server instance that hosts the primary replica.  
  
2.  Use the [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md) statement, as follows:  
  
     ALTER AVAILABILITY GROUP *group_name* REMOVE DATABASE *availability_database_name*  
  
     where *group_name* is the name of the availability group and *database_name* is the name of the database to be removed.  
  
     The following example removes a databases named `Db6` from the `MyAG` availability group.  
  
    ```  
    ALTER AVAILABILITY GROUP MyAG REMOVE DATABASE Db6;  
    ```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To remove an availability database**  
  
1.  Change directory (**cd**) to the server instance that hosts the primary replica.  
  
2.  Use the **Remove-SqlAvailabilityDatabase** cmdlet, specifying the name of the availability database to be removed from the availability group. When you are connected to the server instance that hosts the primary replica, the primary database and its corresponding secondary databases are all removed from the availability group.  
  
     For example, the following command removes the availability database `MyDb9` from the availability group named `MyAg`. Because the command is executed on the server instance that hosts the primary replica, the primary database and all its corresponding secondary databases are removed from the availability group. Data synchronization will no longer occur for this database on any secondary replica.  
  
    ```  
    Remove-SqlAvailabilityDatabase `   
    -Path SQLSERVER:\Sql\PrimaryComputer\InstanceName\AvailabilityGroups\MyAg\AvailabilityDatabases\MyDb9
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
##  <a name="FollowUp"></a> Follow Up: After Removing an Availability Database from an Availability Group  
 Removing an availability database from its availability group ends data synchronization between the former primary database and the corresponding secondary databases. The former primary database remains online. Every corresponding secondary database is placed in the RESTORING state.  
  
 At this point there are alternative ways of dealing with a removed secondary database:  
  
-   If you no longer need a given secondary database, you can drop it.  
  
     For more information, see [Delete a Database](../../../relational-databases/databases/delete-a-database.md).  
  
-   If you want to access a removed secondary database after it has been removed from the availability group, you can recover the database. However, if you recover a removed secondary database, two divergent, independent databases that have the same name are online. You must make sure that clients can access only one of them, typically the most recent primary database.  
  
     For more information, see [Recover a Database Without Restoring Data &#40;Transact-SQL&#41;](../../../relational-databases/backup-restore/recover-a-database-without-restoring-data-transact-sql.md).  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Remove a Secondary Database from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-secondary-database-from-an-availability-group-sql-server.md)  
  
  
