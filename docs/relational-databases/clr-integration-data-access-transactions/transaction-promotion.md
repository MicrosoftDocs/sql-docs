---
title: "Transaction Promotion"
description: In SQL Server CLR integration, a lightweight local transaction can be promoted to a fully distributable transaction through Transaction promotion.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "distributed transactions [CLR integration]"
  - "promoting transactions [CLR integration]"
  - "Enlist keyword"
  - "transaction promotion [CLR integration]"
ms.custom: sfi-ropc-nochange
---
# Transaction promotion

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Transaction *promotion* describes a lightweight, local transaction that can be automatically promoted to a fully distributable transaction as needed. When a managed stored procedure is invoked within a database transaction on the server, the common language runtime (CLR) code is run in the context of a local transaction. If you open a connection to a remote server within a database transaction, the connection to the remote server is *enlisted* into the distributed transaction, and the local transaction is automatically promoted to a distributed transaction.

So, transaction promotion minimizes the overhead of distributed transactions by deferring the creation of a distributed transaction until it's needed. Transaction promotion is automatic if it was enabled using the `Enlist` keyword, and doesn't require intervention from the developer. The .NET Framework Data Provider for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides support for transaction promotion, handled through the classes in the .NET Framework `System.Data.SqlClient` namespace.

## The Enlist keyword

The `ConnectionString` property of a `SqlConnection` object supports the `Enlist` keyword, which indicates whether `System.Data.SqlClient` detects transactional contexts and automatically enlists the connection in a distributed transaction. If this keyword is set to true (the default), the connection is automatically enlisted in the current transaction context of the opening thread. If this keyword is set to false, the `SqlClient` connection doesn't interact with a distributed transaction. If `Enlist` isn't specified in the connection string, the connection is automatically enlisted in a distributed transaction if one is detected at the time the connection is opened.

## Distributed transactions

Distributed transactions typically consume significant system resources. [!INCLUDE [msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) manages such transactions, and integrates all of the resource managers accessed in these transactions. Transaction promotion, on the other hand, is a special form of a `System.Transactions` transaction that effectively delegates the work to a simple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] transaction. `System.Transactions`, `System.Data.SqlClient`, and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] coordinate the work involved in handling the transaction, promoting it to a full distributed transaction as needed.

The benefit of using transaction promotion is that when a connection is opened with an active `TransactionScope` transaction, and no other connections are opened, the transaction commits as a lightweight transaction, rather than incurring the extra overhead of a full distributed transaction. For more information about `TransactionScope`, see [Use System.Transactions](using-system-transactions.md).

## Related content

- [CLR integration and transactions](clr-integration-and-transactions.md)
