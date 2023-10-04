---
title: "Run Transact-SQL statements using secure enclaves"
description: "Run Data Definition Language (DDL) statements to configure encryption for your column or manage indexes on encrypted columns, and query encrypted columns"
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: 02/15/2023
ms.service: sql
ms.subservice: security
ms.topic: conceptual
---
# Run Transact-SQL statements using secure enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) allows some Transact-SQL (T-SQL) statements to perform confidential computations on encrypted database columns in a server-side secure enclave.

## Statements using secure enclaves

The following types of T-SQL statement utilize secure enclaves.

### DDL statements using secure enclaves

The following types of [Data Definition Language (DDL)](../../../t-sql/statements/statements.md#data-definition-language) statements require secure enclaves.

- [ALTER TABLE column_definition (Transact-SQL)](../../../t-sql/statements/alter-table-column-definition-transact-sql.md) statements that trigger in-place cryptographic operations using enclave-enabled keys. For more information, see [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md).
- [CREATE INDEX (Transact-SQL)](../../../t-sql/statements/create-index-transact-sql.md) and [ALTER INDEX (Transact-SQL)](../../../t-sql/statements/alter-index-transact-sql.md) statements that create or alter indexes on enclave-enabled columns using randomized encryption. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md).
  
### DML statements using secure enclaves

The following [Data Manipulation Language (DML)](../../../t-sql/statements/statements.md#data-manipulation-language) statements or queries against enclave-enabled columns using randomized encryption require secure enclaves:

- Queries that use one or more of the following Transact-SQL operators supported inside secure enclaves:
  - [Comparison Operators](../../../mdx/comparison-operators.md)
  - [BETWEEN (Transact-SQL)](../../../t-sql/language-elements/between-transact-sql.md)
  - [IN (Transact-SQL)](../../../t-sql/language-elements/in-transact-sql.md)
  - [LIKE (Transact-SQL)](../../../t-sql/language-elements/like-transact-sql.md)
  - [DISTINCT](../../../t-sql/queries/select-transact-sql.md#c-using-distinct-with-select)
  - [Joins](../../performance/joins.md) - [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] supports only nested loop joins. [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] supports nested loop, hash, and merge joins
  - [SELECT - ORDER BY Clause (Transact-SQL)](../../../t-sql/queries/select-order-by-clause-transact-sql.md). Supported in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and Azure SQL Database. Not supported in [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)]
  - [SELECT - GROUP BY Clause (Transact-SQL)](../../../t-sql/queries/select-group-by-transact-sql.md). Supported in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and Azure SQL Database. Not supported in [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)]
- Queries that insert, update, or delete rows, which in turn triggers inserting and/or removing an index key to/from an index on an enclave-enabled column. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)

> [!NOTE]
> Operations on indexes and confidential DML queries using enclaves are only supported on enclave-enabled columns that use randomized encryption. Deterministic encryption is not supported.
>
> The [compatibility level](../../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) of the database should be set to SQL Server 2022 (160) or higher.
>
> In [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] and in [!INCLUDE[sql-server-2022](../../../includes/sssql22-md.md)], confidential queries using enclaves on a character string column (`char`, `nchar`) require the column uses a [binary-code point (_BIN2) collation or a UTF-8 collation](../../../relational-databases/collations/collation-and-unicode-support.md). In [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)], a_BIN2 collation is required.

### DBCC commands using secure enclaves

[DBCC (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-transact-sql.md) administrative commands that involve checking the integrity of indexes may also require secure enclaves if the database contains indexes on enclave-enabled columns using randomized encryption. For example, [DBCC CHECKDB (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) and [DBCC CHECKTABLE (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md).

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
    |:---|:---|:---|
    | [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] and later | VBS enclaves | Host Guardian Service, None |
    | [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] | SGX enclaves (in DC-series databases) | Microsoft Azure Attestation |
    | [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] | VBS enclaves (preview)  | None |

- Specify an attestation URL that is valid for your environment if you're using attestation.

  - If you're using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), see [Determine and share the HGS attestation URL](always-encrypted-enclaves-host-guardian-service-deploy.md#step-6-determine-and-share-the-hgs-attestation-url).
  - If you're using [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] with Intel SGX enclaves and Microsoft Azure Attestation, see [Determine the attestation URL for your attestation policy](./always-encrypted-enclaves.md#secure-enclave-attestation).

### Prerequisites for running T-SQL statements using enclaves in SSMS

Download the latest version of [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md).

Make sure you run your statements from a query window that uses a connection that has Always Encrypted and attestation parameters correctly configured.

1. In the **Connect to Server** dialog, specify your server name, select an authentication method, and specify your credentials.
2. Select **Options >>** and select the **Connection Properties** tab. Specify your database name.
3. Select the **Always Encrypted** tab.
4. Select **Enable Always Encrypted (column encryption)**.
5. Select **Enable secure enclaves**.
6. Set **Protocol** to:

    1. **Host Guardian Service** if you're using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].
    1. **Microsoft Azure Attestation** if you're using [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] with Intel SGX enclaves.
    1. **None** if you're using [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] with VBS enclaves.

7. Specify your enclave attestation URL. Not applicable when the Protocol is set to *None*. For example, `https://hgs.bastion.local/Attestation` or `https://contososqlattestation.uks.attest.azure.net/attest/SgxEnclave`.

    ![Connect to server with attestation using SSMS](./media/always-encrypted-enclaves/ssms-connect-microsoft-azure-attestation.png)

8. Select **Connect**.
9. If you're prompted to enable Parameterization for Always Encrypted queries, select **Enable**.

For more information, see [Enabling and disabling Always Encrypted for a database connection](always-encrypted-query-columns-ssms.md#en-dis).

### Prerequisites for running T-SQL statements using enclaves in Azure Data Studio

The minimum recommended version **1.23** or higher is recommended.
> [!NOTE]
> Azure Data Studio currently does not support using VBS enclaves without attestation.

Make sure you run your statements from a query window that uses a connection that has Always Encrypted enabled and both the correct attestation protocol and the attestation URL configured.

1. In the **Connection** dialog, select **Advanced...**.
2. To enable Always Encrypted for the connection, set the **Always Encrypted** field to **Enabled**.
3. Specify the attestation protocol and the attestation URL.
    - If you're using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] set **Attestation Protocol** to **Host Guardian Service** and enter your Host Guardian Service attestation URL in the **Enclave Attestation URL** field.
    - If you're using if you're using a DC-series database with Intel SGX in [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)], set **Attestation Protocol** to **Azure Attestation** and enter the attestation URL referencing your policy in Microsoft Azure Attestation in the **Enclave Attestation URL** field.

    ![Connect to server with attestation using Azure Data Studio](./media/always-encrypted-enclaves/azure-data-studio-connect-with-enclaves.png)

4. Select **OK** to close **Advanced Properties**.

For more information, see [Enabling and disabling Always Encrypted for a database connection](always-encrypted-query-columns-ads.md#enabling-and-disabling-always-encrypted-for-a-database-connection).

If you plan to run parameterized DML queries, you also need to enable [Parameterization for Always Encrypted](always-encrypted-query-columns-ads.md#parameterization-for-always-encrypted).

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
GO
```

### Exact match search

The below query performs an exact match search on the encrypted `SSN` string column.

```sql
DECLARE @SSN char(11) = '795-73-9838';
SELECT * FROM [HR].[Employees] WHERE [SSN] = @SSN;
GO
```

### Pattern matching search

The below query performs a pattern matching search on the encrypted `SSN` string column, searching for employees with the specified last for digits of a social security number.

```sql
DECLARE @SSN char(11) = '795-73-9838';
SELECT * FROM [HR].[Employees] WHERE [SSN] = @SSN;
GO
```

### Range comparison

The below query performs a range comparison on the encrypted  `Salary`  column, searching for employees with salaries within the specified range.

```sql
DECLARE @MinSalary money = 40000;
DECLARE @MaxSalary money = 45000;
SELECT * FROM [HR].[Employees] WHERE [Salary] > @MinSalary AND [Salary] < @MaxSalary;
GO
```

### Joins

The below query performs a join between `Employees` and `Jobs` tables using the encrypted `Salary` column. The query retrieves employees with salaries outside of a salary range for employee's job.

```sql
SELECT * FROM [HR].[Employees] e
JOIN [HR].[Jobs] j
ON e.[JobID] = j.[JobID] AND e.[Salary] > j.[MaxSalary] OR e.[Salary] < j.[MinSalary];
GO
```

### Sorting

The below query sorts employee records based on the encrypted `Salary` column, retrieving 10 employees with the highest salaries.
> [!NOTE]
> Sorting encrypted columns is supported in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and Azure SQL Database, but not in [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)].

```sql
SELECT TOP(10) * FROM [HR].[Employees]
ORDER BY [Salary] DESC;
GO
```

## Next steps

- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)

## See also

- [Troubleshoot common issues for Always Encrypted with secure enclaves](always-encrypted-enclaves-troubleshooting.md)
- [Getting started using Always Encrypted with secure enclaves](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)
- [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md)
- [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)
