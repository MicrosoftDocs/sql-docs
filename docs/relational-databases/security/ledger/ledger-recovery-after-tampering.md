---
title: "Recover ledger database after tampering"
description: This article discusses how to recover a database after discovering that it's been tampered with using the ledger feature.
ms.date: "04/05/2022"
ms.service: sql-database
ms.subservice: security
ms.reviewer: kendralittle, mathoma
ms.topic: conceptual
author: VanMSFT
ms.author: vanto
---

# Recover ledger database after tampering

[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]

> [!NOTE]
> Azure SQL Database ledger is currently in public preview.

## How to recover after tampering occurs?

The most straightforward way to repair any kind of tampering is to restore the database to the latest backup that can be verified. To achieve that, users can [**restore**](/azure/azure-sql/database/recovery-using-backups) the database to different points in time and verify the ledger using earlier receipts. The latest point in time that can be verified successfully is the one that is guaranteed to not be tampered with and can be used to continue transactions processing. For this reason, it is critical for backups to be frequent enough to get as close as possible to the time of tampering. Backup scheduling is [automatically done for Azure SQL Database](/azure/azure-sql/database/automated-backups-overview). Although this technique is simple, it has an important caveat: if any transactions were executed after the tampering occurred, users need to accept that these transactions will be lost or they need to manually repair the ledger table by re-inserting the information for the verified transactions and recomputing the hashes for these new transactions that occurred after the first tampering event in the [database ledger](ledger-database-ledger.md). 

## Tampering categories

Depending on the type of tampering, there are cases where we can repair the ledger without losing data. We should consider two categories of tampering events.

### Tampering did not affect further transactions

The tampering event modified some data stored in the ledger but did not affect any further transactions. This might be because the attack was detected before any transactions would operate over the tampered data or because the attack only affected data in a way that does not affect new transactions. For example, if we use a ledger table to store banking transaction details, tampering with details of existing transactions does not impact new transactions, which will work over the current balances.

Since the tampering did not affect any transactions that occurred after the tampering event, the new transaction execution and generated results are correct. Based on that, we should ideally bring the ledger to a consistent state without affecting these transactions. 

If the attacker did not tamper with the database level ledger, this is easy to detect and repair. The database ledger is in a consistent state with all receipts generated and any new transactions appended to it have been hashed using the valid hashes of earlier transactions. Based on that, any receipts that were generated, even for transactions after the tampering occurred, are still valid. The users can attempt to retrieve the correct table ledger payload for the tampered transactions from backups that can still be validated to be secure (using the [ledger validation](ledger-verify-database.md) on them) and repair the operational database by overwriting the tampered data in the table ledger. This will create a new transaction recording the repairing transactions.

### Tampering affected data used by further transactions

The tampering event affected data that was later used by further transactions, therefore affecting their execution. For example, in a banking application where the current account balances are stored in a ledger-enabled table, modifying the current state of the table can be disastrous for further transactions since it can allow new transactions to overspend.

If the attacker tampered with the database ledger, recomputing the hashes of blocks to make it internally consistent (until verified against external receipts), then new transactions and receipts will be generated over invalid hashes. This leads to a fork in the ledger, since the new receipts generated map to an invalid state and even if the users repair the ledger by leveraging earlier backups, all these receipts are permanently invalid. Additionally, since the database ledger is broken, we can’t trust the details about transactions that occurred after tampering until we verify them. Based on that, the tampering can be potentially reverted by:
    
1. Leveraging [backups to restore](/azure/azure-sql/database/recovery-using-backups) the state for the tampered transactions.
1. [Verifying](ledger-verify-database.md) the portion of the ledger after the last transaction recovered by the backup and until the end of the ledger. For this, we have to use the receipts from the forked part of the chain. Although the receipts don’t match the original part of the ledger, it can still verify this later portion of the ledger has not been tampered with. If these also indicate tampering, this means that there have been more than one tampering events and the process needs to be applied recursively to recover the different portions of the ledger from backups. 
1. Manually repair the table ledgers by re-inserting the information for the verified transactions and recomputing the hashes for these new transactions that occurred after the first tampering event in the database ledger.
 
## Recommendation

Ideally, you want to minimize or even eliminate the gap between the time the attack occurred and the time it was detected. Microsoft recommends to schedule the [ledger verification](ledger-database-verification.md) on a regular basis to avoid a restore of the database from days or months ago after tampering was detected. The interval of the verification should be decided by the customer but be aware that ledger verification can be resource consuming and we advise to run this during a maintenance window or off peak hours.

Scheduling database verification can be done with Elastic Jobs or Azure Automation.

## See also

- [Database ledger](ledger-database-ledger.md) 
- [Digest management](ledger-digest-management.md)
- [Database verification](ledger-database-verification.md)
- [Append-only ledger tables](ledger-append-only-ledger-tables.md) 
- [Updatable ledger tables](ledger-updatable-ledger-tables.md)
- [Create and use updatable ledger tables](ledger-how-to-updatable-ledger-tables.md)
- [Access the digests stored in Azure Confidential Ledger (ACL)](ledger-how-to-access-acl-digest.md)
- [Verify a ledger table to detect tampering](ledger-verify-database.md)
