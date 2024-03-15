---
title: "Uninstall a Cumulative Update from SQL Server"
description: This article describes how to remove a Cumulative Update from a stand-alone instance of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/20/2022
ms.service: sql
ms.subservice: install
ms.custom: linux-related-content
ms.topic: conceptual
helpviewer_keywords:
  - "removing CU from SQL Server"
  - "uninstalling CU from SQL Server"
  - "removing Cumulative Update from SQL Server"
  - "uninstalling cumulative update from SQL Server"
  - "uninstall SQL Server CU"
---
# Uninstall a Cumulative Update from SQL Server

[!INCLUDE[_ssnoversion](../../includes/applies-to-version/_ssnoversion.md)]

This article describes how to remove a Cumulative Update (CU) from a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], on both Windows and Linux.

To uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Windows, see [Uninstall SQL Server](uninstall-sql-server.md).

## Considerations

- To remove a CU on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must be a local administrator.

## Prepare

1. **Back up your data.** Create [full backups](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md) of all databases. The `master` database contains all system level information for the instance, such as logins, and schemas. The `msdb` database contains job information such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, backup history, and maintenance plans. For more information about system databases, see [System databases](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).

1. **Stop all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services.** We recommend that you stop all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services before you uninstall a Cumulative Update. Active connections can prevent successful removal.

1. **Use an account that has the appropriate permissions.** Sign in to the server by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account or by using an account that has equivalent permissions. For example, you can sign in on Windows with an account that is a member of the local Administrators group. On Linux, you should run the commands using the root account.

## Remove a Cumulative Update on Windows

Depending on the version of Windows you're using, you can access the list of installed updates in several ways.

#### Locate the list of installed updates

For Windows Server 2016, Windows 10, and later versions:

1. Open the Start menu, and type `update history`.
1. Select **View update history** from the search results.
1. Select **Uninstall updates**.

For older versions of Windows, navigate to **Control Panel > Programs and Features > View installed updates**.

#### Remove the Cumulative Update

Depending on the version of Windows you're using, you can remove the CU from the list of installed updates by either selecting the **Uninstall** option next to the update, or by right-clicking on the update and selecting **Uninstall**.

Follow the instructions to remove the CU. You may be prompted to restart your computer after the CU is removed.

## Remove a Cumulative Update on Linux

To uninstall a Cumulative Update on Linux, you must roll back the package to the previous version.

[!INCLUDE[roll-back-sql-server](../../linux/includes/roll-back-sql-server.md)]

## In the event of failure

If the removal process fails, review the [SQL Server Setup log files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md) to determine the root cause.

## Next steps

- [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)
- [Uninstall an existing instance of SQL Server (Setup)](uninstall-an-existing-instance-of-sql-server-setup.md)
- [Uninstall SQL Server](uninstall-sql-server.md)
