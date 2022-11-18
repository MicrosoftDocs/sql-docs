---
description: "Run an SSIS package from the command prompt with DTExec.exe"
title: "Run an SSIS package from the command prompt | Microsoft Docs"
ms.date: "05/21/2018"
ms.topic: quickstart
ms.service: sql
ms.custom:
  - intro-quickstart
ms.subservice: integration-services
author: chugugrace
ms.author: chugu
---
# Run an SSIS package from the command prompt with DTExec.exe

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]


This quickstart demonstrates how to run an SSIS package from the command prompt by running `DTExec.exe` with the appropriate parameters.

> [!NOTE]
> The method described in this article has not been tested with packages deployed to an Azure SQL Database server.

For more info about `DTExec.exe`, see [dtexec Utility](./packages/dtexec-utility.md).

## Supported platforms

You can use the information in this quickstart to run an SSIS package on the following platforms:

-   SQL Server on Windows.

The method described in this article has not been tested with packages deployed to an Azure SQL Database server. For more info about deploying and running packages in Azure, see [Lift and shift SQL Server Integration Services workloads to the cloud](lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).

You cannot use the information in this quickstart to run an SSIS package on Linux. For more info about running packages on Linux, see [Extract, transform, and load data on Linux with SSIS](../linux/sql-server-linux-migrate-ssis.md).

## Run a package with dtexec

If the folder that contains `DTExec.exe` is not in your `path` environment variable, you may have to use the `cd` command to change to its directory. For SQL Server 2017, this folder is typically `C:\Program Files (x86)\Microsoft SQL Server\140\DTS\Binn`.

With the parameter values used in the following example, the program runs the package in the specified folder path on the SSIS server - that is, the server that hosts the SSIS Catalog database (SSISDB). The `/Server` parameter provides the server name. The program connects as the current user with Windows Integrated Authentication. To use SQL Authentication, specify the `/User` and `Password` parameters with appropriate values.

1. Open a Command Prompt window.

2. Run `DTExec.exe` and provide values at least for the `ISServer` and the `Server` parameters, as shown in the following example:

    ```cmd
    dtexec /ISServer "\SSISDB\Project1Folder\Integration Services Project1\Package.dtsx" /Server "localhost"
    ```

## Next steps
- Consider other ways to run a package.
    - [Run an SSIS package with SSMS](./ssis-quickstart-run-ssms.md)
    - [Run an SSIS package with Transact-SQL (SSMS)](./ssis-quickstart-run-tsql-ssms.md)
    - [Run an SSIS package with Transact-SQL (VS Code)](ssis-quickstart-run-tsql-vscode.md)
    - [Run an SSIS package with PowerShell](ssis-quickstart-run-powershell.md)
    - [Run an SSIS package with C#](./ssis-quickstart-run-dotnet.md)
