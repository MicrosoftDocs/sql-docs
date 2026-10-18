---
title: Troubleshoot Conversation Priorities
description: "This article provides suggestions for correcting common symptoms related to Service Broker conversation priorities."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/03/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Troubleshoot conversation priorities

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This article provides suggestions for correcting common symptoms related to Service Broker conversation priorities.

## Technique: Determine if HONOR_BROKER_PRIORITY is ON

Use the `sys.databasesis_broker_priority_honored` column to determine the state of the `HONOR_BROKER_PRIORITY` database option:

```sql
SELECT name AS database_name,
       CASE is_broker_priority_honored
           WHEN 0 THEN N'OFF'
           WHEN 1 THEN N'ON'
       END AS is_broker_priority_honored
FROM sys.databases
ORDER BY database_name;
```

## Symptom: Messages aren't sent in priority sequence

Open a SQL Server Profiler trace and review the **Broker:Remote Message Ack** events. A value of 1 in the `StarvationElevation` column indicates the priority of messages was elevated to prevent starvation. A value of 0 in the `HonorBrokerPriority` column indicates the `HONOR_BROKER_PRIORITY` option wasn't enabled in the sending database.

Also review the Broker/DBM Transport System Monitor counter to see the transmission rates for messages of different priority levels.

## Symptom: Messages aren't received in priority sequence

A `RECEIVE` statement only retrieves messages from one conversation group. It doesn't receive messages from high priority conversations if they belong to a different conversation group.

A `RECEIVE` statement that doesn't have a `WHERE` clause retrieves messages from the highest priority unlocked conversation group. If the conversation group has a mix of high priority and low priority conversations, the `RECEIVE` statement might retrieve messages from the low priority conversations. This scenario can occur even if the queue has messages from high priority conversations in other groups.

A `RECEIVE` statement that has a `WHERE` clause that specifies a conversation group retrieves only messages from the specified conversation group. The `RECEIVE` statement retrieves messages from low priority conversations in the group regardless of the priority level of messages from other conversation groups.

## Symptom: Messages aren't assigned the expected priority level

View `sys.conversation_endpoint` to see whether the conversation endpoint was assigned the expected priority level. If it wasn't, use `sys.conversation_priorities` to review the properties specified for the conversation priorities in the database against the contract, local service, and remote service that is used for the conversation endpoint.

## Related content

- [Broker:Remote Message Ack Event Class](../../relational-databases/event-classes/broker-remote-message-ack-event-class.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [ALTER BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/alter-broker-priority-transact-sql.md)
- [CREATE BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/create-broker-priority-transact-sql.md)
- [DROP BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/drop-broker-priority-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [SQL Server, Broker/DBM Transport object](../../relational-databases/performance-monitor/sql-server-broker-dbm-transport-object.md)
- [sys.conversation_priorities (Transact-SQL)](../../relational-databases/system-catalog-views/sys-conversation-priorities-transact-sql.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.transmission_queue (Transact-SQL)](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)
- [Conversation priorities](conversation-priorities.md)
