---
title: "CLR Integration and Transactions"
description: For CLR integration and transactions, System.Transactions and ADO.NET work together to extend and simplify the use of local and distributed transactions in managed applications.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "ADO.NET [CLR integration]"
  - "common language runtime [SQL Server], ADO.NET"
  - "managed code [SQL Server], transactions"
  - "common language runtime [SQL Server], transactions"
  - "System.Transactions namespace"
  - "transactions [CLR integration]"
---
# CLR integration and transactions

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `System.Transactions` namespace provides a transaction framework that is fully integrated with ADO.NET and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] common language runtime (CLR) integration. `System.Transactions` and ADO.NET work together to extend and simplify the use of local and distributed transactions in managed applications.

> [!NOTE]  
> A CLR user-defined procedure (UDP) can't establish a connection to the same server it's running on (a loopback connection) and enlist in the same transaction. If this is attempted, the connection attempt will be blocked and control will not be passed back to the UDP. This will result in a timeout error (Msg 1206) on the UDP.

For more information about transactions and the .NET Framework, see [Transaction Processing](/dotnet/framework/data/transactions/).

## In this section

| Article | Description |
| --- | --- |
| [Transaction promotion](transaction-promotion.md) | Describes the ability to promote transactions, and how to use this feature. |
| [Access the current transaction](accessing-the-current-transaction.md) | Describes how to access a transaction currently running in-process on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Use System.Transactions](using-system-transactions.md) | Describes how to use the `System.Transactions` application programming interface (API) in your managed application. |
| [Transaction lifetimes](transaction-lifetimes.md) | Describes the difference in lifetime between transactions started in [!INCLUDE [tsql](../../includes/tsql-md.md)] stored procedures and transactions started in CLR applications. |

## Related content

- [Data access from CLR database objects](../clr-integration/data-access/data-access-from-clr-database-objects.md)
