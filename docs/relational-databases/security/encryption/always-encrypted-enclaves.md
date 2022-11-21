---
title: "Always Encrypted with secure enclaves"
description: Learn about the Always Encrypted with secure enclaves feature for SQL Server. 
ms.custom:
- seo-lt-2019
- event-tier1-build-2022
ms.date: 10/25/2022
ms.service: sql
ms.reviewer: "vanto"
ms.subservice: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15"
---
# Always Encrypted with secure enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

Always Encrypted with secure enclaves expands confidential computing capabilities of [Always Encrypted](always-encrypted-database-engine.md) by enabling in-place encryption and richer confidential queries. Always Encrypted with secure enclaves is available in [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] and later, as well as in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)].

Introduced in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] in 2015 and in [!INCLUDE[sssql16](../../../includes/sssql16-md.md)], Always Encrypted protects the confidentiality of sensitive data from malware and high-privileged *unauthorized* users: Database Administrators (DBAs), computer admins, cloud admins, or anyone else who has legitimate access to server instances, hardware, etc., but shouldn't have access to some or all of the actual data.

Without the enhancements discussed in this article, Always Encrypted protects the data by encrypting it on the client side and *never* allowing the data or the corresponding cryptographic keys to appear in plaintext inside the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)]. As a result, the functionality on encrypted columns inside the database is severely restricted. The only operations the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] can perform on encrypted data are equality comparisons (only available with [deterministic encryption](always-encrypted-database-engine.md#selecting--deterministic-or-randomized-encryption)). All other operations, including cryptographic operations (initial data encryption or key rotation) and richer queries (for example, pattern matching) aren't supported inside the database. Users need to move their data outside of the database to perform these operations on the client-side.

Always Encrypted *with secure enclaves* addresses these limitations by allowing some computations on plaintext data inside a secure enclave on the server side. A secure enclave is a protected region of memory within the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] process. The secure enclave appears as an opaque box to the rest of the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] and other processes on the hosting machine. There's no way to view any data or code inside the enclave from the outside, even with a debugger. These properties make the secure enclave a *trusted execution environment* that can safely access cryptographic keys and sensitive data in plaintext, without compromising data confidentiality.

Always Encrypted uses secure enclaves as illustrated in the following diagram:

![data flow](./media/always-encrypted-enclaves/ae-data-flow.png)

When parsing a Transact-SQL statement submitted by an application, the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] determines if the statement contains any operations on encrypted data that require the use of the secure enclave. For such statements:

- The client driver sends the column encryption keys required for the operations to the secure enclave (over a secure channel), and submits the statement for execution.

- When processing the statement, the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] delegates cryptographic operations or computations on encrypted columns to the secure enclave. If needed, the enclave decrypts the data and performs computations on plaintext.

During statement processing, both the data and the column encryption keys aren't exposed in plaintext in the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] outside of the secure enclave.

## Supported enclave technologies

Always Encrypted uses one of the two enclave technologies, depending on the environment hosting your database:

- In [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] and later, Always Encrypted with secure enclaves uses [Virtualization-based Security (VBS)](https://www.microsoft.com/security/blog/2018/06/05/virtualization-based-security-vbs-memory-enclaves-data-protection-through-isolation/) secure memory enclaves (also known as Virtual Secure Mode, or VSM enclaves) - a software-based technology that relies on Windows hypervisor and doesn't require any special hardware.

- In [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], Always Encrypted with secure enclaves uses [Intel Software Guard Extensions (Intel SGX)](https://www.intel.com/content/www/us/en/architecture-and-technology/software-guard-extensions.html) enclaves - a hardware-based trusted execution environment technology. Intel SGX is available only in databases using the [DC-series](/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#dc-series) hardware configuration.

## Secure enclave attestation

Enclave attestation is a workflow that allows a client application to establish trust with a secure enclave for the database and the application it's connected to, before sharing cryptographic keys and using the enclave for processing sensitive data. The attestation workflow verifies the enclave is a genuine VBS or Intel SGX enclave and the code running inside it is the genuine Microsoft-signed enclave library for Always Encrypted. Enclave attestation can help detect attacks that involve tampering with the enclave code or its environment by malicious administrators.

To attest the enclave, both the client driver within the application and the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)], the application it's connected to, communicate with an external attestation service using a client-specified endpoint.

A valid attestation service depends on the enclave type and your database environment:

- VBS enclaves in [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] require [Windows Defender System Guard runtime attestation](https://www.microsoft.com/security/blog/2018/06/05/virtualization-based-security-vbs-memory-enclaves-data-protection-through-isolation/) using Host Guardian Service (HGS) as an attestation service. For more information, see [Plan for Host Guardian Service attestation](always-encrypted-enclaves-host-guardian-service-plan.md).
- Intel SGX enclaves in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] (DC-series databases) require [Microsoft Azure Attestation](/azure/attestation/overview). For more information, see [Plan for Intel SGX enclaves and attestation in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-plan).

## Supported client drivers

To use Always Encrypted with secure enclaves, an application must use a client driver that supports the feature. Configure the application and the client driver to enable enclave computations and enclave attestation. For details, including the list of supported client drivers, see [Develop applications using Always Encrypted](always-encrypted-client-development.md).

## Terminology

### Enclave-enabled keys

Always Encrypted with secure enclaves introduces the concept of enclave-enabled keys:

- **Enclave-enabled column master key** - a column master key that has the `ENCLAVE_COMPUTATIONS` property specified in the column master key metadata object inside the database. The column master key metadata object must also contain a valid signature of the metadata properties. For more information, see [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md)
- **Enclave-enabled column encryption key** - a column encryption key that is encrypted with an enclave-enabled column master key. Only enclave-enabled column encryption keys can be used for computations inside the secure enclave. 

For more information, see [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md).

### Enclave-enabled columns

An enclave-enabled column is a database column encrypted with an enclave-enabled column encryption key.

## Confidential computing capabilities for enclave-enabled columns

The two key benefits of Always Encrypted with secure enclaves are in-place encryption and rich confidential queries.

### In-place encryption

In-place encryption allows cryptographic operations on database columns inside the secure enclave, without moving the data outside of the database. In-place encryption improves the performance and the reliability of cryptographic operations. You can perform in-place encryption using the [ALTER TABLE (Transact-SQL)](../../../t-sql/statements/alter-table-transact-sql.md) statement. 

The cryptographic operations supported in-place are:

- Encrypting a plaintext column with an enclave-enabled column encryption key.
- Re-encrypting an encrypted enclave-enabled column to:
  - Rotate a column encryption key - re-encrypt the column with a new enclave-enabled column encryption key.
  - Change the encryption type of an enclave-enabled column, for example, from deterministic to randomized.
- Decrypting data stored in an enclave-enabled column (converting the column into a plaintext column).

In-place encryption is allowed with both deterministic and randomized encryption, as long as the column encryption keys involved in a cryptographic operation are enclave-enabled.

### Confidential queries

> [!NOTE]
> [!INCLUDE[sql-server-2022](../../../includes/sssql22-md.md)] adds additional support for confidential queries with JOIN, GROUP BY and ORDER BY operations on encrypted columns.

Confidential queries are [DML queries](../../../t-sql/queries/queries.md) that involve operations on enclave-enabled columns performed inside the secure enclave.

The operations supported inside the secure enclaves are:

| Operation| [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] | [!INCLUDE[sql-server-2022](../../../includes/sssql22-md.md)] | [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] | 
|:---|:---|:---| :---|
| [Comparison Operators](../../../mdx/comparison-operators.md) | Supported | Supported | Supported |
| [BETWEEN (Transact-SQL)](../../../t-sql/language-elements/between-transact-sql.md) | Supported | Supported | Supported |
| [IN (Transact-SQL)](../../../t-sql/language-elements/in-transact-sql.md) | Supported | Supported | Supported |
| [LIKE (Transact-SQL)](../../../t-sql/language-elements/like-transact-sql.md) | Supported | Supported | Supported |
| [DISTINCT](../../../t-sql/queries/select-transact-sql.md#c-using-distinct-with-select) | Supported | Supported | Supported |
| [Joins](../../performance/joins.md) | Supported | Supported | Only nested loop joins supported | 
| [SELECT - ORDER BY Clause (Transact-SQL)](../../../t-sql/queries/select-order-by-clause-transact-sql.md) | Supported | Supported | Not supported | 
| [SELECT - GROUP BY- Transact-SQL](../../../t-sql/queries/select-group-by-transact-sql.md) | Supported | Supported | Not supported |

> [!NOTE]
> The above operations inside secure enclaves require randomized encryption. Deterministic encryption is not supported. Equality comparison remains the operation available for columns using deterministic encryption.
>
> In [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] and in [!INCLUDE[sql-server-2022](../../../includes/sssql22-md.md)], confidential queries using enclaves on a character string column (`char`, `nchar`) require the column uses a [binary-code point (_BIN2) collation or a UTF-8 collation](../../../relational-databases/collations/collation-and-unicode-support.md). In [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)], a_BIN2 collation is required. 

For more information, see [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md).

### Indexes on enclave-enabled columns

You can create nonclustered indexes on enclave-enabled columns using randomized encryption to make confidential DML queries using the secure enclave run faster.

To ensure an index on a column that is encrypted using randomized encryption doesn't leak sensitive data, the key values in the index data structure (B-tree) are encrypted and sorted based on their plaintext values. Sorting by the plaintext value is also useful for processing queries inside the enclave. When the query executor in the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] uses an index on an encrypted column for computations inside the enclave, it searches the index to look up specific values stored in the column. Each search may involve multiple comparisons. The query executor delegates each comparison to the enclave, which decrypts a value stored in the column and the encrypted index key value to be compared, it performs the comparison on plaintext and it returns the result of the comparison to the executor.

Creating indexes on columns that use randomized encryption and aren't enclave-enabled remains unsupported.

An index on a column using deterministic encryption is sorted based on ciphertext (not plaintext), regardless if the column is enclave-enabled or not.

For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md). For general information on how indexing in [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] works, see the article, [Clustered and Nonclustered Indexes Described](../../indexes/clustered-and-nonclustered-indexes-described.md).

### Database recovery

If an instance of SQL Server fails, its databases may be left in a state where the data files may contain some modifications from incomplete transactions. When the instance is started, it runs a process called [database recovery](../../logs/the-transaction-log-sql-server.md#recovery-of-all-incomplete-transactions-when--is-started), which involves rolling back every incomplete transaction found in the transaction log to make sure the integrity of the database is preserved. If an incomplete transaction made any changes to an index, those changes also need to be undone. For example, some key values in the index may need to be removed or reinserted.

> [!IMPORTANT]
> Microsoft strongly recommends enabling [Accelerated database recovery (ADR)](../../backup-restore/restore-and-recovery-overview-sql-server.md#adr) for your database, **before** creating the first index on an enclave-enabled column encrypted with randomized encryption. ADR is enabled by default in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], but not in [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] and later.

With the [traditional database recovery process](/azure/sql-database/sql-database-accelerated-database-recovery#the-current-database-recovery-process) (that follows the [ARIES](https://people.eecs.berkeley.edu/~brewer/cs262/Aries.pdf) recovery model), to undo a change to an index, SQL Server needs to wait until an application provides the column encryption key for the column to the enclave, which can take a long time. Accelerated database recovery (ADR) dramatically reduces the number of undo operations that must be deferred because a column encryption key isn't available in the cache inside the enclave. Consequently, it substantially increases the database availability by minimizing a chance for a new transaction to get blocked. With ADR enabled, SQL Server still may need a column encryption key to complete cleaning up old data versions but it does that as a background task that doesn't impact the availability of the database or user transactions. You may see error messages in the error log, indicating failed cleanup operations due to a missing column encryption key.

## Security considerations

The following security considerations apply to Always Encrypted with secure enclaves.

- The security of your data inside the enclave depends on an attestation protocol and an attestation service. Therefore, you need to ensure the attestation service and attestation policies, the attestation service enforces, are managed by a trusted administrator. Also, attestation services typically support different policies and attestation protocols, some of which perform minimal verification of the enclave and its environment, and are designed for testing and development. Closely follow the guidelines specific to your attestation service to ensure you're using the recommended configurations and policies for your production deployments.
- Encrypting a column using randomized encryption with an enclave-enabled column encryption key may result in leaking the order of data stored in the column, as such columns support range comparisons. For example, if an encrypted column, containing employee salaries, has an index, a malicious DBA could scan the index to find the maximum encrypted salary value and identify a person with the maximum salary (assuming the name of the person isn't encrypted).
- If you use Always Encrypted to protect sensitive data from unauthorized access by DBAs, don't share the column master keys or column encryption keys with the DBAs. A DBA can manage indexes on encrypted columns without having direct access to the keys by using the cache of column encryption keys inside the enclave.

## <a name="anchorname-1-considerations-availability-groups-db-migration"></a> Considerations for business continuity, disaster recovery, and data migration

When configuring a high availability or disaster recovery solution for a database using Always Encrypted with secure enclaves, make sure that all database replicas can use a secure enclave. If an enclave is available for the primary replica, but not for the secondary replica, any statement that attempts to use the functionality of Always Encrypted with secure enclaves will fail after the failover.

When you copy or migrate a database using Always Encrypted with secure enclaves, make sure the target environment always supports enclaves. Otherwise, statements that use enclaves won't work on the copy or the migrated database.

Here are the specific considerations you should keep in mind:

- **SQL Server**
  - When configuring an [Always On availability group](../../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md), make sure that each SQL Server instance hosting a database in the availability group support Always Encrypted with secure enclaves, and have an enclave and attestation configured.
  - When restoring from a backup file of a database that uses the functionality of Always Encrypted with secure enclaves on a SQL Server instance that doesn't have the enclave configured, the restore operation will succeed and all the functionality that doesn't rely on the enclave will be available. However, any subsequent statement using the enclave functionality will fail, and indexes on enclave-enabled columns using randomized encryption will become invalid. The same applies when attaching a database using Always Encrypted with secure enclaves on the instance that doesn't have the enclave configured.
  - If your database contains indexes on enclave-enabled columns using randomized encryption, make sure to enable [Accelerated database recovery (ADR)](../../backup-restore/restore-and-recovery-overview-sql-server.md#adr) in the database before creating a database backup. ADR will ensure the database, including the indexes, is available immediately after you restore the database. For more information, see [Database Recovery](#database-recovery).
  
- **Azure SQL Database**
  - When configuring [active geo-replication](/azure/azure-sql/database/active-geo-replication-overview), make sure a secondary database supports secure enclaves, if the primary database does.

In both SQL Server and Azure SQL Database, when you migrate your database using a bacpac file, you need to make sure you drop all indexes for enclave-enabled columns using randomized encryption before creating the bacpac file.

## Known limitations

Always Encrypted with secure enclaves addresses some limitations of Always Encrypted by supporting in-place encryption and richer confidential queries with indexes, as explained in [Confidential computing capabilities for enclave-enabled columns](#confidential-computing-capabilities-for-enclave-enabled-columns).

All other limitations for Always Encrypted listed in [Limitations](always-encrypted-database-engine.md#limitations) also apply to Always Encrypted with secure enclaves.

The following limitations are specific to Always Encrypted with secure enclaves:

- Clustered indexes can't be created on enclave-enabled columns using randomized encryption.
- Enclave-enabled columns using randomized encryption can't be primary key columns and can't be referenced by foreign key constraints or unique key constraints.
- In [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] (this limitation doesn't apply to [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] or [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]) only nested loop joins (using indexes, if available) are supported on enclave-enabled columns using randomized encryption. For information about other differences among different products, see [Confidential queries](#confidential-queries).
- In-place cryptographic operations can't be combined with any other changes of column metadata, except changing a collation within the same code page and nullability. For example, you can't encrypt, re-encrypt, or decrypt a column AND change a data type of the column in a single `ALTER TABLE`/`ALTER COLUMN` Transact-SQL statement. Use two separate statements.
- Using enclave-enabled keys for columns in in-memory tables isn't supported.
- Expressions defining computed columns can't perform any computations on enclave-enabled columns using randomized encryption (even if the computations are among the supported operations listed in [Confidential queries](#confidential-queries)).
- Escape characters aren't supported in parameters of the LIKE operator on enclave-enabled columns using randomized encryption.
- Queries with the LIKE operator or a comparison operator that has a query parameter using one of the following data types (that become large objects after encryption) ignore indexes and perform table scans.
  - `nchar[n]` and `nvarchar[n]`, if n is greater than 3967.
  - `char[n]`, `varchar[n]`, `binary[n]`, `varbinary[n]`, if n is greater than 7935.
- Tooling limitations:
  - The only supported key stores for storing enclave-enabled column master keys are Windows Certificate Store and Azure Key Vault.
  - To trigger an in-place cryptographic operation via `ALTER TABLE`/`ALTER COLUMN`, you need to issue the statement using a query window in SSMS or Azure Data Studio, or you can write your own program that issues the statement. Currently, the `Set-SqlColumnEncryption` cmdlet in the SqlServer PowerShell module and the Always Encrypted wizard in SQL Server Management Studio don't support in-place encryption. Move the data out of the database for cryptographic operations, even if the column encryption keys used for the operations are enclave-enabled.

## Next steps

- [Tutorial: Getting started with Always Encrypted with secure enclaves in SQL Server](../tutorial-getting-started-with-always-encrypted-enclaves.md)
- [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)
- [Configure and use Always Encrypted with secure enclaves](configure-always-encrypted-enclaves.md)
- Run [Always Encrypted with secure enclaves demos/samples](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/security/always-encrypted-with-secure-enclaves) in the [SQL Server samples](https://github.com/Microsoft/sql-server-samples) GitHub repository
- Learn more about [Azure confidential computing](/azure/confidential-computing/)

## See also

- [Always Encrypted with secure enclaves documentation](/azure/azure-sql/database/always-encrypted-with-secure-enclaves-landing)
- [Azure SQL Database Always Encrypted, SIGMOD '20: Proceedings of the 2020 ACM SIGMOD International Conference on Management of Data](https://dl.acm.org/doi/abs/10.1145/3318464.3386141)
