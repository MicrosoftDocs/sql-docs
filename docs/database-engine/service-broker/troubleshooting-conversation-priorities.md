---
title: Troubleshooting Conversation Priorities
description: "This topic provides suggestions for correcting common symptoms related to Service Broker conversation priorities."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Troubleshooting Conversation Priorities

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This topic provides suggestions for correcting common symptoms related to Service Broker conversation priorities.

## Technique: Determining if HONOR_BROKER_PRIORITY is ON

Use the **sys.databasesis_broker_priority_honored** column to determine the state of the HONOR_BROKER_PRIORITY database option:

```sql
    SELECT name AS database_name,
           CASE is_broker_priority_honored
                WHEN 0 THEN N'OFF'
                WHEN 1 THEN N'ON'
           END AS is_broker_priority_honored
    FROM sys.databases
    ORDER BY database_name;
```

## Symptom: Messages Are Not Sent in Priority Sequence

Open a SQL Server Profiler trace and review the Broker Remote Message Ack events. A value of 1 in the **StarvationElevation** column indicates the priority of messages was elevated to prevent starvation. A value of 0 in the **HonorBokerPriority** column indicates the HONOR_BROKER_PRIORITY option was not enabled in the sending database.

Also review the Broker/DBM Transport System Monitor counter to see the transmission rates for messages of different priority levels.

## Symptom: Messages Are Not Received in Priority Sequence

A RECEIVE statement only retrieves messages from one conversation group. It will not receive messages from high priority conversations if they belong to a different conversation group.

A RECEIVE statement that does not have a WHERE clause retrieves messages from the highest priority unlocked conversation group. If the conversation group has a mix of high priority and low priority conversations, the RECEIVE statement might retrieve messages from the low priority conversations. This can occur even if the queue has messages from high priority conversations in other groups.

A RECEIVE statement that has a WHERE clause that specifies a conversation group retrieves only messages from the specified conversation group. The RECEIVE statement will retrieve messages from low priority conversations in the group regardless of the priority level of messages from other conversation groups.

## Symptom: Messages Are Not Assigned the Expected Priority Level

View **sys.conversation_endpoint** to see whether the conversation endpoint was assigned the expected priority level. If it was not, use **sys.conversation_priorites** to review the properties specified for the conversation priorities in the database against the contract, local service, and remote service that is used for the conversation endpoint.

## See also

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
- [Conversation Priorities](conversation-priorities.md)
