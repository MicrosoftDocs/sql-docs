---
title: "Database verification"
description: This article provides information on database verification for a ledger database.
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

# Database verification

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

Ledger provides a form of data integrity called *forward integrity*, which provides evidence of data tampering on data in your ledger tables. The database verification process takes as input one or more previously generated database digests. It then recomputes the hashes stored in the database ledger based on the current state of the ledger tables. If the computed hashes don't match the input digests, the verification fails. The failure indicates that the data has been tampered with. The verification process reports all inconsistencies that it detects.

## Database verification process

The verification process scans all ledger and history tables. It recomputes the SHA-256 hashes of their rows and compares them against the database digest files passed to the verification stored procedure. 

Because the ledger verification recomputes all of the hashes for transactions in the database, it can be a resource-intensive process for databases with large amounts of data. To reduce the cost of verification, the feature exposes options to verify individual ledger tables or only a subset of the ledger tables.

You accomplish database verification through two stored procedures, depending on whether you [use automatic digest storage](#database-verification-that-uses-automatic-digest-storage) or you [manually manage digests](#database-verification-that-uses-manual-digest-storage).

### Database verification that uses automatic digest storage

> [!NOTE]
> Database verification using automatic digest storage is currently available in Azure SQL Database, but not supported on SQL Server.

When you're using automatic digest storage for generating and storing database digests, the location of the digest storage is in the system catalog view [sys.database_ledger_digest_locations](../../system-catalog-views/sys-database-ledger-digest-locations-transact-sql.md) as JSON objects. Running database verification consists of executing the [sp_verify_database_ledger_from_digest_storage](../../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md) system stored procedure. Specify the JSON objects from the [sys.database_ledger_digest_locations](../../system-catalog-views/sys-database-ledger-digest-locations-transact-sql.md)  system catalog view where database digests are configured to be stored. 

When you use automatic digest storage, you can change storage locations throughout the lifecycle of the ledger tables.  For example, if you start by using Azure immutable storage to store your digest files, but later you want to use Azure Confidential Ledger instead, you can do so. This change in location is stored in [sys.database_ledger_digest_locations](../../system-catalog-views/sys-database-ledger-digest-locations-transact-sql.md). 

When you run ledger verification, inspect the location of **digest_locations** to ensure digests used in verification are retrieved from the locations you expect. You want to make sure that a privileged user hasn't changed locations of the digest storage to an unprotected storage location, such as Azure Storage, without a configured and locked immutability policy. 

To simplify running verification when you use multiple digest storage locations, the following script will fetch the locations of the digests and execute verification by using those locations.

```sql
DECLARE @digest_locations NVARCHAR(MAX) = (SELECT * FROM sys.database_ledger_digest_locations FOR JSON AUTO, INCLUDE_NULL_VALUES);
SELECT @digest_locations as digest_locations;
BEGIN TRY
    EXEC sys.sp_verify_database_ledger_from_digest_storage @digest_locations;
    SELECT 'Ledger verification succeeded.' AS Result;
END TRY
BEGIN CATCH
    THROW;
END CATCH
```

### Database verification that uses manual digest storage

When you're using manual digest storage for generating and storing database digests, the stored procedure [sp_verify_database_ledger](../../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md) is used to verify the ledger database. The JSON content of the digest is appended in the stored procedure. When you're running database verification, you can choose to verify all tables in the database or verify specific tables. 

The following code is an example of running the [sp_verify_database_ledger](../../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md) stored procedure by passing two digests for verification: 

```sql
EXECUTE sp_verify_database_ledger N'
[
    {
        "database_name":  "ledgerdb",
        "block_id":  0,
        "hash":  "0xDC160697D823C51377F97020796486A59047EBDBF77C3E8F94EEE0FFF7B38A6A",
        "last_transaction_commit_time":  "2020-11-12T18:01:56.6200000",
        "digest_time":  "2020-11-12T18:39:27.7385724"
    },
    {
        "database_name":  "ledgerdb",
        "block_id":  1,
        "hash":  "0xE5BE97FDFFA4A16ADF7301C8B2BEBC4BAE5895CD76785D699B815ED2653D9EF8",
        "last_transaction_commit_time":  "2020-11-12T18:39:35.6633333",
        "digest_time":  "2020-11-12T18:43:30.4701575"
    }
]';
```

Return codes for `sp_verify_database_ledger` and `sp_verify_database_ledger_from_digest_storage` are `0` (success) or `1` (failure).

### Recommendation

Ideally, you want to minimize or even eliminate the gap between the time the attack occurred and the time it was detected. Microsoft recommends scheduling the ledger verification] regularly to avoid a restore of the database from days or months ago after [tampering was detected](ledger-how-to-recover-after-tampering.md). The interval of the verification should be decided by the customer, but be aware that ledger verification can be resource consuming. We recommend running this during a maintenance window or off peak hours.

Scheduling database verification can be done with Elastic Jobs or Azure Automation.

### Permissions
Database verification requires the `VIEW LEDGER CONTENT` permission. For details on permissions related to ledger tables, see [Permissions](../permissions-database-engine.md).

## Next steps

- [Ledger overview](ledger-overview.md)
- [Verify a ledger table to detect tampering](ledger-verify-database.md)
- [sys.database_ledger_digest_locations](../../system-catalog-views/sys-database-ledger-digest-locations-transact-sql.md)
- [sp_verify_database_ledger_from_digest_storage](../../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)
- [sp_verify_database_ledger](../../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md)
