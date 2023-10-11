---
title: "Develop applications using Always Encrypted with secure enclaves"
description: "Develop applications using Always Encrypted with secure enclaves"
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: "02/15/2023"
ms.service: sql
ms.subservice: security
ms.topic: conceptual
dev_langs:
  - "CSharp"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Develop applications using Always Encrypted with secure enclaves
[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) extends [Always Encrypted](always-encrypted-database-engine.md) to enable richer functionality of application queries on encrypted sensitive database columns. It leverages secure enclave technologies to allow the query executor in [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] to delegate computations on encrypted columns to a secure enclave inside the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] process.

## Prerequisites

Your environment needs to meet the following requirements to support Always Encrypted with secure enclaves.

- Your [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance or your database server in [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] must be correctly configured to support enclaves and attestation, if applicable/required. For more information, see [Set up the secure enclave and attestation](configure-always-encrypted-enclaves.md#set-up-the-secure-enclave-and-attestation).
- Make sure your application:
  - Uses a client driver version supports Always Encrypted with secure enclaves.
  - Enables Always Encrypted when connecting to your database.
  - Sets an attestation protocol, which determines whether the client driver must attest the enclave before submitting enclave queries, and if so, which attestation service it should use. Most recent driver versions support the following attestation protocols:

    - Microsoft Azure Attestation - enforces attestation using Microsoft Azure Attestation.
    - Host Guardian Service - enforces attestation using Host Guardian Service.
    - None - allows using enclaves without attestation.

    The below table specifies attestation protocols valid for particular SQL products and enclave technologies:

    | Product | Enclave technology | Supported attestation protocols |
    |:---|:---|:---|
    | [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] and later | VBS enclaves | Host Guardian Service, None |
    | [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] | SGX enclaves (in DC-series databases) | Microsoft Azure Attestation |
    | [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] | VBS enclaves (preview)  | None |

  - Sets an attestation URL that is valid for your environment, if you're using attestation.

    - If you're using [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), see [Determine and share the HGS attestation URL](always-encrypted-enclaves-host-guardian-service-deploy.md#step-6-determine-and-share-the-hgs-attestation-url).
    - If you're using [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] with Intel SGX enclaves and Microsoft Azure Attestation, see [Determine the attestation URL for your attestation policy](./always-encrypted-enclaves.md#secure-enclave-attestation).

## Client drivers for Always Encrypted with secure enclaves

To develop applications using Always Encrypted with secure enclaves, you need a SQL client driver version that supports secure enclaves. The client driver plays the following key role:

- Before submitting a query that uses a secure enclave to [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)] for execution, the driver initiates enclave attestation (if it's configured) to verify the secure enclave is trustworthy and can be safely used to process sensitive data. For more information about attestation, see [Secure Enclave Attestation](always-encrypted-enclaves.md#secure-enclave-attestation).
- The client driver establishes a secure session with the enclave by negotiating a shared secret.
- The driver uses the shared secret to encrypt the column encryption keys the enclave will need to process the query, and sends the keys to [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], which forwards them to the secure enclave that decrypts the keys.
- Finally, the driver submits the query for execution, which triggers computations inside the secure enclave.

The following client drivers support Always Encrypted with secure enclaves:

- Microsoft .NET Data Provider for SQL Server in .NET Framework 4.6 or higher and .NET Core 2.1 or higher. If you want to use VBS enclaves without attestation, version 4.1 or later is required, which is compatible with .NET Framework 4.6.1 or higher and .NET Core 3.1.
  - For more information, see [Using Always Encrypted with the Microsoft .NET Data Provider for SQL Server](../../../connect/ado-net/sql/sqlclient-support-always-encrypted.md).
  - For a step-by-step tutorial, see [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](../../../connect/ado-net/sql/tutorial-always-encrypted-enclaves-develop-net-apps.md).
  - Also, see [Example demonstrating use of Azure Key Vault provider and Always Encrypted with secure enclaves](../../../connect/ado-net/sql/azure-key-vault-enclave-example.md).
- Microsoft ODBC Driver for SQL Server, version 17.4 or higher. If you want to use VBS enclaves without attestation, version 18.1 or higher is required.
  - For more information, see [Using Always Encrypted with the ODBC Driver](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md).
  - For information on how to enable enclave computations for a database connection using ODBC, see the [Enabling Always Encrypted with Secure Enclaves](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md#enabling-always-encrypted-with-secure-enclaves) section.
- Microsoft JDBC Driver for SQL Server, version 8.2 or higher. If you want to use VBS enclaves without attestation, version 12.2 or higher is required.
  - For more information, see [Using Always Encrypted with secure enclaves with the JDBC driver](../../../connect/jdbc/using-always-encrypted-with-secure-enclaves-with-the-jdbc-driver.md)
- .NET Framework Data Provider for SQL Server in .NET Framework 4.7.2 or higher.
  - For more information, see [Using Always Encrypted with the .NET Framework Data Provider for SQL Server](../../../relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider.md).
  - For a step-by-step tutorial, see [Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves](../tutorial-always-encrypted-enclaves-develop-net-framework-apps.md)

  > [!NOTE]
  > Using the .NET Framework Data Provider for SQL Server (System.Data.SqlClient) isn't recommended for new development. For more information, see [System.Data.SqlClient](../../../connect/connect-history.md#systemdatasqlclient).

## See also

- [Troubleshoot common issues for Always Encrypted with secure enclaves](always-encrypted-enclaves-troubleshooting.md)
