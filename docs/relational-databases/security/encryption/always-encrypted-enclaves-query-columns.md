---
title: "Query columns using Always Encrypted with secure enclaves"
description: "Run Data Definition Language (DDL) statements to configure encryption for your column or manage indexes on encrypted columns, and query encrypted columns"
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
  - [DISTINCT](../../../t-sql/queries/select-transact-sql.md#c-using-distinct-with-select).
  - [Joins](../../performance/joins.md) - [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)] supports only nested loop joins. [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] supports nested loop, hash, and merge joins
  - [SELECT - ORDER BY Clause (Transact-SQL)](../../../t-sql/queries/select-order-by-clause-transact-sql.md). Supported in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]. Not supported in [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)]
  - [SELECT - GROUP BY Clause (Transact-SQL)](../../../t-sql/queries/select-group-by-transact-sql.md). Supported in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]. Not supported in [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)]
- Queries that insert, update, or delete rows, which in turn triggers inserting and/or removing an index key to/from an index on an enclave-enabled column. For more information, see [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md).

> [!NOTE]
> Operations on indexes and confidential DML queries using enclaves are only supported on enclave-enabled columns that use randomized encryption. Deterministic encryption is not supported.
>
> In [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)], confidential queries using enclaves on character string columns (`char`, `nchar`) require a binary2 sort order (BIN2) collation configured for the column. In [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], using BIN2 or UTF-8 collations is required.

### DBCC commands using secure enclaves

[DBCC (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-transact-sql.md) administrative commands that involve checking the integrity of indexes may also require secure enclaves, if the database contains indexes on enclave-enabled columns using randomized encryption. For example, [DBCC CHECKDB (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) and [DBCC CHECKTABLE (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md).

## Prerequisites for running statements using secure enclaves

Your environment needs to meet the following requirements to support executing statements that use a secure enclave.

- Your [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance or your database and server in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] must be correctly configured to support enclaves and attestation. For more information, see [Set up the secure enclave and attestation](configure-always-encrypted-enclaves.md#set-up-the-secure-enclave-and-attestation).
- You need to obtain an attestation URL for your environment for your attestation service administrator.

  - If you're using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), see [Determine and share the HGS attestation URL](always-encrypted-enclaves-host-guardian-service-deploy.md#step-6-determine-and-share-the-hgs-attestation-url).
  - If you're using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], see [Determine the attestation URL for your attestation policy](always-encrypted-enclaves-sqldbmi-configure-attestation.md#determine-the-attestation-url-for-your-attestation-policy).

- If you're connecting to your database using your application, it must use a client driver that supports Always Encrypted with secure enclaves. The application must connect to the database with Always Encrypted enabled for the database connection and the attestation protocol and the attestation URL properly configured. For detailed information, see [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md).
- If you're using SQL Server Management Studio (SSMS) or Azure SQL Data Studio, you need to enable Always Encrypted and configure the attestation protocol and the attestation URL when connecting to your database. See the following sections for details.

> [!NOTE]
> Connecting to the database with Always Encrypted and attestation configured is not required for the following operations if you are using cached column encryption keys: DDL queries that create or alter indexes, DML queries that update indexes, and DBCC commands that check index integrity. For more information, see [Invoke indexing operations using cached column encryption keys](always-encrypted-enclaves-create-use-indexes.md#invoke-indexing-operations-using-cached-column-encryption-keys).

### Prerequisites for running T-SQL statements using enclaves in SSMS

To use SQL Server Management Studio to run statements using secure enclaves, follow the instructions in [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md). Here are a few things that are specific to enclaves you should be aware of:

Minimum version: **SSMS 18.7.2** or higher is recommended.

Make sure you run your statements from a query window that uses a connection that has Always Encrypted and the correct attestation URL configured.

1. In the **Connect to Server** dialog, specify your server name, select an authentication method, and specify your credentials.
2. Click **Options >>** and select the **Always Encrypted** tab.
3. Select the **Enable Always Encrypted (column encryption)** checkbox and specify your enclave attestation URL. For example, ht<span>tps://</span>hgs.bastion.local/Attestation or ht<span>tps://MyAttestationProvider.us.attest.azure.net/attest/SgxEnclave?api-version=2018-09-01-preview.
4. Select **Connect**.
5. If you're prompted to enable Parameterization for Always Encrypted queries, select **Enable** if you plan to run parameterized DML queries.

### Prerequisites for running T-SQL statements using enclaves in Azure Data Studio

To use Azure Data Studio to run statements using secure enclaves, follow the instructions in [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md). Here are a few things that are specific to enclaves you should be aware of:

Minimum version: **1.23** or higher is recommended.

Make sure you run your statements from a query window that uses a connection that has Always Encrypted enabled and both the correct attestation protocol and the attestation URL configured.

1. In the **Connection** dialog, click **Advanced...**.
2. To enable Always Encrypted for the connection, set the **Always Encrypted** field to **Enabled**.
3. Specify the attestation protocol and the attestation URL.
    - If you're using [!INCLUDE [sssqlv15-md](../../../includes/sssqlv15-md.md)] set **Attestation Protocol** to **Host Guardian Service** and enter your Host Guardian Service attestation URL in the **Enclave Attestation URL** field.
    - If you're using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], set **Attestation Protocol** to **Azure Attestation** and enter the attestation URL referencing your policy in Microsoft Azure Attestation in the **Enclave Attestation URL** field.

4. Click **OK** to close **Advanced Properties**.

## Troubleshoot common issues when running statements using enclaves

This section lists common issues you may find when running T-SQL statements using secure enclaves.

### Database connection errors

To run statements using a secure enclave, you need to enable Always Encrypted and specify an attestation URL for the database connection, as explained in earlier sections. However, your connection will fail if you specify an attestation URL but your database in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] or your target [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]  instance doesn't support secure enclaves, or is incorrectly configured.

- If you're using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], check that your database uses the [DC-series](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#dc-series) hardware configuration. For more information, see [Enable Intel SGX for your Azure SQL database](always-encrypted-enclaves-sqldbmi-enable-sgx.md).
- If you're using [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)], check secure enclave is correctly configured for your instance. For more information, see [Configure the secure enclave in SQL Server](always-encrypted-enclaves-configure-enclave-type.md).

### Attestation errors when using Microsoft Azure Attestation

> [!NOTE]
> This section applies only to [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)].

Before a client driver submits a T-SQL statement to Azure SQL logical server for execution, the driver triggers the following enclave attestation workflow using Microsoft Azure Attestation.

1. The client driver passes the attestation URL, specified in the database connection, to the Azure SQL logical server.
2. The Azure SQL logical server collects the evidence about the enclave, its hosting environment, and the code running inside the enclave. The server then sends an attestation request to the attestation provider, referenced in the attestation URL.
3. The attestation provider validates the evidence against the configured policy and issues an attestation token to the Azure SQL logical server. The attestation provider signs the attestation token with its private key.
4. The Azure SQL logical server sends the attestation token to the client driver.
5. The client contacts the attestation provider at the specified attestation URL to retrieve its public key and it verifies the signature in the attestation token.

Errors can occur at various steps of the above workflow due to misconfigurations. Here are the common attestation errors, their root causes, and the recommended troubleshooting steps:

- Your Azure SQL logical server is unable to connect to the attestation provider in Azure Attestation (step 2 of the above workflow), specified in the attestation URL. The likely causes include:
  - The attestation URL is incorrect or incomplete. For more information, see [Determine the attestation URL for your attestation policy](always-encrypted-enclaves-sqldbmi-configure-attestation.md#determine-the-attestation-url-for-your-attestation-policy).
  - The attestation provider has been accidentally deleted.
  - The firewall was configured for the attestation provider, but it doesn't allow access to Microsoft services.
  - An intermittent network error causes the attestation provider to be unavailable.
- Your Azure SQL logical server is not authorized to send attestation requests to the attestation provider. Make sure the administrator of your attestation provider has added the database server to the Attestation Reader role. For more information, see [Grant your Azure SQL database server access to your attestation provider](always-encrypted-enclaves-sqldbmi-configure-attestation.md#grant-your-azure-sql-database-server-access-to-your-attestation-provider).
- The validation of the attestation policy fails (in step 3 of the above workflow).
  - An incorrect attestation policy is the likely root cause. Make sure you're using the Microsoft-recommended policy. For more information, see [Create and configure an attestation provider](always-encrypted-enclaves-sqldbmi-configure-attestation.md#create-and-configure-an-attestation-provider).
  - The policy validation may also fail as a result of a security breach compromising the server-side enclave.
- Your client application is unable to connect to the attestation provider and retrieve the public signing key (in step 5). The likely causes include:
  - The configuration of firewalls between your application and the attestation provider may block the connections. To troubleshoot the blocked connection, verify you can connect to the OpenId endpoint of your attestation provider. For example, use a web browser from the machine hosting your application to see if you can connect to the OpenID endpoint. For more information, see [Metadata Configuration - Get](https://docs.microsoft.com/rest/api/attestation/metadataconfiguration/get).

### Attestation errors when using Host Guardian Service

> [!NOTE]
> This section applies only to [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)].

Before a client driver submits a T-SQL statement to [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] for execution, the driver triggers the following enclave attestation workflow using Host Guardian Service (HGS).

1. The client driver calls [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to initiate attestation.
2. [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] collects the evidence about its enclave, its hosting environment, and the code running inside the enclave. [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] request a health certificate from the HGS instance, the machine hosting [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] was registered with. For more information, see [Register computer with Host Guardian Service](always-encrypted-enclaves-host-guardian-service-register.md).
3. HGS validates the evidence and issues the health certificate to [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]. HGS signs the health certificate with its private key.
4. [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] sends the health certificate to the client driver.
5. The client driver contacts HGS at the attestation URL, specified for the database connection, to retrieve the HGS public key. The client driver verifies the signature in the health certificate.

Errors can occur at various steps in the above workflow due to misconfigurations. Here are some common attestation errors, their root causes, and recommended troubleshooting steps:

- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] is unable to connect to HGS (step 2 of the above workflow), due to an intermittent network error. To troubleshoot the connection issue, the administrator of the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer should verify the computer can connect to the HGS machine.
- The validation in step 3 fails. To troubleshoot the validation issue:
  - The [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]  computer administrator should work with the client application administrator to verify the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer is registered with same HGS instance as the instance referenced in the attestation URL on the client side.
  - The [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]  computer administrator should confirm the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]  computer can successfully attest, by following the instructions in [Step 5: Confirm the host can attest successfully](always-encrypted-enclaves-host-guardian-service-register.md#step-5-confirm-the-host-can-attest-successfully).
- Your client application is unable to connect to HGS and retrieve its public signing key (in step 5). The likely cause is:
  - The configuration of one of the firewalls between your application and the attestation provider may block the connections. Verify the machine hosting your app can connect to the HGS machine.

### In-place encryption errors

This section lists common errors you may encounter when using `ALTER TABLE`/`ALTER COLUMN` for in-place encryption (in addition to attestation errors described in earlier sections). For more information, see [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md).

- The column encryption key you're trying to use to encrypt, decrypt, or re-encrypt data isn't an enclave-enabled key. For more information about in-lace encryption, see [Prerequisites](always-encrypted-enclaves-configure-encryption.md#prerequisites). For information on how to provision enclave-enabled keys, see [Provision enclave-enabled keys](always-encrypted-enclaves-provision-keys.md).
- You haven't enabled Always Encrypted and enclave computations for the database connection. See [Prerequisites for running statements using secure enclaves](always-encrypted-enclaves-query-columns.md#prerequisites-for-running-statements-using-secure-enclaves).
- Your `ALTER TABLE`/`ALTER COLUMN` statement triggers a cryptographic operation and it changes the column data type or sets a collation with a code page different from the current collation code page. Combining cryptographic operations with data type or collation page changes isn't allowed. To address the problem, use separate statements; one statement to change the data type or collation code page and another statement for in-place encryption.

### Errors when running confidential DML queries using secure enclaves

This section lists common errors you may encounter when you run confidential DML queries using secure enclaves (in addition to attestation errors described in earlier sections).

- The column encryption key configured for the column you're querying isn't an enclave-enabled key.
- You haven't enabled Always Encrypted and enclave computations for the database connection. For more information, see [Prerequisites for running statements using secure enclaves](always-encrypted-enclaves-query-columns.md#prerequisites-for-running-statements-using-secure-enclaves).
- The column you're querying uses deterministic encryption. Confidential DML queries using secure enclaves aren't supported with deterministic encryption. For more information on how to change the encryption type to randomized, see [Enable Always Encrypted with secure enclaves for existing encrypted columns](always-encrypted-enclaves-enable-for-encrypted-columns.md).
- The string column you're querying uses a collation that isn't a BIN2 or UTF-8 collation. Change the collation to BIN2 or UTF-8. For more information, see [DML statements using secure enclaves](#dml-statements-using-secure-enclaves).
- Your query triggers an unsupported operation. For the list of operations supported inside enclaves, see [DML statements using secure enclaves](#dml-statements-using-secure-enclaves).

## See also

- [Tutorial: Getting started with Always Encrypted with secure enclaves in SQL Server](../tutorial-getting-started-with-always-encrypted-enclaves.md)
- [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)