---
title: "Failover Policy for Failover Cluster Instances | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "flexible failover policy"
ms.assetid: 39ceaac5-42fa-4b5d-bfb6-54403d7f0dc9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Failover Policy for Failover Cluster Instances
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  In a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance (FCI), only one node can own the Windows Server Failover Cluster (WSFC) cluster resource group at a given time. The client requests are served through this node in the FCI. In the case of a failure and an unsuccessful restart, the group ownership is moved to another WSFC node in the FCI. This process is called failover. [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] increases the reliability of failure detection and provides a flexible failover policy.  
  
 A [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI depends on the underlying WSFC service for failover detection. Therefore, two mechanisms determine the failover behavior for FCI: the former is native WSFC functionality, and the latter is functionality added by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup.  
  
-   The WSFC cluster maintains the quorum configuration, which ensures a unique failover target in an automatic failover. The WSFC service determines whether the cluster is in optimal quorum health at all times and brings the resource group online and offline accordingly.  
  
-   The active [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance periodically reports a set of component diagnostics to the WSFC resource group over a dedicated connection. The WSFC resource group maintains the failover policy, which defines the failure conditions that trigger restarts and failovers.  
  
 This topic discusses the second mechanism above. For more information on the WSFC behavior for quorum configuration and health detection, see [WSFC Quorum Modes and Voting Configuration &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/wsfc-quorum-modes-and-voting-configuration-sql-server.md).  
  
> [!IMPORTANT]  
>  Automatic failovers to and from an FCI are not allowed in an Always On availability group. However, manual failovers to and from and FCI are allowed in an Always On availability group.  
  
##  <a name="Concepts"></a> Failover Policy Overview  
 The failover process can be broken down into the following steps:  
  
1.  [Monitor the Health Status](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md#monitor)  
  
2.  [Determining Failures](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md#determine)  
  
3.  [Responding to Failures](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md#respond)  
  
###  <a name="monitor"></a> Monitor the Health Status  
 There are three types of health statuses that are monitored for the FCI:  
  
-   [State of the SQL Server service](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md#service)  
  
-   [Responsiveness of the SQL Server instance](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md#instance)  
  
-   [SQL Server component diagnostics](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md#component)  
  
####  <a name="service"></a> State of the SQL Server service  
 The WSFC service monitors the start state of the SQL Server service on the active FCI node to detect when the SQL Server service is stopped.  
  
####  <a name="instance"></a> Responsiveness of the SQL Server instance  
 During SQL Server startup, the WSFC service uses the SQL Server Database Engine resource DLL to create a new connection to on a separate thread that is used exclusively for monitoring the health status. This ensures that there the SQL instance has the required resources to report its health status while under load. Using this dedicated connection, SQL Server runs the [sp_server_diagnostics &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) system stored procedure in repeat mode to periodically report the health status of the SQL Server components to the resource DLL.  
  
 The resource DLL determines the responsiveness of the SQL instance using a health check timeout. The HealthCheckTimeout property defines how long the resource DLL should wait for the sp_server_diagnostics stored procedure before it reports the SQL instance as unresponsive to the WSFC service. This property is configurable using T-SQL as well as in the Failover Cluster Manager snap-in. For more information, see [Configure HealthCheckTimeout Property Settings](../../../sql-server/failover-clusters/windows/configure-healthchecktimeout-property-settings.md). The following items describe how this property affects timeout and repeat interval settings:  
  
-   The resource DLL calls the sp_server_diagnostics stored procedure and sets the repeat interval to one-third of the HealthCheckTimeout setting.  
  
-   If the sp_server_diagnostics stored procedure is slow or is not returning information, the resource DLL will wait for the interval specified by HealthCheckTimeout before it reports to the WSFC service that the SQL instance is unresponsive.  
  
-   If the dedicated connection is lost, the resource DLL will retry the connection to the SQL instance for the interval specified by HealthCheckTimeout before it reports to the WSFC service that the SQL instance is unresponsive.  
  
####  <a name="component"></a> SQL Server component diagnostics  
 The system stored procedure sp_server_diagnostics periodically collects component diagnostics on the SQL instance. The diagnostic information that is collected is surfaced as a row for each of the following components and passed to the calling thread.  
  
1.  system  
  
2.  resource  
  
3.  query process  
  
4.  io_subsystem  
  
5.  events  
  
 The system, resource, and query process components are used for failure detection. The io_subsytem and events components are used for diagnostic purposes only.  
  
 Each rowset of information is also written to the SQL Server cluster diagnostics log. For more information, see [View and Read Failover Cluster Instance Diagnostics Log](../../../sql-server/failover-clusters/windows/view-and-read-failover-cluster-instance-diagnostics-log.md).  
  
> [!TIP]  
>  While the sp_server_diagnostic stored procedure is used by SQL Server Always On technology, it is available for use in any SQL Server instance to help detect and troubleshoot problems.  
  
####  <a name="determine"></a> Determining Failures  
 The SQL Server Database Engine resource DLL determines whether the detected health status is a condition for failure using the FailureConditionLevel property. The FailureConditionLevel property defines which detected health statuses cause restarts or failovers. Multiple levels of options are available, ranging from no automatic restart or failover to all possible failure conditions resulting in an automatic restart or failover. For more information about how to configure this property, see [Configure FailureConditionLevel Property Settings](../../../sql-server/failover-clusters/windows/configure-failureconditionlevel-property-settings.md).  
  
 The failure conditions are set on an increasing scale. For levels 1-5, each level includes all the conditions from the previous levels in addition to its own conditions. This means that with each level, there is an increased probability of a failover or restart. The failure condition levels are described in the following table.  
  
 Review [sp_server_diagnostics &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) as this system stored procedure plays in important role in the failure condition levels.  
  
|Level|Condition|Description|  
|-----------|---------------|-----------------|  
|0|No automatic failover or restart|Indicates that no failover or restart will be triggered automatically on any failure conditions. This level is for system maintenance purposes only.|  
|1|Failover or restart on server down|Indicates that a server restart or failover will be triggered if the following condition is raised:<br /><br /> SQL Server service is down.|  
|2|Failover or restart on server unresponsive|Indicates that a server restart or failover will be triggered if any of the following conditions are raised:<br /><br /> SQL Server service is down.<br /><br /> SQL Server instance is not responsive (Resource DLL cannot receive data from sp_server_diagnostics within the HealthCheckTimeout settings).|  
|3*|Failover or restart on critical server errors|Indicates that a server restart or failover will be triggered if any of the following conditions are raised:<br /><br /> SQL Server service is down.<br /><br /> SQL Server instance is not responsive (Resource DLL cannot receive data from sp_server_diagnostics within the HealthCheckTimeout settings).<br /><br /> System stored procedure sp_server_diagnostics returns 'system error'.|  
|4|Failover or restart on moderate server errors|Indicates that a server restart or failover will be triggered if any of the following conditions are raised:<br /><br /> SQL Server service is down.<br /><br /> SQL Server instance is not responsive (Resource DLL cannot receive data from sp_server_diagnostics within the HealthCheckTimeout settings).<br /><br /> System stored procedure sp_server_diagnostics returns 'system error'.<br /><br /> System  stored procedure sp_server_diagnostics returns 'resource error'.|  
|5|Failover or restart on any qualified failure conditions|Indicates that a server restart or failover will be triggered if any of the following conditions are raised:<br /><br /> SQL Server service is down.<br /><br /> SQL Server instance is not responsive (Resource DLL cannot receive data from sp_server_diagnostics within the HealthCheckTimeout settings).<br /><br /> System stored procedure sp_server_diagnostics returns 'system error'.<br /><br /> System  stored procedure sp_server_diagnostics returns 'resource error'.<br /><br /> System stored procedure sp_server_diagnostics returns 'query_processing error'.|  
  
 *Default Value  
  
####  <a name="respond"></a> Responding to Failures  
 After one or more failure conditions are detected, how the WSFC service responds to the failures depends on the WSFC quorum state and the restart and failover settings of the FCI resource group. If the FCI has lost its WSFC quorum, then the entire FCI is brought offline and the FCI has lost its high availability. If the FCI still retains its WSFC quorum, then the WSFC service may respond by first attempting to restart the failed node and then failover if the restart attempts are unsuccessful. The restart and failover settings are configured in the Failover Cluster Manager snap-in. For more information these settings, see [\<Resource> Properties: Policies Tab](https://technet.microsoft.com/library/cc725685.aspx).  
  
 For more information on maintaining quorum health, see [WSFC Quorum Modes and Voting Configuration &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/wsfc-quorum-modes-and-voting-configuration-sql-server.md).  
  
## See Also  
 [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-server-configuration-transact-sql.md)  
  
  
