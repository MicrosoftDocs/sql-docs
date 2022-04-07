---
title: "Digest management"
description: This article provides information on digest management for a ledger database in Azure SQL Database.
ms.date: "04/05/2022"
ms.service: sql-database
ms.subservice: security
ms.reviewer: kendralittle, mathoma
ms.topic: conceptual
author: VanMSFT
ms.author: vanto
---

# Digest management

[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]

> [!NOTE]
> Azure SQL Database ledger is currently in public preview.

## Database digests

The hash of the latest block in the database ledger is called the *database digest*. It represents the state of all ledger tables in the database at the time when the block was generated. Generating a database digest is efficient, because it involves computing only the hashes of the blocks that were recently appended. 

Database digests can be generated either automatically by the system or manually by the user. You can use them later to verify the integrity of the database. 

Database digests are generated in the form of a JSON document that contains the hash of the latest block, together with metadata for the block ID. The metadata includes the time that the digest was generated and the commit time stamp of the last transaction in this block.

The verification process and the integrity of the database depend on the integrity of the input digests. For this purpose, database digests that are extracted from the database need to be stored in trusted storage that the high-privileged users or attackers of the Azure SQL Database server can't tamper with.

### Automatic generation and storage of database digests

Azure SQL Database ledger integrates with the [immutable storage feature of Azure Blob Storage](../../storage/blobs/immutable-storage-overview.md) and [Azure Confidential Ledger](../../confidential-ledger/index.yml). This integration provides secure storage services in Azure to help protect the database digests from potential tampering. This integration provides a simple and cost-effective way for users to automate digest management without having to worry about their availability and geographic replication. 

You can configure automatic generation and storage of database digests through the Azure portal, PowerShell, or the Azure CLI. See [How to configure automatic database digests](ledger-how-to-configure-automatic-database-digest.md). When you configure automatic generation and storage, database digests are generated on a predefined interval of 30 seconds and uploaded to the selected storage service. If no transactions occur in the system in the 30-second interval, a database digest won't be generated and uploaded. This mechanism ensures that database digests are generated only when data has been updated in your database.



> [!IMPORTANT]
> Configure an [immutability policy](../../storage/blobs/immutable-policy-configure-version-scope.md) on your container after provisioning to ensure that database digests are protected from tampering.

### Manual generation and storage of database digests

You can also generate a database digest on demand so that you can manually store the digest in any service or device that you consider a trusted storage destination. For example, you might choose an on-premises write once, read many (WORM) device as a destination. You manually generate a database digest by running the [sys.sp_generate_database_ledger_digest](/sql/relational-databases/system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql) stored procedure in either [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms) or [Azure Data Studio](/sql/azure-data-studio/download-azure-data-studio).

> [!IMPORTANT]
> Generating database digests requires the **GENERATE LEDGER DIGEST** permission. For details on permissions related to ledger tables, see [Permissions](/sql/relational-databases/security/permissions-database-engine#asdbpermissions). 

```sql
EXECUTE sp_generate_database_ledger_digest
```

The returned result set is a single row of data. It should be saved to the trusted storage location as a JSON document as follows:

```json
    {
        "database_name":  "ledgerdb",
        "block_id":  0,
        "hash":  "0xDC160697D823C51377F97020796486A59047EBDBF77C3E8F94EEE0FFF7B38A6A",
        "last_transaction_commit_time":  "2020-11-12T18:01:56.6200000",
        "digest_time":  "2020-11-12T18:39:27.7385724"
    }
```

## Next steps

- [Azure SQL Database ledger overview](ledger-overview.md)
- [Updatable ledger tables](ledger-updatable-ledger-tables.md)   
- [Append-only ledger tables](ledger-append-only-ledger-tables.md)   
- [Database ledger](ledger-database-ledger.md)