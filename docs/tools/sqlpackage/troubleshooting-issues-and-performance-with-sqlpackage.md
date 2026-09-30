---
title: Troubleshooting Issues and Performance with SqlPackage
description: Learn how to troubleshoot with SqlPackage.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 09/11/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: troubleshooting-general
ms.collection:
  - data-tools
---

# Troubleshoot issues and performance with SqlPackage

In some scenarios, SqlPackage operations take longer than expected or fail to complete. This article describes some frequently suggested tactics to troubleshoot or improve performance of these operations. While reading the specific documentation page for each action to understand the available parameters and properties is recommended, this article serves as a starting point in investigating SqlPackage operations.

## Overall strategy

As general guideline, better performance can be obtained via the [.NET version](sqlpackage-download.md#installation-cross-platform) of SqlPackage instead of the .NET Framework version installed via the DacFramework.msi.

If you can't install the SqlPackage [dotnet tool](sqlpackage-download.md#installation-cross-platform), which enables you to execute SqlPackage commands from the command prompt in any directory:

1. [Download](sqlpackage-download.md#installation-file-download-alternative) the zip for SqlPackage on .NET 8 for your operating system (Windows, macOS, or Linux).
1. Unzip the archive as directed on the download page.
1. Open a command prompt and change directory (`cd`) to the SqlPackage folder.

Use the latest available version of SqlPackage, as performance improvements and bug fixes are released regularly.

### Substitute SqlPackage for the Import/Export Service

If you attempted to use the Import/Export Service to import or export your database, you can use SqlPackage to perform the same operation with more control on optional parameters and properties. The blog post [Optimizing BACPAC Imports - SqlPackage Done Right!](https://techcommunity.microsoft.com/blog/azuredbsupport/azure-sql-optimizing-bacpac-imports---sqlpackage-done-right/4472021) walks through the steps to use SqlPackage instead of the Import/Export Service for a `.bacpac` import.

For Import, an example command is:

```bash
./SqlPackage /Action:Import /sf:<source-bacpac-file-path> /tsn:<full-target-server-name> /tdn:<a new or empty database> /tu:<target-server-username> /tp:<target-server-password> /df:<log-file>
```

For Export, an example command is:

```bash
./SqlPackage /Action:Export /tf:<target-bacpac-file-path> /ssn:<full-source-server-name> /sdn:<source-database-name> /su:<source-server-username> /sp:<source-server-password> /df:<log-file>
```

Use [multifactor authentication](/azure/azure-sql/database/authentication-aad-overview#multifactor-authentication-mfa) as an alternative to username and password, to authenticate with Microsoft Entra authentication. Substitute the username and password parameters for `/ua:true` and `/tid:"contoso.onmicrosoft.com"`.

### Diagnostics

Diagnosing errors and unexpected behavior in SqlPackage is supported by diagnostic logs and a diagnostic package. The diagnostic logs are essential to troubleshooting and are captured to a file with the `/DiagnosticsFile:<filename>` parameter.

Control the level of detail in diagnostic output through the `/DiagnosticsLevel` parameter. Use the `Information` and `Verbose` values to get more details.

Log performance-related trace data by setting the `DACFX_PERF_TRACE=true` environment variable before running SqlPackage. The trace data increases the log output, so only include it when diagnosing performance challenges. To set this environment variable in PowerShell, use the following command:

```powershell
Set-Item -Path Env:DACFX_PERF_TRACE -Value true
```

In SqlPackage [162.5](release-notes-sqlpackage.md) and later, you can generate a diagnostic package to help with troubleshooting. The diagnostic package contains the SqlPackage version, the command executed, information about the source and target database models, and the output of the command. To generate a diagnostic package, use the `/DiagnosticsPackageFile:<filename>` parameter.

## Common issues

### Timeout errors

For timeout issues, use the following properties to tune the connection between SqlPackage and the SQL instance:

- `/p:CommandTimeout=`: Specifies the command timeout in seconds when a query runs. Default: 60
- `/p:DatabaseLockTimeout=`: Specifies the database lock timeout in seconds. Use `-1` to wait indefinitely. Default: 60
- `/p:LongRunningCommandTimeout=`: Specifies the long running command timeout in seconds. The default value, `0`, waits indefinitely.

### Client resource consumption

For the export and extract commands, SqlPackage passes table data to a temporary directory to buffer before writing it to the BACPAC or DACPAC file. This storage requirement can be large and is relative to the full size of the data to export. Specify an alternative temporary directory with the property `/p:TempDirectoryForTableData=<path>`.

SqlPackage compiles the schema model in memory. For large database schemas, the memory requirement on the client machine running SqlPackage can be significant.

### Low server resource consumption

By default, SqlPackage sets the maximum server parallelism to 8. If you notice low server resource consumption, increasing the value of the `MaxParallelism` parameter can improve performance.

### Access token

Using the `/AccessToken:` or `/at:` parameter enables token-based authentication for SqlPackage, but passing the token to the command can be tricky. If you're parsing an access token object in PowerShell, either explicitly pass the string value or wrap the reference to the token property in `$()`. For example:

```powershell
$Account = Connect-AzAccount -ServicePrincipal -Tenant $Tenant -Credential $Credential
$AccessToken_Object = (Get-AzAccessToken -Account $Account -Resource "https://database.windows.net/")
$AccessToken = $AccessToken_Object.Token

SqlPackage /at:$AccessToken
# OR
SqlPackage /at:$($AccessToken_Object.Token)
```

### Connection

If SqlPackage is failing to connect, the server might not have encryption enabled or the configured certificate might not be issued from a trusted certificate authority (such as a self-signed certificate). You can change the SqlPackage command to either connect without encryption or to trust the server certificate. The [best practice](../../relational-databases/security/securing-sql-server.md) is to ensure that a trusted encrypted connection to the server can be established.

- Connect without encryption: `/SourceEncryptConnection:False` or `/TargetEncryptConnection:False`
- Trust server certificate: `/SourceTrustServerCertificate:True` or `/TargetTrustServerCertificate:True`

You might see one or more of the following warning messages when connecting to a SQL instance, indicating that command line parameters might require changes to connect to the server:

```output
The settings for connection encryption or server certificate trust may lead to connection failure if the server is not properly configured.
The connection string provided contains encryption settings which may lead to connection failure if the server is not properly configured.
```

More information about the connection security changes in SqlPackage is available in [Connection Security Improvements in SqlPackage 161](https://aka.ms/dacfx-connection).

### Import action error 2714 for constraint

When you perform an import action, you might receive error 2714 if an object already exists:

```output
*** Error importing database:Could not import package.
Error SQL72014: Core Microsoft SqlClient Data Provider: Msg 2714, Level 16, State 5, Line 1 There is already an object named 'DF_Department_ModifiedDate_0FF0B724' in the database.
Error SQL72045: Script execution error. The executed script:
ALTER TABLE [HumanResources].[Department]
    ADD CONSTRAINT [DF_Department_ModifiedDate_] DEFAULT ('') FOR [ModifiedDate];
```

Here are the causes and solutions to work around this error:

1. Verify that the destination you're importing into is an empty database.
1. If your database has constraints that use the `DEFAULT` attribute (where SQL Server assigns a random name to the constraint) and an explicitly named constraint, a constraint with the same name might be created twice. Use all explicitly named constraints (don't use `DEFAULT`), or use all system-defined names (use `DEFAULT`).
1. Manually edit the `model.xml` file and rename the constraint with the name causing the error to a unique name. This option should be undertaken only if directed by Microsoft support and poses a risk of `.bacpac` corruption.

### Stack overflow exception

Large T-SQL scripts with many nested statements can cause intermittent or persistent stack overflow exceptions. When this condition occurs, the error message includes the text `Stack overflow` and a stack trace:

```output
Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragmentVisitor.Visit(Microsoft.SqlServer.TransactSql.ScriptDom.BinaryQueryExpression)
Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragmentVisitor.ExplicitVisit(Microsoft.SqlServer.TransactSql.ScriptDom.BinaryQueryExpression)
Microsoft.SqlServer.TransactSql.ScriptDom.BinaryQueryExpression.Accept(Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragmentVisitor)
Microsoft.SqlServer.TransactSql.ScriptDom.BinaryQueryExpression.AcceptChildren(Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragmentVisitor)
Microsoft.SqlServer.TransactSql.ScriptDom.BinaryQueryExpression.Accept(Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragmentVisitor)
Microsoft.SqlServer.TransactSql.ScriptDom.BinaryQueryExpression.AcceptChildren(Microsoft.SqlServer.TransactSql.ScriptDom.TSqlFragmentVisitor)
```

A parameter for SqlPackage is available on all commands, `/ThreadMaxStackSize:`, which specifies the maximum stack size for the thread running the SqlPackage process. The default value is determined by the .NET version running SqlPackage. Setting a large value can affect overall performance of SqlPackage. However, increasing this value might resolve the stack overflow exception caused by nested statements. Refactor the T-SQL code to avoid stack overflow exceptions whenever possible. If you are unable to refactor, use the `/ThreadMaxStackSize:` parameter as a workaround.

When you use the `/ThreadMaxStackSize:` parameter, tune repeated operations to the lowest value that resolves the stack overflow exception if you notice a performance impact. The value of the parameter is in megabytes (MB). For example, you can test values like `10` and `100`.

## Import action tips

For imports that contain large tables or tables with many indexes, using `/p:RebuildIndexesOfflineForDataPhase=True` or `/p:DisableIndexesForDataPhase=False` can improve performance. These properties modify the index rebuild operation to occur offline or not occur, respectively. You can use these properties and other properties to tune the [SqlPackage Import](sqlpackage-import.md) operation.

### Indexes are disabled after an import

To load data efficiently, an import disables nonclustered indexes before the data phase and rebuilds them afterward (the default `/p:DisableIndexesForDataPhase=True` behavior). If the import is interrupted or fails after the data loads but before the rebuild finishes, one or more nonclustered indexes can remain disabled. A disabled index stays in metadata, but the query optimizer ignores it, which can cause slow queries after an import that otherwise appears to succeed.

To find disabled indexes, check the `is_disabled` column in the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view:

```sql
SELECT OBJECT_SCHEMA_NAME(object_id) AS schema_name,
       OBJECT_NAME(object_id) AS table_name,
       name AS index_name
FROM sys.indexes
WHERE is_disabled = 1;
```

To re-enable a disabled index, rebuild it with [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md). Use `ALTER INDEX ALL ... REBUILD` to enable all disabled indexes on a table:

```sql
ALTER INDEX ALL ON <schema>.<table> REBUILD;
```

For more information, see [Enable indexes and constraints](../../relational-databases/indexes/enable-indexes-and-constraints.md).

## Export action tips

For an export to be transactionally consistent, ensure either that no write activity is occurring during the export, or that you're exporting from a [transactionally consistent copy](/azure/azure-sql/database/database-copy) of your database. If you receive errors about foreign key constraints during an import, the export might not be transactionally consistent due to inserted or updated records during the export process.

### Performance during export

A common cause of performance degradation during export is unresolved object references. This issue causes SqlPackage to attempt to resolve the object multiple times. For example, a view is defined that references a table but the table no longer exists in the database. If unresolved references appear in the export log, consider correcting the schema of the database to improve the export performance.

During an export process, the table data is compressed in the bacpac file. Setting `/p:CompressionOption` to `Fast`, `SuperFast`, or `NotCompressed` might improve the export process speed while compressing the output bacpac file less.

To obtain the database schema and data while skipping the schema validation, perform an [Export](sqlpackage-export.md) with the property `/p:VerifyExtraction=False`. An invalid export might be produced that can't be imported.

### Disk space during export

In scenarios where the OS disk space is limited and runs out during the export, use `/p:TempDirectoryForTableData` to buffer the data for export on an alternative disk. The space required for this action might be large and is relative to the full size of the database. You can tune the [SqlPackage Export](sqlpackage-export.md) operation by setting this and other properties.

## Azure SQL Database

The following tips are specific to running import or export against Azure SQL Database from an Azure virtual machine (VM):

- Use Business Critical or Premium tier database for best performance.
- Use SSD storage on the VM.
- Ensure there's enough room to unzip the bacpac.
- Execute SqlPackage from a VM in the same region as the database.
- Enable accelerated networking in the VM.

For more information about using a PowerShell script to collect details about an import operation, see [Lesson Learned #211: Monitoring SQLPackage Import Process](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-211-monitoring-sqlpackage-import-process/3556382).

## More resources

The [Azure Database Support Blog](https://techcommunity.microsoft.com/category/azuredatabases/blog/azuredbsupport) contains many articles on troubleshooting and performance tuning for Azure SQL Database, including several articles on SqlPackage.

Some of the most relevant articles include:

- [Optimizing BACPAC Imports - SqlPackage Done Right!](https://techcommunity.microsoft.com/blog/azuredbsupport/azure-sql-optimizing-bacpac-imports---sqlpackage-done-right/4472021)
- [Lessons Learned #535: BACPAC Import Failures in Azure SQL Database due to Incompatible Users](https://techcommunity.microsoft.com/blog/azuredbsupport/lessons-learned-535-bacpac-import-failures-in-azure-sql-database-due-to-incompat/4455456)
- [Lesson Learned #523: Measuring Import Time -Parsing SqlPackage Logs with PowerShell](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-523-measuring-import-time--parsing-sqlpackage-logs-with-powershel/4422436)
- [How to skip external data source references while doing export/Restore of an Azure SQL DB](https://techcommunity.microsoft.com/blog/azuredbsupport/how-to-skip-external-data-source-references-while-doing-exportrestore-of-an-azur/4377910)
- [Migrating an Azure SQL DB to a SQL MI by utilizing SqlPackage/ADF](https://techcommunity.microsoft.com/blog/azuredbsupport/migrating-an-azure-sql-db-to-a-sql-mi-by-utilizing-sqlpackageadf/4061633)
- [Lesson Learned #446: Simplifying SQLPackage Log Debugging with PowerShell](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-446-simplifying-sqlpackage-log-debugging-with-powershell/3960502)
- [How to use Sqlpackage with Managed Identity](https://techcommunity.microsoft.com/blog/azuredbsupport/how-to-use-sqlpackage-with-managed-identity/3642942)
- [Lesson Learned #298: Huge duration of database export using sqlpackage](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-298-huge-duration-of-database-export-using-sqlpackage/3721709)
- [Lesson Learned #281: Export fails due to system out of memory exception](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-281-export-fails-due-to-system-out-of-memory-exception/3715249)
- [Lesson Learned #281: Troubleshooting CHECK constraint issue importing a bacpac due to business logic](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-281-troubleshooting-check-constraint-issue-importing-a-bacpac-due/3715730)
- [Lesson Learned #272: Execution Timeout Expired error message importing a Bacpac file](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-272-execution-timeout-expired-error-message-importing-a-bacpac-fi/3712268)
- [Lesson Learned #213: Cannot set the AccessToken property if the Integrated Security has been set](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-213-cannot-set-the-accesstoken-property-if-the-integrated-securit/3563343)
- [Lesson Learned #211: Monitoring SQLPackage Import Process](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-211-monitoring-sqlpackage-import-process/3556382)
- [Lesson Learned #51: Managed Instance - Import via Sqlpackage.exe doesn't allow autogrow](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-51-managed-instance---import-via-sqlpackage-exe-doesnt-allow-auto/369147)
- [Lesson Learned #32: How to export multiple databases from SQL Server to Bacpac](https://techcommunity.microsoft.com/blog/azuredbsupport/lesson-learned-32-how-to-export-multiple-databases-from-sql-server-to-bacpac/369017)
- [Step By Step: How to use SQLPackage with Access Token](https://techcommunity.microsoft.com/blog/azuredbsupport/step-by-step-how-to-use-sqlpackage-with-access-token/1407819)
- [Collation conflict when moving Azure SQL DB to SQL server on-premises or Azure VM using SQLPackage](https://techcommunity.microsoft.com/blog/azuredbsupport/collation-conflict-when-moving-azure-sql-db-to-sql-server-on-premises-or-azure-v/1547319)

## Related content

- [SqlPackage](sqlpackage.md)
- [SqlPackage Import parameters and properties](sqlpackage-import.md)
- [SqlPackage Export parameters and properties](sqlpackage-export.md)
