---
title: "Delete a Data-Tier Application"
description: "Delete a data-tier application definition from a database."
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
  - "sql13.swb.deletedacwizard.deletedac.f1"
  - "sql13.swb.deletedacwizard.summary.f1"
  - "sql13.swb.deletedacwizard.introduction.f1"
  - "sql13.swb.deletedacwizard.choosemethod.f1"
helpviewer_keywords:
  - "How to [DAC], delete"
  - "data-tier application [SQL Server], delete"
  - "wizard [DAC], delete"
  - "delete DAC"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Delete a data-tier application

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

You can delete a registered data-tier application by using the Delete Data-tier Application wizard in SQL Server Management Studio. You can specify whether the associated database is retained, detached, or dropped.

## Before you begin

When you delete a registered data-tier application (DAC) instance, you choose one of three options specifying what is to be done with the database associated with the data-tier application. All three options delete the DAC definition metadata from the database. The options differ in what they do with the database associated with the data-tier application. The wizard doesn't delete any of the instance-level objects associated with the DAC or database, such as logins.

| Option | Database actions |
| --- | --- |
| Delete registration | The associated database remains intact. |
| Detach database | The associated database is detached. The instance of the Database Engine can't reference the database, but the data and log files are intact. |
| Delete database | The associated database is dropped. The data and log files are deleted. |

## Limitations and Restrictions

There's no automatic mechanism to restore the DAC definition metadata or the database after you delete a DAC. How you can manually rebuild the DAC instance depends on the delete option.

| Option | How to Rebuild the DAC Instance |
| --- | --- |
| Delete registration | Register a DAC from the database left in place. |
| Detach database | Reattach the database by using `sp_attachdb` or [!INCLUDE [ssManStudioFull](../../../../includes/ssmanstudiofull-md.md)], and then register a new DAC instance from the database. |
| Delete database | Restore the database from a full backup made before the DAC was deleted, and then register a new DAC instance from the database. |

> [!WARNING]  
> Rebuilding a DAC instance by registering a DAC from a restored or reattached database doesn't recreate some parts of the original DAC, such as the server selection policy.

## Permissions

A DAC can only be deleted by members of the **sysadmin** or **serveradmin** fixed server roles, or by the database owner. The built-in [!INCLUDE [ssNoVersion](../../../../includes/ssnoversion-md.md)] system administrator account named **sa** can also launch the wizard.

<a id="using-the-delete-data-tier-application-wizard"></a>

## Use the Delete Data-tier Application Wizard

**To Delete a DAC Using a Wizard**

1. In **Object Explorer**, expand the node for the instance containing the DAC to be deleted.
1. Expand the **Management** node.
1. Expand the **Data-tier Applications** node.
1. Right-click the DAC to be deleted, and then select **Delete Data-tier Application...**
1. Complete the wizard dialogs:
   1. [Introduction](#introduction-page)
   1. [Choose Method](#choose-method-page)
   1. [Summary](#summary-page)
   1. [Delete Data-tier Application](#delete-data-tier-application-page)

## Introduction Page

This page describes the steps for deleting a data-tier application.

**Do not show this page again.** - Select the check box to stop the page from being displayed in the future.

**Next >** - Proceeds to the **Choose Method** page.

**Cancel** - Ends the wizard without deleting a data-tier application or database.

## Choose Method Page

Use this page to specify the option for handling the database associated with the DAC to be deleted.

**Delete registration** - Removes the metadata defining the data-tier application, but leaves the associated database intact.

**Detach database** - Removes the metadata defining the data-tier application and detaches the associated database.

The database isn't available to be referenced by that instance of the [!INCLUDE [ssDE](../../../../includes/ssde-md.md)], but the data and log files remain intact.

**Delete database** - Removes the metadata defining the DAC and drops the associated database.

The data and log files for the database are permanently deleted.

**< Previous** - Returns to the **Introduction** page.

**Next >** - Proceeds to the **Summary** page.

**Cancel** - Ends the wizard without deleting the DAC or database.

## Summary Page

Use this page to review the actions the wizard takes when deleting the DAC instance.

**Review your selection summary** - Review the DAC, database, and deletion method displayed in the box. If the information is correct, select either **Next** or **Finish** to delete the DAC. If the DAC and database information isn't correct, select **Cancel** and select the correct DAC. If the deletion method isn't correct, select **Previous** to return to the **Choose Method** page and select a different method.

**< Previous** - Returns to the **Choose Method** page to choose a different delete method.

**Next >** - Deletes the DAC instance using the method you chose on the previous page, and proceeds to the **Delete Data-tier Application** page.

**Cancel** - Ends the wizard without deleting the DAC instance.

## Delete Data-tier Application Page

This page reports the success or failure of the delete operation.

**Deleting the DAC** - Reports the success or failure of each action taken to delete the DAC instance. Review the information to determine the success or failure of each action. Any action that encountered an error has a link in the **Result** column. Select the link to view a report of the error for that action.

**Save Report** - Select this button to save the deletion report to an HTML file. The file reports the status of each action, including all errors generated by any of the actions. The default folder is a `SQL Server Management Studio\DAC Packages` folder in the Documents folder of your Windows account.

**Finish** - Ends the wizard.

## Related content

- [Data-tier applications (DAC) overview](overview.md)
- [Deploy a data-tier application](deploy-data-tier-application.md)
- [Register a database as a DAC](register-database-as-dac.md)
- [Back up and restore of SQL Server databases](../../../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)
- [Database detach and attach (SQL Server)](../../../../relational-databases/databases/database-detach-and-attach-sql-server.md)
