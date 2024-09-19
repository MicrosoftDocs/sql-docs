---
title: "Always Encrypted"
description: Overview of Always Encrypted that supports transparent client-side encryption and confidential computing in SQL Server and Azure SQL Database
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto, randolphwest
ms.date: 08/14/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.alwaysencryptedwizard.encryption.f1"
helpviewer_keywords:
  - "encryption [SQL Server], Always Encrypted"
  - "Always Encrypted"
  - "TCE Always Encrypted"
  - "Always Encrypted, about"
  - "SQL13.SWB.COLUMNMASTERKEY.CLEANUP.F1"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Always Encrypted

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

:::image type="content" source="media/always-encrypted-database-engine/always-encrypted.png" alt-text="Diagram of Always Encrypted.":::

Always Encrypted and [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) are features designed to safeguard sensitive information, including credit card numbers and national or regional identification numbers (such as U.S. social security numbers), in [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)], Azure SQL Managed Instance, and [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] databases. It enables clients to encrypt sensitive data within client applications, ensuring that encryption keys are never exposed to the [!INCLUDE [ssDE](../../../includes/ssde-md.md)]. This provides a separation between those who own the data and can view it, and those who manage the data but should have no access: on-premises database administrators, cloud database operators, or other high-privileged unauthorized users. As a result, Always Encrypted allows customers to securely store their sensitive data in the cloud, reducing the risk of data theft by malicious insiders.

Always Encrypted has certain restrictions, such as the inability to perform operations on encrypted data, including sorting, filtering (except for point-lookups using deterministic encryption), etc. This means that some queries and applications might not be compatible with Always Encrypted or might require significant changes to the application logic.

To address these limitations, [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) enables the database engine to process encrypted data within a protected memory area called a secure enclave. Secure enclaves enhance the confidential computing capabilities of Always Encrypted by supporting pattern matching, various comparison operators, and in-place encryption.

Always Encrypted ensures that encryption is seamless for applications. On the client-side, Always Encrypted-enabled driver encrypts sensitive data before sending it to the [!INCLUDE [ssDE](../../../includes/ssde-md.md)] and automatically rewrites queries to maintain application semantics. It also automatically decrypts query results from encrypted database columns.

## Configure Always Encrypted

> [!NOTE]  
> For applications that need to perform pattern matching, use comparison operators, sort, and index on encrypted columns, you should implement [Always Encrypted with secure enclaves](always-encrypted-enclaves.md).

This section provides an overview of setting up Always Encrypted. For details and to get started, see [Tutorial: Getting started with Always Encrypted](always-encrypted-tutorial-getting-started.md).

To configure Always Encrypted in your database, follow these steps:

1. **Provision cryptographic keys to protect your data**. Always Encrypted uses two types of keys:

    - Column encryption keys.
    - Column master keys.

    A column encryption key is used to encrypt the data within an encrypted column. A column master key is a key-protecting key that encrypts one or more column encryption keys.

    You need to store column master keys in a trusted key store outside of the database system, such as [Azure Key Vault](/azure/key-vault/general/basic-concepts), [Windows certificate store](/windows-hardware/drivers/install/local-machine-and-current-user-certificate-stores), or a hardware security module. After this, you should provision column encryption keys and encrypt each with a column master key.

    Finally, save the metadata about the keys in your database. The column master key metadata includes the location of the column master key. The column encryption key metadata contains the encrypted value of the column encryption key. The [!INCLUDE [ssDE](../../../includes/ssde-md.md)] doesn't store or use any keys in plaintext.

    For more information on managing Always Encrypted keys, see [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md).

   <a name="selecting--deterministic-or-randomized-encryption"></a>

1. **Set up encryption for specific database columns** that include sensitive information to ensure protection. This might require creating new tables with encrypted columns or encrypting the existing columns and data. When configuring encryption for a column, you need to specify details about the encryption algorithm, the column encryption key to safeguard the data, and the type of encryption. Always Encrypted supports two types of encryption:

    - **Deterministic encryption** always generates the same encrypted value for a given plaintext value. Using deterministic encryption allows point lookups, equality joins, grouping, and indexing on encrypted columns. However, it might also allow unauthorized users to guess information about encrypted values by examining patterns in the encrypted column, especially if there's a small set of possible encrypted values, such as True/False, or North/South/East/West region.

    - **Randomized encryption** uses a method that encrypts data unpredictably. Each identical plaintext input results in a distinct encrypted output. This improves the security of randomized encryption.

To perform pattern matching using comparison operators, sorting, and indexing on encrypted columns, you should adopt [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) and apply randomized encryption. Always Encrypted (*without secure enclaves*) randomized encryption doesn't support searching, grouping, indexing, or joining on encrypted columns. Instead, for columns intended for search or grouping purposes, it's essential to use deterministic encryption. This allows operations such as point lookups, equality joins, grouping, and indexing on encrypted columns.

Since the database system has by design no access to cryptographic keys, any column encryption requires moving and encrypting data outside of the database. This means that this encryption process can take a long time and is vulnerable to network interruptions. Additionally, if you need to re-encrypt a column later, such as when rotating the encryption key or changing encryption types, you'll encounter the same difficulties again. Using [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) eliminates the necessity of moving data out of the database. Because the enclave is trusted, a client driver within your application or a tool like Azure Data Studio or SQL Server Management Studio (SSMS) can safely share the keys with the enclave during cryptographic operations. The enclave can then encrypt or re-encrypt columns in place, significantly decreasing the time required for these actions.

For details on Always Encrypted cryptographic algorithms, see [Always Encrypted cryptography](always-encrypted-cryptography.md).

You can perform the above steps using [SQL tools](../../../tools/overview-sql-tools.md):

- [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md)
- [Configure Always Encrypted using PowerShell](configure-always-encrypted-using-powershell.md)
- [sqlpackage](configure-always-encrypted-using-dacpac.md) - which automate the setup process

To ensure Always Encrypted keys and protected sensitive data are never revealed in plaintext to the database environment, the [!INCLUDE [ssDE](../../../includes/ssde-md.md)] can't be involved in key provisioning and data encryption, or decryption operations. Therefore, Transact-SQL (T-SQL) doesn't support key provisioning or cryptographic operations. For the same reason, encrypting existing data or re-encrypting it (with a different encryption type or a column encryption key) needs to be performed outside of the database (SQL tools can automate that).

After changing the definition of an encrypted column, execute [sp_refresh_parameter_encryption](../../system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md) to update the Always Encrypted metadata for the object.

## Limitations

The following limitations apply to queries on encrypted columns:

- No computations on columns encrypted using randomized encryption are allowed. Deterministic encryption supports the following operations involving equality comparisons - no other operations are allowed.
  - [= (Equals) (Transact-SQL)](../../../t-sql/language-elements/equals-transact-sql.md) in point lookup searches.
  - [IN (Transact-SQL)](../../../t-sql/language-elements/in-transact-sql.md).
  - [SELECT - GROUP BY- Transact-SQL](../../../t-sql/queries/select-group-by-transact-sql.md).
  - [DISTINCT](../../../t-sql/queries/select-transact-sql.md#c-using-distinct-with-select).

  > [!NOTE]  
  > For applications that need to perform pattern matching, use comparison operators, sort, and index on encrypted columns, you should implement [Always Encrypted with secure enclaves](always-encrypted-enclaves.md).

- Query statements that trigger computations involving both plaintext and encrypted data aren't allowed. For example:

  - Comparing an encrypted column to a plaintext column or a literal.
  - Copying data from a plaintext column to an encrypted column (or the other way around) **UPDATE**, **BULK INSERT**, **SELECT INTO**, or **INSERT..SELECT**.
  - Inserting literals to encrypted columns.

  Such statements result in operand clash errors like this:

    ```output
    Msg 206, Level 16, State 2, Line 89
        Operand type clash: char(11) encrypted with (encryption_type = 'DETERMINISTIC', encryption_algorithm_name = 'AEAD_AES_256_CBC_HMAC_SHA_256', column_encryption_key_name = 'CEK_1', column_encryption_key_database_name = 'ssn') collation_name = 'Latin1_General_BIN2' is incompatible with char
    ```

  Applications need to use query parameters to provide values for encrypted columns. For example, when you're inserting data into encrypted columns or filtering them using deterministic encryption, query parameters should be used. It isn't supported to pass literals or T-SQL variables that correspond to encrypted columns. For more information specific to a client driver you're using, see [Develop applications using Always Encrypted](always-encrypted-client-development.md).

  In [Azure Data Studio](always-encrypted-query-columns-ads.md#parameterization-for-always-encrypted) or [SSMS](always-encrypted-query-columns-ssms.md#param), it's essential to apply parameterization for Always Encrypted variables to execute queries that handle values associated with encrypted columns. This includes scenarios such as inserting data into encrypted columns or applying filters on them (in cases where deterministic encryption is used).

- [Table-valued parameters](../../tables/use-table-valued-parameters-database-engine.md) targeting encrypted columns aren't supported.

- Queries using the following clauses aren't supported:

  - [FOR XML (SQL Server)](../../xml/for-xml-sql-server.md)
  - [Format query results as JSON with FOR JSON](../../json/format-query-results-as-json-with-for-json-sql-server.md)

- Always Encrypted isn't supported for the columns with the below characteristics:

  - Columns using one of the following data types: **xml**, **timestamp**, **rowversion**, **image**, **ntext**, **text**, **sql_variant**, **hierarchyid**, **geography**, **geometry**, alias, user-defined types.
  - [FILESTREAM](../../../t-sql/statements/create-table-transact-sql.md#filestream) columns
  - Columns with the [IDENTITY](../../../t-sql/statements/create-table-transact-sql.md#identity) property.
  - Columns with [ROWGUIDCOL](../../../t-sql/statements/create-table-transact-sql.md#rowguidcol) property.
  - String (**varchar**, **char**, etc.) columns with collations other than [binary-code point (_BIN2) collations](../../collations/collation-and-unicode-support.md) when using deterministic encryption.
  - Columns that are keys for clustered and nonclustered indices when using randomized encryption (indices on columns using deterministic encryption are supported).
  - Columns included in full-text indexes (Always Encrypted doesn't support [Full-Text Search](../../search/full-text-search.md)).
  - [Specify computed columns in a table](../../tables/specify-computed-columns-in-a-table.md).
  - Columns referenced by computed columns (when the expression does unsupported operations for Always Encrypted).
  - [Use sparse columns](../../tables/use-sparse-columns.md).
  - Columns that are referenced by [statistics](../../statistics/statistics.md) when using randomized encryption (deterministic encryption is supported).
  - [Partitioning columns](../../partitions/partitioned-tables-and-indexes.md#partitioning-column).
  - Columns with [default constraints](../../tables/specify-default-values-for-columns.md).
  - Columns referenced by [unique constraints](../../tables/create-unique-constraints.md) when using randomized encryption (deterministic encryption is supported).
  - Primary key columns when using randomized encryption (deterministic encryption is supported).
  - Referencing columns in [foreign key constraints](../../tables/create-foreign-key-relationships.md) when using randomized encryption or when using deterministic encryption, if the referenced and referencing columns use different keys or algorithms.
  - Columns referenced by [check constraints](../../system-information-schema-views/check-constraints-transact-sql.md).
  - Columns captured/tracked using [change data capture](../../track-changes/about-change-data-capture-sql-server.md).
  - Primary key columns on tables that have [change tracking](../../track-changes/about-change-tracking-sql-server.md).
  - Columns that are masked (using [Dynamic data masking](../dynamic-data-masking.md)).
  - Columns in [stretch database tables](/previous-versions/sql/sql-server/stretch-database/stretch-database). (Tables with columns encrypted with Always Encrypted can be enabled for Stretch.)

  > [!IMPORTANT]  
  > [!INCLUDE [stretch-database-deprecation](../../../includes/stretch-database-deprecation.md)]

  - Columns in external (PolyBase) tables (note: using external tables and tables with encrypted columns in the same query is supported).

- The following features don't work on encrypted columns:

  - [SQL Server Replication](../../replication/sql-server-replication.md) (transactional, merge, or snapshot replication). Physical replication features, including [Always On availability group](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md), are supported.
  - Distributed queries ([linked servers](../../linked-servers/linked-servers-database-engine.md), [OPENROWSET (Transact-SQL)](../../../t-sql/functions/openrowset-transact-sql.md), [OPENDATASOURCE (Transact-SQL)](../../../t-sql/functions/opendatasource-transact-sql.md)).
  - [Cross-Database Queries](../../in-memory-oltp/cross-database-queries.md) that perform joins on columns (using deterministic encryption) from different databases.

## Always Encrypted Transact-SQL reference

Always Encrypted uses the following Transact-SQL statements, system catalog views, system stored procedures, and permissions.

### Statements

| **DDL Statement** | **Description** |
| --- | --- |
| [CREATE COLUMN MASTER KEY](../../../t-sql/statements/create-column-master-key-transact-sql.md) | Creates a column master key metadata object in a database |
| [DROP COLUMN MASTER KEY](../../../t-sql/statements/drop-column-master-key-transact-sql.md) | Drops a column master key from a database. |
| [CREATE COLUMN ENCRYPTION KEY](../../../t-sql/statements/create-column-encryption-key-transact-sql.md) | Creates a column encryption key metadata object. |
| [ALTER COLUMN ENCRYPTION KEY](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md) | Alters a column encryption key in a database, adding or dropping an encrypted value. |
| [DROP COLUMN ENCRYPTION KEY](../../../t-sql/statements/drop-column-encryption-key-transact-sql.md) | Drops a column encryption key from a database. |
| [CREATE TABLE (ENCRYPTED WITH)](../../../t-sql/statements/create-table-transact-sql.md?#encrypted-with) | Specifies encrypting columns |

### System catalog views and stored procedures

| **System catalog views and stored procedures** | **Description** |
| --- | --- |
| [sys.column_encryption_keys](../../system-catalog-views/sys-column-encryption-keys-transact-sql.md) | Returns information about column encryption keys (CEKs) |
| [sys.column_encryption_key_values](../../system-catalog-views/sys-column-encryption-key-values-transact-sql.md) | Returns information about encrypted values of column encryption keys (CEKs) |
| [sys.column_master_keys](../../system-catalog-views/sys-column-master-keys-transact-sql.md) | Returns a row for each database master key |
| [sp_refresh_parameter_encryption](../../system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md) | Updates the Always Encrypted metadata for the parameters of the specified non-schema-bound stored procedure, user-defined function, view, DML trigger, database-level DDL trigger, or server-level DDL trigger |
| [sp_describe_parameter_encryption](../../system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md) | Analyses the specified Transact-SQL statement and its parameters, to determine which parameters correspond to database columns that are protected by using the Always Encrypted feature. |

Also see [sys.columns](../../system-catalog-views/sys-columns-transact-sql.md) for information on encryption metadata stored for each column.

### Database permissions

There are four database permissions for Always Encrypted.

| **System catalog views and stored procedures** | **Description** |
| --- | --- |
| ALTER ANY COLUMN MASTER KEY | Required to create and delete column master key metadata. |
| ALTER ANY COLUMN ENCRYPTION KEY | Required to create and delete column encryption key metadata. |
| VIEW ANY COLUMN MASTER KEY DEFINITION | Required to access and read the column master key metadata, which is needed to query encrypted columns. |
| VIEW ANY COLUMN ENCRYPTION KEY DEFINITION | Required to access and read the column encryption key metadata, which is needed to query encrypted columns. |

The following table summarizes the permissions required for common actions.

| Scenario | **ALTER ANY COLUMN MASTER KEY** | **ALTER ANY COLUMN ENCRYPTION KEY** | **VIEW ANY COLUMN MASTER KEY DEFINITION** | **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** |
| --- | --- | --- | --- | --- |
| Key management (creating/changing/reviewing key metadata in the database) | X | X | X | X |
| Querying encrypted columns | | | X | X |

#### Important considerations

- The **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions are required when selecting encrypted columns, even if the user doesn't have permission to the column master keys (in their key stores), protecting the columns and doesn't access plaintext attempt.

- In [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], both **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions are granted by default to the **public** fixed database role. A database administrator might choose to revoke (or deny) the permissions to the **public** role and grant them to specific roles or users to implement more restricted control.

- In [!INCLUDE [ssSDS](../../../includes/sssds-md.md)], the **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions aren't granted by default to the **public** fixed database role. This enables certain existing legacy tools (using older versions of DacFx) to work properly. To work with encrypted columns (even if not decrypting them), a database administrator must explicitly grant the **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions.

## Related content

- [Always Encrypted documentation](/azure/azure-sql/database/always-encrypted-landing)

## Next step

> [!div class="nextstepaction"]
> [Tutorial: Getting started with Always Encrypted](always-encrypted-tutorial-getting-started.md)
