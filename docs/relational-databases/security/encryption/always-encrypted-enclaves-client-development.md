---
title: "Develop applications using Always Encrypted with secure enclaves | Microsoft Docs"
ms.custom: ""
ms.date: "10/15/2019"
ms.prod: sql
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
dev_langs: 
  - "CSharp"
ms.assetid: 9595eb66-284c-4474-828f-8961a05ce989
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current" 
---
# Develop applications using Always Encrypted with secure enclaves
[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) extends [Always Encrypted](always-encrypted-database-engine.md) to enable richer functionality of application queries on encrypted sensitive database columns. It leverages secure enclave technologies to allow the query executor in [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to delegate computations on encrypted columns to a secure enclave inside the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] process.

## Client driver for Always Encrypted with secure enclaves

To develop applications using Always Encrypted with secure enclaves, you need a SQL client driver version that supports secure enclaves. The client driver plays the following key role:
- Before submitting a query that uses a secure enclave to [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] for execution, the driver initiates enclave attestation to verify the secure enclave is trustworthy and can be safely used to process sensitive data. For more information about attestation, see [Secure Enclave Attestation](always-encrypted-enclaves.md#secure-enclave-attestation).
- Once attestation succeeds, the client driver establishes a secure session with the enclave by negotiating a shared secret.
- The driver uses the shared secret to encrypt the column encryption keys the enclave will need to process the query, and sends the keys to [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], which forwards them to the secure enclave that decrypts the keys. 
- Finally, the driver submits the query for execution, which triggers computations inside the secure enclave.

To use the functionality of the secure enclave, you need to configure your application and your client driver to enable enclave computations when connecting to the database and specify an attestation service endpoint (an enclave attestation URL) that points to an attestation service for your enclave. The details depend on a driver and an attestation service/protocol, you are using.

## Next steps

The following client drivers support Always Encrypted with secure enclaves:
- .NET Framework Data Provider for SQL Server in .NET Framework 4.7.2 or higher. 
    - For more information, see [Using Always Encrypted with the .NET Framework Data Provider for SQL Server](../../../relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider.md).
    - For a step-by-step tutorial, see [Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves](../tutorial-always-encrypted-enclaves-develop-net-framework-apps.md)
- Microsoft .NET Data Provider for SQL Server in .NET Framework 4.6 or higher and .NET Core 2.1 or higher. 
    - For more information, see [Using Always Encrypted with the Microsoft .NET Data Provider for SQL Server](../../../connect/ado-net/sql/sqlclient-support-always-encrypted.md).
    - For a step-by-step tutorial, see [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](../../../connect/ado-net/sql/tutorial-always-encrypted-enclaves-develop-net-apps.md)
- Microsoft ODBC Driver for SQL Server, version 17.4 or higher. 
    - For more information, see [Using Always Encrypted with the ODBC Driver](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md). 
    - For information, on how to enable enclave computations for a database connection using ODBC, see the [Enabling Always Encrypted with Secure Enclaves](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md#enabling-always-encrypted-with-secure-enclaves) section.
