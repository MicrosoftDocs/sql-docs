---
title: SqlPackage CLI Reference
description: Learn how to use SqlPackage with its CLI syntax. View available parameters, properties, and SQLCMD variables.
author: cwebb-msft
ms.author: chweb
ms.reviewer: drskwier
ms.date: 01/27/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
ms.collection:
  - data-tools
ms.custom:
  - sfi-ropc-nochange
---

# SqlPackage CLI reference

SqlPackage is a command-line utility for database portability and deployments in Windows, Linux, and macOS environments. The SqlPackage command-line interface (CLI) parses each invocation for parameters, properties, and SQLCMD variables.

```bash
SqlPackage {parameters} {properties} {SQLCMD variables}
```

- **Parameters** specify the action to perform, the source and target databases, and other general settings.
- **Properties** modify the default behavior of an action.
- **SQLCMD variables** pass values to the SQLCMD variables in the source file.

To create a SqlPackage command, specify an action and its additional parameters. Optionally, add properties and SQLCMD variables to further customize the command.

The following example uses SqlPackage to create a `.dacpac` file of the current database schema:

```bash
SqlPackage /Action:Extract /TargetFile:"C:\sqlpackageoutput\output_current_version.dacpac" \
 /SourceServerName:"localhost" /SourceDatabaseName:"Contoso" \
 /p:IgnoreUserLoginMappings=True /p:Storage=Memory
```

These are the parameters from this example:

- `/Action:Extract`
- `/TargetFile:"C:\sqlpackageoutput\output_current_version.dacpac"`
- `/SourceServerName:"localhost"`
- `/SourceDatabaseName:"Contoso"`

These are the properties from this example:

- `/p:IgnoreUserLoginMappings=True`
- `/p:Storage=Memory`

## SqlPackage actions

| Action | Description |
| --- | --- |
| [Version](#version) | Returns the build number of the SqlPackage application. |
| [Extract](sqlpackage-extract.md) | Creates a data-tier application (`.dacpac`) file containing the schema or schema and user data from a connected SQL database. |
| [Publish](sqlpackage-publish.md) | Incrementally updates a database schema to match the schema of a source `.dacpac` file. If the database doesn't exist on the server, the publish operation creates it. Otherwise, an existing database is updated. |
| [Export](sqlpackage-export.md) | Exports a connected SQL database - including database schema and user data - to a BACPAC file (`.bacpac`). |
| [Import](sqlpackage-import.md) | Imports the schema and table data from a BACPAC file into a new user database. |
| [DeployReport](sqlpackage-deploy-drift-report.md#deployreport-action-parameters) | Creates an XML report representing the changes that a publish action would take. |
| [DriftReport](sqlpackage-deploy-drift-report.md#driftreport-action-parameters) | Creates an XML report representing the changes applied to a registered database since it was last registered. |
| [Script](sqlpackage-script.md) | Creates a Transact-SQL incremental update script that updates the schema of a target to match the schema of a source. |

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

## Parameters

Some parameters are shared between the SqlPackage actions. The following table summarizes the parameters. For more information, use the links in the table heading to visit the specific action pages.

| Parameter | Short Form | [Extract](sqlpackage-extract.md#parameters-for-the-extract-action) | [Publish](sqlpackage-publish.md#parameters-for-the-publish-action) | [Export](sqlpackage-export.md#parameters-for-the-export-action) | [Import](sqlpackage-import.md#parameters-for-the-import-action) | [DeployReport](sqlpackage-deploy-drift-report.md#deployreport-action-parameters) | [DriftReport](sqlpackage-deploy-drift-report.md#driftreport-action-parameters) | [Script](sqlpackage-script.md#parameters-for-the-script-action) |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `/AccessToken:` | `/at` | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| `/ClientId:` | `/cid` | No | Yes | No | No | No | No | No |
| `/DeployScriptPath:` | `/dsp` | No | Yes | No | No | No | No | Yes |
| `/DeployReportPath:` | `/drp` | No | Yes | No | No | No | No | Yes |
| `/Diagnostics:` | `/d` | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| `/DiagnosticsFile:` | `/df` | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| `/DiagnosticsPackageFile:` | `/dpf` | No | Yes | No | Yes | No | No | No |
| `/MaxParallelism:` | `/mp` | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| `/OutputPath:` | `/op` | No | No | No | No | Yes | Yes | Yes |
| `/OverwriteFiles:` | `/of` | Yes | Yes | Yes | No | Yes | Yes | Yes |
| `/Profile:` | `/pr` | No | Yes | No | No | Yes | No | Yes |
| `/Properties:` | `/p` | Yes | Yes | Yes | Yes | Yes | No | Yes |
| `/Quiet:` | `/q` | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| `/Secret:` | `/secr` | No | Yes | No | No | No | No | No |
| `/SourceConnectionString:` | `/scs` | Yes | Yes | Yes | No | Yes | No | Yes |
| `/SourceDatabaseName:` | `/sdn` | Yes | Yes | Yes | No | Yes | No | Yes |
| `/SourceEncryptConnection:` | `/sec` | Yes | Yes | Yes | No | Yes | No | Yes |
| `/SourceFile:` | `/sf` | No | Yes | No | Yes | Yes | No | Yes |
| `/SourcePassword:` | `/sp` | Yes | Yes | Yes | No | Yes | No | Yes |
| `/SourceServerName:` | `/ssn` | Yes | Yes | Yes | No | Yes | No | Yes |
| `/SourceTimeout:` | `/st` | Yes | Yes | Yes | No | Yes | No | Yes |
| `/SourceTrustServerCertificate:` | `/stsc` | Yes | Yes | Yes | No | Yes | No | Yes |
| `/SourceUser:` | `/su` | Yes | Yes | Yes | No | Yes | No | Yes |
| `/TargetConnectionString:` | `/tcs` | No | No | No | Yes | Yes | Yes | Yes |
| `/TargetDatabaseName:` | `/tdn` | No | Yes | No | Yes | Yes | Yes | Yes |
| `/TargetEncryptConnection:` | `/tec` | No | Yes | No | Yes | Yes | Yes | Yes |
| `/TargetFile:` | `/tf` | Yes | No | Yes | No | Yes | No | Yes |
| `/TargetPassword:` | `/tp` | No | Yes | No | Yes | Yes | Yes | Yes |
| `/TargetServerName:` | `/tsn` | No | Yes | No | Yes | Yes | Yes | Yes |
| `/TargetTimeout:` | `/tt` | No | Yes | No | Yes | Yes | Yes | Yes |
| `/TargetTrustServerCertificate:` | `/ttsc` | No | Yes | No | Yes | Yes | Yes | Yes |
| `/TargetUser:` | `/tu` | No | Yes | No | Yes | Yes | Yes | Yes |
| `/TenantId:` | `/tid` | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| `/UniversalAuthentication:` | `/ua` | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| `/Variables:` | `/v` | No | No | No | No | Yes | No | Yes |

## Properties

SqlPackage actions support many properties to modify the default behavior of an action. Add `/p:PropertyName=Value` to the command line to optionally use properties. You can specify multiple properties, and specify some properties more than once. For example, you can use `/p:TableData=Product /p:TableData=ProductCategory`. For more information on properties, see the specific action pages.

## SQLCMD variables

You can build SQLCMD variables into a `.dacpac` file from a SQL project. Set these variables during deployment using SqlPackage [Publish](sqlpackage-publish.md) or [Script](sqlpackage-script.md). For more information on adding SQLCMD variables to a SQL project, see [SQL Database Projects extension](../visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md).

## Utility commands

### Version

Displays the sqlpackage version as a build number. Use it in interactive prompts and in [automated pipelines](sqlpackage-pipelines.md).

```cmd
SqlPackage /Version
```

### Help

Use `/?` or `/help:True` to display SqlPackage usage information.

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

## Related content

- [SqlPackage Extract parameters and properties](sqlpackage-extract.md)
- [SqlPackage Publish parameters, properties, and SQLCMD variables](sqlpackage-publish.md)
- [SqlPackage Export parameters and properties](sqlpackage-export.md)
- [SqlPackage Import parameters and properties](sqlpackage-import.md)
- [Troubleshoot issues and performance with SqlPackage](troubleshooting-issues-and-performance-with-sqlpackage.md)
- [DacFx GitHub repository](https://github.com/microsoft/DacFx)
