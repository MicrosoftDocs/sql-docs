---
title: "Open Log File Viewer"
description: Learn to use Log File Viewer in SQL Server Management Studio to access information about errors and events that are captured in several logs.
author: adbadram
ms.author: adbadram
ms.date: 12/16/2025
ms.service: sql
ms.subservice: supportability
ms.topic: how-to
helpviewer_keywords:
  - "Log File Viewer, opening"
---
# Open Log File Viewer

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

You can use Log File Viewer in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to access information about errors and events that are captured in the following logs:

- Audit collection
- Data collection
- Database Mail
- Job history
- SQL Server
- SQL Server Agent
- Windows events (These Windows events can also be accessed from Event Viewer.)

You can use Registered Servers to view [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] log files from local or remote instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. By using Registered Servers, you can view the log files when the instances are either online or offline.

For more information about how to access offline [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] log files, see [View Offline Log Files](view-offline-log-files.md).

You can open Log File Viewer in several ways, depending on the information that you want to view.

## Permissions

To access log files for instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are online, this requires membership in the **securityadmin** fixed server role.

To access log files for instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are offline, you must have read access to both the `Root\Microsoft\SqlServer\ComputerManagement10` WMI namespace, and to the folder where the log files are stored. For more information, see the Security section of the topic [View Offline Log Files](view-offline-log-files.md).

## View log files

### View logs that are related to general SQL Server activity

1. In Object Explorer, expand **Management**.

1. There are two options to view logs:

   - Right-click **SQL Server Logs**, point to **View**, and then select either **SQL Server Log** or **SQL Server and Windows Log**.

   - Expand **SQL Server Logs**, right-click any log file, and then select **View SQL Server Log**. You can also double-click any log file.

   The logs include **Database Mail**, **SQL Server**, **SQL Server Agent**, and **Windows**.

### View logs that are related to jobs

In Object Explorer, expand **SQL Server Agent**, right-click **Jobs**, and then select **View History**.

The logs include **Database Mail**, **Job History**, and **SQL Server Agent**.

### View logs that are related to maintenance plans

In Object Explorer, expand **Management**, right-click **Maintenance Plans**, and then select **View History**.

The logs include **Database Mail**, **Job History**, **Maintenance Plans**, **Remote Maintenance Plans**, and **SQL Server Agent**.

### View logs that are related to Data Collection

In Object Explorer, expand **Management**, right-click **Data Collection**, and then select **View Logs**.

The logs include **Data Collection**, **Job History**, and **SQL Server Agent**.

### View logs that are related to Database Mail

In Object Explorer, expand **Management**, right-click **Database Mail**, and then select **View Database Mail Log**.

The logs include **Database Mail, Job History**, **Maintenance Plans**, **Remote Maintenance Plans**, **SQL Server**, **SQL Server Agent**, and **Windows**.

### View logs that are related to Audits collections

In Object Explorer, expand **Security**, expand **Audits**, right-click an audit, and then select **View Audit Logs**.

The logs include **Audit Collection** and **Windows**.

## Related content

- [Log File Viewer](log-file-viewer.md)
- [SQL Server Audit (Database Engine)](../security/auditing/sql-server-audit-database-engine.md)
- [View Offline Log Files](view-offline-log-files.md)
