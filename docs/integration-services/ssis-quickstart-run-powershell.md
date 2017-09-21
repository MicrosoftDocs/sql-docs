---
title: "Run an SSIS package with PowerShell | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Run an SSIS package with PowerShell
This quick start tutorial demonstrates how to use a PowerShell script to connect to a database server and run an SSIS package.

## PowerShell script
Provide appropriate values for the variables at the top of the following script, and then run the script to run the SSIS package.

> [!NOTE]
> The following example uses Windows Authentication. To use SQL Server authentication, replace the `Integrated Security=SSPI;` argument with `User ID=<user name>;Password=<password>;`.

```powershell
```

## Next steps
- Consider other ways to run a package.
    - [Run an SSIS package with SSMS](./ssis-quickstart-run-ssms.md)
    - [Run an SSIS package with Transact-SQL (SSMS)](./ssis-quickstart-run-tsql-ssms.md)
    - [Run an SSIS package with Transact-SQL (VS Code)](ssis-quickstart-run-tsql-vscode.md)
    - [Run an SSIS package from the command prompt](./ssis-quickstart-run-cmdline.md)
    - [Run an SSIS package with PowerShell](ssis-quickstart-run-powershell.md)
    - [Run an SSIS package with C#](./ssis-quickstart-run-dotnet.md) 
