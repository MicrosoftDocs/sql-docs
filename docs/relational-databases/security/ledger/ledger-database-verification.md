---
title: "Database verification"
description: This article provides information on database verification for a ledger database.
ms.date: "05/24/2022"
ms.service: sql-database
ms.subservice: security
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

For large ledger tables, database verification can be a resource-intensive process. You should use it only when you need to verify the integrity of a database. 

The verification process can be executed hourly or daily for cases where the integrity of the database needs to be frequently monitored. Or it can be executed only when the organization that's hosting the data goes through an audit and needs to provide cryptographic evidence about the integrity of the data. To reduce the cost of verification, ledger exposes options to verify individual ledger tables or only a subset of the ledger tables. 

You accomplish database verification through two stored procedures, depending on whether you [use automatic digest storage](#database-verification-that-uses-automatic-digest-storage) or you [manually manage digests](#database-verification-that-uses-manual-digest-storage).

> [!IMPORTANT]
> Database verification requires the *View Ledger Content* permission. For details on permissions related to ledger tables, see [Permissions](/sql/relational-databases/security/permissions-database-engine#asdbpermissions).

### Database verification that uses automatic digest storage

> [!NOTE]
> Database verification using automatic digest storage is currently available in Azure SQL Database, but not supported on SQL Server.

When you're using automatic digest storage for generating and storing database digests, the location of the digest storage is in the system catalog view [sys.database_ledger_digest_locations](/sql/relational-databases/system-catalog-views/sys-database-ledger-digest-locations-transact-sql) as JSON objects. Running database verification consists of executing the [sp_verify_database_ledger_from_digest_storage](/sql/relational-databases/system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql) system stored procedure. Specify the JSON objects from the [sys.database_ledger_digest_locations](/sql/relational-databases/system-catalog-views/sys-database-ledger-digest-locations-transact-sql)  system catalog view where database digests are configured to be stored. 

When you use automatic digest storage, you can change storage locations throughout the lifecycle of the ledger tables.  For example, if you start by using Azure immutable storage to store your digest files, but later you want to use Azure Confidential Ledger instead, you can do so. This change in location is stored in [sys.database_ledger_digest_locations](/sql/relational-databases/system-catalog-views/sys-database-ledger-digest-locations-transact-sql). 

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

When you're using manual digest storage for generating and storing database digests, the stored procedure [sp_verify_database_ledger](/sql/relational-databases/system-stored-procedures/sys-sp-verify-database-ledger-transact-sql) is used to verify the ledger database. The JSON content of the digest is appended in the stored procedure. When you're running database verification, you can choose to verify all tables in the database or verify specific tables. 

The following code is an example of running the [sp_verify_database_ledger](/sql/relational-databases/system-stored-procedures/sys-sp-verify-database-ledger-transact-sql) stored procedure by passing two digests for verification: 

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

## Next steps

- [Ledger overview](ledger-overview.md)
- [Verify a ledger table to detect tampering](ledger-verify-database.md)
- [sys.database_ledger_digest_locations](/sql/relational-databases/system-catalog-views/sys-database-ledger-digest-locations-transact-sql)
- [sp_verify_database_ledger_from_digest_storage](/sql/relational-databases/system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql)
- [sp_verify_database_ledger](/sql/relational-databases/system-stored-procedures/sys-sp-verify-database-ledger-transact-sql)
