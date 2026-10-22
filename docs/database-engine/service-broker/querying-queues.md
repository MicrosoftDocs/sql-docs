---
title: Query Queues
description: "Sometimes it may be necessary to inspect the content of a queue as a whole."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Query queues

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Sometimes it might be necessary to inspect the content of a queue as a whole. You might want to know how many messages the queue contains, or you might want to ensure that the application has processed all of the messages for a service that you're about to take offline. You might need to find out why messages aren't being processed by an application.

To get this information, use the name of the queue as the source table of a `SELECT` statement. A `SELECT` statement on a queue has the same format as a `SELECT` statement on a view or a table.

> [!NOTE]  
> Service Broker is designed to allow multiple queue readers to efficiently receive messages from a queue. However, a `SELECT` statement on a queue might cause blocking. When using a `SELECT` statement on a queue, specify the `NOLOCK` hint to avoid blocking applications that use the queue. For a description of the columns in a queue, see [CREATE QUEUE](../../t-sql/statements/create-queue-transact-sql.md).

Following is an example `SELECT` statement to find out the number of messages in the queue **ExpenseQueue**:

```sql
SELECT COUNT(*)
FROM dbo.ExpenseQueue WITH (NOLOCK);
```

The following `SELECT` statement lets the administrator learn whether the queue **ExpenseQueue** contains any messages for the service **//Adventure-Works.com/AccountsPayable/Expenses**:

```sql
IF EXISTS (SELECT *
           FROM dbo.ExpenseQueue WITH (NOLOCK)
           WHERE service_name = '//Adventure-Works.com/AccountsPayable/Expenses')
    PRINT 'The queue contains messages for ' + '//Adventure-Works.com/AccountsPayable/Expenses';
ELSE
    PRINT 'The queue does not contain messages for ' + '//Adventure-Works.com/AccountsPayable/Expenses';
```

Service Broker manages updates to queues. Although the name of a queue can be used in place of a table name in a `SELECT` statement, a queue can't be the target of an `INSERT`, `UPDATE`, `DELETE`, or `TRUNCATE` statement. SQL Server doesn't allow users to create indexes on queues.

## Related content

- [SELECT (Transact-SQL)](../../t-sql/queries/select-transact-sql.md)
- [Queues](queues.md)
- [Message retention](message-retention.md)
