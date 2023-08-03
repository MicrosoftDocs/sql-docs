---
title: "MSSQLSERVER_17883"
description: "MSSQLSERVER_17883"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 03/13/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17883 (Database Engine error)"
---
# MSSQLSERVER_17883

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 17883 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | SRV_SCHEDULER_NONYIELDING |
| Message Text | Process %ld:%ld:%ld (0x%lx) Worker 0x%p appears to be non-yielding on Scheduler %ld. Thread creation time: %I64d. Approx Thread CPU Used: kernel %I64d ms, user %I64d ms. Process Utilization %d%%. System Idle %d%%. Interval: %I64d ms. |

## Explanation

Indicates that there's a possible problem with a thread not yielding on a scheduler. This error may be caused by an operating system condition, environment issue, or software issue in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] isn't getting enough cycles to execute.  This error may go away if the thread eventually yields.

## User action

If you look at the error message information, you see that certain behaviors emerge. For example:

- If user mode time quickly climbs and continues to do so, the likely cause is an unbounded loop in the SQL Server engine that isn't properly yielding.

- If kernel mode time climbs quickly, the thread is spending most its time in the operating system, and requires kernel debugging to determine the root cause of this behavior.

- If the kernel time and the user time aren't increasing quickly, the thread is likely waiting for an API call such as `WaitForSingleObject`, `Sleep`, `WriteFile`, or `ReadFile` to return. Or, the thread may not be getting scheduled by the operating system. API stall conditions generally require kernel mode debugging to determine their root cause.

- If `System Idle %` is low and `Process Utilization %` is low, then SQL Server might not be getting enough CPU cycles. Check the CPU utilization of other applications on the system. Also, check to see if paging is going on in the system. Running `SELECT * FROM sys.dm_os_ring_buffers` can provide more details as well.

- If `kernel + user` times are low but `Process Utilization %` is high, the error condition could indicate that preemptive thread(s) are consuming all the CPU (for example, garbage collection).

Combining information with the system utilization and idle time can provide insight into the nature of the problem.

To understand the detection logic and common reasons that generate this error condition, refer to [How To Diagnose and Correct Errors 17883, 17884, 17887, and 17888](/previous-versions/sql/sql-server-2005/administrator/cc917684(v=technet.10)).
