---
title: "Example demonstrating use of Azure Key Vault provider with Always Encrypted enabled with secure enclaves"
description: "Example demonstrating use of Azure Key Vault provider with Always Encrypted enabled with secure enclaves"
author: David-Engel
ms.author: davidengel
ms.date: 02/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: tutorial
---

# Example demonstrating use of Azure Key Vault provider with Always Encrypted enabled with secure enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

This example shows you how you can use the Azure Key Vault provider with Always Encrypted with secure enclaves. The script will create a column master key in the database based on the Azure Key Vault URL.
Secondly, a column encryption key is created. Once the keys are created, a table with encrypted columns will be created, a few records will be inserted and read again from the table.

## AzureKeyVaultProvider v2.0+

[!code-csharp [Azure Key Vault Provider 2.0 with Enclave Example#1](~/../sqlclient/doc/samples/AzureKeyVaultProviderWithEnclaveProviderExample_2_0.cs#1)]

## AzureKeyVaultProvider v1.x

[!code-csharp [Azure Key Vault Provider with Enclave Example#1](~/../sqlclient/doc/samples/AzureKeyVaultProviderWithEnclaveProviderExample.cs#1)]

> [!NOTE]
>
> - To use Always Encrypted with secure enclaves for .NET Standard application, **Microsoft.Data.SqlClient** version 2.1.0 or higher is required. The supported .NET Standard version is 2.1 or higher.
>
> - To use Always Encrypted with secure enclaves on Linux and macOS, **Microsoft.Data.SqlClient** version 2.1.0 or higher is required.
>
> - To use Always Encrypted with VBS enclaves without attestation, **Microsoft.Data.SqlClient** version 4.1.0 or higher is required.

## See also

- [Example demonstrating use of Azure Key Vault provider with Always Encrypted](azure-key-vault-example.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](tutorial-always-encrypted-enclaves-develop-net-apps.md)
- [Using Always Encrypted with the Microsoft .NET Data Provider for SQL Server](sqlclient-support-always-encrypted.md)
