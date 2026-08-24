---
title: CPU Threshold Exceeded Event Class
description: CPU Threshold Exceeded Event Class
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/13/2025
ms.service: sql
ms.subservice: supportability
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "cpu threshold exceeded event class"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# CPU threshold exceeded event class

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The **CPU threshold exceeded** event class indicates that resource governor detected a batch request that exceeds the CPU threshold specified for the `REQUEST_MAX_CPU_TIME_SEC` argument of a [workload group](../resource-governor/resource-governor-workload-group.md). For more information, see [CREATE WORKLOAD GROUP](../../t-sql/statements/create-workload-group-transact-sql.md#request_max_cpu_time_sec--value).

## CPU threshold exceeded data columns

| Data column name | Data type | Description | Column ID | Filterable |
|:--|:--|:--|:--|:--|
| `CPU` | **int** | CPU usage in milliseconds.| 18 | Yes |
| `EventClass` | **int** | 214 | 27 | No |
| `EventSubClass` | **int** | CPU limit violation. | 21 | Yes |
| `GroupID` | **int** | Group ID where the violation occurred. | 66 | Yes |
| `OwnerID` | **int** | SPID of the process that caused the violation. | 58 | Yes |
| `SPID` | **int** | ID of the server process that fires this event.<br /><br /> Note: This can differ from the actual user SPID if a system thread validates CPU usage as a background task. | 12 | Yes |
| `StartTime` | **datetime** | The time when this event fired. | 14 | Yes |

## Related content

- [sp_trace_setevent (Transact-SQL)](../system-stored-procedures/sp-trace-setevent-transact-sql.md)
- [Resource governor](../resource-governor/resource-governor.md)
- [Resource governor workload group](../resource-governor/resource-governor-workload-group.md)
- [ALTER WORKLOAD GROUP (Transact-SQL)](../../t-sql/statements/alter-workload-group-transact-sql.md)
