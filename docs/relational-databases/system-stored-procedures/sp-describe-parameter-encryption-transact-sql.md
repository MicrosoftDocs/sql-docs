---
title: "sp_describe_parameter_encryption (Transact-SQL)"
description: sp_describe_parameter_encryption analyzes the specified Transact-SQL statement and its parameters.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_describe_parameter_encryption"
  - "sp_describe_parameter_encryption_TSQL"
  - "sys.sp_describe_parameter_encryption"
  - "sys.sp_describe_parameter_encryption_TSQL"
helpviewer_keywords:
  - "sp_describe_parameter_encryption"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_describe_parameter_encryption (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Analyzes the specified [!INCLUDE [tsql](../../includes/tsql-md.md)] statement and its parameters, to determine which parameters correspond to database columns that are protected by using the Always Encrypted feature. Returns encryption metadata for the parameters that correspond to encrypted columns.

## Syntax

```syntaxsql
sp_describe_parameter_encryption
    [ @tsql = ] N'tsql'
    [ , [ @params = ] N'@parameter_name data_type [ , ... n ]' ]
[ ; ]
```

## Arguments

#### [ @tsql = ] '*tsql*'

One or more [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. *@tsql* might be **nvarchar(*n*)** or **nvarchar(max)**.

#### [ @params = ] N'*@parameter_name* *data_type* [ ,... *n* ]'

*@params* provides a declaration string for parameters for *@tsql*, which is similar to `sp_executesql`. Parameters might be **nvarchar(*n*)** or **nvarchar(max)**.

A string that contains the definitions of all parameters that are embedded in the [!INCLUDE [tsql](../../includes/tsql-md.md)]_batch. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. *n* is a placeholder that indicates additional parameter definitions. Every parameter specified in the statement must be defined in *@params*. If the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch in the statement doesn't contain parameters, *@params* isn't required. `NULL` is the default value for this parameter.

## Return value

`0` indicates success. Anything else indicates failure.

## Result set

`sp_describe_parameter_encryption` returns two result sets:

- The result set describing cryptographic keys configured for database columns, the parameters of the specified [!INCLUDE [tsql](../../includes/tsql-md.md)] statement correspond to.

- The result set describing how particular parameters should be encrypted. This result set references the keys described in the first result set.

Each row of the first result set describes a pair of keys: an encrypted column encryption key, and its corresponding column master key (CMK).

| Column name | Data type | Description |
| --- | --- | --- |
| `column_encryption_key_ordinal` | **int** | ID of the row in the resultset. |
| `database_id` | **int** | Database ID. |
| `column_encryption_key_id` | **int** | The column encryption key ID.<br /><br />**Note:** this ID denotes a row in the [sys.column_encryption_keys](../system-catalog-views/sys-column-encryption-keys-transact-sql.md) catalog view. |
| `column_encryption_key_version` | **int** | Reserved for future use. Currently, always contains `1`. |
| `column_encryption_key_metadata_version` | **binary(8)** | A timestamp representing the creation time of the column encryption key. |
| `column_encryption_key_encrypted_value` | **varbinary(4000)** | The encrypted value of the column encryption key. |
| `column_master_key_store_provider_name` | **sysname** | The name of the provider for the key store that contains the CMK, which was used to produce the encrypted value of the column encryption key. |
| `column_master_key_path` | **nvarchar(4000)** | The key path of the CMK, which was used to produce the encrypted value of the column encryption key. |
| `column_encryption_key_encryption_algorithm_name` | **sysname** | The name of the encryption algorithm used to produce the encryption value of the column encryption key. |

Each row of the second result set contains encryption metadata for one parameter.

| Column name | Data type | Description |
| --- | --- | --- |
| `parameter_ordinal` | **int** | ID of the row in the result set. |
| `parameter_name` | **sysname** | Name of one of the parameters specified in the *@params* argument. |
| `column_encryption_algorithm` | **tinyint** | Code indicating the encryption algorithm configured for the column the parameter corresponds to. The currently supported value is `2` for `AEAD_AES_256_CBC_HMAC_SHA_256`. |
| `column_encryption_type` | **tinyint** | Code indicating the encryption type configured for the column, the parameter corresponds to. The supported values are:<br /><br />`0` - plaintext (the column isn't encrypted)<br />`1` - deterministic encryption<br />`2` - randomized encryption. |
| `column_encryption_key_ordinal` | **int** | Code of the row in the first result set. The referenced row describes the column encryption key configured for the column, the parameter corresponds to. |
| `column_encryption_normalization_rule_version` | **tinyint** | Version number of the type normalization algorithm. |

## Remarks

A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] client driver, supporting Always Encrypted, automatically calls `sp_describe_parameter_encryption` to retrieve encryption metadata for parameterized queries issued by the application. Then, the driver uses the encryption metadata to encrypt the values of parameters that correspond to database columns protected with Always Encrypted. It substitutes the plaintext parameter values submitted by the application, with the encrypted parameter values, before sending the query to the database engine.

## Permissions

Require the `VIEW ANY COLUMN ENCRYPTION KEY DEFINITION` and `VIEW ANY COLUMN MASTER KEY DEFINITION` permissions in the database.

## Examples

The following example truncates the value for `ENCRYPTED_VALUE`, for display purposes.

```sql
CREATE COLUMN MASTER KEY [CMK1]
WITH (
    KEY_STORE_PROVIDER_NAME = N'MSSQL_CERTIFICATE_STORE',
    KEY_PATH = N'CurrentUser/my/A66BB0F6DD70BDFF02B62D0F87E340288E6F9305'
);
GO

CREATE COLUMN ENCRYPTION KEY [CEK1]
WITH VALUES (
    COLUMN_MASTER_KEY = [CMK1],
    ALGORITHM = 'RSA_OAEP',
    ENCRYPTED_VALUE = 0x016E00000163007500720072<...> -- truncated in this example
);
GO

CREATE TABLE t1 (
    c1 INT ENCRYPTED WITH (
        COLUMN_ENCRYPTION_KEY = [CEK1],
        ENCRYPTION_TYPE = Randomized,
        ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'
        ) NULL,
);

EXEC sp_describe_parameter_encryption
    N'INSERT INTO t1 VALUES(@c1)',
    N'@c1 INT';
```

Here's the first result set:

| Column | Value |
| --- | --- |
| `column_encryption_key_ordinal` | 1 |
| `database_id` | 5 |
| `column_encryption_key_id` | 1 |
| `column_encryption_key_version` | 1 |
| `column_encryption_key_metadata_version` | `0x99EDA60083A50000` |
| `column_encryption_key_encrypted_value` | `0x016E00000163007500720072<...>` |
| `column_master_key_store_provider_name` | `MSSQL_CERTIFICATE_STORE` |
| `column_master_key_path` | `CurrentUser/my/A66BB0F6DD70BDFF02B62D0F87E340288E6F9305` |
| `column_encryption_key_encryption_algorithm_name` | `RSA_OAEP` |

Here's the second result set:

| Column | Value |
| --- | --- |
| `parameter_ordinal` | 1 |
| `parameter_name` | @c1 |
| `column_encryption_algorithm` | 1 |
| `column_encryption_type` | 1 |
| `column_encryption_key_ordinal` | 1 |
| `column_encryption_normalization_rule_version` | 1 |

## Related content

- [Always Encrypted](../security/encryption/always-encrypted-database-engine.md)
- [Develop applications using Always Encrypted](../security/encryption/always-encrypted-client-development.md)
