---
title: "Recover ledger database after tampering"
description: This article discusses how to recover a database after discovering that it's been tampered with using the ledger feature.
ms.date: "05/24/2022"
ms.service: sql-database
ms.subservice: security
ms.custom:
- event-tier1-build-2022
ms.reviewer: kendralittle, mathoma
ms.topic: conceptual
author: VanMSFT
ms.author: vanto
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
---

# Recover ledger database after tampering

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

## How to recover after tampering occurs?

The most straightforward way to repair any kind of tampering is to restore the database to the latest backup that can be verified. To achieve that, you can restore the database to different points in time and verify the ledger using earlier database digests. The latest point in time that can be verified successfully is the one that is guaranteed to not be tampered with and can be used to continue transactions processing. For this reason, it's critical for backups to be frequent enough to get as close as possible to the time of tampering. Backup scheduling is [automatically done for Azure SQL Database](/azure/azure-sql/database/automated-backups-overview). Although this technique is simple, it has an important caveat: if any transactions were executed after the tampering occurred, you need to accept that these transactions will be lost or they need to manually repair the ledger table by reinserting the information for the verified transactions and recomputing the hashes for these new transactions that occurred after the first tampering event in the [database ledger](ledger-database-ledger.md).

## Tampering categories

Depending on the type of tampering, there are cases where you can repair the ledger without losing data. You should consider two categories of tampering events.

### Tampering didn't affect further transactions

The tampering event modified some data stored in the ledger but didn't affect any further transactions. This might be because the attack was detected before any transactions would operate over the tampered data or because the attack only affected data in a way that doesn't affect new transactions. For example, if you use a ledger table to store banking transaction details, tampering with details of existing transactions doesn't impact new transactions, which will work over the current balances.

Since the tampering didn't affect any transactions that occurred after the tampering event, the new transaction execution and generated results are correct. Based on that, you should ideally bring the ledger to a consistent state without affecting these transactions.

If the attacker didn't tamper with the database level ledger, this is easy to detect and repair. The database ledger is in a consistent state with all database digests generated, and any new transactions appended to it have been hashed using the valid hashes of earlier transactions. Based on that, any database digests that were generated, even for transactions after the tampering occurred, are still valid. You can attempt to retrieve the correct table ledger payload for the tampered transactions from backups that can still be validated to be secure (using the [ledger validation](ledger-verify-database.md) on them) and repair the operational database by overwriting the tampered data in the table ledger. This will create a new transaction recording the repairing transactions.

### Tampering affected data used by further transactions

The tampering event affected data that was later used by further transactions, therefore affecting their execution. For example, in a banking application where the current account balances are stored in a ledger table, modifying the current state of the table can be disastrous for further transactions since it can allow new transactions to overspend.

If the attacker tampered with the database ledger, recomputing the hashes of blocks to make it internally consistent (until verified against external database digests), then new transactions and database digests will be generated over invalid hashes. This leads to a fork in the ledger, since the new database digests generated map to an invalid state and even if you repair the ledger by using earlier backups, all these database digests are permanently invalid. Additionally, since the database ledger is broken, you can't trust the details about transactions that occurred after tampering until you verify them. Based on that, the tampering can be potentially reverted by:

1. Using backups to restore the state for the tampered transactions.
1. [Verifying](ledger-verify-database.md) the portion of the ledger after the last transaction recovered by the backup and until the end of the ledger. For this, you have to use the database digests from the forked part of the chain. Although the database digests don't match the original part of the ledger, it can still verify the forked portion of the ledger hasn't been tampered with. If these also indicate tampering, this means that there have been more than one tampering events and the process needs to be applied recursively to recover the different portions of the ledger from backups.
1. Manually repair the table ledgers by reinserting the information for the verified transactions and recomputing the hashes for these new transactions that occurred after the first tampering event in the database ledger.

## See also

- [Database ledger](ledger-database-ledger.md)
- [Verify a ledger table to detect tampering](ledger-verify-database.md)
- [sys.database_ledger_digest_locations](../../system-catalog-views/sys-database-ledger-digest-locations-transact-sql.md)
- [sp_verify_database_ledger_from_digest_storage](../../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)
- [sp_verify_database_ledger](../../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md)