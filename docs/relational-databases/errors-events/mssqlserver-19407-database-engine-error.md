---
description: "MSSQLSERVER_19407"
title: "MSSQLSERVER_19407 | Microsoft Docs"
ms.custom: ""
ms.date: "11/04/2022"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "19407 (Database Engine error)"
author: pijocoder
ms.author: jopilov
---
# MSSQLSERVER_19407
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|19407|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HADR_AG_LEASE_EXPIRED|  
|Message Text|The lease between availability group '%.*ls' and the Windows Server Failover Cluster has expired. A connectivity issue occurred between the instance of SQL Server and the Windows Server Failover Cluster. To determine whether the availability group is failing over correctly, check the corresponding availability group resource in the Windows Server Failover Cluster.|  
  
## Explanation  

Error 19407 is raised in the SQL Server error log when the communication between SQL Server and the Windows Server Failover cluster is lost and a corrective action takes place. Typically the corrective action is a failover to another Always On node. 

A lease is a time-based communication mechanism that takes place between the SQL Server and the Windows Server Failover Cluster (WSFC) process, specifically the RHS.EXE process. The two processes communicate with each other periodically to ensure the other process is running and responding. This communication takes place using Windows [event objects](/windows/win32/sync/event-objects) and ensures that a fail over of the AG resource does not occur without the knowledge of the WSFC. If one the processes does not respond to the lease communication based on a predefined lease period, a lease timeout occurs. For detailed information see [Lease Mechanism](../../database-engine/availability-groups/windows/availability-group-lease-healthcheck-timeout.md). Also see [How It Works: SQL Server AlwaysOn Lease Timeout[(https://techcommunity.microsoft.com/t5/sql-server-support-blog/how-it-works-sql-server-alwayson-lease-timeout/ba-p/317268)

### Causes

Since Windows Events are light-weight synchronization objects, there is relatively small number of external factors that impact them negatively. Typical issues that can lead to lease timeout involve system-wide problems. Here's a list of possibilities that can cause lease expiration and cause a restart or failover:

- High CPU usage on the system (close to 100%)
- Out-of-memory conditions - low virtual memory and/or one of the processing is being paged out
- SQL Server process not responding while generating a large memory dump 
- WSFC going offline (e.g due to quorum loss)

 
## User Action  

- T-shoot high CPU
    1. Open Task Manager
    1. Go to Performance tab and see if CPUs are close to or at 100% utilization
    1. Go to Processes  tab and sort processes by the CPU column in descending order by clicking on the CPU column
    1. Identify the process that uses most CPU and work on understanding why
    1. If the process is SQL Server, see [Troubleshoot high-CPU-usage issues in SQL Server](/troubleshoot/sql/performance/troubleshoot-high-cpu-usage-issues)
- Low memory
- Avoid any large memory dumps
- If VM check for any VM configuration and if the underlying physical machine is overprovisioned causing strain on CPU and memory resources for the VM
- Check cluster logs for nodes going offline