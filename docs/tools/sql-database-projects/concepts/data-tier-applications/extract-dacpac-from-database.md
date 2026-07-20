---
title: Extract a DACPAC from a Database
description: Extract a DACPAC from a database.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier, wiassaf, maghan
ms.date: 03/13/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: how-to
ms.collection:
  - data-tools
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql13.swb.extractdacwizard.validationandsummary.f1"
  - "sql13.swb.extractdacwizard.introduction.f1"
  - "sql13.swb.extractdacwizard.selectdatapage.f1"
  - "sql13.swb.extractdacwizard.setdacproperties.f1"
  - "sql13.swb.extractdacwizard.buildandsave.f1"
helpviewer_keywords:
  - "extract DAC"
  - "How to [DAC], extract"
  - "data-tier application [SQL Server], extract"
  - "wizard [DAC], extract"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Extract a DACPAC from a database

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The extraction process creates a DAC package file (`.dacpac`) that contains definitions of the database objects and their related instance-level elements. For example, a `.dacpac` file contains the database tables, stored procedures, views, and users, along with the logins that map to the database users. The `.dacpac` file can be used to deploy the database to another instance of SQL Server or Azure SQL Database or to register the database as a data-tier application (DAC) in the current instance.

Options for extracting a `.dacpac` include:

1. the **Extract Data-tier Application** wizard in SQL Server Management Studio (SSMS)
1. SQL Server Data Tools
1. SqlPackage command-line utility
1. MSSQL extension for Visual Studio Code

## Permissions

Extracting a `.dacpac` requires at least `ALTER ANY LOGIN` and database scope `VIEW DEFINITION` permissions, and `SELECT` permissions on `sys.sql_expression_dependencies`. Members of the **securityadmin** fixed server role who are also members of the database_owner fixed database role are eligible to extract a `.dacpac`. Members of the **sysadmin** fixed server role or the built-in SQL Server system administrator account named `sa` can also extract a `.dacpac`.

## [SQL Server Management Studio](#tab/sql-server-management-studio)

<a id="using-the-extract-data-tier-application-wizard"></a>

## Use the Extract Data-tier Application Wizard

**To Extract a DAC Using a Wizard**

1. In **Object Explorer**, expand the node for the instance containing the database from which the `.dacpac` is to be extracted.
1. Expand the **Databases** node.
1. Right-click the node for the database from which the `.dacpac` is to be extracted, point to **Tasks**, and then select **Extract Data-tier Application...**
1. Complete the wizard dialogs:
   1. [Introduction Page](#wizard-introduction-page)
   1. [Select Data Page](#select-data-page)
   1. [Set Properties Page](#set-properties-page)
   1. [Validation and Summary Page](#validation-and-summary-page)
   1. [Build Package Page](#build-package-page)

### Wizard introduction page

This page describes the steps for extracting a data-tier application.

**Do not show this page again.** - Select the check box to stop the page from being displayed in the future.

**Next >** - Proceeds to the **Choose Method** page.

**Cancel** - Ends the wizard without extracting a data-tier application from the database.

### Select data page

Select the reference data that you want to include in your data-tier application (DAC) package file. Including data in your DAC package is optional. The `.dacpac` includes the schema of all supported database objects and instance objects related to your database.

You can include up to 10 MB of reference data in your DAC package file. However, for tables to be included in the DAC, they might not contain binary large object (BLOB) data types such as **image** or **varchar(max)**. To extract larger amounts of data for transferring to another database, use SQL Server Integration Services, the bulk copy utility, or one of many other data migration techniques.

**Database table** - Select the check box next to the database tables which contain the data that you want to include in your DAC package. You can select up to 10 tables that have 10,000 rows or less.

### Set properties page

Use this page of the wizard to describe the data-tier application (DAC). These properties are used to identify the DAC and help distinguish it from others.

**Name** - This name identifies the DAC. It can be different than the name of the DAC package file and should describe your application. For example, if the database is used for a finance application, you might wish to name the DAC Finance.

**Version (use xx.xx.xx.xx, where x is a number)** - A numeric value that identifies the version of the DAC. The DAC version is used in Visual Studio to identify the version of the DAC that developers are working on. When a `.dacpac` is deployed, the version is stored in the `msdb` database and can later be viewed under the **Data-tier Applications** node in [!INCLUDE [ssManStudioFull](../../../../includes/ssmanstudiofull-md.md)].

**Description:** - Optional. Describes the DAC. When a `.dacpac` is deployed, the description is stored in the `msdb` database and can later be viewed under the **Data-tier Applications** node in [!INCLUDE [ssManStudio](../../../../includes/ssmanstudio-md.md)].

**Save to DAC package file (include .dacpac extension with file name):** - Saves the DAC to a DAC package file, with a .dacpac extension. Select the **Browse** button to specify a name and location for the file.

**Overwrite existing file** - Select this check box to replace the DAC package file if one already exists with the same name.

### Validation and summary page

On this page, the wizard validates that all database objects are supported in a data-tier application (DAC). It also checks dependencies across database objects to determine the set of objects that can be successfully included in the DAC. After that, it displays the validation report and summarizes the options that you selected in this wizard. To change an option, select **Previous**. To begin extracting a DAC, select **Next**.

> [!NOTE]  
> If one or more objects aren't supported by a DAC, then the **Next** button is disabled and the extraction process might not continue. In such cases, it's recommended to remove the unsupported objects and then run this wizard again.

**Summary** - A summary of the options you selected are listed under **DAC properties**. The results of the validation are listed under **DAC objects**. There are three types of results from the validation:

- **Objects included in DAC successfully**: these objects and their dependencies are supported, and can be included in the DAC successfully.

- **Objects included in DAC with warnings**: these objects are supported, but depend on other objects that aren't supported in a DAC.

- **Objects not included in DAC**: these objects aren't supported and must be removed from the database before successfully extracting a DAC.

The validation process checks multiple levels of dependencies. For example, if a stored procedure depends on a table that uses the unsupported CLR data type, the stored procedure will be listed under **Objects included in DAC with warnings**.

If one or more objects aren't supported by a DAC, then the **Next** button is disabled and the extraction process won't continue. In such cases, it's recommended to remove the objects that aren't supported and then run this wizard again.

**Save Report** - Enables you to save an HTML-based file that lists all of the objects under the **DAC Objects** node in the summary. This report can be useful when some of your database objects aren't supported in a DAC. Use the report to change or remove objects that aren't supported, before trying to extract the DAC again.

### Build package page

Use this page to monitor the progress of the wizard as it extracts the data-tier application (DAC).

**Action** - During the **Create and save DAC package file** action, the wizard extracts a DAC from your SQL Server database. Then, a DAC package is created in memory and saved to the location you specified. Select the links in the **Result** column to see the outcome of the corresponding step.

**Save Report** - Select to save the results of the wizard's progress to a file.

**Finish** - Select to close the wizard after processing has completed, or if an error occurs.

## [MSSQL extension for VS Code](#tab/mssql-extension)

The MSSQL extension for Visual Studio Code provides a visual interface to extract a `.dacpac` from a connected database.

1. In the **Object Explorer**, right-click the **Databases** node or a specific database, and select **Data-tier Application**.
1. Select **Extract DACPAC** from the available options.
1. Specify the target file path and name for the `.dacpac` file.
1. Review the extraction settings and select **Extract** to create the `.dacpac` file.

The extraction process captures the database schema, including tables, stored procedures, views, and other objects, into a portable `.dacpac` file.

For more information about the full Data-tier Application experience in the MSSQL extension, including deploy, import, and export operations, see [Data-tier Application (DACPAC and BACPAC) import and export](../../../visual-studio-code-extensions/mssql/mssql-data-tier-application.md).

## [SQL Server Data Tools](#tab/sql-server-data-tools)

You can extract a .dacpac from a database connected in SQL Server object explorer. Extract creates a database snapshot file (.dacpac) from a live SQL Server or Azure SQL Database that might contain data from user tables, in addition to the database schema.

Specify the .dacpac file to create. The **DAC Properties** button displays the **DAC Properties** dialog box, which lets you specify properties of the .dacpac file.

Modify, as needed, the configuration of the extract process. In the **Extract Settings** section, you can choose to extract schema only or to extract schema and include table data. If you choose to extract schema and data, you can select the tables for which you would like to extract data.

## [SqlPackage](#tab/sqlpackage)

You can extract a .dacpac from a database using the SqlPackage command-line utility. The command is as follows:

```bash
SqlPackage /Action:Extract {parameters} {properties}
```

For more information about the parameters and properties, see [SqlPackage extract documentation](../../../sqlpackage/sqlpackage-extract.md).

---

## Related content

- [Data-tier applications (DAC) overview](overview.md)
- [Data-tier Application (DACPAC and BACPAC) import and export](../../../visual-studio-code-extensions/mssql/mssql-data-tier-application.md)
- [SqlPackage in development pipelines](../../../sqlpackage/sqlpackage-pipelines.md)
