---
title: "Remove a Secondary Replica from an Availability Group (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.availabilitygroup.removesecondaryar.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], configuring"
ms.assetid: 35ddc8b6-3e7c-4417-9a0a-d4987a09ddf7
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Remove a Secondary Replica from an Availability Group (SQL Server)
  This topic describes how to remove a secondary replica from an AlwaysOn availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **To remove a secondary replica, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [PowerShell](#PowerShellProcedure)  
  
-   **Follow Up:**  [After Removing a Secondary Replica](#PostBestPractices)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   This task is supported only on the primary replica.  
  
-   Only a secondary replica can be removed from an availability group.  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   You must be connected to the server instance that hosts the primary replica of the availability group.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To remove a secondary replica**  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica, and expand the server tree.  
  
2.  Expand the **AlwaysOn High Availability** node and the **Availability Groups** node.  
  
3.  Select the availability group, and expand the **Availability Replicas** node.  
  
4.  This step depends on whether you want to remove multiple replicas or only one replica, as follows:  
  
    -   To remove multiple replicas, use the **Object Explorer Details** pane to view and select all the replicas that you want to remove. For more information, see [Use the Object Explorer Details to Monitor Availability Groups &#40;SQL Server Management Studio&#41;](use-object-explorer-details-to-monitor-availability-groups.md).  
  
    -   To remove a single replica, select it in either the **Object Explorer** pane or the **Object Explorer Details** pane.  
  
5.  Right-click the selected secondary replica or replicas, and select **Remove from Availability Group** in the command menu.  
  
6.  In the **Remove Secondary Replicas from Availability Group** dialog box, to remove all the listed secondary replicas, click **OK**. If you do not want to remove all the listed replicas, click **Cancel**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To remove a secondary replica**  
  
1.  Connect to the server instance that hosts the primary replica.  
  
2.  Use the [ALTER AVAILABILITY GROUP](/sql/t-sql/statements/alter-availability-group-transact-sql) statement, as follows:  
  
     ALTER AVAILABILITY GROUP *group_name* REMOVE REPLICA ON '*instance_name*' [,...*n*]  
  
     where *group_name* is the name of the availability group and *instance_name* is the server instance where the secondary replica is located.  
  
     The following example removes a secondary replica from the *MyAG* availability group. The target secondary replica is located on a server instance named *HADR_INSTANCE* on a computer named *COMPUTER02*.  
  
    ```  
    ALTER AVAILABILITY GROUP MyAG REMOVE REPLICA ON 'COMPUTER02\HADR_INSTANCE';  
    ```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To remove a secondary replica**  
  
1.  Change directory (`cd`) to the server instance that hosts the primary replica.  
  
2.  Use the **Remove-SqlAvailabilityReplica** cmdlet.  
  
     For example, the following command removes the availability replica on the server `MyReplica` from the availability group named `MyAg`.  This command must be run on the server instance that hosts the primary replica of the availability group.  
  
    ```  
    Remove-SqlAvailabilityReplica `   
    -Path SQLSERVER:\SQL\PrimaryServer\InstanceName\AvailabilityGroups\MyAg\AvailabilityReplicas\MyReplica  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the `Get-Help` cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../powershell/sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md)  
  
##  <a name="PostBestPractices"></a> Follow Up: After Removing a Secondary Replica  
 If you specify a replica that is currently unavailable, when the replica comes online, it will discover that it has been removed.  
  
 Removing a replica causes it to stop receiving data. After a secondary replica confirms that it has been removed from the global store, the replica removes the availability group settings from its databases, which remain on the local server instance in the RECOVERING state.  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](add-a-secondary-replica-to-an-availability-group-sql-server.md)   
 [Remove an Availability Group &#40;SQL Server&#41;](remove-an-availability-group-sql-server.md)  
  
  
