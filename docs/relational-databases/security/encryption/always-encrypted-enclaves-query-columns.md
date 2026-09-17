---
title: "Run Transact-SQL Statements Using Secure Enclaves"
description: "Run Data Definition Language (DDL) statements to configure encryption for your column or manage indexes on encrypted columns, and query encrypted columns"
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto, randolphwest
ms.date: 09/15/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - ignite-2023
  - sfi-image-nochange
---
# Run Transact-SQL statements using secure enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) allows some Transact-SQL (T-SQL) statements to perform confidential computations on encrypted database columns in a server-side secure enclave.

## Statements using secure enclaves

The following types of T-SQL statement utilize secure enclaves.

### DDL statements using secure enclaves

The following types of [Data Definition Language (DDL)](../../../t-sql/statements/statements.md#data-definition-language) statements require secure enclaves.

- [ALTER TABLE column_definition](../../../t-sql/statements/alter-table-column-definition-transact-sql.md) statements that trigger in-place cryptographic operations using enclave-enabled keys. For more information, see [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md).
- [CREATE INDEX](../../../t-sql/statements/create-index-transact-sql.md) and [ALTER INDEX](../../../t-sql/statements/alter-index-transact-sql.md) statements that create or alter indexes on enclave-enabled columns using randomized encryption. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md).

### DML statements using secure enclaves

The following [Data Manipulation Language (DML)](../../../t-sql/statements/statements.md#data-manipulation-language) statements or queries against enclave-enabled columns using randomized encryption require secure enclaves:

- Queries that use one or more of the following Transact-SQL operators supported inside secure enclaves:
  - [Comparison Operators](../../../mdx/comparison-operators.md)
  - [BETWEEN](../../../t-sql/language-elements/between-transact-sql.md)
  - [IN](../../../t-sql/language-elements/in-transact-sql.md)
  - [LIKE](../../../t-sql/language-elements/like-transact-sql.md)
  - [DISTINCT](../../../t-sql/queries/select-transact-sql.md#c-using-distinct-with-select)
  - [Joins](../../performance/joins.md) - [!INCLUDE [sql-server-2019](../../../includes/sssql19-md.md)] supports only nested loop joins. [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] supports nested loop, hash, and merge joins
  - [SELECT - ORDER BY clase](../../../t-sql/queries/select-order-by-clause-transact-sql.md). Supported in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and Azure SQL Database. Not supported in [!INCLUDE [sql-server-2019](../../../includes/sssql19-md.md)]
  - [SELECT - GROUP BY clase](../../../t-sql/queries/select-group-by-transact-sql.md). Supported in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and Azure SQL Database. Not supported in [!INCLUDE [sql-server-2019](../../../includes/sssql19-md.md)]
- Queries that insert, update, or delete rows, which in turn triggers inserting and/or removing an index key to/from an index on an enclave-enabled column. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)

> [!NOTE]  
> Operations on indexes and confidential DML queries using enclaves are only supported on enclave-enabled columns that use randomized encryption. Deterministic encryption isn't supported.
>
> The [compatibility level](../../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) of the database should be set to SQL Server 2022 (160) or higher.
>
> In [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] and in [!INCLUDE [sql-server-2022](../../../includes/sssql22-md.md)], confidential queries using enclaves on a character string column (`char`, `nchar`) require the column uses a [binary-code point (_BIN2) collation or a UTF-8 collation](../../collations/collation-and-unicode-support.md). In [!INCLUDE [sql-server-2019](../../../includes/sssql19-md.md)], a_BIN2 collation is required.

### DBCC commands using secure enclaves

[DBCC](../../../t-sql/database-console-commands/dbcc-transact-sql.md) administrative commands that involve checking the integrity of indexes might also require secure enclaves if the database contains indexes on enclave-enabled columns using randomized encryption. For example, [DBCC CHECKDB](../../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) and [DBCC CHECKTABLE](../../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md).

## Prerequisites for running statements using secure enclaves

Your environment needs to meet the following requirements to support executing statements that use a secure enclave.

- Your [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance or your database server in [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] must be correctly configured to support enclaves and attestation, if applicable/required. For more information, see [Set up the secure enclave and attestation](configure-always-encrypted-enclaves.md#set-up-the-secure-enclave-and-attestation).
- When you're connecting to your database from an application or a tool (such as SQL Server Management Studio), make sure to:

  - Use a client driver version or a tool version that supports Always Encrypted with secure enclaves.

    - See [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md) for information about client drivers supporting Always Encrypted with secure enclaves.
    - See the following sections for information about tools supporting Always Encrypted with secure enclaves.

  - Enable Always Encrypted for the database connection.
  - Specify an attestation protocol, which determines whether your application or tool must attest the enclave before submitting enclave queries, and which attestation service it should use. Most tools and drivers support the following attestation protocols:

    - Microsoft Azure Attestation - enforces attestation using Microsoft Azure Attestation.
    - Host Guardian Service - enforces attestation using Host Guardian Service.
    - None - allows using enclaves without attestation.

    The below table specifies attestation protocols valid for particular SQL products and enclave technologies:

    | Product | Enclave technology | Supported attestation protocols |
    | --- | --- | --- |
    | [!INCLUDE [sql-server-2019](../../../includes/sssql19-md.md)] and later | VBS enclaves | Host Guardian Service, None |
    | [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] | SGX enclaves (in DC-series databases) | Microsoft Azure Attestation |
    | [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] | VBS enclaves | None |

- Specify an attestation URL that is valid for your environment if you're using attestation.

  - If you're using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), see [Determine and share the HGS attestation URL](always-encrypted-enclaves-host-guardian-service-deploy.md#step-6-determine-and-share-the-hgs-attestation-url).
  - If you're using [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] with Intel SGX enclaves and Microsoft Azure Attestation, see [Determine the attestation URL for your attestation policy](always-encrypted-enclaves.md#secure-enclave-attestation).

<a id="prerequisites-for-running-t-sql-statements-using-enclaves-in-azure-data-studio"></a>

### Prerequisites for running T-SQL statements using enclaves in SSMS

Install the latest version of [SQL Server Management Studio (SSMS)](/ssms/install/install).

Make sure you run your statements from a query window that uses a connection that has Always Encrypted and attestation parameters correctly configured.

1. In the **Connect to Server** dialog, specify your server name, select an authentication method, and specify your credentials.
1. Select **Options >>** and select the **Connection Properties** tab. Specify your database name.
1. Select the **Always Encrypted** tab.
1. Select **Enable Always Encrypted (column encryption)**.
1. Select **Enable secure enclaves**.
1. Set **Protocol** to:

   1. **Host Guardian Service** if you're using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].
   1. **Microsoft Azure Attestation** if you're using [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] with Intel SGX enclaves.
   1. **None** if you're using [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] with VBS enclaves.

1. Specify your enclave attestation URL. Not applicable when the Protocol is set to *None*. For example, `https://hgs.bastion.local/Attestation` or `https://contososqlattestation.uks.attest.azure.net/attest/SgxEnclave`.

   :::image type="content" source="media/always-encrypted-enclaves/ssms-connect-microsoft-azure-attestation.png" alt-text="Screenshot of Connect to server with attestation using SSMS.":::

1. Select **Connect**.
1. If you're prompted to enable Parameterization for Always Encrypted queries, select **Enable**.

For more information, see [Enabling and disabling Always Encrypted for a database connection](always-encrypted-query-columns-ssms.md#en-dis).

## Examples

This section includes examples of DML queries using enclaves.

The examples use the below schema.

```sql
CREATE SCHEMA [HR];
GO

CREATE TABLE [HR].[Jobs](
 [JobID] [int] IDENTITY(1,1) PRIMARY KEY,
 [JobTitle] [nvarchar](50) NOT NULL,
 [MinSalary] [money] ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL,
 [MaxSalary] [money] ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
);
GO

CREATE TABLE [HR].[Employees](
 [EmployeeID] [int] IDENTITY(1,1) PRIMARY KEY,
 [SSN] [char](11) ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL,
 [FirstName] [nvarchar](50) NOT NULL,
 [LastName] [nvarchar](50) NOT NULL,
 [Salary] [money] ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL,
 [JobID] [int] NULL,
 FOREIGN KEY (JobID) REFERENCES [HR].[Jobs] (JobID)
);
```

### Exact match search

The below query performs an exact match search on the encrypted `SSN` string column.

```sql
DECLARE @SSN AS CHAR (11) = '987-65-4320';

SELECT *
FROM [HR].[Employees]
WHERE [SSN] = @SSN;
```

### Pattern matching search

The below query performs a pattern matching search on the encrypted `SSN` string column, searching for employees with the specified last for digits of a social security number.

```sql
DECLARE @SSN AS CHAR (11) = '987-65-4320';

SELECT *
FROM [HR].[Employees]
WHERE [SSN] = @SSN;
```

### Range comparison

The below query performs a range comparison on the encrypted `Salary` column, searching for employees with salaries within the specified range.

```sql
DECLARE @MinSalary AS MONEY = 40000;
DECLARE @MaxSalary AS MONEY = 45000;

SELECT *
FROM [HR].[Employees]
WHERE [Salary] > @MinSalary
      AND [Salary] < @MaxSalary;
```

### Joins

The below query performs a join between `Employees` and `Jobs` tables using the encrypted `Salary` column. The query retrieves employees with salaries outside of a salary range for employee's job.

```sql
SELECT *
FROM [HR].[Employees] AS e
     INNER JOIN [HR].[Jobs] AS j
         ON e.[JobID] = j.[JobID]
        AND e.[Salary] > j.[MaxSalary]
        OR e.[Salary] < j.[MinSalary];
```

### Sorting

The below query sorts employee records based on the encrypted `Salary` column, retrieving 10 employees with the highest salaries.
> [!NOTE]  
> Sorting encrypted columns is supported in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and Azure SQL Database, but not in [!INCLUDE [sql-server-2019](../../../includes/sssql19-md.md)].

```sql
SELECT TOP (10) *
FROM [HR].[Employees]
ORDER BY [Salary] DESC;
```

## Next step

> [!div class="nextstepaction"]
> [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)

## Related content

- [Troubleshoot common issues for Always Encrypted with secure enclaves](always-encrypted-enclaves-troubleshooting.md)
- [Getting started using Always Encrypted with secure enclaves](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)
- [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md)
- [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)
