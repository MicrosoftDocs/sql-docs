---
title: Troubleshooting issues and performance with SqlPackage
description: Learn how to troubleshoot with SqlPackage.
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 05/31/2024
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
---

# Troubleshooting issues and performance with SqlPackage

In some scenarios, SqlPackage operations take longer than expected or fail to complete.  This article describes some frequently suggested tactics to troubleshoot or improve performance of these operations. While reading the specific documentation page for each action to understand the available parameters and properties is recommended, this article serves as a starting point in investigating SqlPackage operations.

## Overall strategy

As general guideline, better performance can be obtained via the [.NET version](sqlpackage-download.md#installation-cross-platform) of SqlPackage instead of the .NET Framework version installed via the DacFramework.msi.

If you're unable to install the SqlPackage [dotnet tool](sqlpackage-download.md#installation-cross-platform), which enables executing SqlPackage commands from the command prompt in any directory:

1. [Download](sqlpackage-download.md#installation-file-download-alternative) the zip for SqlPackage on .NET 8 for your operating system (Windows, macOS, or Linux).
1. Unzip archive as directed on the download page.
1. Open a command prompt and change directory (`cd`) to the SqlPackage folder.

It's important to use the latest available version of SqlPackage as performance improvements and bug fixes are released regularly.

### Substitute SqlPackage for the Import/Export Service
If you have attempted to use the Import/Export Service to import or export your database, you can use SqlPackage to perform the same operation with more control on optional parameters and properties.

For Import, an example command is:

```bash
./SqlPackage /Action:Import /sf:<source-bacpac-file-path> /tsn:<full-target-server-name> /tdn:<a new or empty database> /tu:<target-server-username> /tp:<target-server-password> /df:<log-file>
```

For Export, an example command is:

```bash
./SqlPackage /Action:Export /tf:<target-bacpac-file-path> /ssn:<full-source-server-name> /sdn:<source-database-name> /su:<source-server-username> /sp:<source-server-password> /df:<log-file>
```

Alternative to username and password, [multifactor authentication](/azure/azure-sql/database/authentication-mfa-ssms-overview) can be used to authenticate via Microsoft Entra authentication (formerly Azure Active Directory) with multifactor authentication. Substitute the username and password parameters for `/ua:true` and `/tid:"yourdomain.onmicrosoft.com"`.

## Common issues

### Timeout errors

For issues related to timeouts, the following properties can be used to tune the connection between SqlPackage and the SQL instance:

- `/p:CommandTimeout=`: Specifies the command timeout in seconds when a query is executed. Default: 60
- `/p:DatabaseLockTimeout=`: Specifies the database lock timeout in seconds.  -1 can be used to wait indefinitely, default: 60
- `/p:LongRunningCommandTimeout=`: Specifies the long running command timeout in seconds.  The default value, 0, is used to wait indefinitely.

### Client resource consumption

For the export and extract commands, table data is passed to a temporary directory to buffer before being written to the bacpac/dacpac file. This storage requirement can be large and is relative to the full size of the data to be exported.  Specify an alternative temporary directory with the property `/p:TempDirectoryForTableData=<path>`.

The schema model is compiled in memory, so for large database schemas the memory requirement on the client machine running SqlPackage can be significant.


### Low server resource consumption

By default, SqlPackage sets the maximum server parallelism to 8.  If you note low server resource consumption, increasing the value of the `MaxParallelism` parameter can improve performance.

### Access token

Using the `/AccessToken:` or `/at:` parameter enables token-based authentication for SqlPackage, however passing the token to the command can be tricky.  If you're parsing an access token object in PowerShell either explicitly pass the string value or wrap the reference to the token property in $().  For example:

```powershell
$Account = Connect-AzAccount -ServicePrincipal -Tenant $Tenant -Credential $Credential
$AccessToken_Object = (Get-AzAccessToken -Account $Account -Resource "https://database.windows.net/")
$AccessToken = $AccessToken_Object.Token

SqlPackage /at:$AccessToken
# OR
SqlPackage /at:$($AccessToken_Object.Token) 
```

### Connection

If SqlPackage is failing to connect, the server might not have encryption enabled or the configured certificate might not be issued from a trusted certificate authority (such as a self-signed certificate).  You can change the SqlPackage command to either connect without encryption or to trust the server certificate.  The [best practice](../../relational-databases/security/securing-sql-server.md) is to ensure that a trusted encrypted connection to the server can be established.

- Connect without encryption: /SourceEncryptConnection=False or /TargetEncryptConnection=False
- Trust server certificate: /SourceTrustServerCertificate=True or /TargetTrustServerCertificate=True

You could see any of the following warning messages when connecting to a SQL instance, indicating that command line parameters could require changes to connect to the server:

```output
The settings for connection encryption or server certificate trust may lead to connection failure if the server is not properly configured.
The connection string provided contains encryption settings which may lead to connection failure if the server is not properly configured.
```

More information about the connection security changes in SqlPackage is available in [Connection Security Improvements in SqlPackage 161](https://aka.ms/dacfx-connection).


### Import action error 2714 for constraint

When performing an import action, you may receive error 2714 if an object already exists:

```output
*** Error importing database:Could not import package.
Error SQL72014: Core Microsoft SqlClient Data Provider: Msg 2714, Level 16, State 5, Line 1 There is already an object named 'DF_Department_ModifiedDate_0FF0B724' in the database.
Error SQL72045: Script execution error.  The executed script:
ALTER TABLE [HumanResources].[Department]
    ADD CONSTRAINT [DF_Department_ModifiedDate_] DEFAULT ('') FOR [ModifiedDate];
```

Here are the causes and solutions to work around this error:

1. Verify that the destination you're importing into is an empty database.
1. If your database has constraints that are using the DEFAULT attribute (where SQL Server assigns a random name to the constraint) and an explicitly named constraint, a constraint with the same name might be created twice. You should use all explicitly named constraints (not using DEFAULT), or all system-defined names (using DEFAULT).
1. Manually edit the model.xml and rename the constraint with the name experiencing the error to a unique name. This option should be undertaken only if directed by Microsoft support and poses a risk of .bacpac corruption.

### Stack overflow exception

Intermittent or persistent stack overflow exceptions are often caused by large T-SQL scripts with many nested statements.  A parameter for SqlPackage is available on all commands, `/ThreadMaxStackSize:`, which specifies the maximum stack size for the thread running the SqlPackage process.  The default value is determined by the .NET version running SqlPackage. Setting a large value can impact overall performance of SqlPackage, however increasing this value may resolve the stack overflow exception caused by nested statements.  Refactoring the T-SQL code is recommended to avoid stack overflow exceptions whenever possible, but the `/ThreadMaxStackSize:` parameter can be used as a workaround.  When using the `/ThreadMaxStackSize:` parameter, it is recommended to tune repeated operations to the lowest value that resolves the stack overflow exception if performance impact is noted.  Be aware that the value is in megabytes (MB), example values for testing as a workaround include 10 and 100.

## Diagnostics
Logs are essential to troubleshooting. Capture the diagnostic logs to a file with the `/DiagnosticsFile:<filename>` parameter.

More performance-related trace data can be logged by setting the environment variable `DACFX_PERF_TRACE=true` before running SqlPackage.  To set this environment variable in PowerShell, use the following command:

``` powershell
Set-Item -Path Env:DACFX_PERF_TRACE -Value true
```

## Import action tips
For imports that contain large tables or tables with many indexes, the use of `/p:RebuildIndexesOfflineForDataPhase=True` or `/p:DisableIndexesForDataPhase=False` can improve performance. These properties modify the index rebuild operation to occur offline or not occur, respectively. Those and other properties are available to tune the [SqlPackage Import](sqlpackage-import.md) operation.

## Export action tips
A common cause of performance degradation during export is unresolved object references, which causes SqlPackage to attempt to resolve the object multiple times. For example, a view is defined that references a table and the table no longer exists in the database. If unresolved references appear in the export log, consider correcting the schema of the database to improve the export performance.

In scenarios where the OS disk space is limited and runs out during the export, the use of `/p:TempDirectoryForTableData` allows the data for export to be buffered on an alternative disk. The space required for this action may be large and is relative to the full size of the database. That and other properties are available to tune the [SqlPackage Export](sqlpackage-export.md) operation.

During an export process, the table data is compressed in the bacpac file. The use of `/p:CompressionOption` set to `Fast`, `SuperFast`, or `NotCompressed` may improve the export process speed while compressing the output bacpac file less.

To obtain the database schema and data while skipping the schema validation, perform an [Export](sqlpackage-export.md) with the property `/p:VerifyExtraction=False`.  An invalid export may be produced that cannot be imported.

## Azure SQL Database

The following tips are specific to running import or export against Azure SQL Database from an Azure virtual machine (VM):

- Use Business Critical or Premium tier database for best performance.
- Use SSD storage on the VM and ensure there's enough room to unzip the bacpac.
- Execute SqlPackage from a VM in the same region as the database.
- Enable accelerated networking in the VM.

For more information on utilizing a PowerShell script to collect more information about an import operation, see [Lesson Learned #211: Monitoring SQLPackage Import Process](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-211-monitoring-sqlpackage-import-process/ba-p/3556382).

## Additional resources

The [Azure Database Support Blog](https://techcommunity.microsoft.com/t5/azure-database-support-blog/bg-p/AzureDBSupport) contains many articles on troubleshooting and performance tuning for Azure SQL Database, including several articles on SqlPackage.

Some of the most relevant articles include:

- [Migrating an Azure SQL DB to a SQL MI by utilizing SqlPackage/ADF](https://techcommunity.microsoft.com/t5/azure-database-support-blog/migrating-an-azure-sql-db-to-a-sql-mi-by-utilizing-sqlpackage/ba-p/4061633)
- [Lesson Learned #446: Simplifying SQLPackage Log Debugging with PowerShell](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-446-simplifying-sqlpackage-log-debugging-with/ba-p/3960502)
- [How to use Sqlpackage with Managed Identity](https://techcommunity.microsoft.com/t5/azure-database-support-blog/how-to-use-sqlpackage-with-managed-identity/ba-p/3642942)
- [Lesson Learned #298: Huge duration of database export using sqlpackage](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-298-huge-duration-of-database-export-using/ba-p/3721709)
- [Lesson Learned #281: Export fails due to system out of memory exception](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-281-export-fails-due-to-system-out-of-memory/ba-p/3715249)
- [Lesson Learned #281: Troubleshooting CHECK constraint issue importing a bacpac due to business logic](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-281-troubleshooting-check-constraint-issue/ba-p/3715730)
- [Lesson Learned #272: Execution Timeout Expired error message importing a Bacpac file](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-272-execution-timeout-expired-error-message/ba-p/3712268)
- [Lesson Learned #213: Cannot set the AccessToken property if the Integrated Security has been set](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-213-cannot-set-the-accesstoken-property-if-the/ba-p/3563343)
- [Lesson Learned #211: Monitoring SQLPackage Import Process](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-211-monitoring-sqlpackage-import-process/ba-p/3556382)
- [Lesson Learned #51: Managed Instance - Import via Sqlpackage.exe doesn't allow autogrow](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-51-managed-instance-import-via-sqlpackage-exe/ba-p/369147)
- [Lesson Learned #32: How to export multiple databases from SQL Server to Bacpac](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-32-how-to-export-multiple-databases-from-sql/ba-p/369017)
- [Step By Step: How to use SQLPackage with Access Token](https://techcommunity.microsoft.com/t5/azure-database-support-blog/step-by-step-how-to-use-sqlpackage-with-access-token/ba-p/1407819)
- [Collation conflict when moving Azure SQL DB to SQL server on-premises or Azure VM using SQLPackage.](https://techcommunity.microsoft.com/t5/azure-database-support-blog/collation-conflict-when-moving-azure-sql-db-to-sql-server-on/ba-p/1547319)

## Related content

- [SqlPackage overview](sqlpackage.md)
- Learn more about [SqlPackage Import](sqlpackage-import.md)
- Learn more about [SqlPackage Export](sqlpackage-export.md)