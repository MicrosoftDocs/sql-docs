---
title: "Overview of PowerShell Cmdlets for availability groups"
description: "A reference for the different PowerShell cmdlets available to manage Always On availability groups. "
ms.custom: "seodec18"
ms.date: "08/30/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], PowerShell cmdlets"
  - "Availability Groups [SQL Server], about"
  - "PowerShell [SQL Server], cmdlets"
ms.assetid: b3fef0d5-b6d7-4386-a0f0-d06c165ad4de
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Overview of PowerShell Cmdlets for Always On Availability Groups
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] PowerShell is a task-based command-line shell and scripting language designed especially for system administration. [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] provides a set of PowerShell cmdlets in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] that enable you to deploy, manage, and monitor availability groups, availability replicas, and availability databases.  
  
> [!NOTE]  
>  A PowerShell cmdlet can complete by successfully initiating an action. This does not indicate that the intended work, such as the fail over of an availability group, has completed. When scripting a sequence of actions, you might have to check the status of actions, and wait for them to complete.  
  
> [!NOTE]  
>  For a list of topics in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] Books Online that describe how to use cmdlets to perform [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] tasks, see the "Related Tasks" section of [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
##  <a name="ConfiguringServerInstance"></a> Configuring a Server Instance for Always On Availability Groups  
  
|Cmdlets|Description|Supported on|  
|-------------|-----------------|------------------|
|[**Disable-SqlAlwaysOn**](/powershell/module/sqlserver/disable-sqlalwayson)|Disables the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature on a server instance.|The server instance that is specified by the **Path**, **InputObject**, or **Name** parameter. (Must be an edition of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that supports [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)].)|  
|[**Enable-SqlAlwaysOn**](/powershell/module/sqlserver/enable-sqlalwayson)|Enables [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] on an instance of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] that supports the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature. For information about support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).|Any edition of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that supports [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)].|  
|[**New-SqlHadrEndPoint**](/powershell/module/sqlserver/new-sqlhadrendpoint)|Creates a new database mirroring endpoint on a server instance. This endpoint is required for data movement between primary and secondary databases.|Any instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]|  
|[**Set-SqlHadrEndpoint**](/powershell/module/sqlserver/set-sqlhadrendpoint)|Changes the properties of an existing database mirroring endpoint, such as the name, state, or authentication properties.|A server instance that supports [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] and lacks a database mirroring endpoint|  

  
##  <a name="BnRcmdlets"></a> Backing Up and Restoring Databases and Transaction Logs  
  
|Cmdlets|Description|Supported on|  
|-------------|-----------------|------------------|  
|[**Backup-SqlDatabase**](/powershell/module/sqlserver/backup-sqldatabase)|Creates a data or log backup.|Any online database (for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], a database on the server instance that hosts the primary replica)|  
|[**Restore-SqlDatabase**](/powershell/module/sqlserver/restore-sqldatabase)|Restores a backup.|Any instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] (for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], a server instance that hosts a secondary replica)<br /><br />

  >[!Important]
  >When preparing a secondary database, you must use the **-NoRecovery** parameter in every **Restore-SqlDatabase** command. 
  
 For information about using these cmdlets to prepare a secondary database, see [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md).  
  
##  <a name="DeployManageAGs"></a> Creating and Managing an Availability Group  
  
|Cmdlets|Description|Supported on|  
|-------------|-----------------|------------------|  
|[**New-SqlAvailabilityGroup**](/powershell/module/sqlserver/new-sqlavailabilitygroup)|Creates a new availability group.|Server instance to host primary replica|  
|[**Remove-SqlAvailabilityGroup**](/powershell/module/sqlserver/remove-sqlavailabilitygroup)|Deletes availability group.|HADR-enabled server instance|  
|[**Set-SqlAvailabilityGroup**](/powershell/module/sqlserver/set-sqlavailabilitygroup)|Sets the properties of an availability group; take an availability group online/offline|Server instance that hosts primary replica|  
|[**Switch-SqlAvailabilityGroup**](/powershell/module/sqlserver/switch-sqlavailabilitygroup)|Initiates one of the following forms of failover:<br /><br /> A forced failover of an availability group (with possible data loss).<br /><br /> A manual failover of an availability group.|Server instance that hosts target secondary replica|  
  
##  <a name="AGlisteners"></a> Creating and Managing an Availability Group Listener  
  
|Cmdlet|Description|Supported on|  
|------------|-----------------|------------------|  
|[**New-SqlAvailabilityGroupListener**](/powershell/module/sqlserver/new-sqlavailabilitygrouplistener)|Creates a new availability group listener and attaches it to an existing availability group.|Server instance that hosts primary replica|  
|[**Set-SqlAvailabilityGroupListener**](/powershell/module/sqlserver/set-sqlavailabilitygrouplistener)|Modifies the port setting on an existing availability group listener.|Server instance that hosts primary replica|  
|[**Add-SqlAvailabilityGroupListenerStaticIp**](/powershell/module/sqlserver/add-sqlavailabilitygrouplistenerstaticip)|Adds a static IP address to an existing availability group listener configuration. The IP address can be an IPv4 address with subnet, or an IPv6 address.|Server instance that hosts primary replica|  
  
##  <a name="DeployManageARs"></a> Creating and Managing an Availability Replica  
  
|Cmdlets|Description|Supported on|  
|-------------|-----------------|------------------|  
|[**New-SqlAvailabilityReplica**](/powershell/module/sqlserver/new-sqlavailabilityreplica)|Creates a new availability replica. You can Use the **-AsTemplate** parameter to create an in-memory availability-replica object for each new availability replica.|Server instance that hosts primary replica|  
|[**Join-SqlAvailabilityGroup**](/powershell/module/sqlserver/join-sqlavailabilitygroup)|Joins a secondary replica to the availability group.|Server instance that hosts secondary replica|  
|[**Remove-SqlAvailabilityReplica**](/powershell/module/sqlserver/remove-sqlavailabilityreplica)|Deletes an availability replica.|Server instance that hosts primary replica|  
|[**Set-SqlAvailabilityReplica**](/powershell/module/sqlserver/set-sqlavailabilityreplica)|Sets the properties of an availability replica.|Server instance that hosts primary replica|  
  
##  <a name="DeployManageDbs"></a> Adding and Managing an Availability Database  
  
|Cmdlets|Description|Supported on|  
|-------------|-----------------|------------------|  
|[**Add-SqlAvailabilityDatabase**](/powershell/module/sqlserver/add-sqlavailabilitydatabase)|On the primary replica, adds a database to an availability group.<br /><br /> On a secondary replica, joins a secondary database to an availability group.|Any server instance that hosts an availability replica (behavior differs for primary and secondary replicas)|  
|[**Remove-SqlAvailabilityDatabase**](/powershell/module/sqlserver/remove-sqlavailabilitydatabase)|On the primary replica, removes the database from the availability group.<br /><br /> On a secondary replica, removes the local secondary database from the local secondary replica.|Any server instance that hosts an availability replica (behavior differs for primary and secondary replicas)|  
|[**Resume-SqlAvailabilityDatabase**](/powershell/module/sqlserver/resume-sqlavailabilitydatabase)|Resumes the data movement for a suspended availability database.|The server instance on which the database was suspended.|  
|[**Suspend-SqlAvailabilityDatabase**](/powershell/module/sqlserver/suspend-sqlavailabilitydatabase)|Suspends the data movement for an availability database.|Any server instance that hosts an availability replica.|  
  
##  <a name="MonitorTblshtAGs"></a> Monitoring Availability Group Health  
 The following [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cmdlets enable you to monitor the health of an availability group and its replicas and databases.  
  
> [!IMPORTANT]  
>  You must have CONNECT, VIEW SERVER STATE, and VIEW ANY DEFINITION permissions to execute these cmdlets.  
  
|Cmdlet|Description|Supported on|  
|------------|-----------------|------------------|  
|[**Test-SqlAvailabilityGroup**](/powershell/module/sqlserver/test-sqlavailabilitygroup)|Assesses the health of an availability group by evaluating SQL Server policy based management (PBM) policies.|Any server instance that hosts an availability replica.*|  
|[**Test-SqlAvailabilityReplica**](/powershell/module/sqlserver/test-sqlavailabilityreplica)|Assesses the health of availability replicas by evaluating SQL Server policy based management (PBM) policies.|Any server instance that hosts an availability replica.*|  
|[**Test-SqlDatabaseReplicaState**](/powershell/module/sqlserver/test-sqldatabasereplicastate)|Assesses the health of an availability database on all joined availability replicas by evaluating SQL Server policy based management (PBM) policies.|Any server instance that hosts an availability replica.*|  
  
 *To view information about all of the availability replicas in an availability group, use to the server instance that hosts the primary replica.  
  
 For more information, see [Use Always On Policies to View the Health of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/use-always-on-policies-to-view-the-health-of-an-availability-group-sql-server.md).  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md)  
  
  
