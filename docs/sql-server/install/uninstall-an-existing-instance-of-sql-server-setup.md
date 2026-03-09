---
title: Uninstall an Existing Instance of SQL Server (Setup)
description: This article describes how to uninstall a stand-alone instance of SQL Server, which also prepares the system so that you can reinstall SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/26/2026
ms.service: sql
ms.subservice: install
ms.topic: how-to
helpviewer_keywords:
  - "removing instances of SQL Server"
  - "uninstalling instances of SQL Server"
  - "removing SQL Server"
  - "instances of SQL Server, uninstalling"
  - "uninstalling SQL Server"
---

# Uninstall an existing instance of SQL Server (Setup)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article describes how to uninstall a stand-alone instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. By following the steps in this article, you also prepare the system so that you can reinstall [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

To uninstall a Cumulative Update, see [Uninstall a Cumulative Update from SQL Server](uninstall-a-cumulative-update-from-sql-server.md).

> [!NOTE]  
> To uninstall a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster, use the Remove Node functionality provided by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup to remove each node individually. For more information, see [Add or remove nodes in a failover cluster instance (Setup)](../failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).

## Considerations

- To uninstall SQL Server, you must be a local administrator with permissions to log on as a service.

- If your computer has the *minimum* required amount of physical memory, increase the size of the page file to two times the amount of physical memory. Insufficient virtual memory can result in an incomplete removal of SQL Server.

- On a system with multiple instances of SQL Server, the SQL Server browser service is uninstalled only once the last instance of SQL Server is removed. You can remove the SQL Server Browser service manually, using the same procedure as removing SQL Server.

- Uninstalling [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] deletes `tempdb` data files that the install process adds. The uninstall process deletes files with the `tempdb_mssql_*.ndf` name pattern if they exist in the system database directory.

## Prepare

1. **Back up your data.** Either create [full backups](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md) of all databases, including system databases, or manually copy the `.mdf` and `.ldf` files to a separate location. The `master` database contains all system level information for the server, such as logins, and schemas. The `msdb` database contains job information such as SQL Server agent jobs, backup history, and maintenance plans. For more information about system databases, see [Back up and restore: System databases (SQL Server)](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).

   The files that you must save include the following database files <sup>1</sup>:

   - `master.mdf`
   - `mastlog.ldf`
   - `msdbdata.mdf`
   - `msdblog.ldf`
   - `model.mdf`
   - `modellog.ldf`
   - `mssqlsystemresource.mdf`
   - `mssqlsystemresource.ldf`
   - `ReportServer[$InstanceName]` data and log files <sup>2</sup>
   - `ReportServer[$InstanceName]TempDB` data and log files <sup>2</sup>

   <sup>1</sup> You can't back up the `tempdb` system database.

   <sup>2</sup> The *ReportServer* databases are included with SQL Server Reporting Services.

1. **Stop all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services.** Stop all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services before you uninstall [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components. Active connections can prevent successful uninstallation.

1. **Use an account that has the appropriate permissions.** Sign in to the server by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account or by using an account that has equivalent permissions. For example, you can connect to the server by using an account that is a member of the local Administrators group.

## Uninstall

To uninstall SQL Server from Windows 10, Windows Server 2016, Windows Server 2019, and later versions, follow these steps.

1. Go to **Settings** from the Start menu. Depending on which version of Windows you installed, choose **Apps**, or **Apps > Installed apps**.

1. Search for `sql` in the search box.

1. Select **Microsoft SQL Server (Version) (Bit)**. For example, `Microsoft SQL Server 2017 (64-bit)`.

1. Select **Uninstall**.

   :::image type="content" source="media/uninstall-an-existing-instance-of-sql-server-setup/uninstall-sql-server-windows-10.png" alt-text="Screenshot showing how to uninstall SQL Server.":::

   If you use a newer version of Windows, the user interface might look different.

1. Select **Remove** on the SQL Server dialog pop-up to launch the Microsoft SQL Server installation wizard.

   :::image type="content" source="media/uninstall-an-existing-instance-of-sql-server-setup/remove-sql-2017.png" alt-text="Screenshot showing how to remove SQL Server.":::

1. On the **Select Instance** page, use the dropdown list to specify an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to remove, or specify the option to remove only the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] shared features and management tools. Select **Next** to continue.

1. On the **Select Features** page, specify the features to remove from the specified instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. On the **Ready to Remove** page, review the list of components and features to uninstall. Select **Remove** to begin uninstalling.

1. Refresh the **Apps and Features** window to verify the SQL Server instance was removed successfully, and determine which, if any, SQL Server components still exist. Remove these components from this window as well, if you choose.

## In the event of failure

If the removal process fails, review the [SQL Server Setup log files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md) to find the root cause.

## Related content

- [View and read SQL Server Setup log files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)
- [Uninstall a Cumulative Update from SQL Server](uninstall-a-cumulative-update-from-sql-server.md)
- [Uninstall SQL Server](uninstall-sql-server.md)
