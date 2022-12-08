---
title: "Example demonstrating use of Azure Key Vault provider with Always Encrypted"
description: "Example demonstrating use of Azure Key Vault provider with Always Encrypted"
author: David-Engel
ms.author: v-davidengel
ms.date: 05/24/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: tutorial
ms.custom: event-tier1-build-2022
---

# Example demonstrating use of Azure Key Vault provider with Always Encrypted

[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

This example demonstrates use of Azure Key Vault Provider when accessing encrypted columns.

## AzureKeyVaultProvider v2.0+

[!code-csharp [Azure Key Vault Provider 2.0 Example#1](~/../sqlclient/doc/samples/AzureKeyVaultProviderExample_2_0.cs#1)]

### Legacy callback implementation design example with v2.0+

[!code-csharp [Azure Key Vault Provider 2.0 Legacy Example#2](~/../sqlclient/doc/samples/AzureKeyVaultProviderLegacyExample_2_0.cs#1)]

## AzureKeyVaultProvider v1.x

[!code-csharp [Azure Key Vault Provider Example#1](~/../sqlclient/doc/samples/AzureKeyVaultProviderExample.cs#1)]

> [!NOTE]
>
> - To use Always Encrypted feature without secure enclaves for .NET Standard application, **Microsoft.Data.SqlClient** version 2.1.0 or higher is required. The supported .NET Standard version is 2.0 or higher.
>
> - To use Always Encrypted feature on Linux and macOS, **Microsoft.Data.SqlClient** version 2.1.0 or higher is required.

## See also

- [Example demonstrating use of Azure Key Vault provider with Always Encrypted enabled with secure enclaves](azure-key-vault-enclave-example.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](tutorial-always-encrypted-enclaves-develop-net-apps.md)
- [Using Always Encrypted with the Microsoft .NET Data Provider for SQL Server](sqlclient-support-always-encrypted.md)
