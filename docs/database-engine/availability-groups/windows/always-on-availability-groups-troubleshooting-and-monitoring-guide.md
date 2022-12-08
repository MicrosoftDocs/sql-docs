---
title: "Monitor and troubleshoot availability groups guide"
description: "An index of content to help you get started on monitoring and troubleshooting some of the common issues found with Always On availability groups."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/10/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: reference
ms.custom: seo-lt-2019
---
# Monitor and troubleshoot availability groups
 This guide helps you get started on monitoring Always On Availability Groups and troubleshooting some of the common issues in availability groups. It provides original content as well as a landing page of useful information that is published elsewhere. While this guide cannot fully discuss all the issues that can occur in the large area of availability groups, it can point you in the right direction in your root-cause analysis and resolution of issues. 
 
 Because availability groups are an integrated technology, many problems you encounter may be symptoms of other issues in your database system. Some issues are caused by settings within an availability group, such as an availability database being suspended. Other issues can include problems with other aspects of SQL Server, such as SQL Server settings, database file deployments, and systemic performance issues unrelated to availability. Still other problems can exist outside of SQL Server, such as network I/O, TCP/IP, Active Directory, and Windows Server Failover Clustering (WSFC) issues. Often, problems that surface in an availability group, replica, or database require you to troubleshoot multiple technologies to identify the root cause.  
  
  
##  <a name="BKMK_SCENARIOS"></a> Troubleshooting scenarios  
 The following table contains links to the common troubleshooting scenarios for availability groups. They are categorized by their scenario types, such as configuration, client connectivity, failover, and performance.  
  
|Scenario|Scenario type|Description|  
|--------------|-------------------|-----------------|  
|[Troubleshoot Always On Availability Groups configuration &#40;SQL Server&#41;](troubleshoot-always-on-availability-groups-configuration-sql-server.md)|Configuration|Provides information to help you troubleshoot typical problems with configuring server instances for availability groups. Typical configuration problems include availability groups are disabled, accounts are incorrectly configured, the database mirroring endpoint does not exist, the endpoint is inaccessible (SQL Server Error 1418), network access does not exist, and a join database command fails (SQL Server Error 35250).|  
|[Troubleshoot a failed add-file operation &#40;Always On Availability Groups&#41;](troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)|Configuration|An add-file operation caused the secondary database to be suspended and be in the NOT SYNCHRONIZING state.|  
|[Cannot connect to availability group listener in a multi-subnet environment](https://support.microsoft.com/kb/2792139/en-us)|Client Connectivity|After you configure the availability group listener, you are unable to ping the listener or connect to it from an application.|  
|[Troubleshoot failed automatic failovers](https://support.microsoft.com/kb/2833707)|Failover|An automatic failover did not complete successfully.|  
|[Troubleshoot: Availability group exceeded RTO](troubleshoot-availability-group-exceeded-rto.md)|Performance|After an automatic failover or a planned manual failover without data loss, the failover time exceeds your RTO. Or, when you estimate the failover time of a synchronous-commit secondary replica (such as an automatic failover partner), you find that it exceeds your RTO.|  
|[Troubleshoot: Availability group exceeded RPO](troubleshoot-availability-group-exceeded-rpo.md)|Performance|After you perform a forced manual failover, your data loss is more than your RPO. Or, when you calculate the potential data loss of an asynchronous-commit secondary replica, you find that it exceeds your RPO.|  
|[Troubleshoot: Changes on the primary replica are not reflected on the secondary replica](troubleshoot-primary-changes-not-reflected-on-secondary.md)|Performance|The client application completes an update on the primary replica successfully, but querying the secondary replica shows that the change is not reflected.|  
|[Troubleshoot: High HADR_SYNC_COMMIT wait type with Always On Availability Groups](/archive/blogs/sql_server_team/troubleshooting-high-hadr_sync_commit-wait-type-with-always-on-availability-groups)|Performance|If HADR_SYNC_COMMIT is unusually long, there is a performance issue in data movement flow or secondary replica log hardening.|  

##  <a name="BKMK_TOOLS"></a> Useful tools for troubleshooting  
 When configuring or running availability groups, different tools can help you diagnose different types of issues. The following table provides links to useful information about the tools.  
  
|Tool|Description|  
|----------|-----------------|  
|[Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)|Reports an at-a-glance view of the health of your availability group in a user-friendly interface.|  
|[Always On policies](always-on-policies.md)|Used by the Always On Dashboard.|  
|[SQL Server Error Log &#40;Always On Availability Groups&#41;](sql-server-error-log-always-on-availability-groups.md)|Logs state transition events for availability groups, replicas, and databases, statuses of other Always On components, and Always On errors.|  
|[CLUSTER.LOG &#40;Always On Availability Groups&#41;](cluster-log-always-on-availability-groups.md)|Logs cluster events, including state transitions of the availability group resource, as well as events and errors from SQL Server resource DLL.|  
|[Always On health diagnostics log](always-on-health-diagnostics-log.md)|Logs SQL Server health diagnostics as reported to the WSFC cluster (SQL Server resource DLL) by [sp_server_diagnostics &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md).|  
|[Dynamic management views and system catalog views &#40;Always On Availability Groups&#41;](dynamic-management-views-and-system-catalog-views-always-on-availability-groups.md)|Reports information on the availability groups such as configuration, health status, and performance metrics.|  
|[Always On extended events](always-on-extended-events.md)|Provides detailed diagnostics of the availability groups and useful for root-cause analysis.|  
|[Always On wait types](always-on-wait-types.md)|Provides wait statistics specific to availability groups and useful for performance tuning.|  
|Always On performance counters|Monitor availability groups activity, are reflected in System Monitor, and are useful for performance tuning. For more information, see [SQL Server, availability replica](~/relational-databases/performance-monitor/sql-server-availability-replica.md) and [SQL Server, database replica](~/relational-databases/performance-monitor/sql-server-database-replica.md).|  
|[Always On ring buffers](always-on-ring-buffers.md)|Record alerts within the SQL Server system for internal diagnostics, and can be used to debug issues related to the availability groups.|  
  
##  <a name="BKMK_MONITOR"></a> Monitoring availability groups  
 The ideal time to troubleshoot an availability group is before a problem necessitates a failover, whether automatic or manual. This can be done by monitoring the availability group's performance metrics and sending alerts when the availability replicas are performing outside the bounds of your service-level agreement (SLA). For example, if a synchronous secondary replica has performance issues that cause the estimated failover time to increase, you do not want to wait until an automatic failover occurs and you find out that the failover time exceeds your recovery time objective.  
  
 Because availability groups are a high availability and disaster recovery solution, the most important performance metrics to monitor are the estimated failover time, which affects your recovery time objective (RTO), and the potential data loss in a disaster, which affects your recovery point objective (RPO). You can gather these metrics from the data that SQL Server exposes at any given time, so you can be alerted of a problem in the High Availability Disaster Recovery (HADR) capabilities of your system before actual failure events occur. Therefore, it is important to familiarize yourself with the data synchronization process of availability groups and gather the metrics accordingly.  
  
 This table below points you to topics that can help you monitor the health of your availability groups solution.  
  
|Topic|Description|  
|-----------|-----------------|  
|[Monitor performance for Always On Availability Groups](monitor-performance-for-always-on-availability-groups.md)|Describes the data synchronization process for availability groups, the flow control gates, and useful metrics when monitoring an availability group; and also shows how to gather RTO and RPO metrics.|  
|[Monitoring of availability groups &#40;SQL Server&#41;](monitoring-of-availability-groups-sql-server.md)|Provides information on tools for monitoring an availability group.|  
|[The Always On health model, part 1: Health model architecture](/archive/blogs/sqlalwayson/the-alwayson-health-model-part-1-health-model-architecture)|Provides an overview of the Always On health model.|  
|[The Always On health model, part 2: Extending the health model](/archive/blogs/sqlalwayson/the-alwayson-health-model-part-2-extending-the-health-model)|Shows how to customize the Always On health model and customize the Always On Dashboard to show extra information.|  
|[Monitoring Always On health with PowerShell, part 1: Basic cmdlet overview](/archive/blogs/sqlalwayson/monitoring-alwayson-health-with-powershell-part-1-basic-cmdlet-overview)|Provides a basic overview of the Always On PowerShell cmdlets that can be used to monitor the health of an availability group.|  
|[Monitoring Always On health with PowerShell, part 2: Advanced cmdlet usage](/archive/blogs/sqlalwayson/monitoring-alwayson-health-with-powershell-part-2-advanced-cmdlet-usage)|Provides information on advanced usage of the Always On PowerShell cmdlets to monitor the health of an availability group.|  
|[Monitoring Always On health with PowerShell, part 3: A simple monitoring application](/archive/blogs/sqlalwayson/monitoring-alwayson-health-with-powershell-part-3-a-simple-monitoring-application)|Shows how to automatically monitor an availability group with an application.|  
|[Monitoring Always On health with PowerShell, part 4: Integration with SQL Server Agent](/archive/blogs/sqlalwayson/monitoring-alwayson-health-with-powershell-part-4-integration-with-sql-server-agent)|Provides information on how to integrate availability group monitoring with SQL Server Agent and configure notification to the appropriate parties when problems arise.|  

## Next steps  
 [SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)   
 [CSS SQL Server Engineers Blogs](/archive/blogs/psssql/)  
  
