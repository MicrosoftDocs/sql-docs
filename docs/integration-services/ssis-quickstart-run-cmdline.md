---
title: "Run a package from the command prompt | Microsoft Docs"
ms.date: "08/21/2017"
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

For more info about `DTExec.exe`, see [dtexec Utility](https://docs.microsoft.com/en-us/sql/integration-services/packages/dtexec-utility).

## Steps
1. Open a Command Prompt window.

2. Run `DTExec.exe` and provide values at least for the `ISServer` and the `Server` parameters, as shown in the following example.

    ```cmd
    dtexec /ISServer "\SSISDB\Integration Services Project1\Package.dtsx" /Server "mysqldbserver.database.windows.net"
    ```

    With these parameter values, the program runs the package in the specified folder path on the SSIS server - that is, the server that hosts the SSIS Catalog database (SSISDB). The `Server` parameter provides the fully-qualified server name. The program connects as the current user with Windows Integrated Authentication.

    If the folder that contains `DTExec.exe` is not in your `path` environment variable, you may have to use the `cd` command to change to its directory. For SQL Server 2017, this is typically `C:\Program Files (x86)\Microsoft SQL Server\140\DTS\Binn`.

## Next steps
- Schedule a package. For more info, see [Schedule page](ssis-everest-howto-schedule-package.md)
