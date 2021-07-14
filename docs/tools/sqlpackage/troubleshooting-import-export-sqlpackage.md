---
title: Troubleshooting Import/Export with SqlPackage
description: Learn how to troubleshoot import and export with SqlPackage.exe.
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan; sstein"
ms.date: 5/5/2021
---

# Troubleshooting Import/Export with SqlPackage

In some scenarios, import or export operations take longer than expected or fail to complete.  The following are some frequently suggested tactics to troubleshoot import and export operations. While reading the specific documentation page for each action to understand the available parameters and properties is recommended, this article serves as a starting point in investigating SqlPackage Import or Export operations.

## Overall strategy
As general guideline, better performance can be obtained via the [.NET Core version](sqlpackage-download.md#get-sqlpackage-net-core-for-windows) of SqlPackage.exe.

1. [Download](sqlpackage-download.md#get-sqlpackage-net-core-for-windows) the zip for SqlPackage on .NET Core for your operating system (Windows, macOS, or Linux).
2. Unzip archive as directed on the download page.
3. Open a command prompt and change directory (`cd`) to the SqlPackage folder.

For Import, an example command is:
```bash
./SqlPackage /Action:Import /sf:<source-bacpac-file-path> /tsn:<full-target-server-name> /tdn:<a new or empty database> /tu:<target-server-username> /tp:<target-server-password> /df:<log-file>
```

For Export, an example command is:
```bash
./SqlPackage /Action:Export /tf:<target-bacpac-file-path> /ssn:<full-source-server-name> /sdn:<source-database-name> /su:<source-server-username> /sp:<source-server-password> /df:<log-file>
```

Alternative to username and password, [Universal Authentication](/azure/azure-sql/database/authentication-mfa-ssms-overview) can be used to authenticate via Azure AD with MFA.  Substitute the username and password parameters for `/ua:true` and `/tid:"yourdomain.onmicrosoft.com"`.

For issues related to timeouts, the properties `CommandTimeout` and `LongRunningCommandTimeout` can be used to tune the connection between SqlPackage.exe and the SQL instance.

## Diagnostics
Logs are essential to troubleshooting. Capture the diagnostic logs to a file with the `/DiagnosticsFile:<filename>` parameter.

Additional performance-related trace data can be logged by setting the environment variable `DACFX_PERF_TRACE=true` before running SqlPackage.  To set this environment variable in PowerShell, use the following command:
``` powershell
Set-Item -Path Env:DACFX_PERF_TRACE -Value true
```

## Import action tips
For imports that contain large tables or tables with many indexes, the use of `/p:RebuildIndexesOfflineForDataPhase=True` or `/p:DisableIndexesForDataPhase=False` may improve performance. These properties modify the index rebuild operation to occur offline or not occur, respectively. Those and other properties are available to tune the [SqlPackage.exe Import](sqlpackage-import.md) operation.

## Export action tips
As an alternative operation to obtain the database schema and data while skipping the schema validation, perform an [Extract](sqlpackage-extract.md) with `/p:ExtractAllTableData=True` and `/p:VerifyExtraction=True`.

In scenarios where the OS disk space is limited and runs out during the export, the use of `/p:TempDirectoryForTableData` allows the data for export to be buffered on an alternative disk. The space required for this action may be large and is relative to the full size of the database. That and other properties are available to tune the [SqlPackage.exe Export](sqlpackage-export.md) operation.

## Next steps

- Learn more about [SqlPackage Import](sqlpackage-import.md)
- Learn more about [SqlPackage Export](sqlpackage-export.md)
