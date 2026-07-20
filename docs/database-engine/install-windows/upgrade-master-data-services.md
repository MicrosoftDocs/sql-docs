---
title: "Upgrade Master Data Services"
description: Discover the four scenarios for upgrading Microsoft SQL Server Master Data Services. Learn about file locations and troubleshooting for upgrades.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: master-data-services
ms.topic: upgrade-and-migration-article
monikerRange: ">=sql-server-2017"
---
# Upgrade Master Data Services

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The following are the scenarios for upgrading Microsoft [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] Master Data Services.

- [Upgrade without Database Engine Upgrade](../../database-engine/install-windows/upgrade-master-data-services.md#noengine)
- [Upgrade with Database Engine Upgrade](../../database-engine/install-windows/upgrade-master-data-services.md#engine)
- [Upgrade in Two-Computer Scenario](../../database-engine/install-windows/upgrade-master-data-services.md#twocomputer)
- [Upgrade with Restoring a Database from Backup](../../database-engine/install-windows/upgrade-master-data-services.md#restore)

[!INCLUDE [support-notice](../../master-data-services/includes/support-notice.md)]

## Before you upgrade

Back up your database before performing any upgrade.

The upgrade process recreates stored procedures and upgrades tables used by [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)]. Any customizations you make to either of these components might be lost.

Model deployment packages can be used only in the edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] they were created in. You can't deploy model deployment packages created in [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)].

After you upgrade Data Quality Services (DQS) and Master Data Services (MDS) to the latest version of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], any earlier version of the MDS add-in for Excel no longer works. You can download the [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] MDS add-in for Excel from [Master Data Services Add-in for Microsoft Excel](../../master-data-services/microsoft-excel-add-in/master-data-services-add-in-for-microsoft-excel.md).

<a id="fileLocation"></a>

## File location

By default, the files are installed at `<drive>:\Program Files\Microsoft SQL Server\<nnn>\Master Data Services`, where `<nnn>` represents the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] version. For example, [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] is `140`, and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] is `150`.

<a id="noengine"></a>

## Upgrade without Database Engine upgrade

In this scenario you continue to use [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] to host your MDS database. However, you must upgrade the schema of the MDS database, and then create a current [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)] web application to access the MDS database. After the upgrade, the MDS database can't be accessed by the earlier web application.

You can install the current [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] and an earlier version of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] on the same computer. The files are installed in different locations, as shown in [File Location](#fileLocation).

1. Install [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] and any other features you want.

   1. Open the [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)] Setup wizard.

   1. In the left pane, select **Installation**.

   1. In the right pane, select **New SQL Server stand-alone installation or add features to an existing installation**.

   1. On the **Feature Selection** page, select **[!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)]** and any other features you want to install.

   1. Complete the wizard.

1. Upgrade the MDS database schema.

   1. Open the current [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].

      To upgrade the MDS database schema, you must be logged in as the Administrator Account that was specified when the MDS database was created. In the MDS database, in `mdm.tblUser`, this user has the `ID` value of `1`.

   1. In the left pane, select **Database Configuration**.

   1. In the right pane, select **Select Database** and specify the information for your [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] database instance.

   1. Select **Upgrade Database** to start the **Upgrade Database Wizard**. For more information, see [Upgrade Database Wizard (Master Data Services Configuration Manager)](../../master-data-services/upgrade-database-wizard-master-data-services-configuration-manager.md).

1. Create a web application.

   1. Open the current [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].

   1. In the left pane, select **Web Configuration**.

   1. In the right pane, from the **Website** list, select one of the following options:

      - **Default Web Site**, then select **Create Application**.

      - **Create new site**. A new web application is automatically created when the website is created.

      Your existing MDS web application from an earlier version of SQL Server ([!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]) is available for selection in the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] version of Master Data Services Configuration Manager. You must not select the existing web application, and instead must create a [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] web application for MDS. Otherwise, you receive an error when you try to associate the web application with the upgraded MDS database, stating that the requested page can't be accessed because the related configuration data for the page is invalid.

      If you want to use the same name (alias) for MDS web application as your existing ([!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]) web application, you must first delete the web application and the associated application pool from IIS, and then create a web application with the same name using [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] version of Master Data Services Configuration Manager. For information about removing web application and application pools from IIS, see [Remove an Application (IIS)](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc771205(v=ws.10)) and [Remove an Application Pool (IIS)](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc772406(v=ws.10)).

1. Associate the new web application with the upgraded MDS database.

   1. In the **Associate Application with Database** section, choose **Select**.

   1. Select the MDS database.

   1. Select **Apply**.

<a id="engine"></a>

## Upgrade with Database Engine upgrade

In this scenario, you upgrade both the database engine and [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] application from an earlier version to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] or a later version.

1. **For [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] only**: Open **Control Panel** > **Programs and Features** and uninstall Microsoft [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)].

1. Upgrade the database engine to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] or a later version. For more information, see [Choose a Database Engine upgrade method](choose-a-database-engine-upgrade-method.md).

1. Complete all the steps in [Upgrade without Database Engine Upgrade](#noengine).

<a id="twocomputer"></a>

## Upgrade in two-computer scenario

In this scenario, you upgrade a system in which SQL Server is installed on two computers: one with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] or [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)], and the other with an earlier version of [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)].

If an earlier version of [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)] is installed, you continue to use the earlier version to host your MDS database on one computer. However, you must upgrade the schema of the MDS database, and then use the [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] or [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] web application respectively to access the MDS database. The MDS database can't be accessed by the earlier version web application.

**To upgrade in two-computer scenario**

- Complete all the steps in [Upgrade without Database Engine Upgrade](#noengine).

<a id="restore"></a>

## Upgrade by restoring a database from backup

In this scenario, either [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] or [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] is installed along with an earlier version on the same computer or two different computers. A database was backed up on a version earlier than the [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] or [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] release, before upgrade, and the database has to be restored.

1. Install [!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)] and any other features you want.

   1. Open the [!INCLUDE [sssnoversion](../../includes/ssnoversion-md.md)] Setup wizard.

   1. In the left pane, select **Installation**.

   1. In the right pane, select **New SQL Server stand-alone installation or add features to an existing installation**.

   1. On the **Feature Selection** page, select **[!INCLUDE [ssMDSshort](../../includes/ssmdsshort-md.md)]** and any other features you want to install.

   1. Complete the wizard.

1. Restore the database that was backed up.

1. Upgrade the MDS database schema, create a web application, and associate the new web application with the upgraded MDS database. For the instructions, see steps 2 - 4 in [Upgrade without Database Engine Upgrade](#noengine)

## Troubleshooting

**Issue:** When you open the [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] web application, a "client version isn't compatible with the database version" error message is displayed.

**Solution:** This issue occurs when a [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Master Data Manager web application tries to access a database that has been upgraded to or [!INCLUDE [ssSQL16](../../includes/sssql17-md.md)] MDS. You must use a [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] or [!INCLUDE [ssSQL16](../../includes/sssql17-md.md)] web application instead.

This issue might also occur if you didn't stop and restart the **MDS Application Pool** in IIS when upgrading the MDS database schema. Restart the **MDS Application Pool** to correct the issue.

## Related content

- [Installation Tasks for Master Data Services](../../master-data-services/install-windows/install-master-data-services.md)
