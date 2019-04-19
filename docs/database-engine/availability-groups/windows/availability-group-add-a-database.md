---
title: "Add a database to an availability group"
description: "Add a database to an Always On availability group using Transact-SQL (T-SQL), PowerShell, or SQL Server Management Studio. "
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "primary databases [SQL Server], in availability group"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], databases"
ms.assetid: 2a54eef8-9e8e-4e04-909c-6970112d55cc
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Add a Database to an Always On availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to add a database to an Always On availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  

  
## Prerequisites and Restrictions  
  
-   You must be connected to the server instance that hosts the primary replica.  
  
-   The database must reside on the server instance that hosts the primary replica and comply with the prerequisites and restrictions for availability databases. For more information, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
 
##  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  

  
1.  In Object Explorer, connect to the server instance that hosts the primary replica, and expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  Right-click the availability group, and select one of the following commands:  
  
    -   To launch the Add Database to Availability Group Wizard, select the **Add Database** command. For more information, see [Use the Add Database to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/availability-group-add-database-to-group-wizard.md).  
  
    -   To add one or more databases by specifying them in the **Availability Group Properties** dialog box, select the **Properties** command. The steps for adding a database are as follows:  
  
        1.  In the **Availability Databases** pane, click the **Add** button. This creates and selects a blank database field.  
  
        2.  Enter the name of a database that meets the availability-databases prerequisites.  
  
         To add another database, repeat the preceding steps. When you are done specifying databases, click **OK** to complete the operation.  
  
         After you use the **Availability Group Properties** dialog box to add a database to an availability group, you need to configure the corresponding secondary database on each server instance that hosts a secondary replica. For more information, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  

  
1.  Connect to the server instance that hosts the server instance that hosts the primary replica.    
2.  Use the [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md) statement, as follows:  
  
     ALTER AVAILABILITY GROUP *group_name* ADD DATABASE *database_name* [,...*n*]  
  
     where *group_name* is the name of the availability group and *database_name* is the name of a database to be added to the group.  
  
     The following example adds the *MyDb3* database to the *MyAG* availability group.  
  
    ```  
    -- Connect to the server instance that hosts the primary replica.  
    -- Add an existing database to the availability group.  
    ALTER AVAILABILITY GROUP MyAG ADD DATABASE MyDb3;  
    GO  
    ```  
  
3.  After you add a database to an availability group, you need to configure the corresponding secondary database on each server instance that hosts a secondary replica. For more information, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
##  <a name="PowerShellProcedure"></a> Use PowerShell  

  
1.  Change directory (**cd**) to the server instance that hosts the primary replica.  
  
2.  Use the **Add-SqlAvailabilityDatabase** cmdlet.  
  
     For example, the following command adds the secondary database `MyDd` to the `MyAG` availability group, whose primary replica is hosted by `PrimaryServer\InstanceName`.  
  
    ```  
  
    Add-SqlAvailabilityDatabase `   
    -Path SQLSERVER:\SQL\PrimaryServer\InstanceName\AvailabilityGroups\MyAG `   
    -Database "MyDb"  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
3.  After you add a database to an availability group, you need to configure the corresponding secondary database on each server instance that hosts a secondary replica. For more information, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
 For a complete example, see [Example (PowerShell)](#PSExample), below.  
  
###  <a name="PSExample"></a> Example (PowerShell)  
 The following example shows the full process for preparing a secondary database from a database on the server instance that hosts the primary replica of an availability group, adding the database to an availability group (as a primary database), and then joining the secondary database to the availability group. First, the example backs up the database and its transaction log. Then the example restores the database and log backups to the server instances that host a secondary replica.  
  
 The example calls **Add-SqlAvailabilityDatabase** twice: first on the primary replica to add the database to the availability group, and then on the secondary replica to join the secondary database on that replica to the availability group. If you have more than one secondary replica, restore and join the secondary database on each of them.  
  
```  
$DatabaseBackupFile = "\\share\backups\MyDatabase.bak"  
$LogBackupFile = "\\share\backups\MyDatabase.trn"  
$MyAgPrimaryPath = "SQLSERVER:\SQL\PrimaryServer\InstanceName\AvailabilityGroups\MyAg"  
$MyAgSecondaryPath = "SQLSERVER:\SQL\SecondaryServer\InstanceName\AvailabilityGroups\MyAg"  
  
Backup-SqlDatabase -Database "MyDatabase" -BackupFile $DatabaseBackupFile -ServerInstance "PrimaryServer\InstanceName"  
Backup-SqlDatabase -Database "MyDatabase" -BackupFile $LogBackupFile -ServerInstance "PrimaryServer\InstanceName" -BackupAction 'Log'  
  
Restore-SqlDatabase -Database "MyDatabase" -BackupFile $DatabaseBackupFile -ServerInstance "SecondaryServer\InstanceName" -NoRecovery  
Restore-SqlDatabase -Database "MyDatabase" -BackupFile $LogBackupFile -ServerInstance "SecondaryServer\InstanceName" -RestoreAction 'Log' -NoRecovery  
  
Add-SqlAvailabilityDatabase -Path $MyAgPrimaryPath -Database "MyDatabase"  
Add-SqlAvailabilityDatabase -Path $MyAgSecondaryPath -Database "MyDatabase"  
  
```  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)  
