---
title: "Perform a planned manual failover of an availability group"
description: "This topic describes how to perform a planned manual failover of an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.date: "10/25/2017"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seodec18
f1_keywords:
  - "sql13.swb.availabilitygroup.manualfailover.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], failover"
  - "failover [SQL Server], AlwaysOn Availability Groups"
  - "failover [SQL Server], Always On Availability Groups"
---

# Perform a planned manual failover of an Always On availability group (SQL Server)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
This topic describes how to perform a manual failover without data loss (a *planned manual failover*) on an Always On availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)]. An availability group fails over at the level of an availability replica. A planned manual failover, like any Always On availability group failover, transitions a secondary replica to primary role. Concurrently, the failover transitions the former primary replica to the secondary role.  
  
A planned manual failover is supported only when the primary replica and the target secondary replica are running in synchronous-commit mode and are currently synchronized. A planned manual failover preserves all the data in the secondary databases that are joined to the availability group on the target secondary replica. After the former primary replica transitions to the secondary role, its databases become secondary databases. Then they begin to synchronize with the new primary databases. After they all transition into the SYNCHRONIZED state, the new secondary replica becomes eligible to serve as the target of a future planned manual failover.  
  
> [!NOTE]  
>  If the secondary and primary replicas are both configured for automatic failover mode, after the secondary replica is synchronized, it also can serve as the target for an automatic failover. For more information, see [Availability modes &#40;Always On availability groups&#41;](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md).  
   
##  <a name="BeforeYouBegin"></a> Before you begin 

>[!IMPORTANT]
>There are specific procedures to fail over a read-scale availability group with no cluster manager. When an availability group has CLUSTER_TYPE = NONE, follow the procedures under [Fail over the primary replica on a read-scale availability group](#fail-over-the-primary-replica-on-a-read-scale-availability-group).

###  <a name="Restrictions"></a> Limitations and restrictions 
  
- A failover command returns as soon as the target secondary replica has accepted the command. However, database recovery occurs asynchronously after the availability group has finished failing over. 
- Cross-database consistency across databases within the availability group might not be maintained on failover. 
  
    > [!NOTE] 
    >  Support for cross-database and distributed transactions vary by SQL Server and operating system versions. For more information, see [Cross-database transactions and distributed transactions for Always On availability groups and database mirroring &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md). 
  
###  <a name="Prerequisites"></a> Prerequisites and restrictions 
  
-   Both the target secondary replica and the primary replica must be running in synchronous-commit availability mode. 
-   Currently, the target secondary replica must be synchronized with the primary replica. All the secondary databases on this secondary replica must be joined to the availability group. They also must be synchronized with their corresponding primary databases (that is, the local secondary databases must be SYNCHRONIZED). 
  
    > [!TIP] 
    >  To determine the failover readiness of a secondary replica, query the **is_failover_ready** column in the [sys.dm_hadr_database_replica_cluster_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-cluster-states-transact-sql.md) dynamic management view. Or you can look at the **Failover Readiness** column of the [Always On group dashboard](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md). 
-   This task is supported only on the target secondary replica. You must be connected to the server instance that hosts the target secondary replica. 
  
###  <a name="Security"></a> Security 
  
####  <a name="Permissions"></a> Permissions 
 The ALTER AVAILABILITY GROUP permission is required on the availability group. The CONTROL AVAILABILITY GROUP permission, the ALTER ANY AVAILABILITY GROUP permission, or the CONTROL SERVER permission also is required. 
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio 
 To manually fail over an availability group: 
  
1. In Object Explorer, connect to a server instance that hosts a secondary replica of the availability group that needs to be failed over. Expand the server tree. 
  
2. Expand the **Always On High Availability** node and the **Availability Groups** node. 
  
3. Right-click the availability group to be failed over, and select **Failover**. 
  
4. The Failover Availability Group wizard starts. For more information, see [Use the Failover Availability Group wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-fail-over-availability-group-wizard-sql-server-management-studio.md). 
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL 
 To manually fail over an availability group: 
  
1. Connect to the server instance that hosts the target secondary replica. 
  
2. Use the [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md) statement, as follows: 
  
     ALTER AVAILABILITY GROUP *group_name* FAILOVER 
  
     In the statement, *group_name* is the name of the availability group. 
  
     The following example manually fails over the *MyAg* availability group to the connected secondary replica: 
  
    ```  
    ALTER AVAILABILITY GROUP MyAg FAILOVER;  
    ```  
  
##  <a name="PowerShellProcedure"></a> Use PowerShell 
 To manually fail over an availability group: 
  
1. Change the directory (**cd**) to the server instance that hosts the target secondary replica. 
  
2. Use the **Switch-SqlAvailabilityGroup** cmdlet. 
  
    > [!NOTE] 
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get help for SQL Server PowerShell](../../../powershell/sql-server-powershell.md). 
  
     The following example manually fails over the *MyAg* availability group to the secondary replica with the specified path: 
  
    ```  
    Switch-SqlAvailabilityGroup -Path SQLSERVER:\Sql\SecondaryServer\InstanceName\AvailabilityGroups\MyAg  
    ```  
  
    To set up and use the SQL Server PowerShell provider: 
  
    -   [SQL Server PowerShell provider](../../../powershell/sql-server-powershell-provider.md) 
    -   [Get help for SQL Server PowerShell](../../../powershell/sql-server-powershell.md) 

##  <a name="FollowUp"></a> Follow up: After you manually fail over an availability group 
 If you failed over outside the [!INCLUDE[ssFosAuto](../../../includes/ssfosauto-md.md)] of the availability group, adjust the quorum votes of the Windows Server failover clustering nodes to reflect your new availability group configuration. For more information, see [Windows Server failover clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md). 

<a name = "ReadScaleOutOnly"><a/>

## Fail over the primary replica on a read-scale availability group

[!INCLUDE[Force failover](../../../includes/ss-force-failover-read-scale-out.md)]

## See also 

 * [Overview of Always On availability groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) 
 * [Failover and failover modes &#40;Always On availability groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md) 
 * [Perform a forced manual failover of an availability group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md) 
  
