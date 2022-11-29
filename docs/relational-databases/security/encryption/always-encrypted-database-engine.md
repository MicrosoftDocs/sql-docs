---
title: "Always Encrypted"
description: Overview of Always Encrypted that supports transparent client-side encryption and confidential computing in SQL Server and Azure SQL Database
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 10/25/2022
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "encryption [SQL Server], Always Encrypted"
  - "Always Encrypted"
  - "TCE Always Encrypted"
  - "Always Encrypted, about"
  - "SQL13.SWB.COLUMNMASTERKEY.CLEANUP.F1"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Always Encrypted

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

:::image type="content" source="media/always-encrypted-database-engine/always-encrypted.png" alt-text="Diagram of Always Encrypted.":::

Always Encrypted is a feature designed to protect sensitive data, such as credit card numbers or national identification numbers (for example, U.S. social security numbers), stored in [!INCLUDE[ssSDSFull](../../../includes/sssdsfull-md.md)], Azure SQL Managed Instance, and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases. Always Encrypted allows clients to encrypt sensitive data inside client applications and never reveal the encryption keys to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)]. This provides a separation between those who own the data and can view it, and those who manage the data but should have no access - on-premises database administrators, cloud database operators, or other high-privileged unauthorized users. As a result, Always Encrypted enables customers to confidently store their sensitive data in the cloud, and to reduce the likelihood of data theft by malicious insiders.

Always Encrypted can be configured to support limited confidential queries on encrypted data, the queries that involve equality comparisons. For example, point lookup-searches or equality joins. Such queries leverage [deterministic encryption](#selecting--deterministic-or-randomized-encryption).

> [!NOTE]  
> Secure enclaves extend confidential computing capabilities of Always Encrypted with pattern matching, other comparison operators and in-place encryption. For more information, see [Always Encrypted with secure enclaves](always-encrypted-enclaves.md).

Always Encrypted makes encryption transparent to applications. An Always Encrypted-enabled driver installed on the client computer achieves this by automatically encrypting and decrypting sensitive data in the client application. The driver encrypts the data in sensitive columns before passing the data to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)], and automatically rewrites queries so that the semantics to the application are preserved. Similarly, the driver transparently decrypts data, stored in encrypted database columns, contained in query results.

## Configure Always Encrypted

This section provides an overview of setting up Always Encrypted. For details and to get started, see [Tutorial: Getting started with Always Encrypted](always-encrypted-tutorial-getting-started.md).

To set up Always Encrypted in your database, you need to:

1. **Provision cryptographic keys to protect your data**. Always Encrypted uses two types of keys:

    - Column encryption keys.
    - Column master keys.

    A column encryption key is used to encrypt data in an encrypted column. A column master key is a key-protecting key that encrypts one or more column encryption keys.

    You need to store column master keys in a trusted key store outside of the database system, such as [Azure Key Vault](/azure/key-vault/general/basic-concepts), [Windows certificate store](/windows-hardware/drivers/install/local-machine-and-current-user-certificate-stores), or a hardware security module.

    Then, you need provision column encryption keys and encrypt each of them with a column master key.

    Finally, you need to store the metadata about the keys in your database.
      - The column master key metadata captures the location of the column master key. 
      - The column encryption key metadata contains the encrypted value of the column encryption key. The [!INCLUDE[ssDE](../../../includes/ssde-md.md)] never stores or uses the keys of either type in plaintext.

    For more information about managing Always Encrypted keys, see [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md)

   <a name="selecting--deterministic-or-randomized-encryption"></a>

1. **Configure encryption for selected database columns** that contain sensitive data to be protected. This can involve creating new tables with encrypted columns or encrypting existing database columns and existing data. When setting up encryption for a column, you specify the information about an encryption algorithm, a column encryption key to protect the data in the column, and an encryption type. Always Encrypted supports two encryption types: 

    - **Deterministic encryption** always generates the same encrypted value for a given plaintext value. Using deterministic encryption allows point lookups, equality joins, grouping and indexing on encrypted columns. However, it may also allow unauthorized users to guess information about encrypted values by examining patterns in the encrypted column, especially if there's a small set of possible encrypted values, such as True/False, or North/South/East/West region. 

    - **Randomized encryption** uses a method that encrypts data in a less predictable manner. Randomized encryption is more secure, but prevents searching, grouping, indexing, and joining on encrypted columns.

    Use deterministic encryption for columns that will be used as search or grouping parameters. For example, a government ID number. Use randomized encryption for data such as confidential investigation comments, which aren't grouped with other records and aren't used to join tables.

For details on Always Encrypted cryptographic algorithms, see [Always Encrypted cryptography](../../../relational-databases/security/encryption/always-encrypted-cryptography.md).

You can perform the above steps using [SQL tools](../../../tools/overview-sql-tools.md):

- [SQL Server Management Studio (SSMS)](configure-always-encrypted-keys-using-ssms.md)
- [SQL Server PowerShell](configure-always-encrypted-using-powershell.md)
- [sqlpackage](configure-always-encrypted-using-dacpac.md) - which automate the setup process

To ensure Always Encrypted keys and protected sensitive data are never revealed in plaintext to the database environment, the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] can't be involved in key provisioning and  data encryption, or decryption operations. Therefore, Transact-SQL (T-SQL) doesn't support key provisioning or cryptographic operations. For the same reason, encrypting existing data or re-encrypting it (with a different encryption type or a column encryption key) needs to be performed outside of the database (SQL tools can automate that).

> [!NOTE]  
> [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) lifts some of the above restrictions by allowing cryptographic operations on existing data using T-SQL, and eliminates the need to move the data outside of the database.

## How queries against encrypted columns work

To run a query on encrypted database columns, insert data to encrypted columns, retrieve plaintext values from encrypted columns, or perform supported operations (for example, point lookup searches) on columns using deterministic encryption, a user or an application issuing the query must meet the following prerequisites:

- Have access to the column master key protecting the data. Key access is required in addition to the database-level permissions, such as `SELECT` on the table containing the data.
- Connect to the database with Always Encrypted enabled in the database connection. Most SQL tools and SQL client drivers support enabling Always Encrypted for database connections.

> [!NOTE]  
> If the user has required database permissions to read the data, but no access to the keys protecting it, the user can still retrieve cyphertext (encrypted) data by connecting to the database without enabling Always Encrypted in the database connection.

Here's how queries on encrypted columns work:

1. When an application issues a parameterized query, the SQL client driver within the application transparently contacts the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] (by calling [sp_describe_parameter_encryption (Transact-SQL)](../../system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md) to determine which parameters target encrypted columns and should be encrypted. For each parameter that needs to be encrypted, the driver receives the encryption algorithm, encryption type, and key metadata, including the encrypted column encryption key and the location of its corresponding column master key.
1. The driver calls the key store, containing column master keys in order to decrypt the encrypted column encryption key values. The resultant plaintext column encryption keys are cached to reduce the number of round trips to the key store on subsequent uses of the same column encryption keys.
1. The driver uses the obtained plaintext column encryption keys to encrypt the query parameters corresponding to encrypted columns.
1. The driver substitutes the plaintext values of the parameters targeting encrypted columns with their encrypted values, and it sends the query to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] for processing.
1. The [!INCLUDE[ssDE](../../../includes/ssde-md.md)] executes the query, which may involve equality comparisons on columns using deterministic encryption.
1. If query results include data from encrypted columns, the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] attaches encryption metadata for each column, including the information about the encryption algorithm, the encryption type, and key metadata to the result set.
1. The [!INCLUDE[ssDE](../../../includes/ssde-md.md)] sends the result set to the client application.
1. For each encrypted column in the received result set, the driver first tries to find the plaintext column encryption key in the local cache, and only makes a round trip to a key store holding the column master key if it can't find the key in the cache.
1. The driver decrypts the results and returns plaintext values to the application.

A client driver interacts with a key store, containing a column master key, using a column master key store provider, which is a client-side software component that encapsulates a key store containing the column master key. Providers for common types of key stores are available in client-side driver libraries from Microsoft, or as standalone downloads. You can also implement your own provider. Always Encrypted capabilities, including built-in column master key store providers vary by a driver library and its version.

See [Develop applications using Always Encrypted](always-encrypted-client-development.md) for the list of client drivers supporting Always Encrypted and for information on how to develop applications that query encrypted columns.

You can also query encrypted columns using SQL tools, for example [Azure Data Studio](always-encrypted-query-columns-ads.md) or [SSMS](always-encrypted-query-columns-ssms.md).

## Limitations

The following limitations apply to queries on encrypted columns:

- Deterministic encryption supports the following operations involving equality comparisons - no other operations are allowed.
  - [= (Equals)](../../../t-sql/language-elements/equals-transact-sql.md) in point lookup searches.
  - [IN](../../../t-sql/language-elements/in-transact-sql.md).
  - [SELECT - GROUP BY](../../../t-sql/queries/select-group-by-transact-sql.md).
  - [DISTINCT](../../../t-sql/queries/select-transact-sql.md#c-using-distinct-with-select).

- No computations on columns encrypted using randomized encryption are allowed. 

  > [!NOTE]  
  > [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) relaxes the above restriction by allowing pattern matching, comparison operators, sorting, and indexing on columns using randomized encryption.

- Query statements that trigger computations involving both plaintext and encrypted data aren't allowed. For example:

  - Comparing an encrypted column to a plaintext column or a literal.
  - Copying data from a plaintext column to an encrypted column (or the other way around) **UPDATE**, **BULK INSERT**, **SELECT INTO**, or **INSERT..SELECT**.
  - Inserting literals to encrypted columns.

  Such statements result in operand clash errors like this:

    ```output
    Msg 206, Level 16, State 2, Line 89
        Operand type clash: char(11) encrypted with (encryption_type = 'DETERMINISTIC', encryption_algorithm_name = 'AEAD_AES_256_CBC_HMAC_SHA_256', column_encryption_key_name = 'CEK_1', column_encryption_key_database_name = 'ssn') collation_name = 'Latin1_General_BIN2' is incompatible with char
    ```

- Applications must use query parameters to pass values that correspond to encrypted columns. For example, when inserting data to encrypted columns or filtering by encrypted columns (when using deterministic encryption). Passing literals or T-SQL variables corresponding to encrypted columns isn't supported. For more information specific to a client driver you're using, see [Develop applications using Always Encrypted](always-encrypted-client-development.md).

- You must use Parameterization for Always Encrypted variables in [Azure Data Studio](always-encrypted-query-columns-ads.md#parameterization-for-always-encrypted) or [SSMS](always-encrypted-query-columns-ssms.md#param) to issue queries that pass values corresponding to encrypted columns in these tools. For example, when inserting data to encrypted columns or filtering by encrypted columns (when using deterministic encryption).

- [Table-valued parameters](../../tables/use-table-valued-parameters-database-engine.md) targeting encrypted columns aren't supported.
 
- Queries using the following clauses aren't supported:

  - [FOR XML (SQL Server)](../../xml/for-xml-sql-server.md)
  - [FOR JSON (SQL Server)](../../json/format-query-results-as-json-with-for-json-sql-server.md)

- After changing the definition of an encrypted column, execute [sp_refresh_parameter_encryption](../../../relational-databases/system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md) to update the Always Encrypted metadata for the object.


- Always Encrypted isn't supported for the columns with the below characteristics:

  - Columns using one of the following data types: **xml**, **timestamp**, **rowversion**, **image**, **ntext**, **text**, **sql_variant**, **hierarchyid**, **geography**, **geometry**, alias, user-defined types.
  - [FILESTREAM](../../../t-sql/statements/create-table-transact-sql.md#filestream) columns
  - Columns with the [IDENTITY](../../../t-sql/statements/create-table-transact-sql.md#identity) property.
  - Columns with [ROWGUIDCOL](../../../t-sql/statements/create-table-transact-sql.md#rowguidcol) property.
  - String (**varchar**, **char**, etc.) columns with collations other than [binary-code point (_BIN2) collations](../../../relational-databases/collations/collation-and-unicode-support.md) when using deterministic encryption.
  - Columns that are keys for clustered and nonclustered indices when using randomized encryption (indices on columns using deterministic encryption are supported).
  - Columns included in full-text indexes (Always Encrypted doesn't support [Full Text Search](../../../relational-databases/search/full-text-search.md)).
  - [Computed columns](../../tables/specify-computed-columns-in-a-table.md).
  - Columns referenced by computed columns (when the expression does unsupported operations for Always Encrypted).
  - [Sparse column set](../../tables/use-sparse-columns.md).
  - Columns that are referenced by [statistics](../../statistics/statistics.md) when using randomized encryption (deterministic encryption is supported).
  - [Partitioning columns](../../partitions/partitioned-tables-and-indexes.md#partitioning-column).
  - Columns with [default constraints](../../tables/specify-default-values-for-columns.md).
  - Columns referenced by [unique constraints](../../tables/create-unique-constraints.md) when using randomized encryption (deterministic encryption is supported).
  - Primary key columns when using randomized encryption (deterministic encryption is supported).
  - Referencing columns in [foreign key constraints](../../tables/create-foreign-key-relationships.md)  when using randomized encryption or when using deterministic encryption, if the referenced and referencing columns use different keys or algorithms.
  - Columns referenced by [check constraints](../../system-information-schema-views/check-constraints-transact-sql.md).
  - Columns captured/tracked using [change data capture](../../track-changes/about-change-data-capture-sql-server.md).
  - Primary key columns on tables that have [change tracking](../../track-changes/about-change-tracking-sql-server.md).
  - Columns that are masked (using [Dynamic Data Masking](../dynamic-data-masking.md)).
  - Columns in [stretch database tables](../../../sql-server/stretch-database/stretch-database.md). (Tables with columns encrypted with Always Encrypted can be enabled for Stretch.)

  > [!IMPORTANT]  
  > Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../../includes/ssnotedepfutureavoid-md.md)]

  - Columns in external (PolyBase) tables (note: using external tables and tables with encrypted columns in the same query is supported).

- The following features don't work on encrypted columns:

  - [SQL Server replication](../../replication/sql-server-replication.md) (transactional, merge, or snapshot replication). Physical replication features, including [Always](../../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md), are supported.
  - Distributed queries ([linked servers](../../linked-servers/linked-servers-database-engine.md), [OPENROWSET (Transact-SQL)](../../../t-sql/functions/openrowset-transact-sql.md), [OPENDATASOURCE (Transact-SQL)](../../../t-sql/functions/opendatasource-transact-sql.md)).
  - [Cross-database queries](../../in-memory-oltp/cross-database-queries.md) that perform joins on columns (using deterministic encryption) from different databases.

## Always Encrypted Transact-SQL reference

Always Encrypted uses the following Transact-SQL statements, system catalog views, system stored procedures and permissions.

### Statements

- [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md)
- [DROP COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/drop-column-master-key-transact-sql.md)
- [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md)
- [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md)
- [DROP COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/drop-column-encryption-key-transact-sql.md)
- [CREATE TABLE (ENCRYPTED WITH)](../../../t-sql/statements/create-table-transact-sql.md?#encrypted-with)

### System catalog views and stored procedures

- [sys.column_encryption_keys  &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md)
- [sys.column_encryption_key_values &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-column-encryption-key-values-transact-sql.md)
- [sys.column_master_keys &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)
- [sp_refresh_parameter_encryption &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md)
- [sp_describe_parameter_encryption (Transact-SQL)](../../system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md)

Also see [sys.columns &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-columns-transact-sql.md) for information on encryption metadata stored for each column.

### Database permissions

There are four database permissions for Always Encrypted:

- **ALTER ANY COLUMN MASTER KEY**  - required to create and delete column master key metadata.

- **ALTER ANY COLUMN ENCRYPTION KEY** - required to create and delete column encryption key metadata.

- **VIEW ANY COLUMN MASTER KEY DEFINITION** - required to access and read the column master key metadata, which is needed to query encrypted columns.

- **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** - required to access and read the column master key metadata, which is needed to query encrypted columns.

The following table summarizes the permissions required for common actions.

|Scenario|**ALTER ANY COLUMN MASTER KEY**|**ALTER ANY COLUMN ENCRYPTION KEY**|**VIEW ANY COLUMN MASTER KEY DEFINITION**|**VIEW ANY COLUMN ENCRYPTION KEY DEFINITION**|
| --- | --- | --- | --- | --- |
|Key management (creating/changing/reviewing key metadata in the database)|X|X|X|X|
|Querying encrypted columns|||X|X|

#### Important considerations

- The **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions are required when selecting encrypted columns, even if the user doesn't have permission to the column master keys (in their key stores), protecting the columns and doesn't access plaintext attempt.

- In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], both **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions are granted by default to the **public** fixed database role. A database administrator may choose to revoke (or deny) the permissions to the **public** role and grant them to specific roles or users to implement more restricted control.

- In [!INCLUDE[ssSDS](../../../includes/sssds-md.md)], the **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions aren't granted by default to the **public** fixed database role. This enables certain existing legacy tools (using older versions of DacFx) to work properly. Consequently, to work with encrypted columns (even if not decrypting them) a database administrator must explicitly grant the **VIEW ANY COLUMN MASTER KEY DEFINITION** and **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** permissions.

## Next steps

- To try Always Encrypted, see [Tutorial: Getting started with Always Encrypted](always-encrypted-tutorial-getting-started.md).

## See also

- [Always Encrypted documentation](/azure/azure-sql/database/always-encrypted-landing)
