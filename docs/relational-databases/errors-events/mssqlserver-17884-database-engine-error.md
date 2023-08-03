---
title: "MSSQLSERVER_17884"
description: "MSSQLSERVER_17884"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 03/13/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17884 (Database Engine error)"
---
# MSSQLSERVER_17884

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 17884 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | SRV_SCHEDULER_DEADLOCK |
| Message Text | New queries assigned to process on Node %d haven't been picked up by a worker thread in the last %d seconds. Blocking or long-running queries can contribute to this condition, and may degrade client response time. Use the "max worker threads" configuration option to increase number of allowable threads, or optimize current running queries. SQL Process Utilization: %d%%. System Idle: %d%%. |

## Explanation

There's no sign of progress in each of the schedulers, and could be caused by deadlocks where none of the threads can advance, and/or no new work can be picked up and processed. If process utilization is low, then other processes on the machine may be causing the server process CPU starvation.

## User action

Determine why there's blocking and no progress being made and resolve situation accordingly. If process utilization is low, check the load on the system caused by other processes.

To understand common reasons that generate this error condition, refer to [How To Diagnose and Correct Errors 17883, 17884, 17887, and 17888](/previous-versions/sql/sql-server-2005/administrator/cc917684(v=technet.10)).
