---
title: "Monitoring of Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], troubleshooting"
ms.assetid: 1d5e3291-0d0a-45a1-88e5-1fc242d17210
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Monitoring of Availability Groups (SQL Server)
  To monitor the properties and state of an AlwaysOn availability group you can use the following tools.  
  
|Tool|Brief Description|Links|  
|----------|-----------------------|-----------|  
|System Center Monitoring pack for SQL Server|The Monitoring pack for SQL Server (SQLMP) is the recommended solution for monitoring availability groups, availability replica and availability databases for IT administrators. Monitoring features that are particularly relevance to [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] include the following:<br /><br /> Automatic discoverability of availability groups, availability replicas, and availability database from among hundreds of computers. This enables you to easily keep track of your [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] inventory.<br /><br /> Fully capable System Center Operations Manager (SCOM) alerting and ticketing. These features provide detailed knowledge that enables faster resolution to a problem.<br /><br /> A custom extension to AlwaysOn Health monitoring using Policy Based management (PBM).<br /><br /> Health roll ups from availability databases to availability replicas.<br /><br /> Custom tasks that manage [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] from the System Center Operations Manager console.|To download the monitoring pack (SQLServerMP.msi) and *SQL Server Management Pack Guide for System Center Operations Manager* (SQLServerMPGuide.doc), see:<br /><br /> [System Center Monitoring pack for SQL Server](https://www.microsoft.com/download/details.aspx?displaylang=en&id=10631)|  
|[!INCLUDE[tsql](../../../includes/tsql-md.md)]|[!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] catalog and dynamic management views provide a wealth of information about your availability groups and their replicas, databases, listeners, and WSFC cluster environment.|[Monitor Availability Groups &#40;Transact-SQL&#41;](monitor-availability-groups-transact-sql.md)|  
|[!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]|The **Object Explorer Details** pane displays basic information about the availability groups hosted on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to which you are connected.<br /><br /> Tip: Use this pane to select multiple availability groups, replicas, or databases and to perform routine administrative tasks on the selected objects; for example, removing multiple availability replicas or databases from an availability group.|[Use the Object Explorer Details to Monitor Availability Groups &#40;SQL Server Management Studio&#41;](use-object-explorer-details-to-monitor-availability-groups.md)|  
|[!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]|**Properties** dialog boxes enable you to view the properties of availability groups, replicas, or listeners and, in some cases, to change their values.|[View Availability Group Properties &#40;SQL Server&#41;](view-availability-group-properties-sql-server.md)<br /><br /> [View Availability Replica Properties &#40;SQL Server&#41;](view-availability-replica-properties-sql-server.md)<br /><br /> [View Availability Group Listener Properties &#40;SQL Server&#41;](view-availability-group-listener-properties-sql-server.md)|  
|System Monitor|The **SQLServer:Availability Replica** performance object contains performance counters that report information about availability replicas.|[SQL Server, Availability Replica](../../../relational-databases/performance-monitor/sql-server-availability-replica.md)|  
|System Monitor|The **SQLServer:Database Replica** performance object contains performance counters that report information about the secondary databases on a given secondary replica.<br /><br /> The **SQLServer:Databases** object in SQL Server contains performance counters that monitor transaction log activities, among other things. The following counters are particularly relevant for monitoring transaction-log activity on availability databases: **Log Flush Write Time (ms)**, **Log Flushes/sec**, **Log Pool Cache Misses/sec**, **Log Pool Disk Reads/sec**, and **Log Pool Requests/sec**.|[SQL Server, Database Replica](../../../relational-databases/performance-monitor/sql-server-database-replica.md) and [SQL Server, Databases Object](../../../relational-databases/performance-monitor/sql-server-databases-object.md)|  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [The AlwaysOn Health Model Part 1 -- Health Model Architecture](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/09/overview-of-the-alwayson-manageability-health-model.aspx)  
  
     [The AlwaysOn Health Model Part 2 -- Extending the Health Model](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/13/extending-the-alwayson-health-model.aspx)  
  
     [Monitoring AlwaysOn Health with PowerShell - Part 1: Basic Cmdlet Overview](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/13/monitoring-alwayson-health-with-powershell-part-1.aspx)  
  
     [Monitoring AlwaysOn Health with PowerShell - Part 2: Advanced Cmdlet Usage](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/13/monitoring-alwayson-health-with-powershell-part-2.aspx)  
  
     [Monitoring AlwaysOn Health with PowerShell - Part 3 : A Simple Monitoring Application](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/15/monitoring-alwayson-health-with-powershell-part-3.aspx)  
  
     [Monitoring AlwaysOn Health with PowerShell - Part 4 : Integration with SQL Server Agent](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/15/the-always-on-health-model-part-4.aspx)  
  
     [SQL Server AlwaysOn Team Blogs: The official SQL Server AlwaysOn Team Blog](https://blogs.msdn.com/b/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)  
  
-   **Whitepapers:**  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](http://sqlcat.com/)  
  
## See Also  
 [AlwaysOn Availability Groups Catalog Views &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql)   
 [AlwaysOn Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions)   
 [Flexible Failover Policy for Automatic Failover of an Availability Group &#40;SQL Server&#41;](flexible-automatic-failover-policy-availability-group.md)   
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Automatic Page Repair &#40;For Availability Groups and Database Mirroring&#41;](../../../sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring.md)   
 [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)  
  
  
