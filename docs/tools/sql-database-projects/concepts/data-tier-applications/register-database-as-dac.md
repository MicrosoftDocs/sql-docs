---
title: "Register a Database as a DAC"
description: "Data-tier application (DAC) registration process and tools."
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
  - "sql13.swb.registerdacwizard.summary.f1"
  - "sql13.swb.registerdacwizard.introduction.f1"
  - "sql13.swb.registerdacwizard.setproperties.f1"
  - "sql13.swb.registerdacwizard.registerdac.f1"
helpviewer_keywords:
  - "wizard [DAC], register"
  - "How to [DAC], register"
  - "register DAC"
  - "data-tier application [SQL Server], register"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Register a database as a DAC

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The registration process creates a data-tier application (DAC) definition that defines the objects in the database. Register a database as a data-tier application (DAC) to build a data-tier application (DAC) definition that describes the objects in an existing database and write that DAC definition in the `msdb` system database (`master` in [!INCLUDE [ssazure-sqldb](../../../../includes/ssazure-sqldb.md)]).

To register a database as a DAC, use:

- The Register Data-tier Application Wizard in SQL Server Management Studio
- SQL Server Data Tools
- The SqlPackage command-line utility

## Permissions

Registering a DAC in an instance of [!INCLUDE [ssDE](../../../../includes/ssde-md.md)] requires at least ALTER ANY sign in and database scope VIEW DEFINITION permissions, SELECT permissions on `sys.sql_expression_dependencies`, and membership in the **dbcreator** fixed server role. Members of the **sysadmin** fixed server role or the built-in SQL Server system administrator account named **sa** can also register a DAC. Registering a DAC that doesn't contain logins in [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)] requires membership in the **dbmanager** or **serveradmin** roles. Registering a DAC that contains logins in [!INCLUDE [ssSDS](../../../../includes/sssds-md.md)] requires membership in the **loginmanager** or **serveradmin** roles.

## [SQL Server Management Studio](#tab/sql-server-management-studio)

**To Register a DAC Using a Wizard**

1. In **Object Explorer**, expand the node for the instance containing the database to be registered as a DAC.
1. Expand the **Databases** node.
1. Right-click the database to be registered, point to **Tasks**, and then select **Register As Data-tier Application...**
1. Complete the wizard dialogs:
    1. [Introduction Page](#introduction-page)
    1. [Set Properties Page](#set-properties-page
    1. [Validation and Summary Page](#validation-and-summary-page
    1. [Register DAC Page](#register-dac-page)

## Introduction Page

This page describes the steps for registering a data-tier application.

**Do not show this page again.** - select the check box to stop the page from being displayed in the future.

**Next >** - Proceeds to the **Set Properties** page.

**Cancel** - Terminates the wizard without registering a DAC.

## Set Properties Page

Use this page to specify DAC-level properties such as the application name and version.

**Application name.** - A string that specifies the name used to identify the DAC definition, the field has been populated with the database name.

**Version.** - A numeric value that identifies the version of the DAC. The DAC version is used in Visual Studio to identify the version of the DAC that developers are working on. When deploying a DAC, the version is stored in the `msdb` database and can later be viewed under the **Data-tier Applications** node in [!INCLUDE [ssManStudioFull](../../../../includes/ssmanstudiofull-md.md)].

**Description.** - Optional. Text that explains the purpose of the DAC. When deploying a DAC, the description is stored in the `msdb` database and can later be viewed under the **Data-tier Applications** node in [!INCLUDE [ssManStudio](../../../../includes/ssmanstudio-md.md)].

**< Previous** - Returns you to the **Introduction** page.

**Next >** - Verifies that a DAC can be built from the objects in the database, and displays the results in the **Validation and Summary** page.

**Cancel** - Terminates the wizard without registering the DAC.

## Validation and Summary Page

Use this page to review the actions the wizard takes when registering the DAC. The page transitions through three states as it verifies that a DAC can be built from the objects in the database.

### Retrieve Objects

**Retrieving database and server objects.** - Displays a progress bar as the wizard retrieves all of the required objects from the database and the instance of the Database Engine.

**< Previous** - Returns you to the **Set Properties** page to change your entries.

**Next >** - Registers the DAC and displays the results in the **Register DAC** page.

**Cancel** - Terminates the wizard without registering the DAC.

### Validate Objects

**Checking** *SchemaName* **.** *ObjectName* **.** - Displays a progress bar as the wizard verifies the dependencies of the retrieved objects, and verifies that they're all valid objects for a DAC. *SchemaName***.***ObjectName* identify which object is currently being verified.

**< Previous** - Returns you to the **Set Properties** page to change your entries.

**Next >** - Registers the DAC and displays the results in the **Register DAC** page.

**Cancel** - Terminates the wizard without registering the DAC.

### Summary

**The following setting will be used to register your DAC.** - Displays a report of the properties and objects that are included in the DAC.

**Save Report** - Select this button to save a copy of the validation report to an HTML file. The default folder is a **SQL Server Management Studio\DAC Packages** folder in the Documents folder of your Windows account.

**< Previous** - Returns you to the **Set Properties** page to change your entries.

**Next >** - Registers the DAC and displays the results in the **Register DAC** page.

**Cancel** - Terminates the wizard without registering the DAC.

## Register DAC Page

This page reports the success or failure of the registration.

**Registering the DAC** - Reports the success or failure of each action taken to register the DAC. Review the information to determine the success or failure of each action. Any action that encountered an error has a link in the **Result** column. Select the link to view a report of the error for that action.

**Save Report** - Select this button to save the registration report to an HTML file. The file reports the status of each action, including all errors generated by any of the actions. The default folder is a **SQL Server Management Studio\DAC Packages** folder in the Documents folder of your Windows account. The file name is in the format \<DACPackageName>_RegisterDACReport_yyyymmdd.html, where \<*DACPackageName*> is the name of the package being deployed, *yyyy* = the current year, *mm* = the current month, and *dd* = the current day.

**Finish** - Terminates the wizard.

## [SQL Server Data Tools](#tab/sql-server-data-tools)

By right-clicking a connected database in SQL Server Object Explorer, you can register a database as a data-tier application (DAC) within the instance. This stores a representation of the current state of the database schema in system metadata.

In the **Register Data-tier Application** dialog box, specify the properties of the registered DAC.

## [SqlPackage](#tab/sqlpackage)

To register a database as a DAC during SqlPackage publish, use the SqlPackage command-line utility property `/p:RegisterDataTierApplication=true` as part of the [SqlPackage publish command](../../../sqlpackage/sqlpackage-publish.md). The command is as follows:

```bash
SqlPackage /Action:Publish {parameters} /p:RegisterDataTierApplication=true {additionalproperties} {sqlcmd variables}
```

---

## Related content

- [Data-tier applications (DAC) overview](overview.md)
