---
title: "Configure Column Encryption In-Place with Transact-SQL"
description: "Configure column encryption in-place with Transact-SQL"
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: 01/26/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
---
# Configure column encryption in-place with Transact-SQL

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

This article describes how to perform cryptographic operations in-place on columns using Always Encrypted with secure enclaves and the [ALTER TABLE Statement](../../../odbc/microsoft/alter-table-statement.md)/`ALTER COLUMN` statement. For basic information about in-place encryption and general prerequisites, see [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md).

With the `ALTER TABLE` or `ALTER COLUMN` statement, you can set the target encryption configuration for a column. When you execute the statement, the server-side secure enclave encrypts, re-encrypts, or decrypts the data stored in the column, depending on the current and the target encryption configuration specified in the column definition in the statement.

- If the column is currently not encrypted, it's encrypted if you specify the `ENCRYPTED WITH` clause in the column definition.
- If the column is currently encrypted, it's decrypted (converted to a plaintext column), if you don't specify the `ENCRYPTED WITH` clause in the column definition.
- If the column is currently encrypted, it's re-encrypted if you specify the `ENCRYPTED WITH` clause and the specified column encryption type or the column encryption key are different from the currently used encryption type or the column encryption key.

> [!NOTE]  
> You can't combine cryptographic operations with other changes in a single `ALTER TABLE`/`ALTER COLUMN` statement, except for changing the column to `NULL` or `NOT NULL`, or changing a collation. For example, you can't encrypt a column and change the data type of the column in a single `ALTER TABLE`/`ALTER COLUMN` Transact-SQL statement. Use two separate statements.

As with any query that uses a server-side secure enclave, you must send an `ALTER TABLE`/`ALTER COLUMN` statement that triggers in-place encryption over a connection with Always Encrypted and enclave computations enabled.

The remainder of this article describes how to trigger in-place encryption using the `ALTER TABLE`/`ALTER COLUMN` statement from SQL Server Management Studio. Alternatively, you can issue `ALTER TABLE`/`ALTER COLUMN` from Visual Studio Code or your application.

> [!NOTE]  
> Currently, the [Invoke-Sqlcmd](/powershell/module/sqlserver/invoke-sqlcmd) cmdlet in the SqlServer PowerShell module and [sqlcmd](../../../tools/sqlcmd/sqlcmd-utility.md) don't support using `ALTER TABLE`/`ALTER COLUMN` for in-place cryptographic operations.

## Perform in-place encryption with Transact-SQL in SSMS

### Prerequisites

- Prerequisites described in [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md).
- Install the latest version of [SQL Server Management Studio (SSMS)](/ssms/install/install).

### Steps

1. Open a query window with Always Encrypted and enclave computations enabled in the database connection. For details, see [Enabling and disabling Always Encrypted for a database connection](always-encrypted-query-columns-ssms.md#en-dis).
1. In the query window, run the `ALTER TABLE`/`ALTER COLUMN` statement. Specify the target encryption configuration for a column you want to encrypt, decrypt, or re-encrypt. If you're encrypting or re-encrypting the column, use the `ENCRYPTED WITH` clause. If your column is a string column (for example, `char`, `varchar`, `nchar`, `nvarchar`), you might also need to change the collation to a BIN2 collation.

   > [!NOTE]  
   > If your column master key is stored in Azure Key Vault, you might be prompted to sign in to Azure.

1. Clear the plan cache for all batches and stored procedures that access the table, to refresh parameters encryption information.

   ```sql
   ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
   ```

   > [!NOTE]  
   > If you don't remove the plan for the affected query from the cache, the first execution of the query after encryption might fail.
   >
   > Use `ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE` or `DBCC FREEPROCCACHE` to clear the plan cache carefully, as it might result in temporary query performance degradation. To minimize the negative impact of clearing the cache, you can selectively remove the plans for the affected queries only.

1. Call [sp_refresh_parameter_encryption](../../system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md) to update the metadata for the parameters of each module (stored procedure, function, view, trigger) that are persisted in [sys.parameters](../../system-catalog-views/sys-parameters-transact-sql.md) and might be invalidated by encrypting the columns.

### Examples

<a id="encrypting-a-column-in-place"></a>

#### Encrypt a column in-place

The following example assumes:

- `CEK1` is an enclave-enabled column encryption key.
- The `SSN` column is plaintext and is currently using the default database collation, such as Latin1, non-BIN2 collation (for example, `Latin1_General_CI_AI_KS_WS`).

The statement encrypts the `SSN` column using deterministic encryption and the enclave-enabled column encryption key in-place. It also overwrites the default database collation with the corresponding (in the same code page) BIN2 collation.

The operation is performed online (`ONLINE = ON`). Take note of the call to `ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE`, which recreates the plans of the queries affected by the table schema change.

```sql
ALTER TABLE [dbo].[Employees]
ALTER COLUMN [SSN] CHAR (11) COLLATE Latin1_General_BIN2
ENCRYPTED WITH (
    COLUMN_ENCRYPTION_KEY = [CEK1],
    ENCRYPTION_TYPE = DETERMINISTIC,
    ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'
) NOT NULL WITH (ONLINE = ON);
GO

ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
```

#### Re-encrypt a column in-place to change encryption type

The following example assumes:

- The `SSN` column is encrypted using deterministic encryption and an enclave-enabled column encryption key, `CEK1`.
- The current collation, set at the column level, is `Latin1_General_BIN2`.

The following statement re-encrypts the column using randomized encryption and the same key (`CEK1`).

```sql
ALTER TABLE [dbo].[Employees]
ALTER COLUMN [SSN] CHAR (11) COLLATE Latin1_General_BIN2
ENCRYPTED WITH (
    COLUMN_ENCRYPTION_KEY = [CEK1],
    ENCRYPTION_TYPE = RANDOMIZED,
    ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'
) NOT NULL;
GO

ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
```

#### Re-encrypt a column in-place to rotate a column encryption key

The following example assumes:

- The `SSN` column is encrypted using randomized encryption and an enclave-enabled column encryption key, `CEK1`.
- `CEK2` is an enclave-enabled column encryption key (different from `CEK1`).
- The current collation, set at the column level, is `Latin1_General_BIN2`.

The following statement re-encrypts the column with `CEK2`.

```sql
ALTER TABLE [dbo].[Employees]
ALTER COLUMN [SSN] CHAR (11) COLLATE Latin1_General_BIN2
ENCRYPTED WITH (
    COLUMN_ENCRYPTION_KEY = [CEK2],
    ENCRYPTION_TYPE = RANDOMIZED,
    ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'
) NOT NULL;
GO

ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
```

#### Decrypt a column in-place

The following example assumes:

- The `SSN` column is encrypted using an enclave-enabled column encryption key.
- The current collation, set at the column level, is `Latin1_General_BIN2`.

The following statement decrypts the column and keeps the collation unchanged. Alternatively, you can choose to change the collation. For example, change the collation to a non-BIN2 collation in the same statement.

```sql
ALTER TABLE [dbo].[Employees]
ALTER COLUMN [SSN] CHAR (11) COLLATE Latin1_General_BIN2
WITH (ONLINE = ON);
GO

ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
```

## Related content

- [Troubleshoot common issues for Always Encrypted with secure enclaves](always-encrypted-enclaves-troubleshooting.md)
- [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md)
- [Enable Always Encrypted with secure enclaves for existing encrypted columns](always-encrypted-enclaves-enable-for-encrypted-columns.md)
- [Getting started using Always Encrypted with secure enclaves](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)
- [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md)
- [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)
