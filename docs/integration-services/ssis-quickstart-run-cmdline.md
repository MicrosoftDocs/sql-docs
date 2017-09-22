---
title: "Run an SSIS package from the command prompt | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Run an SSIS package from the command prompt with DTExec.exe
This quick start tutorial demonstrates how to run an SSIS package from the command prompt by running `DTExec.exe` with the appropriate parameters.

> [!NOTE]
> The method described in this article has not been tested with packages deployed to an Azure SQL Database server.

For more info about `DTExec.exe`, see [dtexec Utility](https://docs.microsoft.com/en-us/sql/integration-services/packages/dtexec-utility).

## Steps

If the folder that contains `DTExec.exe` is not in your `path` environment variable, you may have to use the `cd` command to change to its directory. For SQL Server 2017, this is typically `C:\Program Files (x86)\Microsoft SQL Server\140\DTS\Binn`.

With the parameter values used in the following example, the program runs the package in the specified folder path on the SSIS server - that is, the server that hosts the SSIS Catalog database (SSISDB). The `/Server` parameter provides the server name. The program connects as the current user with Windows Integrated Authentication. To use SQL Authentication, specify the `/User` and `Password` parameters with appropriate values.

1. Open a Command Prompt window.

2. Run `DTExec.exe` and provide values at least for the `ISServer` and the `Server` parameters, as shown in the following example.

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
