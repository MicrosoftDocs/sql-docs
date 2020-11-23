---
description: "Configure column encryption using Always Encrypted with a DAC package"
title: "Configure column encryption using Always Encrypted with a DAC package   | Microsoft Docs"
ms.custom: ""
ms.date: 06/26/2019
ms.prod: sql
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Always Encrypted, configure with SSMS"
ms.assetid: 29816a41-f105-4414-8be1-070675d62e84
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure column encryption using Always Encrypted with a DAC package 
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]

A [data-tier application (DAC) package](../../data-tier-applications/data-tier-applications.md), also known as a DACPAC, is a portable unit of SQL Server database deployment that defines all of the SQL Server objects, including tables and columns inside the tables. When you publish a DACPAC to a database (when you upgrade a database using a DACPAC), the schema of the target database gets update to match the schema in the DACPAC. You can publish a DACPAC using [the Upgrade Data-tier Application Wizard](../../data-tier-applications/upgrade-a-data-tier-application.md#UsingDACUpgradeWizard) in SQL Server Management Studio, [PowerShell](../../data-tier-applications/upgrade-a-data-tier-application.md#UpgradeDACPowerShell), or [sqlpackage](../../../tools/sqlpackage.md#publish-parameters-properties-and-sqlcmd-variables).

This article addresses special considerations for upgrading a database when the DACPAC or/and the target database contains columns protected with [Always Encrypted](always-encrypted-database-engine.md). If the encryption scheme for a column in the DACPAC differs from the encryption scheme for an existing column in the target database, publishing the DACPAC results in encrypting, decrypting, or re-encrypting the data stored in the column. See the below table for details.

| Condition|Action|
|:---|:---|
|The column is encrypted in the DACPAC and it isn't encrypted in the database.| The data in the column will be encrypted.|
|The column isn't encrypted in the DACPAC and it's encrypted in the database.| The data in the column will be decrypted (the encryption will be removed for the column).|
| The column is encrypted both in the DACPAC and the database, but the column in the DACPAC uses a different encryption type or/and a different column encryption key than the corresponding column in the database.|The data in the column will be decrypted and then re-encrypted to match the encryption configuration in the DACPAC.|

Deploying a DAC package may also result in creating or removing metadata objects for column master keys or column encryption keys for Always Encrypted.

## Performance considerations
To perform cryptographic operations, a tool you use to deploy a DACPAC needs to move the data out of the database. The tool creates a new table (or tables) with the desired encryption configuration in the database, loads all data from the original tables, performs the requested cryptographic operations, uploads the data to the new table(s), and then swaps the original table(s) with the new table(s). Running cryptographic operations can take a long time. During that time, your database is not available to write transactions. 

::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"

> [!NOTE]
> If you are using [!INCLUDE [sssqlv15-md](../../../includes/sssqlv15-md.md)] and your SQL Server instance is configured with a secure enclave, you can run cryptographic operations in-place, without moving data out of the database. See [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md). Note that in-place encryption is not available for DACPAC deployments.

::: moniker-end

## Permissions for publishing a DAC package if Always Encrypted is set up

To publish DAC package if Always Encrypted is set up in the DACPAC or/and in the target database, you might need some or all of the below permissions, depending on the differences between the schema in the DACPAC and the target database schema.

*ALTER ANY COLUMN MASTER KEY*, *ALTER ANY COLUMN ENCRYPTION KEY*, *VIEW ANY COLUMN MASTER KEY DEFINITION*, *VIEW ANY COLUMN ENCRYPTION KEY DEFINITION*

If the upgrade operation triggers a data encryption operation, you also need to be able to access column master keys configured for the impacted columns:

- **Certificate Store - Local computer** - you must have Read access to the certificate that is used a column master key, or be the administrator on the computer.
- **Azure Key Vault** - you need the *create*, *get*, *unwrapKey*, *wrapKey*, *sign*, and *verify* permissions on the vault containing the column master key.
- **Key Store Provider (CNG)** - you might be prompted for the required permission and credentials when using a key store or a key, depending on the store and the KSP configuration.
- **Cryptographic Service Provider (CAPI)** - you might be prompted for the required permission and credentials when using a key store or a key, depending on the store and the CSP configuration.

For more information, see [Create and Store Column Master Keys (Always Encrypted)](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md). 

 
## Next Steps
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)

## See Also  
 - [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
 - [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md) 
 - [Configure Always Encrypted using SQL Server Management Studio](configure-always-encrypted-using-sql-server-management-studio.md)
 - [Configure column encryption using Always Encrypted Wizard](always-encrypted-wizard.md)
 - [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md)
 
