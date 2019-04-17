---
title: "Remove a Secondary Database from an Availability Group (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.availabilitygroup.unjoindb.f1"
helpviewer_keywords: 
  - "secondary databases [SQL Server], in availability group"
  - "Availability Groups [SQL Server], removing"
  - "Availability Groups [SQL Server], databases"
ms.assetid: 4e51a570-58d7-4f01-9390-4198f3602576
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Remove a Secondary Database from an Availability Group (SQL Server)
  This topic describes how to remove a secondary database from an AlwaysOn availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **To remove a secondary database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [PowerShell](#PowerShellProcedure)  
  
-   **Follow Up:**  [After Removing a Secondary Database from an Availability Group](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a>   
###  <a name="Prerequisites"></a> Prerequisites and Restrictions  
  
-   This task is supported only on secondary replicas. You must be connected to the server instance that hosts the secondary replica from which the database is to be removed.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To remove a secondary database from an availability group**  
  
1.  In Object Explorer, connect to the server instance that hosts the secondary replica from which you want to remove one or more secondary databases, and expand the server tree.  
  
2.  Expand the **AlwaysOn High Availability** node and the **Availability Groups** node.  
  
3.  Select the availability group, and expand the **Availability Databases** node.  
  
4.  This step depends on whether you want to remove multiple databases groups or only one database, as follows:  
  
    -   To remove multiple databases, use the **Object Explorer Details** pane to view and select all the databases that you want to remove. For more information, see [Use the Object Explorer Details to Monitor Availability Groups &#40;SQL Server Management Studio&#41;](use-object-explorer-details-to-monitor-availability-groups.md).  
  
    -   To remove a single database, select it in either the **Object Explorer** pane or the **Object Explorer Details** pane.  
  
5.  Right-click the selected database or databases, and select **Remove Secondary Database** in the command menu.  
  
6.  In the **Remove Database from Availability Group** dialog box, to remove all the listed databases, click **OK**. If you do not want to remove all the listed databases, click **Cancel**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To remove a secondary database from an availability group**  
  
1.  Connect to the server instance that hosts the secondary replica.  
  
2.  Use the [SET HADR clause of the ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql-set-hadr) statement, as follows:  
  
     ALTER DATABASE *database_name* SET HADR OFF  
  
     where *database_name* is the name of a secondary database to be removed from the availability group to which it belongs.  
  
     The following example removes the local secondary database *MyDb2* from its availability group.  
  
    ```  
    ALTER DATABASE MyDb2 SET HADR OFF;  
    GO  
    ```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To remove a secondary database from an availability group**  
  
1.  Change directory (`cd`) to the server instance that hosts the secondary replica.  
  
2.  Use the **Remove-SqlAvailabilityDatabase** cmdlet, specifying the name of the availability database to be removed from the availability group. When you are connected to a server instance that hosts a secondary replica, only the local secondary database is removed from the availability group.  
  
     For example, the following command removes the secondary database `MyDb8` from the secondary replica hosted by the server instance named `SecondaryComputer\Instance`. Data synchronization to the removed secondary databases ceases. This command does not affect the primary database or any other secondary databases.  
  
    ```  
    Remove-SqlAvailabilityDatabase `  
    -Path SQLSERVER:\Sql\SecondaryComputer\InstanceName\AvailabilityGroups\MyAg\Databases\MyDb8  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the `Get-Help` cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
##  <a name="FollowUp"></a> Follow Up: After Removing a Secondary Database from an Availability Group  
 When a secondary database is removed, it is no longer joined to the availability group and all information about the removed secondary database is discarded by the availability group. The removed secondary database is placed in the RESTORING state.  
  
> [!TIP]  
>  For a short time after removing a secondary database, you might be able to restart AlwaysOn data synchronization on the database by re-joining it to the availability group. For more information, see [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](join-a-secondary-database-to-an-availability-group-sql-server.md).  
  
 At this point there are alternative ways of dealing with a removed secondary database:  
  
-   If you no longer need the secondary database, you can drop it.  
  
     For more information, see [DROP DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-database-audit-specification-transact-sql) or [Delete a Database](../../../relational-databases/databases/delete-a-database.md).  
  
-   If you want to access a removed secondary database after it has been removed from the availability group, you can recover the database. However, if you recover a removed secondary database, two divergent, independent databases that have the same name are online. You must make sure that clients can access only the current primary database.  
  
     For more information, see [Recover a Database Without Restoring Data &#40;Transact-SQL&#41;](../../../relational-databases/backup-restore/recover-a-database-without-restoring-data-transact-sql.md).  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Remove a Primary Database from an Availability Group &#40;SQL Server&#41;](remove-a-primary-database-from-an-availability-group-sql-server.md)  
  
  
