---
description: "Run DDL statements to configure encryption for your column or manage indexes on encrypted columns, and query encrypted columns"
title: "Query columns using Always Encrypted with secure enclaves | Microsoft Docs"
ms.custom: ""
ms.date: 12/09/2020
ms.reviewer: vanto
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Run Transact-SQL statements using secure enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) allows  some Transact-SQL statements to perform confidential computations on encrypted database columns in a server-side secure enclave.

## Statements using secure enclaves

The following types of Transact-SQL statement utilize secure enclaves.

- The following types of [Data Definition Language (DDL)](../../../t-sql/statements/statements.md#data-definition-language) statements:
  - [ALTER TABLE column_definition (Transact-SQL)](../../../t-sql/statements/alter-table-column-definition-transact-sql.md) statements that trigger in-place cryptographic operations using enclave-enabled keys - see [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md).
  - [CREATE INDEX (Transact-SQL)](../../../t-sql/statements/create-index-transact-sql.md), [ALTER INDEX (Transact-SQL)](../../../t-sql/statements/alter-index-transact-sql.md) statements that create or alter indexes on enclave-enabled columns using randomized encryption. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md).
  
- [Data Manipulation Language (DML)](../../../t-sql/statements/statements.md#data-manipulation-language) statements/queries against enclave-enabled columns using randomized encryption that:
  - Use one or more of the following Transact-SQL operators:
    - [Comparison Operators](../../../mdx/comparison-operators.md).
    - [BETWEEN (Transact-SQL)](../../../t-sql/language-elements/between-transact-sql.md).
    - [IN (Transact-SQL)](../../../t-sql/language-elements/in-transact-sql.md).
    - [LIKE (Transact-SQL)](../../../t-sql/language-elements/like-transact-sql.md).
    - [DISTINCT](../../../t-sql/queries/select-transact-sql.md#c-using-distinct-with-select).
    - [Joins](../../performance/joins.md). [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)] supports only nested loop joins. [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] supports nested loop, hash and merge joins.
    - [SELECT - ORDER BY Clause (Transact-SQL)](../../../t-sql/queries/select-order-by-clause-transact-sql.md). Supported in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]. Not supported in [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)].
    - [SELECT - GROUP BY Clause (Transact-SQL)](../../../t-sql/queries/select-group-by-transact-sql.md). Supported in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]. Not supported in [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)].
  - Insert, update or delete rows, which in turn triggers inserting and/or removing an index key to/from an index on an enclave-enabled column. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md).
- [DBCC (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-transact-sql.md) administrative commands that involve checking the integrity of indexes on enclave-enabled columns using randomized encryption, for example [DBCC CHECKDB (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) or [DBCC CHECKTABLE (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md).

> [!NOTE]
> Operations on indexes and confidential DML queries using enclaves are only supported on enclave-enabled columns that use randomized encryption. Deterministic encryption is not supported.

> [!NOTE]
> In [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)], confidential queries using enclaves on character string columns (`char`, `nchar`) require a binary2 sort order (BIN2) collation is configured for the column. In [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], using BIN2 or UTF-8 collations is required.

## Pre-requisites for running statements using secure enclaves

Your environment needs to meet te following requirements to support executing statements that use a secure enclave.

- Your [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] instance or your database and server in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] must be correctly configured to support enclaves and attestation. See [Set up the secure enclave and attestation](configure-always-encrypted-enclaves.md#set-up-the-secure-enclave-and-attestation).
- You need to obtain an attestation URL for your environment for your attestation service administrator.

  - If you are using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and Host Guardian Service, see [Deploy the Host Guardian Service for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]](always-encrypted-enclaves-host-guardian-service-deploy.md).
  - If you are using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], see [Determine the attestation URL for your attestation policy](always-encrypted-enclaves-sqldbmi-configure-attestation.md#determine-the-attestation-url-for-your-attestation-policy).

- If you are connecting to your database using your application, it must use a client driver that supports Always Encrypted with secure enclaves and it must connect to the database with Always Encrypted enabled for the database connection and the attestation protocol and the attestation URL properly configured. For detailed information, see [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md).
- If you are using SQL Server Management Studio or Azure SQL Data Studio, you need to enable Always Encrypted and configure the attestation protocol and the attestation URL when connecting to your database. See the following sections for details.

## Run Transact-SQL statements using enclaves in SSMS

To use SQL Server Management Studio to run statements using secure enclaves, follow the instructions in [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md). Here are a few things that are specific to enclaves you should be aware of:

Minimum version: SSMS 18.7.2 or higher is recommended.

You need to make sure you run your statements from a query window that uses a connection that has Always Encrypted and the correct attestation URL configured. 

1. In the **Connect to Server** dialog, specify your server name, select an authentication method and specify your credentials.
2. Click **Options >>** and select the **Always Encrypted** tab.
3. Select the **Enable Always Encrypted (column encryption)** checkbox and specify your enclave attestation URL. For example ht<span>tps://</span>hgs.bastion.local/Attestation or ht<span>tps://MyAttestationProvider.us.attest.azure.net/attest/SgxEnclave?api-version=2018-09-01-preview.
4. Select **Connect**.
5. If you are prompted to enable Parameterization for Always Encrypted queries, select **Enable** if you plan to run parameterized DML queries.

## Run Transact-SQL statements using enclaves in Azure Data Studio

To use Azure Data Studio to run statements using secure enclaves, follow the instructions in [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md). Here are a few things that are specific to enclaves you should be aware of:

Minimum version: 1.23 or higher is recommended.

You need to make sure you run your statements from a query window that uses a connection that has Always Encrypted enabled and both the correct attestation protocol and the attestation URL configured.

1. In the **Connection** dialog, click **Advanced...**.
2. To enable Always Encrypted for the connection, set the **Always Encrypted** field to **Enabled**. 
3. Specify the attestation protocol and the attestation URL.
    - If you're using [!INCLUDE [sssqlv15-md](../../../includes/sssqlv15-md.md)] set **Attestation Protocol** to **Host Guardian Service** and enter your Host Guardian Service attestation URL in the **Enclave Attestation URL** field.
    - If you're using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], set **Attestation Protocol** to **Azure Attestation** and enter the attestation URL referencing your policy in Microsoft Azure Attestation in the **Enclave Attestation URL** field.

6. Click **OK** to close **Advanced Properties**.

## Troubleshooting common issues when running statements using enclaves

### Database connection errors

To run statements using a secure enclave, you need to enable Always Encrypted and specify an attestation URL for the database connection. Your connection will fail if you specify an attestation URL but your database in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] or your target [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] instance does not support secure enclaves or is incorrectly configured.

- If you are using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], check your database uses the [DC-series](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#dc-series) hardware configuration. See [Enable Intel SGX for your Azure SQL database](always-encrypted-enclaves-sqldbmi-enable-sgx.md) for more details.
- If you are using [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)], check the secure enclave is correctly configured for your instance. For details, see [Configure the secure enclave in SQL Server](always-encrypted-enclaves-configure-enclave-type.md)

### Attestation errors

When you or your application attempts to run a Transact-SQL statement using the secure enclave, the client driver inside your tool or inside the application triggers enclave attestation using an attestation service. [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] supports Host Guardian Service , while !INCLUDE[ssde-md](../../../includes/ssde-md.md)] supports Microsoft Azure Attestation for attestation.

Attestation involves the following steps:

1. The client driver passes the attestation URL, specified in the database connection to, [!INCLUDE[ssde-md](../../../includes/ssde-md.md)].
2. [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] collects the evidence about the enclave, its hosting environment and the code running inside the enclave and sends it to the attestation service referenced in the attestation URL. 
3. The attestation service validates the evidence and issues an attestation report or an attestation token to [!INCLUDE[ssde-md](../../../includes/ssde-md.md)]. The attestation report/token is signed with the attestation service private key.
4. [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] sends the attestation report/token back to the client driver.
5. The client contacts the attestation service at the specified attestation URL to retrieve the public key and it verifies the attestation report/token.

### Attestation errors when using Microsoft Azure Attestation

The common attestation failures are:

- Your Azure SQL database server is unable to connect to the attestation provider in Azure Attestation, specified in the attestation URL. The likely causes include:
  - The attestation URL is incorrect or incomplete. See [Determine the attestation URL for your attestation policy](always-encrypted-enclaves-sqldbmi-configure-attestation.md#determine-the-attestation-url-for-your-attestation-policy) for details.
    - The attestation provider has been accidentally deleted.
    - The firewall was configured for the attestation provider, but it doesn't allow access to Microsoft services.
    - An intermittent network error causes the attestation provider to be unavailable.
- The validation of the attestation policy fails. 
  - The most likely root cause is the configured attestation policy is inconsistent with the Microsoft-recommended policy. See [Create and configure an attestation provider](always-encrypted-enclaves-sqldbmi-configure-attestation.md#create-and-configure-an-attestation-provider).
  - The policy validation may also fail as a result of a security breach compromising the server-side enclave.



TITLE: Connect to Server
------------------------------

Cannot connect to jaszymastest99.database.windows.net.

------------------------------
ADDITIONAL INFORMATION:
ss
You have specified the enclave attestation URL in the connection string, but the SQL Server instance in use does not support enclave based computations. (System.Data)

------------------------------
BUTTONS:

OK
------------------------------


## See Also
- [Tutorial: Getting started with Always Encrypted with secure enclaves in SQL Server](../tutorial-getting-started-with-always-encrypted-enclaves.md)
- [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](../tutorial-always-encrypted-enclaves-getting-started-sqldb.md)

