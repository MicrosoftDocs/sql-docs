---
title: "MSSQLSERVER_19421"
description: "MSSQLSERVER_19421"
author: pijocoder
ms.author: jopilov
ms.date: 01/13/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "19421 (Database Engine error)"
---
# MSSQLSERVER_19421

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 19421 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | HADR_AG_LEASE_RENEWAL_TIMEOUT |
| Message Text | SQL Server hosting availability group '%.*ls' did not receive a process event signal from the Windows Server Failover Cluster within the lease timeout period. |

## Explanation

Error 19421 is raised in the SQL Server error log when the lease helper on the Windows cluster side didn't signal the SQL Server lease worker thread within the pre-defined lease period. Specifically, SQL Server calls [WaitForMultipleObjects()](/windows/win32/api/synchapi/nf-synchapi-waitformultipleobjects) waiting for the Lease timeout event to be set in a signaled state. If the function returns WAIT_TIMEOUT, because it has exceeded the specified Lease interval, then error 19421 is raised.

A lease is a time-based communication mechanism that takes place between the SQL Server and the Windows Server Failover Cluster (WSFC) process, specifically the RHS.EXE process. The two processes communicate with each other periodically to ensure the other process is running and responding. This communication takes place using Windows [Event objects](/windows/win32/sync/event-objects) and ensures that a failover of the AG resource doesn't occur without the knowledge of the WSFC. If one of the processes doesn't respond to the lease communication based on a predefined lease period, a lease timeout occurs. For detailed information, see [Lease Mechanism](../../database-engine/availability-groups/windows/availability-group-lease-healthcheck-timeout.md). Also see [How It Works: SQL Server AlwaysOn Lease Timeout](https://techcommunity.microsoft.com/t5/sql-server-support-blog/how-it-works-sql-server-alwayson-lease-timeout/ba-p/317268)

This error is related to other lease timeout errors and provides more specific detail for error [MSSQLSERVER_19407](mssqlserver-19407-database-engine-error.md)

### Causes

Since Windows Events are light-weight synchronization objects, there's relatively small number of external factors that affect them negatively. Typical issues that can lead to lease timeout involve system-wide problems. Here's a list of possibilities that can cause lease expiration and cause a restart or failover:

- High CPU usage on the system (close to 100%)
- Out-of-memory conditions - low virtual memory and/or one of the processes is being paged out
- SQL Server process not responding while generating a large memory dump
- WSFC going offline (e.g due to quorum loss)

## User action

Check corresponding Availability Group resource in WSFC cluster to see if it reported any errors.

For detailed troubleshooting, see User action in [MSSQLSERVER_19407](mssqlserver-19407-database-engine-error.md#user-action)

- Troubleshoot high CPU issues
- Troubleshoot low memory issues
- Reduce or avoid large memory dumps of the SQL Server or cluster process
- Check virtual machine (VM) configuration for overprovisioning
- Check for virtual machine (VM) migration or backup causing issues
