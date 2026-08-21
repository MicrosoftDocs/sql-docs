---
title: "Import a BACPAC File to Create a New Database"
description: "Import a BACPAC File to create a new database."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier, wiassaf, maghan
ms.date: 03/11/2025
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: how-to
ms.collection:
  - data-tools
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql13.swb. importdac.results.f1"
  - "sql13.swb.importdac.settings.f1"
  - "sql13.swb.importdac.storagebrowser.f1"
  - "sql13.swb.importdac.results.f1"
  - "sql13.swb.importdac.progress.f1"
  - "sql13.swb. importdac.summary.f1"
  - "sql13.swb.importdac.summary.f1"
  - "sql13.swb. importdac.progress.f1"
  - "sql13.swb.importdac.welcome.f1"
  - "sql13.swb. importdac.settings.f1"
  - "sql13.swb.dbdeployment.settings.f1"
  - "sql13.swb.dbdeployment.progress.f1"
  - "sql13.swb.dbdeployment.summary.f1"
  - "sql13.swb.dbdeployment.results.f1"
  - "sql13.swb.dbdeployment.welcome.f1"
helpviewer_keywords:
  - "Data-tier application"
  - "SQL Server DAC"
  - "Migrate database"
  - "DAC"
  - "deploy database wizard"
  - "database deploy [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Import a BACPAC file to create a new database

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Import a `.bacpac` file - to create a copy of the original database, with the data, on a new instance of the Database Engine, or to Azure SQL Database. An export operation can be combined with an import operation to migrate a database between instances or to create a copy of a database deployed in Azure SQL Database. Options for easily importing a `.bacpac` include:

1. the **Import Data-tier Application** Wizard in SQL Server Management Studio
1. the **Deploy Database to Microsoft Azure SQL Database** Wizard in SQL Server Management Studio to deploy a database between an instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] and a [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)] server, or between two [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)] servers
1. the **SqlPackage** command-line utility

## Before you begin

The import process builds a new database in two stages.

1. The import creates a new database using the database model definition stored in the `.bacpac` export file, the same way a `.dacpac` deploy creates a new database from the definition in a `.dacpac` file.

1. The import bulk copies in the data from the `.bacpac` export file.

## Database Options and Settings

By default, the database created during the import has all of the default settings from the CREATE DATABASE statement, except that the database collation and compatibility level are set to the values defined in the `.bacpac` export file. A `.bacpac` export file uses the values from the original database.

Some database options, such as TRUSTWORTHY, DB_CHAINING, and HONOR_BROKER_PRIORITY, can't be adjusted as part of the import process. Physical properties, such as the number of filegroups, or the numbers and sizes of files can't be altered as part of the import process. After the import completes, you can use the ALTER DATABASE statement, [!INCLUDE [ssManStudioFull](../../../../includes/ssmanstudiofull-md.md)], or [!INCLUDE [ssNoVersion](../../../../includes/ssnoversion-md.md)] PowerShell to tailor the database. For more information, see [Databases](../../../../relational-databases/databases/databases.md).

## Security

To improve security, SQL Server Authentication logins are stored in a `.bacpac` export file without a password. When the file is imported, the login is created as a disabled login with a generated password. To enable the logins, sign in with `ALTER ANY LOGIN` permission and use `ALTER LOGIN` to enable the login and assign a new password. This extra step isn't needed for Windows Authentication logins because their passwords aren't managed by SQL Server.

## Permissions

A `.bacpac` can only be imported by members of the **sysadmin** or **serveradmin** fixed server roles, or by logins that are in the **dbcreator** fixed server role and have `ALTER ANY LOGIN` permissions. The built-in [!INCLUDE [ssNoVersion](../../../../includes/ssnoversion-md.md)] system administrator account named `sa` can also import a `.bacpac`. Importing a `.bacpac` with logins to [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)] requires membership in the **loginmanager** or **serveradmin** roles. Importing a `.bacpac` without logins to [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)] requires membership in the **dbmanager** or **serveradmin** roles.

The **Deploy Database to Microsoft Azure SQL Database** Wizard in SQL Server Management Studio requires more permissions to export the source database. The login requires at least `ALTER ANY LOGIN` and database scope `VIEW DEFINITION` permissions, as well as `SELECT` permissions on `sys.sql_expression_dependencies`. Members of the **securityadmin** fixed server role who are also members of the **database_owner** fixed database role are permitted to export a `.bacpac`. Members of the **sysadmin** fixed server role or the built-in SQL Server system administrator account named `sa` can also export a `.bacpac`.

# [Import wizard](#tab/import-wizard)

<a id="using-the-import-data-tier-application-wizard"></a>

## Use the Import Data-tier Application Wizard

**To launch the wizard, use the following steps:**

1. Connect to the instance of [!INCLUDE [ssNoVersion](../../../../includes/ssnoversion-md.md)], whether on-premises or in [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)].

1. In **Object Explorer**, right-click on **Databases**, and then select the **Import Data-tier Application** menu item to launch the wizard.

1. Complete the wizard dialogs:

    - [Introduction Page](#Introduction)

    - [Import Settings Page](#Import_settings)

    - [Database Settings Page](#Database_settings)

    - [Summary Page](#Summary)

    - [Progress Page](#Progress)

    - [Results Page](#Results)

<a id="Introduction"></a>

### Introduction Page

This page describes the steps for the Data-tier Application Import Wizard.

**Options**

- **Do not show this page again.** - Select the check box to stop the Introduction page from being displayed in the future.

- **Next** - Proceeds to the **Import Settings** page.

- **Cancel** - Cancels the operation and closes the wizard.

<a id="Import_settings"></a>

### Import Settings Page

Use this page to specify the location of the `.bacpac` file to import.

- **Import from local disk** - Select **Browse...** to navigate the local computer, or specify the path in the space provided. The path name must include a file name and the `.bacpac` extension.

- **Import from Azure** - Imports a `.bacpac` file from a Microsoft Azure container. You must connect to a Microsoft Azure container to validate this option. The Import from Azure option also requires that you specify a local directory for the temporary file. The temporary file will be created at the specified location and will remain there after the operation completes.

    When browsing Azure, you're able to switch between containers within a single account. You must specify a single `.bacpac` file to continue the import operation. You can sort columns by **Name**, **Size**, or **Date Modified**.

    To continue, specify the `.bacpac` file to import, and then select **Open**.

<a id="Database_settings"></a>

### Database Settings Page

Use this page to specify details for the database that will be created.

**For a local instance of SQL Server:**

- **New database name** - Provide a name for the imported database.

- **Data file path** - Provide a local directory for data files. Select **Browse...** to navigate the local computer, or specify the path in the space provided.

- **Log file path** - Provide a local directory for log files. Select **Browse...** to navigate the local computer, or specify the path in the space provided.

To continue, select **Next**.

**For an Azure SQL Database:**

- **[Import a BACPAC file to create a new Azure SQL database](/azure/azure-sql/database/database-import)** provides step by step instructions using the Azure portal, PowerShell, SQL Server Management Studio, or SqlPackage.
- Consult **[SQL Database options and performance: Understand what's available in each service tier](/azure/azure-sql/database/purchasing-models)** for a detailed look at the different service tiers.

### Validation Page

Use this page to review any issues that block the operation. To continue, resolve blocking issues and then select **Re-run Validation** to ensure that validation is successful.

To continue, select **Next**.

<a id="Summary"></a>

### Summary Page

Use this page to review the specified source and target settings for the operation. To complete the import operation using the specified settings, select **Finish**. To cancel the import operation and exit the wizard, select **Cancel**.

<a id="Progress"></a>

### Progress Page

This page displays a progress bar that indicates the status of the operation. To view detailed status, select the **View details** option.

To continue, select **Next**.

<a id="Results"></a>

### Results Page

This page reports the success or failure of the import and creates database operations, showing the success or failure of each action. Any action that encountered an error has a link in the **Result** column. Select the link to view a report of the error for that action.

Select **Close** to close the wizard.

# [Deploy Database wizard](#tab/deploy-database-wizard)

<a id="LimitationsRestrictions"></a>

## Limitations

The **Deploy Database** wizard supports deploying a database:

- From an instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] to [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)].

- From [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)] to an instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)].

- Between two [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)] servers.

The wizard doesn't support deploying databases between two instances of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)].

An instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] must be running [!INCLUDE [ssVersion2005](../../../../includes/ssversion2005-md.md)] Service Pack 4 (SP4) or later to work with the wizard. If a database on an instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] contains objects not supported on [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)], you can't use the wizard to deploy the database to [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)]. If a database on [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)] contains objects not supported by [!INCLUDE [ssNoVersion](../../../../includes/ssnoversion-md.md)], you can't use the wizard to deploy the database to instances of [!INCLUDE [ssNoVersion](../../../../includes/ssnoversion-md.md)].

<a id="UsingDeployDACWizard"></a>

<a id="using-the-deploy-database-wizard"></a>

## Use the Deploy Database Wizard

**To migrate a database using the Deploy Database Wizard**

1. Connect to the location of the database you want to deploy. You can specify either an instance of [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] or a [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)] server.

1. In **Object Explorer**, expand the node for the instance that has the database.

1. Expand the **Databases** node.

1. Right-click the database you want to deploy, select **Tasks**, and then select **Deploy Database to Microsoft Azure SQL Database**

1. Complete the Wizard dialogs:

    - [Introduction Page](#Introduction)

    - [Deployment Settings](#Deployment_settings)

    - [Summary Page](#Summary)

    - [Progress](#Progress)

    - [Results](#Results)

<a id="Introduction"></a>

## Introduction Page

This page describes the steps for the **Deploy Database** Wizard.

**Options**

- **Do not show this page again.** - Select the check box to stop the Introduction page from being displayed in the future.

- **Next** - Proceeds to the **Deployment Settings** page.

- **Cancel** - Cancels the operation and closes the Wizard.

<a id="Deployment_settings"></a>

## Deployment Settings Page

Use this page to specify the destination server and to provide details about your new database.

**Local host:**

- **Server connection** - Specify server connection details and then select **Connect** to verify the connection.

- **New database name** - Specify a name for the new database.

**[!INCLUDE [ssSDS](../../../../includes/sssds-md.md)] database settings:**

- **[!INCLUDE [ssSDS](../../../../includes/sssds-md.md)] edition** - Select the edition of [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)] from the dropdown list.

- **Maximum database size** - Select the maximum database size from the dropdown list.

**Other settings:**

- Specify a local directory for the temporary file, which is the `.bacpac` archive file. The file will be created at the specified location and will remain there after the operation completes.

<a id="Summary"></a>

## Summary Page

Use this page to review the specified source and target settings for the operation. To complete the deploy operation using the specified settings, select **Finish**. To cancel the deploy operation and exit the Wizard, select **Cancel**.

<a id="Progress"></a>

## Progress Page

This page displays a progress bar that indicates the status of the operation. To view detailed status, select the **View details** option.

<a id="Results"></a>

## Results Page

This page reports the success or failure of the deploy operation, showing the results of each action. Any action that encountered an error has a link in the **Result** column. Select the link to view a report of the error for that action.

Select **Finish** to close the Wizard.

# [SqlPackage CLI](#tab/sqlpackage)

<a id="using-sqlpackage"></a>

## Use SqlPackage

**SqlPackage** initiates the actions specified using the parameters, properties, and SQLCMD variables specified on the command line. The command line syntax is as follows:

```bash
SqlPackage /Action:Import {parameters} {properties}
```

More information on the available parameters and properties can be found in the [SqlPackage import documentation](../../../sqlpackage/sqlpackage-import.md).

---

## Related content

- [Import a BACPAC file to create a new Azure SQL database](/azure/azure-sql/database/database-import)
- [Data-tier applications (DAC) overview](overview.md)
- [Export a BACPAC file](export-bacpac-file.md)
- [SqlPackage](../../../sqlpackage/sqlpackage.md)
