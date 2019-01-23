---
title: "Configure a flexible automatic failover policy for an availability group"
description: "A description of the various options available when deciding how flexible your failover policy should be for an Always On availability group. "
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], flexible failover policy"
  - "Availability Groups [SQL Server], failover"
  - "flexible failover policy"
  - "failover [SQL Server], AlwaysOn Availability Groups"
ms.assetid: 8c504c7f-5c1d-4124-b697-f735ef0084f0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Configure a flexible automatic failover policy for an Always On availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  A flexible failover policy provides granular control over the conditions that cause [automatic failover](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md) for an availability group. By changing the failure conditions that trigger an automatic failover and the frequency of health checks, you can increase or decrease the likelihood of an automatic failover to support your SLA for high availability.  
  
 The flexible failover policy of an availability group is defined by its failure-condition level and health-check timeout threshold. On detecting that an availability group has exceeded its failure condition level or its health-check timeout threshold, the availability group's resource DLL responds back to the Windows Server Failover Clustering (WSFC) cluster. The WSFC cluster then initiates an automatic failover to the secondary replica.  
  
> [!IMPORTANT]  
>  If an availability group exceeds its WSFC failure threshold, the WSFC cluster will not attempt an automatic failover for the availability group. Furthermore, the WSFC resource group of the availability group remains in a failed state until either the cluster administrator manually brings the failed resource group online or the database administrator performs a manual failover of the availability group. The *WSFC failure threshold* is defined as the maximum number of failures supported for the availability group during a given time period. The default time period is six hours, and the default value for the maximum number of failures during this period is *n*-1, where *n* is the number of WSFC nodes. To change the failure-threshold values for a given availability group, use the WSFC Failover Manager Console.  
  
 This topic contains the following sections:  
  
-   [Health-Check Timeout Threshold](#HCtimeout)  
  
-   [Failure-Condition Level](#FClevel)  
  
-   [Related Tasks](#RelatedTasks)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="HCtimeout"></a> Health-Check Timeout Threshold  
 WSFC resource DLL of the availability group performs a *health check* of the primary replica by calling the [sp_server_diagnostics](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) stored procedure on the instance of SQL Server that hosts the primary replica. **sp_server_diagnostics** returns results at an interval that equals 1/3 of the health-check timeout threshold for the availability group. The default health-check timeout threshold is 30 seconds, which causes **sp_server_diagnostics** to return at a 10-second interval. If **sp_server_diagnostics** is slow or is not returning information, the resource DLL will wait for the full interval of the health-check timeout threshold before determining that the primary replica is unresponsive. If the primary replica is unresponsive, an automatic failover is initiated, if currently supported.  
  
> [!IMPORTANT]  
>  **sp_server_diagnostics** does not perform health checks at the database level.  
  
##  <a name="FClevel"></a> Failure-Condition Level  
 Whether the diagnostic data and health information returned by **sp_server_diagnostics** warrants an automatic failover depends on the failure-condition level of the availability group. The *failure-condition level* specifies what failure conditions trigger an automatic failover. There are five failure-condition levels, which range from the least restrictive (level one) to the most restrictive (level five). A given level encompasses the less restrictive levels. Thus, the strictest level, five, includes the four less restrictive conditions, and so forth.  
  
> [!IMPORTANT]  
>  Damaged databases and suspect databases are not detected by any failure-condition level. Therefore, a database that is damaged or suspect (whether due to a hardware failure, data corruption, or other issue) never triggers an automatic failover.  
  
 The following table describes the failure-conditions that corresponds to each level.  
  
|Level|Failure Condition|[!INCLUDE[tsql](../../../includes/tsql-md.md)] Value|PowerShell Value|  
|-----------|-----------------------|------------------------------|----------------------|  
|One|On server down. Specifies that an automatic failover is initiated when one the following occurs:<br /><br /> The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service is down.<br /><br /> The lease of the availability group for connecting to the WSFC cluster expires because no ACK is received from the server instance. For more information, see [How It Works: SQL Server Always On Lease Timeout](https://blogs.msdn.com/b/psssql/archive/2012/09/07/how-it-works-sql-server-Always%20On-lease-timeout.aspx).<br /><br /> <br /><br /> This is the least restrictive level.|1|**OnServerDown**|  
|Two|On server unresponsive. Specifies that an automatic failover is initiated when one of the following occurs:<br /><br /> The instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not connect to cluster, and the user-specified health check timeout threshold of the availability group is exceeded.<br /><br /> The availability replica is in failed state.|2|**OnServerUnresponsive**|  
|Three|On critical server error. Specifies that an automatic failover is initiated on critical [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] internal errors, such as orphaned spinlocks, serious write-access violations, or too much dumping.<br /><br /> This is the default level.|3|**OnCriticalServerError**|  
|Four|On moderate server error. Specifies that an automatic failover is initiated on moderate [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] internal errors, such as a persistent out-of-memory condition in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] internal resource pool.|4|**OnModerateServerError**|  
|Five|On any qualified failure conditions. Specifies that an automatic failover is initiated on any qualified failure conditions, including:<br /><br /> Detection of Scheduler deadlock.<br /><br /> Detection of an unsolvable deadlock.<br /><br /> <br /><br /> This is the most restrictive level.|5|**OnAnyQualifiedFailureConditions**|  
  
> [!NOTE]  
>  Lack of response by an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to client requests is irrelevant to availability groups.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To configure automatic failover**  
  
-   [Change the Availability Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-availability-mode-of-an-availability-replica-sql-server.md) (automatic failover requires synchronous-commit availability mode)  
  
-   [Change the Failover Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-failover-mode-of-an-availability-replica-sql-server.md)  
  
-   [Configure the Flexible Failover Policy to Control Conditions for Automatic Failover &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [How It Works: SQL Server Always On Lease Timeout](https://blogs.msdn.com/b/psssql/archive/2012/09/07/how-it-works-sql-server-Always%20On-lease-timeout.aspx)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md)   
 [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md)   
 [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)   
 [Failover Policy for Failover Cluster Instances](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)   
 [sp_server_diagnostics &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md)  
  
  
