---
title: SQL Server Service Broker
description: Learn about Service Broker. See how it provides native support for messaging in the SQL Server Database Engine and Azure SQL Managed Instance.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/23/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
f1_keywords:
  - "SQL13.SWB.SSBMSGTYPEPROPERTIES.GENERAL.F1"
  - "SQL13.SWB.SSBCONTRACTPROPERTIES.GENERAL.F1"
  - "SQL13.SWB.SSBQUEUEPROPERTIES.GENERAL.F1"
  - "SQL13.SWB.SSBREMSVCBINDPROPERTIES.GENERAL.F1"
  - "SQL13.SWB.SSBROUTEPROPERTIES.GENERAL.F1"
  - "SQL13.SWB.SSBPRIORITYPROPERTIES.GENERAL.F1"
  - "SQL13.SWB.SSBSERVICEPROPERTIES.GENERAL.F1"
helpviewer_keywords:
  - "Broker See Service Broker"
  - "SQL Server Service Broker"
  - "Service Broker"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017 || >=sql-server-linux-2017"
---

# Service Broker

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssSB](../../includes/sssb-md.md)] provide native support for messaging and queuing in the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-index). Developers can easily create sophisticated applications that use the [!INCLUDE [ssDE](../../includes/ssde-md.md)] components to communicate between disparate databases, and build distributed and reliable applications.

## When to use Service Broker

Use Service Broker components to implement native in-database asynchronous message processing functionalities. Application developers who use [!INCLUDE [ssSB](../../includes/sssb-md.md)] can distribute data workloads across several databases without programming complex communication and messaging internals. Service Broker reduces development and test work because [!INCLUDE [ssSB](../../includes/sssb-md.md)] handles the communication paths in the context of a conversation. It also improves performance. For example, front-end databases supporting Web sites can record information and send process intensive tasks to queue in back-end databases. [!INCLUDE [ssSB](../../includes/sssb-md.md)] ensures that all tasks are managed in the context of transactions to assure reliability and technical consistency.

## Overview

Service Broker is a message delivery framework that enables you to create native in-database service-oriented applications. Unlike classic query processing functionalities that constantly read data from the tables and process them during the query lifecycle, service-oriented applications have database services that are exchanging the messages. Every service has a queue where the messages are placed until they're processed.

:::image type="content" source="media/service-broker.png" alt-text="Diagram of Service Broker process flow.":::

The messages in the queues can be fetched using the Transact-SQL `RECEIVE` command, or by the activation procedure that is called whenever the message arrives in the queue.

### Create services

>[!NOTE]
> A target service must expose one or more **contracts**. If you create a service without a contracts it will not be able to receive messages. Messages sent will appear to succeed, but the messages will remain on the initiator's [sys.transmission_queue](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)

```sql
/*
In this example, the initiator must then use ON CONTRACT [DEFAULT] and a MESSAGE TYPE [DEFAULT]. [DEFAULT] is a delimited identifier for the built‑in contract and isn't a T‑SQL keyword, so it must be bracketed or quoted.
*/
CREATE QUEUE dbo.ExpenseQueue;
GO

CREATE SERVICE ExpensesService
ON QUEUE dbo.ExpenseQueue ([DEFAULT]);
```

### Send messages

Messages are sent on the conversation between the services using the [SEND](../../t-sql/statements/send-transact-sql.md) Transact-SQL statement. A conversation is a communication channel that is established between the services using the `BEGIN DIALOG` Transact-SQL statement.

```sql
-- Begin a dialog
DECLARE @dialog_handle AS UNIQUEIDENTIFIER;

BEGIN DIALOG @dialog_handle
    FROM SERVICE ExpensesClient
    TO SERVICE N'ExpensesService'
    ON CONTRACT [DEFAULT];

-- Send a message
SEND ON CONVERSATION (@dialog_handle)
    MESSAGE TYPE [DEFAULT] (N'<Expense ExpenseId="1" Amount="123.45" Currency="USD"/>');
```

The message is sent to the `ExpensesService` and placed in `dbo.ExpenseQueue`. Because there's no activation procedure associated with this queue, the message remains in the queue until someone reads it.

### Process messages

The messages that are placed in the queue can be selected by using a standard `SELECT` query. The `SELECT` statement doesn't modify the queue and remove the messages. To read and pull the messages from the queue, you can use the [RECEIVE](../../t-sql/statements/receive-transact-sql.md) Transact-SQL statement.

```sql
RECEIVE TOP (1)
    conversation_handle,
    message_type_name,
    TRY_CAST (message_body AS NVARCHAR (MAX)) AS message_body_text
FROM dbo.ExpenseQueue;
GO
```

Once you process all messages from the queue, you should close the conversation using the [END CONVERSATION](../../t-sql/statements/end-conversation-transact-sql.md) Transact-SQL statement.

```sql
-- Drain any remaining target conversations for the from the queue
DECLARE @conversation_hdl AS UNIQUEIDENTIFIER;

WHILE EXISTS (SELECT 1 FROM dbo.ExpenseQueue)
    BEGIN
        RECEIVE TOP (1) @conversation_hdl = conversation_handle FROM dbo.ExpenseQueue;
        END CONVERSATION @conversation_hdl;
    END
GO
```

## Service Broker documentation

For more information about [!INCLUDE [ssSB](../../includes/sssb-md.md)], see:

- [Data Definition Language statements](../../t-sql/statements/statements.md) for `CREATE`, `ALTER`, and `DROP` statements
- [Transact-SQL statements](../../t-sql/statements/statements.md)
- [Service Broker Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/service-broker-catalog-views-transact-sql.md)
- [Service Broker Related Dynamic Management Views (Transact-SQL)](../../relational-databases/system-dynamic-management-views/service-broker-related-dynamic-management-views-transact-sql.md)
- [ssbdiagnose utility (Service Broker)](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md)

You can also refer to the [previously published documentation](/previous-versions/sql/sql-server-2008-r2/bb522893(v=sql.105)) for [!INCLUDE [ssSB](../../includes/sssb-md.md)] concepts and for development and management tasks.

## What's new in Service Broker

### Service Broker and Azure SQL Managed Instance

Cross-instance Service Broker message exchange between instances of Azure SQL Managed Instance and message exchange between SQL Server and Azure SQL Manage Instance is currently in public preview:

- `CREATE ROUTE`: Port specified must be 4022. See [CREATE ROUTE (Transact-SQL)](../../t-sql/statements/create-route-transact-sql.md).
- `ALTER ROUTE`: Port specified must be 4022. See [ALTER ROUTE (Transact-SQL)](../../t-sql/statements/alter-route-transact-sql.md).

Transport security is supported, while dialog security is not:

- `CREATE REMOTE SERVICE BINDING` isn't supported.

Service Broker is enabled by default and can't be disabled. The following `ALTER DATABASE` options aren't supported:

- `ENABLE_BROKER`
- `DISABLE_BROKER`

No significant changes were introduced in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. The following changes were introduced in [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].

### Messages can be sent to multiple target services (multicast)

The syntax of the [SEND](../../t-sql/statements/send-transact-sql.md) statement was extended to enable multicast by supporting multiple conversation handles.

### Queues expose the message enqueued time

Queues have a new column, `message_enqueue_time`, that shows how long a message has been in the queue.

### Poison message handling can be disabled

The [CREATE QUEUE](../../t-sql/statements/create-queue-transact-sql.md) and [ALTER QUEUE](../../t-sql/statements/alter-queue-transact-sql.md) statements now have the ability to enable or disable poison message handling by adding the clause, `POISON_MESSAGE_HANDLING (STATUS = ON | OFF)`. The catalog view `sys.service_queues` now has the column `is_poison_message_handling_enabled` to indicate whether poison message is enabled or disabled.

### Availability group support in Service Broker

For more information, see [Service Broker with Always On Availability Groups (SQL Server)](../availability-groups/windows/service-broker-with-always-on-availability-groups-sql-server.md).

## Related content

- [Event notifications](../../relational-databases/service-broker/event-notifications.md)
- [Implement Event Notifications](../../relational-databases/service-broker/implement-event-notifications.md)
- [Configure Dialog Security for Event Notifications](../../relational-databases/service-broker/configure-dialog-security-for-event-notifications.md)
- [Get Information About Event Notifications](../../relational-databases/service-broker/get-information-about-event-notifications.md)
