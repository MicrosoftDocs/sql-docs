---
title: Connect to Server (Always Encrypted page) - Database Engine
description: This article describes how to use the Connect to Server (Always Encrypted page) Database Engine.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 11/22/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.connecttosqlserver.alwaysencrypted.f1"
---

# Connect to Server (Always Encrypted page) - Database Engine

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Use this tab to view or specify Always Encrypted column encryption options when you connect to Azure SQL Database, Azure SQL Managed Instance, or SQL Server. Use this tab to view or specify Always Encrypted secure enclave options when you connect to Azure SQL Database or SQL Server. Access this tab by selecting **Options >>** on the **Login** tab.

## Always Encrypted tab

The **Always Encrypted** tab has two checkboxes.

#### Enable Always Encrypted (column encryption)

By default, Always Encrypted is disabled for a database connection. Enabling Always Encrypted for a database connection instructs the .NET Framework Data Provider for SQL Server, used by SQL Server Management Studio (SSMS), to attempt to transparently:

- Decrypt any values that are retrieved from encrypted columns and returned in query results.
- Encrypt the values of the parameterized Transact-SQL variables that target encrypted database columns.

SSMS uses the .NET Framework Data Provider for SQL Server. If you don't enable Always Encrypted for a connection, the .NET Framework Data Provider doesn't try to encrypt query parameters or decrypt results. For more information, see [Query columns using Always Encrypted with SQL Server Management Studio](../../relational-databases/security/encryption/always-encrypted-query-columns-ssms.md).

#### Enable secure enclaves

Enable this option when you connect to a database configured to use Always Encrypted with secure enclaves. For more information, see [Getting started using Always Encrypted with secure enclaves](/azure/azure-sql/database/always-encrypted-enclaves-getting-started).

## Enclave attestation

The **Enclave attestation** section has two options.

#### Protocol

Select the attestation protocol to use when you connect to a database configured to use Always Encrypted with secure enclaves. The protocol value determines:

- If the client app uses attestation, and, if so,
- It specifies the type of the attestation service it uses.

Available options are **None**, **Host Guardian Service**, and **Microsoft Azure Attestation**. For more information, see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md#secure-enclave-attestation).

#### URL

Enter the configured URL for the selected attestation protocol. The URL is specific to the type of enclave and SQL in use.

- [Azure SQL Database with SGX enclaves](/azure/azure-sql/database/always-encrypted-enclaves-configure-attestation#determine-the-attestation-url-for-your-attestation-policy)
- [SQL Server](../../relational-databases/security/encryption/always-encrypted-enclaves-host-guardian-service-deploy.md)

Attestation isn't supported for Azure SQL Database with VBS enclaves. The URL option is disabled if **None** is selected for **Protocol**.

## Related content

- [Tutorial: Getting started using Always Encrypted with secure enclaves in SQL Server with attestation using HGS](../../relational-databases/security/tutorial-getting-started-with-always-encrypted-enclaves-hgs.md)
- [Tutorial: Getting started using Always Encrypted with Intel SGX enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started-sgx)
- [Tutorial: Getting started using Always Encrypted with VBS enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started-vbs)
