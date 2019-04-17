---
title: "Join a secondary replica to an availability group"
description: "Steps to join a secondary replica to an Always On availability group using either Transact-SQL (T-SQL), PowerShell, or SQL Server Management Studio."
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.availabilitygroup.joinreplica.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], joining"
  - "Availability Groups [SQL Server], configuring"
ms.assetid: e5bd2489-097a-490e-8ea1-34fe48378ad1
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Join a secondary replica to an Always On availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to join a secondary replica to an Always On availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. After a secondary replica is added to an Always On availability group, the secondary replica must be joined to the availability group. The join-replica operation must be performed on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that is hosting the secondary replica.  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **To prepare a secondary database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [PowerShell](#PowerShellProcedure)  
  
-   **Follow Up:** [Configure Secondary Databases](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   The primary replica of the availability group must currently be online.  
  
-   You must be connected to the server instance that hosts a secondary replica that has not yet have been joined to the availability group.  
  
-   The local server instance must be able to connect to the database mirroring endpoint of the server instance that is hosting the primary replica.  
  
> [!IMPORTANT]  
>  If any prerequisite is not met, the join operation fails. After a failed join attempt, you might need to connect to the server instance that hosts the primary replica to remove and re-add the secondary replica before you can join it to the availability group. For more information, see [Remove a Secondary Replica from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-secondary-replica-from-an-availability-group-sql-server.md) and [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To join an availability replica to an availability group**  
  
1.  In Object Explorer, connect to the server instance that hosts the secondary replica, and click the server name to expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  Select the availability group of the secondary replica to which you are connected.  
  
4.  Right-click the secondary replica, and click **Join to Availability Group**.  
  
5.  This opens the **Join Replica to Availability Group** dialog box.  
  
6.  To join the secondary replica to the availability group, click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To join an availability replica to an availability group**  
  
1.  Connect to the server instance that hosts the secondary replica.  
  
2.  Use the [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md) statement, as follows:  
  
     ALTER AVAILABILITY GROUP *group_name* JOIN  
  
     where *group_name* is the name of the availability group.  
  
     The following example, joins the secondary replica to the `MyAG` availability group.  
  
    ```  
    ALTER AVAILABILITY GROUP MyAG JOIN;  
    ```  
  
    > [!NOTE]  
    >  To see this [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement used in context, see [Create an Availability Group &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md).  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To join an availability replica to an availability group**  
  
 In the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell provider:  
  
1.  Change directory (**cd**) to the server instance that hosts the secondary replica.  
  
2.  Join the secondary replica to the availability group by executing the **Join-SqlAvailabilityGroup** cmdlet with the name of the availability group.  
  
     For example, the following command joins a secondary replica hosted by the server instance located at the specified path to the availability group named `MyAg`.  This server instance must host a secondary replica in this availability group.  
  
    ```  
    Join-SqlAvailabilityGroup -Path SQLSERVER:\SQL\SecondaryServer\InstanceName -Name 'MyAg'  
    ```  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
##  <a name="FollowUp"></a> Follow Up: Configure Secondary Databases  
 For every database in the availability group, you need a secondary database on the server instance that is hosting the secondary replica. You can configure secondary databases either before or after you join a secondary replica to an availability group, as follows:  
  
1.  Restore recent database and log backups of each primary database onto the server instance that hosts the secondary replica, using RESTORE WITH NORECOVERY for every restore operation. For more information, see [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md).  
  
2.  Join each secondary database to the availability group. For more information, see [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md).  
  
## See Also  
 [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
  
  
