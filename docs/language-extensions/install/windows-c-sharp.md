---
title: Install .NET Language Extension on Windows
titleSuffix: SQL Server Language Extensions
description: Learn how to install the SQL Server .NET Language Extension feature on Windows.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: monamaki, randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: how-to
ms.custom:
  - intro-installation
monikerRange: ">=sql-server-ver15"
---

# Install SQL Server .NET Language Extension on Windows

[!INCLUDE [sqlserver2019-and-later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

Learn how to install the [.NET Language Extension](../csharp-overview.md) component (used by C#) for SQL Server on Windows. The .NET Language Extension is part of [SQL Server Language Extensions](../language-extensions-overview.md).

> [!NOTE]  
> This article is for installation of .NET Language Extension for SQL Server on Windows. Linux isn't supported.

## Prerequisites

> [!NOTE]  
> Feature capabilities and installation options vary between versions of SQL Server. Use the version selector dropdown list to choose the appropriate version of SQL Server.

- SQL Server Setup is required if you want to install support for the .NET Language Extension.

- The .NET Language Extension supports .NET 6 and later runtimes, and is only supported on Windows.

- A [!INCLUDE [ssde-md](../../includes/ssde-md.md)] instance is required. You can't install just the .NET Language Extension features, although you can add them incrementally to an existing instance.

- For business continuity, [Always On availability groups](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) are supported for Language Extensions. You have to install language extensions, and configure packages, on each node. Installing the .NET Language Extension also is supported on a failover cluster instance in SQL Server.

- Don't install SQL Server Language Extensions or the .NET Language Extension on a domain controller. The Language Extensions portion of setup fails.

- Language Extensions and [Machine Learning Services](../../machine-learning/index.yml) are installed by default on SQL Server Big Data Clusters. If you use Big Data Clusters, you don't need to follow the steps in this article. For more information, see [Run Python and R scripts with Machine Learning Services on SQL Server 2019 Big Data Clusters](../../big-data-cluster/machine-learning-services.md).

> [!IMPORTANT]  
> After setup is complete, be sure to complete the post-configuration steps described in this article. These steps include enabling SQL Server to use external code, and adding accounts required for SQL Server to run C# code on your behalf. Configuration changes generally require a restart of the instance, or a restart of the Launchpad service.

## .NET runtime

.NET 6 long-term support (LTS) is the earliest supported runtime. You can [download the .NET runtime](https://dotnet.microsoft.com/en-us/download/dotnet) for Windows. Linux isn't supported.

If you want to use the latest LTS release of the .NET runtime, you must recompile the .NET Language Extension.

## Get the installation media

[!INCLUDE [GetInstallationMedia](../../includes/getssmedia.md)]

## Run Setup

For local installations, you must run Setup as an administrator. If you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

1. Start the setup wizard for SQL Server.

1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

1. On the **Feature Selection** page, select these options:

   **Database Engine Services**: To use Language Extensions with SQL Server, you must install an instance of the [!INCLUDE [ssde-md](../../includes/ssde-md.md)]. You can use either a default or a named instance.

   **Machine Learning Services and Language Extensions**: This option installs the Language Extensions component that support C# code execution.

   :::image type="content" source="../media/windows-java/2022/sql-server-2022-machine-learning-services-feature-selection.png" alt-text="Screenshot of instance features." lightbox="../media/windows-java/2022/sql-server-2022-machine-learning-services-feature-selection.png":::

1. On the **Ready to Install** page, verify that these selections are included, and select **Install**.

   - [!INCLUDE [ssde-md](../../includes/ssde-md.md)] Services
   - Machine Learning Services and Language Extensions

   Note the location of the folder under the path `..\Setup Bootstrap\Log` where the configuration files are stored. When setup is complete, you can review the installed components in the Summary file.

1. After setup is complete, if you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you finish with Setup. For more information, see [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).

## Register the language extension

1. Follow these steps to download and register the .NET language extension, which is used for running C# code.

   1. Download the `dotnet-core-CSharp-lang-extension-windows-release.zip` file from [the .NET language extension for SQL Server GitHub repo](https://github.com/microsoft/sql-server-language-extensions/releases). Download the latest Windows `dotnet-core-CSharp-lang-extension-windows-release.zip` file. If you prefer to use a newer .NET runtime, you need to compile `dotnet-core-CSharp-lang-extension` from GitHub source code.

   1. Use [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio) to connect to your SQL Server instance and run the following Transact-SQL (T-SQL) command to register the .NET language extension with [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md).

   1. Modify the path in this statement to reflect the location of the downloaded language extension zip file (`dotnet-core-CSharp-lang-extension-windows-release.zip`).

   ```sql
   CREATE EXTERNAL LANGUAGE [dotnet]
   FROM (CONTENT = N'C:\path\to\dotnet-core-CSharp-lang-extension-windows-release.zip',
       FILE_NAME = 'dotnetextension.dll');
   GO
   ```

1. Restart [Launchpad](../concepts/extensibility-framework.md#launchpad).

   1. Open [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

   1. Under SQL Server Services, right-click SQL Server Launchpad and select **Restart**.

## Restart the service

When the installation is complete, restart the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] before continuing to the next step, enabling script execution.

Restarting the service also automatically restarts the related SQL Server Launchpad service.

You can restart the service using the right-click **Restart** command for the instance in SSMS, with the **Services** panel in Control Panel, or by using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

## Enable script execution

1. Open [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Connect to the instance where you installed Language Extensions, select **New Query** to open a query window, and run the following command:

   ```sql
   EXEC sp_configure;
   ```

   The feature is off (`value` is `0`) by default, and must be explicitly enabled by an administrator before you can run C# code.

1. To enable the external scripting feature, run the following statement:

   ```sql
   EXEC sp_configure 'external scripts enabled', 1;
   GO
   RECONFIGURE WITH OVERRIDE
   ```

   If you already enabled the feature for Machine Learning Services, don't run reconfigure a second time for Language Extensions. The underlying extensibility platform supports both.

## Register external language

For each database you want to use language extensions in, you need to register the external language with [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md).

The following example adds an external language called `dotnet` to a database on SQL Server on Windows.

```sql
CREATE EXTERNAL LANGUAGE [dotnet]
FROM (CONTENT = N'<path-to-zip>', FILE_NAME = 'dotnetextension.dll');
GO
```

For more information, see [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md).

## Verify installation

Check the installation status of the instance in the setup logs.

Use the following steps to verify that all components used to launch external script are running.

1. In SQL Server Management Studio or Azure Data Studio, open a new query window, and run the following statement:

   ```sql
   EXEC sp_configure 'external scripts enabled';
   ```

   The `run_value` is now set to `1`.

1. Open the **Services** panel or SQL Server Configuration Manager, and verify **SQL Server Launchpad service** is running. You should have one service for every [!INCLUDE [ssde-md](../../includes/ssde-md.md)] instance that has language extensions installed. For more information about the service, see [Extensibility architecture in SQL Server Language Extensions](../concepts/extensibility-framework.md).

## Additional configuration

If the verification step was successful, you can run C# code from SQL Server Management Studio, Azure Data Studio, Visual Studio Code, or any other client that can send T-SQL statements to the server.

If you got an error when running the command, review the additional configuration steps in this section. You might need to make extra appropriate configurations to the service or database.

At the instance level, extra configuration might include:

- [Firewall configuration for SQL Server Machine Learning Services](../../machine-learning/security/firewall-configuration.md)
- [Enable or disable a server network protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)
- [Configure remote access (server configuration option)](../../database-engine/configure-windows/configure-the-remote-access-server-configuration-option.md)
- [Create a login for SQLRUserGroup](../../machine-learning/security/create-a-login-for-sqlrusergroup.md)

On the database, you might need the following configuration updates:

- [Grant database users permission to execute Python and R scripts with SQL Server Machine Learning Services](../../machine-learning/security/user-permission.md)
- [Give users permission to execute a specific language](../../t-sql/statements/create-external-language-transact-sql.md#permissions)

> [!NOTE]  
> Whether additional configuration is required depends on your security schema, where you installed SQL Server, and how you expect users to connect to the database and run external scripts.

## Suggested optimizations

Now that you have everything working, you might also want to optimize the server to support .NET Language Extension.

### Optimize the server for .NET Language Extension

The default settings for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup are intended to optimize the balance of the server for a variety of services supported by the [!INCLUDE [ssde-md](../../includes/ssde-md.md)], which might include extract, transform, and load (ETL) processes, reporting, auditing, and applications that use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data. Therefore, under the default settings, you might find that resources for language extensions are sometimes restricted or throttled, particularly in memory-intensive operations.

To ensure that language extensions jobs are prioritized and resourced appropriately, we recommend that you use SQL Server Resource Governor to configure an external resource pool. You might also want to change the amount of memory allocated to the [!INCLUDE [ssde-md](../../includes/ssde-md.md)], or increase the number of accounts that run under the [!INCLUDE [rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

- To configure a resource pool for managing external resources, see [CREATE EXTERNAL RESOURCE POOL (Transact-SQL)](../../t-sql/statements/create-external-resource-pool-transact-sql.md).

- To change the amount of memory reserved for the database, see [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).

If you use Standard edition and don't have Resource Governor, you can use dynamic management views (DMVs) and Extended Events, as well as Windows event monitoring, to help manage the server resources.

## Next step

C# developers can get started with some simple examples, and learn the basics of how C# works with SQL Server. For your next step, see the following link:

> [!div class="nextstepaction"]
> [Tutorial: Regular expressions with C#](../tutorials/search-for-string-using-regular-expressions-in-c-sharp.md)
