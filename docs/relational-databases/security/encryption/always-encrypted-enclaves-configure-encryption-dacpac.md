---
title: "Configure column encryption using Always Encrypted with secure enclaves with a DAC package"
description: "Configure column encryption using Always Encrypted with secure enclaves with a DAC package"
author: PieterVanhove
ms.author: pivanho
ms.reviewer: vanto
ms.date: 04/05/2023
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "Always Encrypted, configure with DACPAC"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure column encryption in-place with DAC package 
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]

A [data-tier application (DAC) package](../../data-tier-applications/data-tier-applications.md), also known as a DACPAC, is a portable unit of SQL Server database deployment that defines all of the SQL Server objects, including tables and columns inside the tables. When you publish a DACPAC to a database (when you upgrade a database using a DACPAC), the schema of the target database gets update to match the schema in the DACPAC. You can publish a DACPAC using [the Upgrade Data-tier Application Wizard](../../data-tier-applications/upgrade-a-data-tier-application.md#UsingDACUpgradeWizard) in SQL Server Management Studio, [PowerShell](../../data-tier-applications/upgrade-a-data-tier-application.md#UpgradeDACPowerShell), or [sqlpackage](../../../tools/sqlpackage/sqlpackage-publish.md).

This article addresses special considerations for upgrading a database when the DACPAC or/and the target database contains columns protected with [Always Encrypted](always-encrypted-database-engine.md). If the encryption scheme for a column in the DACPAC differs from the encryption scheme for an existing column in the target database, publishing the DACPAC results in encrypting, decrypting, or re-encrypting the data stored in the column. See the below table for details.

| Condition|Action|
|:---|:---|
|The column is encrypted in the DACPAC and it isn't encrypted in the database.| The data in the column will be encrypted.|
|The column isn't encrypted in the DACPAC and it's encrypted in the database.| The data in the column will be decrypted (the encryption will be removed for the column).|
| The column is encrypted both in the DACPAC and the database, but the column in the DACPAC uses a different encryption type or/and a different column encryption key than the corresponding column in the database.|The data in the column will be decrypted and then re-encrypted to match the encryption configuration in the DACPAC.|

Deploying a DAC package may also result in creating or removing metadata objects for column master keys or column encryption keys for Always Encrypted.

::: moniker range=">=sql-server-ver15"

> [!NOTE]
> If you are using [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] or later or Azure SQL Database, and your SQL Server instance or database is configured with a secure enclave, you can run cryptographic operations in-place, without moving data out of the database. See [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md). To trigger in-place encryption with a DAC package, the user needs to specify the **EnclaveAttestationProtocol** and **EnclaveAttestationUrl** properties. 

::: moniker-end

## Permissions for publishing a DAC package if Always Encrypted with secure enclaves is set up

To publish DAC package if Always Encrypted with secure enclaves is set up in the DACPAC or/and in the target database, you might need some or all of the below permissions, depending on the differences between the schema in the DACPAC and the target database schema.

*ALTER ANY COLUMN MASTER KEY*, *ALTER ANY COLUMN ENCRYPTION KEY*, *VIEW ANY COLUMN MASTER KEY DEFINITION*, *VIEW ANY COLUMN ENCRYPTION KEY DEFINITION*

If the upgrade operation triggers a data encryption operation, you also need key store permissions to access and use your column master key. For detailed information on key store permissions, go to [Provision enclave-enabled keys](always-encrypted-enclaves-provision-keys.md) and find a section relevant for your key store.

 
## Next steps
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)
- [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns-ssms.md)

## See also  
 - [Always Encrypted with secure enclaves](../../../relational-databases/security/encryption/always-encrypted-enclaves.md)
 - [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md) 
 - [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
 - [Configure column encryption in-place with PowerShell](always-encrypted-enclaves-configure-encryption-powershell.md)
 - [Configure column encryption in-place with the Always Encrypted wizard in SSMS](always-encrypted-wizard.md)
