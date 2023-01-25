---
title: "Digest management"
description: This article provides information on digest management for a ledger database.
ms.date: 07/25/2022
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

# Digest management

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

## Database digests

The hash of the latest block in the database ledger is called the *database digest*. It represents the state of all ledger tables in the database at the time when the block was generated. Generating a database digest is efficient, because it involves computing only the hashes of the blocks that were recently appended. 

Database digests can be generated either automatically by the system or manually by the user. You can use them later to verify the integrity of the database. 

Database digests are generated in the form of a JSON document that contains the hash of the latest block, together with metadata for the block ID. The metadata includes the time that the digest was generated and the commit time stamp of the last transaction in this block.

The verification process and the integrity of the database depend on the integrity of the input digests. For this purpose, database digests that are extracted from the database need to be stored in trusted storage that the high-privileged users or attackers of the database can't tamper with.

### Automatic generation and storage of database digests

> [!NOTE]
> Automatic generation and storage of database digests in SQL Server only supports Azure Storage accounts.

Ledger integrates with the [immutable storage feature of Azure Blob Storage](/azure/storage/blobs/immutable-storage-overview) and [Azure Confidential Ledger](/azure/confidential-ledger/index). This integration provides secure storage services in Azure to help protect the database digests from potential tampering. This integration provides a simple and cost-effective way for users to automate digest management without having to worry about their availability and geographic replication.  Azure Confidential Ledger has a stronger integrity guarantee for customers who might be concerned about privileged administrators access to the digest. [This table](/azure/architecture/guide/technology-choices/multiparty-computing-service#confidential-ledger-and-azure-blob-storage) compares the immutable storage feature of Azure Blob Storage with Azure Confidential Ledger.  

You can configure automatic generation and storage of database digests through the Azure portal, PowerShell, or the Azure CLI. For more information, see [Enable automatic digest storage](ledger-how-to-enable-automatic-digest-storage.md). When you configure automatic generation and storage, database digests are generated on a predefined interval of 30 seconds and uploaded to the selected storage service. If no transactions occur on the system in the 30-second interval, a database digest won't be generated and uploaded. This mechanism ensures that database digests are generated only when data has been updated in your database. When the endpoint is an Azure Blob Storage, the Azure SQL database server will create a new container, named **sqldbledgerdigests** and use a naming pattern like:
ServerName/DatabaseName/CreationTime. The creation time is needed because a database with the same name can be dropped and recreated or restored, allowing for different "incarnations" of the database under the same name. See [Digest Management Considerations](ledger-digest-management.md).

> [!NOTE]
> For SQL Server, the container needs to be created manually by the user.

#### Azure Storage Account Immutability Policy

If you use an Azure Storage account for the storage of the database digests, configure an [immutability policy](/azure/storage/blobs/immutable-policy-configure-version-scope) on your container after provisioning to ensure that database digests are protected from tampering. Make sure the immutability policy allows protected append writes to append blobs and that the policy is locked.

#### Azure Storage account permission

If you use **Azure SQL Database**, make sure that your logical server (System Identity) has sufficient RBAC permissions to write digests by adding it to the [Storage Blob Data Contributor](/azure/role-based-access-control/built-in-roles#storage-blob-data-contributor) role.

If you use **SQL Server**, you have to create a shared access signature (SAS) on the digest container to allow SQL Server to connect and authenticate against the Azure Storage account.

- Create a container on the Azure Storage account, named **sqldbledgerdigests**.
- Create a policy on a container with the *Read*, *Add*, *Create*, *Write*, and *List* permissions, and generate a shared access signature key.
- For each container used for digest file storage, create a [SQL Server credential](../authentication-access/credentials-database-engine.md) whose name matches the container path.

The following example assumes that an Azure Storage container, a policy, and a SAS key have been created. This is needed by SQL Server to access the digest files in the container.

In the following code snippet, replace `<your SAS key>` with the SAS key. The SAS key will look like `'sr=c&si=<MYPOLICYNAME>&sig=<THESHAREDACCESSSIGNATURE>'`.

```sql
CREATE CREDENTIAL [https://ledgerstorage.blob.core.windows.net/sqldbledgerdigests]  
WITH IDENTITY='SHARED ACCESS SIGNATURE',  
SECRET = '<your SAS key>'   
```  

#### Azure Confidential Ledger Permission

If you use **Azure SQL Database**, make sure that your logical server (System Identity) has sufficient RBAC permissions to write digests by adding it to the **Contributor** role.

> [!NOTE]
> Automatic generation and storage of database digests in SQL Server only supports Azure Storage accounts.

### Manual generation and storage of database digests

You can also generate a database digest on demand so that you can manually store the digest in any service or device that you consider a trusted storage destination. For example, you might choose an on-premises write once, read many (WORM) device as a destination. You manually generate a database digest by running the [sys.sp_generate_database_ledger_digest](../../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md) stored procedure in either [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

```sql
EXECUTE sp_generate_database_ledger_digest;
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

#### Permissions
Generating database digests requires the `GENERATE LEDGER DIGEST` permission. For details on permissions related to ledger tables, see [Permissions](../permissions-database-engine.md). 
> 
## Digest management considerations

### Database restore

Restoring the database back to an earlier point in time, also known as [Point in Time Restore](/azure/azure-sql/database/recovery-using-backups#point-in-time-restore), is an operation frequently used when a mistake occurs and users need to quickly revert the state of the database back to an earlier point in time. When uploading the generated digests to Azure Storage or Azure Confidential Ledger, the *create time* of the database is captured that these digests map to. Every time the database is restored, it's tagged with a new *create time* and this technique allows us to store the digests across different "incarnations" of the database. For SQL Server, the *create time* is the current UTC time when the digest upload is enabled for the first time. Ledger preserves the information regarding when a restore operation occurred, allowing the verification process to use all the relevant digests across the various incarnations of the database. Additionally, users can inspect all digests for different create times to identify when the database was restored and how far back it was restored to. Since this data is written in immutable storage, this information will be protected as well.

### Active geo-replication and Always On Availability Groups

Active geo-replication can be configured for an Azure SQL Database. Replication across geographic regions is asynchronous for performance reasons and, thus, allows the secondary database to be slightly behind compared to the primary. In the event of a geographic failover, any latest data that hasn't yet been replicated is lost. Ledger will only issue database digests for data that has been replicated to geographic secondaries to guarantee that digests will never reference data that might be lost in case of a geographic failover. This only applies for automatic generation and storage of database digests.

Dropping the link between the primary and the secondaries when ledger digests are configured isn't supported. You should first disable the *Enable automatic digest storage* database setting, remove the synchronization between the primary and the secondary and re-enable the *Enable automatic digest storage* database setting.

When your database is part of an Always On Availability Group in SQL Server, the same principle as active geo-replication is used. The upload of the digests is only done if all transactions have been replicated to the secondary replicas.

## Next steps

- [Ledger overview](ledger-overview.md)
- [Enable automatic digest storage](ledger-how-to-enable-automatic-digest-storage.md)
- [sys.sp_generate_database_ledger_digest](../../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md)
