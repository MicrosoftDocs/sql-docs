---
title: "Deploy a Data-Tier Application"
description: "Deploy a data-tier application to create or update a database."
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
  - intro-deployment
  - ignite-2025
f1_keywords:
  - "sql13.swb.deploydacwizard.introduction.f1"
  - "sql13.swb.deploydacwizard.deploydac.f1"
  - "sql13.swb.deploydacwizard.summary.f1"
  - "sql13.swb.deploydacwizard.updateconfiguration.f1"
  - "sql13.swb.deploydacwizard.selectdac.f1"
  - "sql.data.tools.DacTableChooser"
  - "sql.data.tools.DacPublishDialog"
  - "sql.data.tools.DacPropertiesDialog"
  - "sql.data.tools.DacExtractDialog"
  - "sql13.swb.upgradedacwizard.summary.f1"
  - "sql13.swb.upgradedacwizard.reviewplan.f1"
  - "sql13.swb.upgradedacwizard.upgradedac.f1"
  - "sql13.swb.upgradedacwizard.selectpackage.f1"
  - "sql13.swb.upgradedacwizard.reviewpolicy.f1"
  - "sql13.swb.upgradedacwizard.selectoptions.f1"
  - "sql13.swb.upgradedacwizard.checkdrift.f1"
  - "sql13.swb.upgradedacwizard.introduction.f1"
helpviewer_keywords:
  - "Deploy data-tier application"
  - "deploy DAC"
  - "data-tier application [SQL Server], deploy"
  - "How to [DAC], deploy"
  - "wizard [DAC], deploy"
  - "upgrade DAC"
  - "data-tier application [SQL Server], upgrade"
  - "wizard [DAC], upgrade"
  - "How to [DAC], upgrade"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Deploy a data-tier application

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Deploying, or publishing, a registered data-tier application (DAC) from a DAC package to an existing instance of the database engine or Azure SQL Database is available in a wizard from SQL Server Management Studio or Visual Studio SQL Server Data Tools. The publish action incrementally updates a database schema to match the schema of a source `.dacpac` file. If the database doesn't exist on the server, the publish operation creates it.

The deployment process registers a DAC instance by storing the DAC definition in the `msdb` system database (`master` in [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)]); creates a database, then populates that database with all the database objects defined in the DAC.

You can deploy the same DAC package to a single instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] multiple times, but you must run the deployments one at a time. The DAC instance name specified for each deployment must be unique within the instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)].

## Database Options and Settings

By default, the database created during the deployment has all of the default settings from the CREATE DATABASE statement, except:

- The database collation and compatibility level are set to the values defined in the DAC package. A DAC package built from a database project in the SQL Server Developer Tools uses the values set in the database project. A package extracted from an existing database uses the values from the original database.

- You can adjust some of the database settings, such as database name and file paths, in the **Update Configuration** page. You can't set the file paths when deploying to [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)].

Some database options, such as TRUSTWORTHY, DB_CHAINING, and HONOR_BROKER_PRIORITY, can't be adjusted as part of the deployment process. Physical properties, such as the number of filegroups, or the numbers and sizes of files can't be altered as part of the deployment process. After the deployment completes, you can use the ALTER DATABASE statement, [!INCLUDE [ssManStudioFull](../../../../includes/ssmanstudiofull-md.md)], or [!INCLUDE [ssNoVersion](../../../../includes/ssnoversion-md.md)] PowerShell to tailor the database.

## Security and permissions

Authentication logins are stored in a DAC package without a password. When the package is deployed or upgraded, the login is created as a disabled login with a generated password. To enable the logins, log in with the ALTER ANY LOGIN permission and use ALTER LOGIN to enable the login and assign a new password that can be communicated to the user. This isn't required for Windows Authentication logins because their passwords aren't managed by SQL Server.

A DAC can only be deployed by members of the **sysadmin** or **serveradmin** fixed server roles, or by logins in the **dbcreator** fixed server role with ALTER ANY LOGIN permissions. The built-in [!INCLUDE [ssNoVersion](../../../../includes/ssnoversion-md.md)] system administrator account named **sa** can also deploy a DAC.

Deploying a DAC with logins to [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)] requires membership in the loginmanager or serveradmin roles. Deploying a DAC without logins to [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)] requires membership in the dbmanager or serveradmin roles.

## Deploy a DAC

## [SQL Server Management Studio](#tab/sql-server-management-studio)

1. In **Object Explorer**, expand the node for the instance to which you want to deploy the DAC.

1. Right-click the **Databases** node, then select **Deploy Data-tier Application...**

1. Complete the wizard dialogs and select finish.

More about some of the wizard pages below:

### Select DAC Package Page

Specify the DAC package that contains the data-tier application to be deployed. The page transitions through three states.

### Select the DAC Package

Choose the DAC package to deploy. The DAC package must be a valid DAC package file and must have a `.dacpac` extension.

**DAC Package** - Specify the path and file name of the DAC package that contains the data-tier application to be deployed. You can select the **Browse** button at the right of the box to browse to the location of the DAC package.

**Application Name** - A read-only box that displays the DAC name assigned when the DAC was authored or extracted from a database.

**Version** - A read-only box that displays the version assigned when the DAC was authored or extracted from a database.

**Description** - A read-only box that displays the description written when the DAC was authored or extracted from a database.

### Validate the DAC Package

Displays a progress bar as the wizard confirms that the selected file is a valid DAC package. If the DAC package is validated, the wizard proceeds to the final version of the **Select Package** page where you can review the results of the validation. If the file isn't a valid DAC package, the wizard remains on the **Select DAC Package**. Either select another valid DAC package or cancel the wizard and generate a new DAC package.

### Review Policy Page

Review the results of evaluating the DAC server selection policy (if used). The DAC server selection policy is optional, and is assigned to the DAC when it's created in Visual Studio. The policy uses the server selection policy facets to specify conditions an instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] should meet to host the DAC.

**Evaluation results of policy conditions** - Shows whether the DAC deployment policy conditions succeeded. The results of evaluating each condition are reported on a separate line.

The following server selection policies always evaluate to false when deploying a DAC to [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)]: operating system version, language, named pipes enabled, platform, and tcp enabled.

**Ignore policy violations** - Use this check box to proceed with the deployment if one or more of the policy conditions failed. Only select this option if you're sure that all of the conditions, which failed won't prevent the successful operation of the DAC.

### Update Configuration Page

Specify the names of the deployed DAC instance and the database created by the deployment, and to set database options.

**Database Name:** - Specify the name of the database to be created by the deployment. The default is the name of the source database the DAC was extracted from. The name must be unique within the instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] and comply with the rules for [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] identifiers.

If you change the database name, the names of the data file and log files change to match the new value.

The database name is also used as the name of the DAC instance. The instance name is displayed on the node for the DAC under the **Data-tier Applications** node in **Object Explorer**.

The following options don't apply to [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)], and aren't displayed when deploying to [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)].

**Use the default database location** - Select this option to create the database data and log files in the default location for the instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)]. The file names are built using the database name.

**Specify database files** - Select this option to specify a different location or name for the data and log files.

**Data file path and name: -** Specify the full path and file name for the data file. The box is populated with the default path and file name. Edit the string in the box to change the default, or use the Browse button to navigate to the folder where the data file is to be placed.

**Log file path and name:** - Specify the full path and file name for the log file. The box is populated with the default path and file name. Edit the string in the box to change the default, or use the **Browse** button to navigate to the folder where the log file is to be placed.

<a id="Summary"></a>

### Summary Page

Use this page to review the actions the wizard takes when deploying the DAC.

**The following settings will be used to deploy your DAC.** - Review the information displayed to ensure the actions taken will be correct. The window displays the DAC package you selected, and the name you selected for the deployed DAC instance. The window also displays the settings that will be used when creating the database associated with the DAC.

### Deploy Page

This page reports the success or failure of the deploy operation.

**Deploying the DAC** - Reports the success or failure of each action taken to deploy the DAC. Review the information to determine the success or failure of each action. Any action that encountered an error has a link in the **Result** column. Select the link to view a report of the error for that action.

**Save Report** - Select this button to save the deployment report to an HTML file. The file reports the status of each action, including all errors generated by any of the actions. The default folder is the SQL Server Management Studio\DAC Packages folder in the Documents folder of your Windows account.

## [SQL Server Data Tools](#tab/sql-server-data-tools)

This action is available by selecting the Databases node in object explorer.

Select the `.dacpac` file. After you specify a `.dacpac` file, the **DAC Properties** button is enabled. The **DAC Properties** dialog box displays information about the `.dacpac` file.

Specify the connection string to the database server, and then specify the database name, if the database name isn't in the connection string.

The **Publish** and **Generate Script** buttons are now enabled. If you generate a script, it opens in a document window and can be saved as required. If you choose to publish directly to the database, a summary of the update and the actual script used can be viewed from the **Data Tools Operations** window.

When checked, the **Register as a Data-tier Application** check box causes the resulting database to be registered as a data-tier application in the server metadata. If the database to which you're publishing is registered, you can cause publishing to fail if the schema of the database is different from its current registered `.dacpac`.

Additional publishing configuration is available in the **Advanced Publish Settings** dialog box, which you can access by selecting the **Advanced** button.

## [SqlPackage CLI](#tab/sqlpackage-cli)

The SqlPackage command-line utility can be used to deploy a DAC package. The command is:

```bash
SqlPackage /Action:Publish {parameters} {properties} {sqlcmd variables}
```

For more information about the command-line options, see [SqlPackage](../../../sqlpackage/sqlpackage-publish.md).

---

## Related content

- [Data-tier applications (DAC) overview](overview.md)
- [Extract a DACPAC from a database](extract-dacpac-from-database.md)
- [Database identifiers](../../../../relational-databases/databases/database-identifiers.md)
