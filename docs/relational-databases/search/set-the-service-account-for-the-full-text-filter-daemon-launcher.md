---
title: Set Service Account for Full-Text Filter Daemon Launcher
description: Learn how to set or change the service account for the SQL Full-text Filter Daemon Launcher service (MSSQLFDLauncher).
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: how-to
ms.custom:
  - sfi-image-nochange
helpviewer_keywords:
  - "full-text search [SQL Server], FDHOST Launcher (MSSQLFDLauncher) service account"
  - "FDHOST Launcher (MSSQLFDLauncher) [SQL Server]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Set the service account for the full-text Filter Daemon Launcher

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to set or change the service account for the SQL Full-text Filter Daemon Launcher service (`MSSQLFDLauncher`) by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. The default service account used by SQL Server setup is `NT Service\MSSQLFDLauncher`.

## About the SQL full-text Filter Daemon Launcher service

The SQL Full-text Filter Daemon Launcher service is used by SQL Server Full-Text Search to start the filter daemon host process, which handles full-text search filtering and word breaking. The Launcher service must be running to use full-text search.

The SQL Full-text Filter Daemon Launcher service is an instance-aware service that is associated with a specific instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The SQL Full-text Filter Daemon Launcher service propagates the service account information to each filter daemon host process that it launches.

<a id="setting"></a>

## Set the service account

1. On the **Start** menu, point to **All Programs**, expand [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)], and then select **SQL Server *nnnn* Configuration Manager**, where *nnnn* is the version you have installed.

1. In **SQL Server Configuration Manager**, select **SQL Server Services**, right-click **SQL Full-text Filter Daemon Launcher (***instance name***)**, and then select **Properties**.

1. Select the **Log On** tab of the dialog box, and then select or enter the account under which to run the processes that the SQL Full-text Filter Daemon Launcher service starts.

1. After you close the dialog box, select **Restart** to restart the SQL Full-text Filter Daemon Launcher service.

:::image type="content" source="media/set-the-service-account-for-the-full-text-filter-daemon-launcher/sql-full-text-filter-daemon-launch-process-properties.png" alt-text="Screenshot of SQL Full-text Filter Daemon Launch process properties.":::

<a id="error"></a>

## Troubleshoot the SQL full-text Filter Daemon Launcher service if it doesn't start

If the SQL Full-text Filter Daemon Launcher service doesn't start, review the following possible causes:

### Permissions issues

- The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service group doesn't have permission to start the SQL Full-text Filter Daemon Launcher service.

  Make sure the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service group has permissions to the SQL Full-text Filter Daemon Launcher service account. During the installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service group is granted default permission to manage, query, and start the SQL Full-text Filter Daemon Launcher service. If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service group permissions to the SQL Full-text Filter Daemon Launcher service account have been removed after [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation, the SQL Full-text Filter Daemon Launcher service doesn't start, and full-text search is disabled.

- The account used to connect to the service doesn't have privileges.

  You might be using an account that doesn't have login privileges on the computer where the server instance is installed. Verify that you're signing in with an account that has user rights and permissions on the local computer.

### Service account and password issues

- The user account or password of the service account is incorrect.

  In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, make sure the service is using the correct service account and password.

- The password associated with the SQL Full-text Filter Daemon Launcher service account has expired.

  If you use a local user account for the SQL Full-text Filter Daemon Launcher service and the password expires, you have to:

  1. Set a new Windows password for the account.

  1. In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, update the SQL Full-text Filter Daemon Launcher service to use the new password.

### Named pipes configuration issues

- The SQL Full-text Filter Daemon Launcher service isn't configured correctly.

  If named pipes functionality has been disabled on the local computer, or if [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has been configured to use a named pipe other than the default named pipe, the SQL Full-text Filter Daemon Launcher service might not start.

- Another instance of the same named pipe is already running.

  The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service acts as a named pipe server for the SQL Full-text Filter Daemon Launcher service client. If the named pipe was already created by another process before [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] starts, an error is logged in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log and the Windows Event Log, and full-text search isn't available. Determine what process or application is attempting to use the same named pipe and stop the application.

## Related content

- [SQL Server Configuration Manager: Connect to another computer](../../database-engine/configure-windows/scm-services-connect-to-another-computer.md)
- [Upgrade Full-Text Search (SQL Server Search)](upgrade-full-text-search.md)
