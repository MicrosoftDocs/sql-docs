---
title: "Example demonstrating use of Azure Key Vault provider with Always Encrypted enabled with secure enclaves"
description: "Example demonstrating use of Azure Key Vault provider with Always Encrypted enabled with secure enclaves"
author: David-Engel
ms.author: v-davidengel
ms.date: 03/03/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: tutorial
---

# Example demonstrating use of Azure Key Vault provider with Always Encrypted enabled with secure enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

## AzureKeyVaultProvider v2.0+

[!code-csharp [Azure Key Vault Provider 2.0 with Enclave Example#1](~/../sqlclient/doc/samples/AzureKeyVaultProviderWithEnclaveProviderExample_2_0.cs#1)]

## AzureKeyVaultProvider v1.x

[!code-csharp [Azure Key Vault Provider with Enclave Example#1](~/../sqlclient/doc/samples/AzureKeyVaultProviderWithEnclaveProviderExample.cs#1)]

> [!NOTE]
>
> - To use Always Encrypted with secure enclaves for .NET Standard application, **Microsoft.Data.SqlClient** version 2.1.0 or higher is required. The supported .NET Standard version is 2.1 or higher.
>
> - To use Always Encrypted with secure enclaves on Linux and macOS, **Microsoft.Data.SqlClient** version 2.1.0 or higher is required.

## See also

- [Example demonstrating use of Azure Key Vault provider with Always Encrypted](azure-key-vault-example.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](tutorial-always-encrypted-enclaves-develop-net-apps.md)
- [Using Always Encrypted with the Microsoft .NET Data Provider for SQL Server](sqlclient-support-always-encrypted.md)
