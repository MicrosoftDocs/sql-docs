---
title: "Server Configuration: blocked process threshold"
description: Learn how to use the blocked process threshold option to specify the interval at which SQL Server generates blocked process reports and issues alerts.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "thresholds [SQL Server]"
  - "blocked process threshold option"
---
# Server configuration: blocked process threshold

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `blocked process threshold` option to specify the minimum threshold, in seconds, at which blocked process reports are generated. The actual time to detect a blocked process might be several seconds longer than the configured value. The option accepts values from 0 to 86,400; `0` disables reporting. Low threshold values can substantially increase the number of reports when many tasks are blocked.

By default, no blocked process reports are produced. This event isn't generated for system tasks or for tasks that are waiting on resources that don't generate detectable deadlocks.

You can define an [alert](/ssms/agent/alerts) to be executed when this event is generated. So for example, you can choose to page the administrator to take appropriate action to handle the blocking situation.

Blocked process threshold uses the deadlock monitor background thread to walk through the list of tasks waiting for a time greater than or multiples of the configured threshold. The event is generated once per reporting interval for each of the blocked tasks.

The blocked process report is done on a best effort basis. There's no guarantee of any real-time or even close to real-time reporting.

The setting takes effect immediately without a server stop and restart.

## Examples

The following example sets the `blocked process threshold` to `20` seconds, generating a blocked process report for each task that is blocked.

```sql
EXECUTE sp_configure 'show advanced options', 1;
GO

RECONFIGURE;
GO

EXECUTE sp_configure 'blocked process threshold', 20;
GO

RECONFIGURE;
GO
```

## Related content

- [sp_trace_setevent (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)
- [Blocked Process Report Event Class](../../relational-databases/event-classes/blocked-process-report-event-class.md)
