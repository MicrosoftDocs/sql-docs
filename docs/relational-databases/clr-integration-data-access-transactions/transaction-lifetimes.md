---
title: "Transaction Lifetimes"
description: Learn about transaction lifetimes in SQL Server CLR integration. Transactions started in Transact-SQL stored procedures differ from ones started in managed code.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "lifetimes [SQL Server]"
  - "Transact-SQL vs. managed code"
---
# Transaction lifetimes

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

There's an important difference between transactions started in [!INCLUDE [tsql](../../includes/tsql-md.md)] stored procedures, and transactions started in managed code: common language runtime (CLR) code can't unbalance the transaction state on entry or exit of a CLR invocation. Be aware of the following implications of this difference:

- A transaction started inside a CLR frame must be committed or rolled back, or else [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] generates an error when the frame is exited.

- An outer transaction can't be committed or rolled back inside the CLR code.

- An attempt to commit a transaction not started in the same procedure causes a run-time error.

- An attempt to roll back a transaction not started in the same procedure causes the transaction to stop responding (preventing any other side-effecting operation from happening). The transaction discontinues until the CLR code goes out of scope. This behavior can be useful when you detect an error inside your procedure and want to make sure the whole transaction terminates.

## Related content

- [CLR integration and transactions](clr-integration-and-transactions.md)
