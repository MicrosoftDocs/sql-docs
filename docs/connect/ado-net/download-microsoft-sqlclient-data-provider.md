---
title: Install, Update, and Deploy Microsoft.Data.SqlClient
description: Install Microsoft.Data.SqlClient, choose a supported release, add optional feature packages, and prepare a .NET application for deployment.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, paulmedynski, cmalhotra
ms.date: 09/15/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Install, update, and deploy Microsoft.Data.SqlClient

Microsoft.Data.SqlClient is the supported .NET data provider for SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Microsoft Fabric, and Warehouse in Microsoft Fabric. The driver ships as NuGet packages independently of the .NET runtime.

## Choose a release

Use a supported General Availability (GA) release for production applications.

| Release line | Support level | Choose it when |
| --- | --- | --- |
| 7.0 | Standard Term Support (STS) | You need current driver features and can update on the regular release cadence. |
| 6.1 | Long Term Support (LTS) | You prefer a longer support period and don't need features introduced in 7.0. |

For current patch versions, support dates, target frameworks, and database compatibility, see [SqlClient driver support lifecycle](sqlclient-driver-support-lifecycle.md).

## Install the core package

Run the following command from the directory that contains your project file:

```console
dotnet add package Microsoft.Data.SqlClient
```

The command selects the latest stable version compatible with the project. To pin a version for repeatable builds, specify the version:

```console
dotnet add package Microsoft.Data.SqlClient --version <version>
```

Restore dependencies and build the project:

```console
dotnet restore
dotnet build
```

Import the driver namespace in C#:

```csharp
using Microsoft.Data.SqlClient;
```

For new applications, don't reference the older `System.Data.SqlClient` package or namespace. To update an existing application, see [Migrate from System.Data.SqlClient to Microsoft.Data.SqlClient](migrate-system-data-sql-client-to-microsoft-data-sql-client.md).

## Install with Visual Studio

1. In **Solution Explorer**, right-click the project, and select **Manage NuGet Packages**.
1. On the **Browse** tab, search for `Microsoft.Data.SqlClient`.
1. Select the package owned by Microsoft.
1. Select a supported stable version, and then select **Install**.

For more information, see [Install and manage packages in Visual Studio](/nuget/consume-packages/install-use-packages-visual-studio).

## Add optional packages

Install optional packages only when your application uses the related feature.

| Package | Add it when |
| --- | --- |
| `Microsoft.Data.SqlClient.Extensions.Azure` | Microsoft.Data.SqlClient 7.0 or later uses a driver-provided Microsoft Entra authentication mode, such as `Active Directory Default`, `Active Directory Interactive`, or `Active Directory Managed Identity`. |
| `Microsoft.Data.SqlClient.AlwaysEncrypted.AzureKeyVaultProvider` | Always Encrypted stores column master keys in Azure Key Vault. |

For Microsoft Entra authentication with SqlClient 7.0 or later, run:

```console
dotnet add package Microsoft.Data.SqlClient.Extensions.Azure --version <same-version-as-Microsoft.Data.SqlClient>
```

The 7.0 core package no longer includes Azure Identity dependencies. Starting with version 7.0.2, the core driver and companion packages use aligned versions. Reference the same version of `Microsoft.Data.SqlClient` and `Microsoft.Data.SqlClient.Extensions.Azure`. Applications that don't use driver-provided Microsoft Entra authentication don't need the Azure extension package. For authentication setup, see [Microsoft Entra authentication](sql/azure-active-directory-authentication.md).

## Update the driver

Before you update:

1. Read [What's new in Microsoft.Data.SqlClient](microsoft-data-sql-client-release-notes.md) and the upstream release notes for each version crossed by the update.
1. Check the application's direct and transitive package references.

   ```console
   dotnet list package --include-transitive
   ```

1. Update the package reference.

   ```console
   dotnet add package Microsoft.Data.SqlClient --version <version>
   ```

1. Restore, build, and run the application's tests.
1. Test connection establishment, authentication, certificate validation, connection pooling, data type conversions, transactions, and retry behavior against each supported database target.

Major and patch releases can require application changes. For example, version 4.0 enabled encryption by default, version 5.0 changed `SqlConnectionStringBuilder.Encrypt` from `bool` to `SqlConnectionEncryptOption`, and version 7.0 moved driver-provided Microsoft Entra authentication into a separate package.

Starting with version 7.0.2, the assembly versions of `Microsoft.Data.SqlClient.Extensions.Azure`, `Microsoft.Data.SqlClient.Extensions.Abstractions`, and `Microsoft.Data.SqlClient.Internal.Logging` changed from `1.0.0.0` to `7.0.0.0`. .NET Framework applications must rebuild against the aligned packages or add binding redirects. Applications on current .NET aren't affected by this assembly identity change.

## Understand target frameworks

Microsoft.Data.SqlClient 7.0 supports applications on:

- .NET Framework 4.6.2 or later on Windows.
- .NET 8 or later on supported Windows, Linux, and macOS versions.

The NuGet package also contains a .NET Standard 2.0 compatibility asset for libraries. An executable application must target a supported .NET or .NET Framework runtime. A library's .NET Standard target doesn't make every consuming runtime supported.

The package restores its managed and native dependencies through NuGet. Don't copy individual driver assemblies or native SQL Network Interface (SNI) libraries between applications.

## Prepare for deployment

1. Publish for the same operating system and architecture used in production. For a framework-dependent deployment:

   ```console
   dotnet publish --configuration Release
   ```

   For a runtime-specific deployment:

   ```console
   dotnet publish --configuration Release --runtime <runtime-identifier>
   ```

1. Deploy the complete publish output. Don't select only `Microsoft.Data.SqlClient.dll`.
1. Confirm that the target has the required .NET runtime unless you publish a self-contained application.
1. Test the published output on the target operating system and architecture.
1. Test every authentication mode used in production. Integrated authentication, Kerberos, certificates, managed identity, and Azure Key Vault access depend on the deployment environment.
1. Scan the resolved dependency graph for known vulnerabilities and update supported package lines when fixes are available.

Globalization-invariant mode isn't supported. For current platform qualifications, see [Supported operating systems](sqlclient-driver-support-lifecycle.md#supported-operating-systems).

## Offline package installation

Download the package and its dependencies from [NuGet.org](https://www.nuget.org/packages/Microsoft.Data.SqlClient), copy them to an internal package source, and restore from that source. For repeatable offline builds, pin package versions and preserve the complete dependency set.

## Related content

- [Get started with the SqlClient driver](get-started-sqlclient-driver.md)
- [What's new in Microsoft.Data.SqlClient](microsoft-data-sql-client-release-notes.md)
- [SqlClient driver support lifecycle](sqlclient-driver-support-lifecycle.md)
- [Microsoft.Data.SqlClient releases](https://github.com/dotnet/SqlClient/releases)
