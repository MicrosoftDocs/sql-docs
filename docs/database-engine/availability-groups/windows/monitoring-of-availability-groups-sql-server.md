---
title: "Tools to monitor availability groups"
description: "A reference for the various tools available to monitor the performance and health of Always On availability groups. "
author: MashaMSFT
ms.author: mathoma
ms.date: 10/05/2021
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seodec18
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], troubleshooting"
---
# Tools to monitor Always On availability groups
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  To monitor the properties and state of an Always On availability group you can use the following tools.  
  
|Tool|Brief Description|Links|  
|----------|-----------------------|-----------|  
|System Center Monitoring pack for SQL Server|The Monitoring pack for SQL Server (SQLMP) is the recommended solution for monitoring availability groups, availability replica and availability databases for IT administrators. Monitoring features that are particularly relevance to [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] include the following:<br /><br /> Automatic discoverability of availability groups, availability replicas, and availability database from among hundreds of computers. This enables you to easily keep track of your [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] inventory.<br /><br /> Fully capable System Center Operations Manager (SCOM) alerting and ticketing. These features provide detailed knowledge that enables faster resolution to a problem.<br /><br /> A custom extension to Always On Health monitoring using Policy Based management (PBM).<br /><br /> Health roll ups from availability databases to availability replicas.<br /><br /> Custom tasks that manage [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] from the System Center Operations Manager console.|To download the monitoring pack (SQLServerMP.msi) and *SQL Server Management Pack Guide for System Center Operations Manager* (SQLServerMPGuide.doc), see:<br /><br /> [System Center Monitoring pack for SQL Server](https://www.microsoft.com/download/details.aspx?id=56203)|  
|[!INCLUDE[tsql](../../../includes/tsql-md.md)]|[!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] catalog and dynamic management views provide a wealth of information about your availability groups and their replicas, databases, listeners, and WSFC cluster environment.|[Monitor Availability Groups &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)|  
|[!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]|The **Object Explorer Details** pane displays basic information about the availability groups hosted on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to which you are connected.<br /><br /> **\*\* Tip \*\*** Use this pane to select multiple availability groups, replicas, or databases and to perform routine administrative tasks on the selected objects; for example, removing multiple availability replicas or databases from an availability group.|[Use the Object Explorer Details to Monitor Availability Groups &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-object-explorer-details-to-monitor-availability-groups.md)|  
|[!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]|**Properties** dialog boxes enable you to view the properties of availability groups, replicas, or listeners and, in some cases, to change their values.|-   [View Availability Group Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-group-properties-sql-server.md)<br />-   [View Availability Replica Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-replica-properties-sql-server.md)<br />-   [View Availability Group Listener Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-group-listener-properties-sql-server.md)|  
|System Monitor|The **SQLServer:Availability Replica** performance object contains performance counters that report information about availability replicas.|[SQL Server, Availability Replica](../../../relational-databases/performance-monitor/sql-server-availability-replica.md)|  
|System Monitor|The **SQLServer:Database Replica** performance object contains performance counters that report information about the secondary databases on a given secondary replica.<br /><br /> The **SQLServer:Databases** object in SQL Server contains performance counters that monitor transaction log activities, among other things. The following counters are particularly relevant for monitoring transaction-log activity on availability databases: **Log Flush Write Time (ms)**, **Log Flushes/sec**, **Log Pool Cache Misses/sec**, **Log Pool Disk Reads/sec**, and **Log Pool Requests/sec**.|[SQL Server, Database Replica](../../../relational-databases/performance-monitor/sql-server-database-replica.md) and [SQL Server, Databases Object](../../../relational-databases/performance-monitor/sql-server-databases-object.md)|  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [The Always On Health Model Part 1 -- Health Model Architecture](/archive/blogs/sqlalwayson/the-alwayson-health-model-part-1-health-model-architecture)  
  
     [The Always On Health Model Part 2 -- Extending the Health Model](/archive/blogs/sqlalwayson/the-alwayson-health-model-part-2-extending-the-health-model)  
  
     [Monitoring Always On Health with PowerShell - Part 1: Basic Cmdlet Overview](/archive/blogs/sqlalwayson/monitoring-alwayson-health-with-powershell-part-1-basic-cmdlet-overview)  
  
     [Monitoring Always On Health with PowerShell - Part 2: Advanced Cmdlet Usage](/archive/blogs/sqlalwayson/monitoring-alwayson-health-with-powershell-part-2-advanced-cmdlet-usage)  
  
     [Monitoring Always On Health with PowerShell - Part 3 : A Simple Monitoring Application](/archive/blogs/sqlalwayson/monitoring-alwayson-health-with-powershell-part-3-a-simple-monitoring-application)  
  
     [Monitoring Always On Health with PowerShell - Part 4 : Integration with SQL Server Agent](/archive/blogs/sqlalwayson/monitoring-alwayson-health-with-powershell-part-4-integration-with-sql-server-agent)  
  
     [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](/archive/blogs/psssql/)  
  
-   **Whitepapers:**  
  
     [Microsoft White Papers for SQL Server 2012](https://social.technet.microsoft.com/wiki/contents/articles/13146.white-paper-gallery-for-sql-server.aspx#[Category]SQLServer2012)  
  
     [SQL Server Customer Advisory Team Whitepapers](https://techcommunity.microsoft.com/t5/DataCAT/bg-p/DataCAT/)  
  
## See Also  
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)   
 [Flexible Failover Policy for Automatic Failover of an Availability Group &#40;SQL Server&#41;](./configure-flexible-automatic-failover-policy.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Automatic Page Repair &#40;Availability Groups: Database Mirroring&#41;](../../../sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring.md)   
 [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
