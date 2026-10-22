---
title: Monitor Replication with Performance Monitor
description: Learn which performance objects and counters to use when monitoring replication with Performance Monitor.
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 06/23/2025
ms.service: sql
ms.subservice: replication
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "monitoring performance [SQL Server replication], Performance Monitor"
  - "Performance Monitor [SQL Server], replication"
  - "performance counters [SQL Server replication]"
---
# Monitor replication with Performance Monitor

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Windows Performance Monitor allows you to:

- Use graphs, charts, and reports to gauge the efficiency of your computer, and
- Identify and troubleshoot possible problems (such as unbalanced resource use, insufficient hardware, or poor program design), and
- Plan for extra hardware needs.

For more information, see [Monitor Resource Usage (Performance Monitor)](../../performance-monitor/monitor-resource-usage-system-monitor.md).

Performance Monitor uses performance objects and counters, which provide information on the performance of various processes. You can measure replication performance through counters associated with the replication agents:

| Agent | Performance object | Counter | Description |
| --- | --- | --- | --- |
| All agents | `SQLServer:Replication Agents` | `Running` | The number of replication agents currently running. |
| Snapshot Agent | `SQLServer:Replication Snapshot` | `Snapshot:Delivered Cmds/sec` | The number of commands per second delivered to the Distributor. |
| Snapshot Agent | `SQLServer:Replication Snapshot` | `Snapshot:Delivered Trans/sec` | The number of transactions per second delivered to the Distributor. |
| Log Reader Agent | `SQLServer:Replication Logreader` | `Logreader:Delivered Cmds/sec` | The number of commands per second delivered to the Distributor. |
| Log Reader Agent | `SQLServer:Replication Logreader` | `Logreader:Delivered Trans/sec` | The number of transactions per second delivered to the Distributor. |
| Log Reader Agent | `SQLServer:Replication Logreader` | `Logreader:Delivery Latency` | The current amount of time (in milliseconds) elapsed from when transactions are applied at the Publisher to when they're delivered to the Distributor. |
| Distribution Agent | `SQLServer:Replication Dist.` | `Dist:Delivered Cmds/sec` | The number of commands per second delivered to the Subscriber. |
| Distribution Agent | `SQLServer:Replication Dist.` | `Dist:Delivered Trans/sec` | The number of transactions per second delivered to the Subscriber. |
| Distribution Agent | `SQLServer:Replication Dist.` | `Dist:Delivery Latency` | The current amount of time (in milliseconds) elapsed from when transactions are delivered to the Distributor to when they're applied at the Subscriber. |
| Merge Agent | `SQLServer:Replication Merge` | `Conflicts/sec` | The number of conflicts per second occurring during the merge process. |
| Merge Agent | `SQLServer:Replication Merge` | `Downloaded Changes/sec` | The number of rows per second replicated from the Publisher to the Subscriber. |
| Merge Agent | `SQLServer:Replication Merge` | `Uploaded Changes/sec` | The number of rows per second replicated from the Subscriber to the Publisher. |

## Related content

- [Monitoring (Replication)](monitoring-replication.md)
