---
title: SqlPackage
description: Learn how to automate database development tasks with SqlPackage. View examples and available parameters, properties, and SQLCMD variables.
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 4/29/2024
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
---

# SqlPackage

**SqlPackage** is a command-line utility that automates the database development tasks by exposing some of the public Data-Tier Application Framework (DacFx) APIs.  The primary use cases for SqlPackage focus on database portability and deployments for the SQL Server, Azure SQL, and Azure Synapse Analytics family of databases. SqlPackage can be automated using [Azure DevOps pipelines and GitHub actions](sqlpackage-pipelines.md) or other CI/CD tools.

## Portability

Database portability is the ability to move a database schema and data between different instances of SQL Server, Azure SQL, and Azure Synapse Analytics. Exporting a database from Azure SQL Database to an on-premises SQL Server instance, or from SQL Server to Azure SQL Database, are examples of database portability. SqlPackage supports database portability through the [Export](sqlpackage-export.md) and [Import](sqlpackage-import.md) actions, which create and consume BACPAC files. SqlPackage also supports database portability through the [Extract](sqlpackage-extract.md) and [Publish](sqlpackage-publish.md) actions, which create and consume DACPAC files, which can either contain the data directly or reference [data stored in Azure Blob Storage](sqlpackage-with-data-in-parquet-files.md).

## Deployments

Database deployments are the process of updating a database schema and data to match a desired state. SqlPackage supports database deployments through the [Publish](sqlpackage-publish.md) and [Extract](sqlpackage-extract.md) actions. The Publish action updates a database schema to match the schema of a source .dacpac file, while the Extract action creates a data-tier application (.dacpac) file containing the schema or schema and user data from a connected SQL database. SqlPackage enables deployments against both new or existing databases from the same artifact (.dacpac) by automatically creating a deployment plan that will apply the necessary changes to the target database.  The deployment plan can be reviewed before applying the changes to the target database with either the [Script](sqlpackage-script.md) or [DeployReport](sqlpackage-deploy-drift-report.md) actions.

### SqlPackage actions
  
- [Version](#version): Returns the build number of the SqlPackage application.

- [Extract](sqlpackage-extract.md): Creates a data-tier application (.dacpac) file containing the schema or schema and user data from a connected SQL database. 
  
- [Publish](sqlpackage-publish.md): Incrementally updates a database schema to match the schema of a source .dacpac file. If the database doesn't exist on the server, the publish operation creates it. Otherwise, an existing database is updated. 
  
- [Export](sqlpackage-export.md): Exports a connected SQL database - including database schema and user data - to a BACPAC file (.bacpac). 
  
- [Import](sqlpackage-import.md): Imports the schema and table data from a BACPAC file into a new user database. 
  
- [DeployReport](sqlpackage-deploy-drift-report.md): Creates an XML report representing the changes that a publish action would take. 
  
- [DriftReport](sqlpackage-deploy-drift-report.md): Creates an XML report representing the changes applied to a registered database since it was last registered. 
  
- [Script](sqlpackage-script.md): Creates a Transact-SQL incremental update script that updates the schema of a target to match the schema of a source. 
  
The **SqlPackage** command line tool allows you to specify these actions along with action-specific parameters and properties. 

**[Download the latest version](sqlpackage-download.md)**. For details about the latest release, see the [release notes](release-notes-sqlpackage.md).
  
[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

## Command-Line Syntax

**SqlPackage** initiates the actions specified using the [parameters](cli-reference.md#parameters), [properties](cli-reference.md#properties), and SQLCMD variables specified on the command line. 
  
```bash
SqlPackage {parameters} {properties} {SQLCMD variables}
```
More information on the SqlPackage command-line syntax is detailed in the [SqlPackage CLI reference](cli-reference.md) and individual action pages.

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

## Authentication

SqlPackage authenticates using methods available in [SqlClient](/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring). Configuring the authentication type can be accomplished via the connection string parameters for each SqlPackage action (`/SourceConnectionString` and `/TargetConnectionString`) or through individual parameters for connection properties. The following authentication methods are supported in a connection string:

- SQL Server authentication
- Active Directory (Windows) authentication
- [Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview)
    - Username/password
    - Integrated authentication
    - Universal authentication
    - **Managed identity**
    - Service principal


### Managed identity

[!INCLUDE [entra-id](../../includes/entra-id.md)]

In automated environments, [Microsoft Entra managed identity](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity) is the recommended authentication method. This method doesn't require passing credentials to SqlPackage at runtime. When the managed identity is configured for the environment where the SqlPackage action is run, the SqlPackage action can use that identity to authenticate to Azure SQL. For more information on configuring a managed identity for your environment, see the [Managed identity documentation](/azure/active-directory/managed-identities-azure-resources/overview).

An example connection string using system-assigned managed identity is:

```bash
Server=sampleserver.database.windows.net; Authentication=Active Directory Managed Identity; Database=sampledatabase;
```


## Environment variables

### Connection pooling

Connection pooling can be enabled for all connections made by SqlPackage by setting the `CONNECTION_POOLING_ENABLED` environment variable to `True`. This setting is recommended for operations with Microsoft Entra username and password connections to avoid throttling by the Microsoft Authentication Library (MSAL).


### Temporary files

During SqlPackage operations, the table data is written to temporary files before compression or after decompression. For large databases these temporary files can take up a significant amount of disk space but their location can be specified. The export and extract operations include an optional property to specify `/p:TempDirectoryForTableData` to override the SqlPackage's default value.

The .NET API [GetTempPath](/dotnet/api/system.io.path.gettemppath) is used to determine the default value within SqlPackage.

For Windows, the following environment variables are checked in the following order and the first path that exists is used:
1. The path specified by the `TMP` environment variable.
2. The path specified by the `TEMP` environment variable.
3. The path specified by the `USERPROFILE` environment variable.
4. The Windows directory.

For Linux and macOS, if the path isn't specified in the `TMPDIR` environment variable, the default path `/tmp/` is used.

## SqlPackage and database users

[Contained database users](../../relational-databases/security/contained-database-users-making-your-database-portable.md) are included in SqlPackage operations. However, the password portion of the definition is set to a randomly generated string by SqlPackage, the existing value isn't transferred. It's recommended that the new user's password is reset to a secure value following the import of a `.bacpac` or the deployment of a `.dacpac`. In an automated environment the password values can be retrieved from a secure keystore, such as Azure Key Vault, in a step following SqlPackage.


## Usage data collection

SqlPackage contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft.

SqlPackage may collect standard computer, use, and performance information that may be transmitted to Microsoft and analyzed to improve the quality, security, and reliability of SqlPackage.

SqlPackage doesn't collect user specific or personal information. To help approximate a single user for diagnostic purposes, SqlPackage generates a random GUID for each computer it runs on and use that value for all events it sends.

For details, see the [Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkID=824704), and [SQL Server Privacy supplement](../../sql-server/sql-server-privacy.md).

### Disable telemetry reporting

To disable telemetry collection and reporting, update the environment variable `DACFX_TELEMETRY_OPTOUT` to `true` or `1`.

## Support

The DacFx library and the SqlPackage CLI tool have adopted the [Microsoft Modern Lifecycle Policy](https://support.microsoft.com/help/30881/modern-lifecycle-policy). All security updates, fixes, and new features are released only in the latest point version of the major version. Maintaining your DacFx or SqlPackage installations to the current version helps ensure that you receive all applicable bug fixes in a timely manner.

Get help with SqlPackage, submit feature requests, and report issues in the [DacFx GitHub repository](https://github.com/microsoft/DacFx).

### Supported SQL offerings

SqlPackage and DacFx support all [supported SQL versions](/lifecycle/products/?products=sql-server) at time of the SqlPackage/DacFx release. For example, a SqlPackage release on January 14 2022 supports all supported versions of SQL in January 14 2022. For more on SQL support policies, see [the SQL support policy](/troubleshoot/sql/general/support-policy-sql-server#support-policy).

In addition to SQL Server, SqlPackage and DacFx supports Azure SQL Managed Instance, Azure SQL Database, Azure Synapse Analytics, and Synapse Data Warehouse in Microsoft Fabric.

## Next steps

- Learn more about [SqlPackage Extract](sqlpackage-extract.md)
- Learn more about [SqlPackage Publish](sqlpackage-publish.md)
- Learn more about [SqlPackage Export](sqlpackage-export.md)
- Learn more about [SqlPackage Import](sqlpackage-import.md)
- Learn more about [troubleshooting issues with SqlPackage](troubleshooting-issues-and-performance-with-sqlpackage.md)
- Share feedback on SqlPackage in the [DacFx GitHub repository](https://github.com/microsoft/DacFx)
