---
title: Handling Transact-SQL Errors (Service Broker)
description: "Two general principles apply when handling Transact-SQL errors in a Service Broker application."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Handling Transact-SQL Errors (Service Broker)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Two general principles apply when handling Transact-SQL errors in a Service Broker application.

First, an application should not permanently remove a message from a queue without acting upon the message. In most cases, this means that an application should always receive a message within a transaction.

Second, an application should always hold a lock on a conversation group before updating the state of the conversation group or the state of any message in the conversation group. When an application receives a message within a transaction, the application automatically locks the conversation group.

## See also

- [SAVE TRANSACTION (Transact-SQL)](../../t-sql/language-elements/save-transaction-transact-sql.md)
- [Understanding Database Engine Errors](../../relational-databases/errors-events/understanding-database-engine-errors.md)