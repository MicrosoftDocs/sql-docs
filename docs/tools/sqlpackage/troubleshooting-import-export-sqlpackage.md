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

## Overall Strategies
As general guideline, better performance can be obtained via the .NET Core version of SqlPackage.exe.

For issues related to timeouts, the properties `CommandTimeout` and `LongRunningCommandTimeout` can be used to tune the connection between SqlPackage.exe and the SQL instance.

## Diagnostics
Logs are essential to any troubleshooting. Capture the diagnostic logs from any SqlPackage operation to a file with the `/DiagnosticsFile:<filename>` parameter. 
Additional performance-related data can be logged by setting the environment variable `DACFX_PERF_TRACE=true`.  To set this environment variable in PowerShell, use the following command:
``` powershell
Set-Item -Path Env:DACFX_PERF_TRACE -Value true
```

## Import action Tips
For imports that contain large tables or tables with many indexes, the use of `/p:RebuildIndexesOfflineForDataPhase=True` or `/p:DisableIndexesForDataPhase=False` may improve performance. Those and other properties are available to tune the [SqlPackage.exe Import](sqlpackage-import.md) operation.

## Export action Tips
As an alternative operation to obtain the database schema and data while skipping the schema validation , perform an [Extract](sqlpackage-extract.md) with `/p:ExtractAllTableData=True` and `/p:VerifyExtraction=True`.

In scenarios where disk space is limited and running out during the export, the use of `/p:TempDirectoryForTableData` allows the data to be buffered from an alternative disk. That and other properties are available to tune the [SqlPackage.exe Export](sqlpackage-export.md) operation.

## Next Steps

- Learn more about [SqlPackage Import](sqlpackage-import.md)
- Learn more about [SqlPackage Export](sqlpackage-export.md)