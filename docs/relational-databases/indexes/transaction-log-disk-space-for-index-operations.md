---
title: "Transaction Log Disk Space for Index Operations"
description: Transaction Log Disk Space for Index Operations
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/22/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: best-practice
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "index disk space [SQL Server]"
  - "space [SQL Server], indexes"
  - "transaction logs [SQL Server], disk space"
  - "disk space [SQL Server], transaction logs"
  - "space [SQL Server], transaction logs"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Transaction log disk space for index operations

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Large-scale index operations can generate large data loads that can cause the transaction log to fill quickly. To make sure that the index operation can be rolled back, the transaction log can't be truncated until the index operation has completed; however, the log can be backed up during the index operation. Therefore, the transaction log must have sufficient room to store both the index operation transactions and any concurrent user transactions for the duration of the index operation.

This is true for both offline and online index operations. Because the underlying tables can't be accessed during an offline index operation, there might be few user transactions and the log might not grow as quickly. Online index operations don't prevent concurrent user activity, therefore, large-scale online index operations combined with significant concurrent user transactions can cause continuous growth of the transaction log without an option to truncate the log.

## Recommendations

When you run large-scale index operations, consider the following recommendations:

1. Make sure the transaction log is backed up and truncated before running large-scale index operations online, and that the log has sufficient space to store the projected index and user transactions.

1. Consider setting the `SORT_IN_TEMPDB` option to `ON` for the index operation. This separates the index transactions from the concurrent user transactions. The index transactions are stored in the `tempdb` transaction log, and the concurrent user transactions are stored in the transaction log of the user database. This allows for the transaction log of the user database to be truncated during the index operation if necessary. Additionally, if the `tempdb` log isn't on the same disk as the user database log, the two logs aren't competing for the same disk space.

   > [!NOTE]  
   > Verify that the `tempdb` database and transaction log have sufficient disk space to handle the index operation. The `tempdb` transaction log can't be truncated until the index operation is completed.

1. Use a database recovery model that allows for minimal logging of the index operation. This might reduce the size of the log and prevent the log from filling the log space.

1. Don't run the online index operation in an explicit transaction. The log isn't truncated until the explicit transaction ends.

## Related content

- [Disk space requirements for index DDL operations](disk-space-requirements-for-index-ddl-operations.md)
- [Index disk space example](index-disk-space-example.md)

