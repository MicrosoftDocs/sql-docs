---
title: "Troubleshoot common issues for Always Encrypted with secure enclaves"
description: "Troubleshoot common issues for Always Encrypted with secure enclaves"
ms.custom: ""
ms.date: 01/15/2021
ms.reviewer: vanto
ms.service: sql
ms.subservice: security
ms.topic: how-to
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---

# Troubleshoot common issues for Always Encrypted with secure enclaves

This article describes how to identify and resolve common issues you may find when running Transact-SQL (TSQL) statements using [Always Encrypted with secure enclaves](always-encrypted-enclaves.md).

For information on how to run queries using secure enclaves, see [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md).

## Database connection errors

To run statements using a secure enclave, you need to enable Always Encrypted and specify an attestation URL for the database connection, as explained [Prerequisites for running statements using secure enclaves](always-encrypted-enclaves-query-columns.md#prerequisites-for-running-statements-using-secure-enclaves). However, your connection will fail if you specify an attestation URL but your database in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] or your target [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance doesn't support secure enclaves, or is incorrectly configured.

- If you're using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], check that your database uses the [DC-series](/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#dc-series) hardware configuration. For more information, see [Enable Intel SGX for your Azure SQL database](/azure/azure-sql/database/always-encrypted-enclaves-enable-sgx).
- If you're using [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)], check secure enclave is correctly configured for your instance. For more information, see [Configure the secure enclave in SQL Server](always-encrypted-enclaves-configure-enclave-type.md).

## Attestation errors when using Microsoft Azure Attestation

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
  - The attestation URL is incorrect or incomplete. For more information, see [Determine the attestation URL for your attestation policy](/azure/azure-sql/database/always-encrypted-enclaves-configure-attestation#determine-the-attestation-url-for-your-attestation-policy).
  - The attestation provider has been accidentally deleted.
  - The firewall was configured for the attestation provider, but it doesn't allow access to Microsoft services.
  - An intermittent network error causes the attestation provider to be unavailable.
- Your Azure SQL logical server is not authorized to send attestation requests to the attestation provider. Make sure the administrator of your attestation provider has added the database server to the Attestation Reader role. For more information, see [Grant your Azure SQL database server access to your attestation provider](/azure/azure-sql/database/always-encrypted-enclaves-configure-attestation#grant-your-azure-sql-database-server-access-to-your-attestation-provider).
- The validation of the attestation policy fails (in step 3 of the above workflow).
  - An incorrect attestation policy is the likely root cause. Make sure you're using the Microsoft-recommended policy. For more information, see [Create and configure an attestation provider](/azure/azure-sql/database/always-encrypted-enclaves-configure-attestation#create-and-configure-an-attestation-provider).
  - The policy validation may also fail as a result of a security breach compromising the server-side enclave.
- Your client application is unable to connect to the attestation provider and retrieve the public signing key (in step 5). The likely causes include:
  - The configuration of firewalls between your application and the attestation provider may block the connections. To troubleshoot the blocked connection, verify you can connect to the OpenId endpoint of your attestation provider. For example, use a web browser from the machine hosting your application to see if you can connect to the OpenID endpoint. For more information, see [Metadata Configuration - Get](/rest/api/attestation/metadataconfiguration/get).

## Attestation errors when using Host Guardian Service

> [!NOTE]
> This section applies only to [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)].

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

## In-place encryption errors

This section lists common errors you may encounter when using `ALTER TABLE`/`ALTER COLUMN` for in-place encryption (in addition to attestation errors described in earlier sections). For more information, see [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md).

- The column encryption key you're trying to use to encrypt, decrypt, or re-encrypt data isn't an enclave-enabled key. For more information about prerequisites for in-place encryption, see [Prerequisites](always-encrypted-enclaves-configure-encryption.md#prerequisites). For information on how to provision enclave-enabled keys, see [Provision enclave-enabled keys](always-encrypted-enclaves-provision-keys.md).
- You haven't enabled Always Encrypted and enclave computations for the database connection. See [Prerequisites for running statements using secure enclaves](always-encrypted-enclaves-query-columns.md#prerequisites-for-running-statements-using-secure-enclaves).
- Your `ALTER TABLE`/`ALTER COLUMN` statement triggers a cryptographic operation and it changes the column data type or sets a collation with a code page different from the current collation code page. Combining cryptographic operations with data type or collation page changes isn't allowed. To address the problem, use separate statements; one statement to change the data type or collation code page and another statement for in-place encryption.

## Errors when running confidential DML queries using secure enclaves

This section lists common errors you may encounter when you run confidential DML queries using secure enclaves (in addition to attestation errors described in earlier sections).

- The column encryption key configured for the column you're querying isn't an enclave-enabled key.
- You haven't enabled Always Encrypted and enclave computations for the database connection. For more information, see [Prerequisites for running statements using secure enclaves](always-encrypted-enclaves-query-columns.md#prerequisites-for-running-statements-using-secure-enclaves).
- The column you're querying uses deterministic encryption. Confidential DML queries using secure enclaves aren't supported with deterministic encryption. For more information on how to change the encryption type to randomized, see [Enable Always Encrypted with secure enclaves for existing encrypted columns](always-encrypted-enclaves-enable-for-encrypted-columns.md).
- The string column you're querying uses a collation that isn't a BIN2 or UTF-8 collation. Change the collation to BIN2 or UTF-8. For more information, see [DML statements using secure enclaves](always-encrypted-enclaves-query-columns.md#dml-statements-using-secure-enclaves).
- Your query triggers an unsupported operation. For the list of operations supported inside enclaves, see [DML statements using secure enclaves](always-encrypted-enclaves-query-columns.md#dml-statements-using-secure-enclaves).
## Next Steps

- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)

## See also

- [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md).
- [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md)
- [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)