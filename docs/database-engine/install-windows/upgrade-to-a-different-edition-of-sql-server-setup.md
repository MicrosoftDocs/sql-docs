---
title: Upgrade to a different edition of SQL Server
description: SQL Server Setup supports edition upgrade among various editions of SQL Server. Before you begin an edition upgrade, review the resources in this article.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/22/2024
ms.service: sql
ms.subservice: install
ms.topic: conceptual
monikerRange: ">=sql-server-2016"
---
# Upgrade to a different edition of SQL Server (Setup)

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup supports edition upgrade among various editions of [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)]. For information about supported edition upgrade paths, see [Supported version and edition upgrades (SQL Server 2022)](supported-version-and-edition-upgrades-2022.md).

[!INCLUDE [editions-supported-features-windows](../../includes/editions-supported-features-windows.md)]

Before you initiate the edition upgrade of an instance of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], review the following articles:

- [Compute capacity limits by edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md)
- [SQL Server 2022: Hardware and software requirements](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md)

## Remarks

For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a failover cluster instance, running edition upgrade on one of the nodes of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance is sufficient. This node can be either active or passive, and the engine doesn't bring the resources offline during the edition upgrade. After the edition upgrade, you must either restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance, or fail over to a different node.

## Prerequisites

For local installations, you must run Setup as an administrator. If you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read permissions on the remote share.

> [!IMPORTANT]  
> For the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition change to be activated, Setup must restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services. This will result in application down time while services are offline.

## Upgrade process

1. Insert the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation media. From the root folder, double-click `setup.exe` or launch the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center from Configuration Tools. To install from a network share, locate the root folder on the share, and then double-click `setup.exe`.

1. To upgrade an existing instance of [!INCLUDE [ssNoversion](../../includes/ssnoversion-md.md)] to a different edition, from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center select **Maintenance**, and then select **Edition Upgrade**.

1. If Setup support files are required, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup installs them. If you're instructed to restart your computer, restart before you continue.

1. The System Configuration Checker runs a discovery operation on your computer. To continue, select **OK**.

1. On the **Product Key** page, select a radio button to indicate whether you're upgrading to a free edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], or whether you have a PID key for a production version of the product. For more information, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md) and [Supported version and edition upgrades (SQL Server 2022)](supported-version-and-edition-upgrades-2022.md).

1. On the License Terms page, read the license agreement, and then select the check box to accept the licensing terms and conditions. To continue, select **Next**. To end Setup, select **Cancel**.

1. On the **Select Instance** page, specify the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to upgrade.

1. The **Edition Upgrade Rules** page validates your computer configuration before the edition upgrade operation begins.

1. The **Ready to Upgrade Edition** page shows a tree view of installation options that were specified during Setup. To continue, select **Upgrade**.

1. During the edition upgrade process, the services need to be restarted to pick up the new setting. After edition upgrade, the **Complete** page provides a link to the summary log file for the edition upgrade. To close the wizard, select **Close**.

1. The **Complete** page provides a link to the summary log file for the installation and other important notes.

1. If you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you finish Setup. For information about Setup log files, see [View and Read SQL Server Setup Log Files](view-and-read-sql-server-setup-log-files.md).

1. If you upgraded from [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)], you must perform extra steps before you can use your upgraded instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

   - Enable the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service in Windows SCM.

   - Configure the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

In addition to the previous steps, you might need to do the following if you upgraded from [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)]:

- Users that were created and configured in [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] remain configured after the upgrade. Specifically, the `BUILTIN\Users` group remains configured. Disable, remove, or reconfigure these accounts as needed. For more information, see [Configure Windows service accounts and permissions](../configure-windows/configure-windows-service-accounts-and-permissions.md).

- Sizes and recovery model for the `tempdb` and `model` system databases remain unchanged after the upgrade. Reconfigure these settings as needed. For more information, see [Back up and restore: System databases (SQL Server)](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).

- Template databases remain on the computer after the upgrade.

If the procedure fails on `Engine_SqlEngineHealthCheck` rule, then you can use the command line installation option to skip this specific rule to allow the upgrade process to complete successfully. To skip checking this rule, open a command prompt, change to the path that contains SQL Server Setup (`setup.exe`). Then, type the following command:

```cmd
setup.exe /q /ACTION=editionupgrade /InstanceName=MSSQLSERVER /PID=<appropriatePid> /SkipRules=Engine_SqlEngineHealthCheck
```

## Related content

- [Upgrade SQL Server](upgrade-sql-server.md)
- [Backward compatibility](/previous-versions/sql/sql-server-2016/cc280407(v=sql.130))
- [Compatibility certification](compatibility-certification.md)
