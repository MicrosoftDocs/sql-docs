---
title: SqlPackage
description: Learn how to automate database development tasks with SqlPackage. View examples and available parameters, properties, and SQLCMD variables.
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 5/10/2023
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
---

# SqlPackage

**SqlPackage** is a command-line utility that automates the following database development tasks by exposing some of the public Data-Tier Application Framework (DacFx) APIs:  
  
- [Version](#version): Returns the build number of the SqlPackage application.  Added in version 18.6.

- [Extract](sqlpackage-extract.md): Creates a data-tier application (.dacpac) file containing the schema or schema and user data from a connected SQL database.  
  
- [Publish](sqlpackage-publish.md): Incrementally updates a database schema to match the schema of a source .dacpac file. If the database does not exist on the server, the publish operation creates it. Otherwise, an existing database is updated.  
  
- [Export](sqlpackage-export.md): Exports a connected SQL database - including database schema and user data - to a BACPAC file (.bacpac).  
  
- [Import](sqlpackage-import.md): Imports the schema and table data from a BACPAC file into a new user database.  
  
- [DeployReport](sqlpackage-deploy-drift-report.md): Creates an XML report of the changes that would be made by a publish action.  
  
- [DriftReport](sqlpackage-deploy-drift-report.md): Creates an XML report of the changes that have been made to a registered database since it was last registered.  
  
- [Script](sqlpackage-script.md): Creates a Transact-SQL incremental update script that updates the schema of a target to match the schema of a source.  
  
The **SqlPackage** command line tool allows you to specify these actions along with action-specific parameters and properties.  

**[Download the latest version](sqlpackage-download.md)**. For details about the latest release, see the [release notes](release-notes-sqlpackage.md).
  
## Command-Line Syntax

**SqlPackage** initiates the actions specified using the [parameters](#parameters), [properties](#properties), and SQLCMD variables specified on the command line.  
  
```bash
SqlPackage {parameters} {properties} {SQLCMD variables}
```

### Exit codes

SqlPackage commands return the following exit codes:

- 0 = success
- non-zero = failure

### Usage example

Further examples are available on the individual action pages.

**Creating a .dacpac file of the current database schema:**

```cmd
SqlPackage /TargetFile:"C:\sqlpackageoutput\output_current_version.dacpac" /Action:Extract /SourceServerName:"." /SourceDatabaseName:"Contoso.Database"
```

### Parameters

Some parameters are shared between the SqlPackage actions. Below is a table summarizing the parameters, for more information click into the specific action pages.

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

### Properties

SqlPackage actions support a large number of properties to modify the default behavior of an action.  For more information click into the specific action pages.


## Utility commands

### Version

Displays the sqlpackage version as a build number.  Can be used in interactive prompts as well as in [automated pipelines](sqlpackage-pipelines.md).

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

## Authentication

SqlPackage authenticates using methods available in [SqlClient](/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring). Configuring the authentication type can be accomplished via the connection string parameters for each SqlPackage action (`/SourceConnectionString` and `/TargetConnectionString`) or through individual parameters for connection properties. The following authentication methods are supported in a connection string:

- SQL Server authentication
- Active Directory (Windows) authentication
- [Azure Active Directory authentication](/azure/azure-sql/database/authentication-aad-overview)
    - Username/password
    - Integrated authentication
    - Universal authentication
    - **Managed identity**
    - Service principal


### Managed identity

In automated environments [Azure Active Directory Managed identity](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity) is the recommended authentication method.  This method does not require passing credentials to SqlPackage at runtime.  The managed identity is configured for the environment where the SqlPackage action is run and the SqlPackage action will use that identity to authenticate to Azure SQL.  For more information on configuring Managed identity for your environment, please see the [Managed identity documentation](/azure/active-directory/managed-identities-azure-resources/overview).

An example connection string using system-assigned managed identity is:

```bash
Server=sampleserver.database.windows.net; Authentication=Active Directory Managed Identity; Database=sampledatabase;
```


## Environment variables

### Connection pooling

Connection pooling can be enabled for all connections made by SqlPackage by setting the `CONNECTION_POOLING_ENABLED` environment variable to `True`.  This setting is recommended for operations with Azure Active Directory username/password connections to avoid MSAL throttling.


### Temporary files

During SqlPackage operations the table data is written to temporary files before compression or after decompression. For large databases these temporary files can take up a significant amount of disk space but their location can be specified.  The export and extract operations include an optional property to specify `/p:TempDirectoryForTableData` to override the SqlPackage's default value.

The default value is established by [GetTempPath](/dotnet/api/system.io.path.gettemppath) within SqlPackage.

For Windows, the following environment variables are checked in the following order and the first path that exists is used:
1. The path specified by the TMP environment variable.
2. The path specified by the TEMP environment variable.
3. The path specified by the USERPROFILE environment variable.
4. The Windows directory.

For Linux and macOS, if the path is not specified in the TMPDIR environment variable, the default path /tmp/ is used.

## SqlPackage and database users

[Contained database users](../../relational-databases/security/contained-database-users-making-your-database-portable.md) are included in SqlPackage operations.  However, the password portion of the definition is set to a randomly generated string by SqlPackage, the existing value is not transferred. It is recommended that the new user's password is reset to a secure value following the import of a `.bacpac` or the deployment of a `.dacpac`.  In an automated environment the password values can be retrieved from a secure keystore, such as Azure Key Vault, in a step following SqlPackage.


## Usage data collection

SqlPackage contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft.

SqlPackage may collect standard computer, use, and performance information that may be transmitted to Microsoft and analyzed to improve the quality, security, and reliability of SqlPackage.

SqlPackage doesn't collect user specific or personal information. To help approximate a single user for diagnostic purposes, SqlPackage will generate a random GUID for each computer it runs on and use that value for all events it sends.

For details, see the [Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkID=824704), and [SQL Server Privacy supplement](../../sql-server/sql-server-privacy.md).

### Disable telemetry reporting

To disable telemetry collection and reporting, update the environment variable `DACFX_TELEMETRY_OPTOUT` to `true` or `1`.

## Support

 The DacFx library and the SqlPackage CLI tool have adopted the [Microsoft Modern Lifecycle Policy](https://support.microsoft.com/help/30881/modern-lifecycle-policy). All security updates, fixes, and new features will be released only in the latest point version of the major version. Maintaining your DacFx or SqlPackage installations to the current version helps ensure that you will receive all applicable bug fixes in a timely manner.

### Supported SQL offerings

SqlPackage and DacFx supports all [supported SQL versions](/lifecycle/products/?products=sql-server) at time of the SqlPackage/DacFx release. For example, a SqlPackage release on January 14th 2022 supports all supported versions of SQL in January 14th 2022. For more on SQL support policies, see [the SQL support policy](/troubleshoot/sql/general/support-policy-sql-server#support-policy).

## Next steps

- Learn more about [SqlPackage Extract](sqlpackage-extract.md)
- Learn more about [SqlPackage Publish](sqlpackage-publish.md)
- Learn more about [SqlPackage Export](sqlpackage-export.md)
- Learn more about [SqlPackage Import](sqlpackage-import.md)
