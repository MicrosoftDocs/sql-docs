---
title: SqlPackage CLI reference
description: Learn how to use SqlPackage with its CLI syntax. View available parameters, properties, and SQLCMD variables.
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 4/29/2024
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
---

# SqlPackage CLI reference

SqlPackage is a command-line utility for database portability and deployments in Windows, Linux, and macOS environments.  The SqlPackage command-line interface (CLI) parses each invocation for parameters, properties, and SQLCMD variables.

```bash
SqlPackage {parameters} {properties} {SQLCMD variables}
```

- **Parameters** are used to specify the action to be performed, the source and target databases, and other general settings.
- **Properties** are used to modify the default behavior of an action.
- **SQLCMD variables** are used to pass values to the SQLCMD variables in the source file.

To create a SqlPackage command, you must specify an action and its additional parameters. Optionally you may add properties and SQLCMD variables to further customize the command.

In the following example, SqlPackage is used to create a .dacpac file of the current database schema:

```cmd
SqlPackage /Action:Extract /TargetFile:"C:\sqlpackageoutput\output_current_version.dacpac" \
 /SourceServerName:"localhost" /SourceDatabaseName:"Contoso" \
 /p:IgnoreUserLoginMappings=True /p:Storage=Memory
```
In that example, the parameters were:
- `/Action:Extract`
- `/TargetFile:"C:\sqlpackageoutput\output_current_version.dacpac"`
- `/SourceServerName:"localhost"`
- `/SourceDatabaseName:"Contoso"`

In that example, the properties were:
- `/p:IgnoreUserLoginMappings=True`
- `/p:Storage=Memory`

## SqlPackage actions
  
- [Version](#version): Returns the build number of the SqlPackage application.

- [Extract](sqlpackage-extract.md): Creates a data-tier application (.dacpac) file containing the schema or schema and user data from a connected SQL database. 
  
- [Publish](sqlpackage-publish.md): Incrementally updates a database schema to match the schema of a source .dacpac file. If the database doesn't exist on the server, the publish operation creates it. Otherwise, an existing database is updated. 
  
- [Export](sqlpackage-export.md): Exports a connected SQL database - including database schema and user data - to a BACPAC file (.bacpac). 
  
- [Import](sqlpackage-import.md): Imports the schema and table data from a BACPAC file into a new user database. 
  
- [DeployReport](sqlpackage-deploy-drift-report.md): Creates an XML report representing the changes that a publish action would take. 
  
- [DriftReport](sqlpackage-deploy-drift-report.md): Creates an XML report representing the changes applied to a registered database since it was last registered. 
  
- [Script](sqlpackage-script.md): Creates a Transact-SQL incremental update script that updates the schema of a target to match the schema of a source. 
  
  
[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]


## Parameters

Some parameters are shared between the SqlPackage actions. Below is a table summarizing the parameters, for more information, click into the specific action pages.

| Parameter | Short Form | [Extract](sqlpackage-extract.md#parameters-for-the-extract-action) | [Publish](sqlpackage-publish.md#parameters-for-the-publish-action) | [Export](sqlpackage-export.md#parameters-for-the-export-action) | [Import](sqlpackage-import.md#parameters-for-the-import-action) | [DeployReport](sqlpackage-deploy-drift-report.md#deployreport-action-parameters) | [DriftReport](sqlpackage-deploy-drift-report.md#driftreport-action-parameters) | [Script](sqlpackage-script.md#parameters-for-the-script-action) |
|---|---|---|---|---|---|---|---|---|
|**/AccessToken:**|**/at**| x | x | x | x | x | x | x |
|**/ClientId:**|**/cid**| | x | | | | | |
|**/DeployScriptPath:**|**/dsp**| | x | | | | | x |
|**/DeployReportPath:**|**/drp**| | x | | | | | x |
|**/Diagnostics:**|**/d**| x | x | x | x | x | x | x |
|**/DiagnosticsFile:**|**/df**| x | x | x | x | x | x | x |
|**/MaxParallelism:**|**/mp**| x | x | x | x | x | x | x |
|**/OutputPath:**|**/op**|  |  |  | | x | x | x |
|**/OverwriteFiles:**|**/of**| x | x | x | | x | x | x |
|**/Profile:**|**/pr**| | x | | | x | | x |
|**/Properties:**|**/p**| x | x | x | x | x | | x |
|**/Quiet:**|**/q**| x | x | x | x | x | x | x |
|**/Secret:**|**/secr**| | x | | | | | |
|**/SourceConnectionString:**|**/scs**| x | x | x | | x | | x |
|**/SourceDatabaseName:**|**/sdn**| x | x | x | | x | | x |
|**/SourceEncryptConnection:**|**/sec**| x | x | x | | x | | x |
|**/SourceFile:**|**/sf**| | x | | x | x | | x |
|**/SourcePassword:**|**/sp**| x | x | x | | x | | x |
|**/SourceServerName:**|**/ssn**| x | x | x | | x | | x |
|**/SourceTimeout:**|**/st**| x | x | x | | x | | x |
|**/SourceTrustServerCertificate:**|**/stsc**| x | x | x | | x | | x |
|**/SourceUser:**|**/su**| x | x | x | | x | | x |
|**/TargetConnectionString:**|**/tcs**| | | | x | x | x | x |
|**/TargetDatabaseName:**|**/tdn**| | x | | x | x | x | x |
|**/TargetEncryptConnection:**|**/tec**| | x | | x | x | x | x |
|**/TargetFile:**|**/tf**| x | | x | | x | | x |
|**/TargetPassword:**|**/tp**| | x | | x | x | x | x |
|**/TargetServerName:**|**/tsn**| | x | | x | x | x | x |
|**/TargetTimeout:**|**/tt**| | x | | x | x | x | x |
|**/TargetTrustServerCertificate:**|**/ttsc**| | x | | x | x | x | x |
|**/TargetUser:**|**/tu**| | x | | x | x | x | x |
|**/TenantId:**|**/tid**| x | x | x | x | x | x | x |
|**/UniversalAuthentication:**|**/ua**| x | x | x | x | x | x | x |
|**/Variables:**|**/v**| | | | | x | | x |

## Properties

SqlPackage actions support a large number of properties to modify the default behavior of an action. The optional use of properties is accomplished by adding `/p:PropertyName=Value` to the command line. Multiple properties can be specified and some properties can be specified more than once (e.g. `/p:TableData=Product /p:TableData=ProductCategory`). For more information on properties refer to the specific action pages.

## SQLCMD variables

SQLCMD variables can be built into a .dacpac file from a SQL project, then set during deployment with SqlPackage [Publish](sqlpackage-publish.md) or [Script](sqlpackage-script.md). For more information on adding SQLCMD variables to a SQL project, see the [SQL projects documentation](/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects#sqlcmd-variables).


## Utility commands

### Version

Displays the sqlpackage version as a build number. Can be used in interactive prompts and in [automated pipelines](sqlpackage-pipelines.md).

```cmd
SqlPackage /Version
```

### Help

You can display SqlPackage usage information by using `/?` or `/help:True`.

```cmd
SqlPackage /?
```

For parameter and property information specific to a particular action, use the help parameter in addition to that action's parameter.

```cmd
SqlPackage /Action:Publish /?
```

## Exit codes

SqlPackage commands return the following exit codes:

- 0 = success
- nonzero = failure

## Next steps

- Learn more about [SqlPackage Extract](sqlpackage-extract.md)
- Learn more about [SqlPackage Publish](sqlpackage-publish.md)
- Learn more about [SqlPackage Export](sqlpackage-export.md)
- Learn more about [SqlPackage Import](sqlpackage-import.md)
- Learn more about [troubleshooting issues with SqlPackage](troubleshooting-issues-and-performance-with-sqlpackage.md)
- Share feedback on SqlPackage in the [DacFx GitHub repository](https://github.com/microsoft/DacFx)
