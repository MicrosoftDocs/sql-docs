---
description: "Example demonstrating use of Azure Key Vault provider with Always Encrypted"
title: "Example demonstrating use of Azure Key Vault provider with Always Encrypted | Microsoft Docs"
ms.custom: ""
ms.date: 11/17/2020
ms.reviewer: v-kaywon
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.tgt_pltfrm: ""
ms.topic: tutorial
author: karinazhou
ms.author: v-jizho2
---

# Example demonstrating use of Azure Key Vault provider with Always Encrypted

[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

[!INCLUDE [appliesto-netfx-netcore-xxxx-md](../../../includes/appliesto-netfx-netcore-netst-md.md)]

This example demonstrates use of Azure Key Vault Provider when accessing encrypted columns.

[!code-csharp [AKVProvider Example#1](~/../sqlclient/doc/samples/AzureKeyVaultProviderExample.cs#1)]

> [!NOTE]
> - To use Always Encrypted feature without secure enclaves for .NET Standard application, **Microsoft.Data.SqlClient** version 2.1.0 or higher is required. The supported .NET Standard version is 2.0 or higher. 
>
> - To use Always Encrypted feature on Linux and macOS, **Microsoft.Data.SqlClient** version 2.1.0 or higher is required.

## See Also

- [Example demonstrating use of Azure Key Vault provider with Always Encrypted enabled with Secure Enclaves](azure-key-vault-enclave-example.md)
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](tutorial-always-encrypted-enclaves-develop-net-apps.md)
- [Using Always Encrypted with the Microsoft .NET Data Provider for SQL Server](sqlclient-support-always-encrypted.md)
