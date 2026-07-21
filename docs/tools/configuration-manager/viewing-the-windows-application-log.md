---
title: View the Windows Application Log to Troubleshoot SQL Server
description: Find out how to view and manage the Windows application log. You can configure SQL Server to write event information to this log.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/26/2024
ms.service: sql
ms.subservice: tools-other
ms.topic: troubleshooting-general
ms.collection:
  - data-tools
helpviewer_keywords:
  - "application logs [SQL Server]"
  - "Windows application logs [SQL Server]"
  - "viewing Windows application logs"
  - "errors [SQL Server], logs"
  - "system logs [SQL Server]"
  - "security logs [SQL Server]"
  - "displaying Windows application logs"
  - "logs [SQL Server], Windows application logs"
monikerRange: ">=sql-server-2017"
---

# View the Windows Application log to troubleshoot SQL Server

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

When SQL Server is configured to use the Microsoft Windows application log, each SQL Server session writes new events to that log. Unlike the SQL Server error log, a new application log isn't created each time you start an instance of  SQL Server.

View and manage the Windows application log by using Windows Event Viewer or the Log Viewer in SQL Server Management Studio.

There are three logs that can be viewed with Event Viewer.

| Windows log type | Description |
| --- | --- |
| System log | Records events logged by the Windows operating system components. For example, the failure of a driver or other system component to load during startup is recorded in the system log. |
| Security log | Records security events, such as failed login attempts. This helps track changes to the security system and identify possible breaches to security. For example, attempts to log on to the system might be recorded in the security log, depending on the audit settings in the User Manager.<br /><br />Only members of the **sysadmin** fixed server role can view the security log. |
| Application log | Records events that are logged by applications. For example, a database application might record a file error in the application log. |

For more information about using Event Viewer, managing the application log, and understanding the information it presents, see the Windows documentation.

## To view the Windows application log

### Permissions required

To view the Windows Application Log using SQL Server Management Studio (SSMS), the following permissions are required:

- The user account must have access to the Event Viewer.
- Typically, the account must be a member of the **Administrators group** or the **Event Log Readers** group on the server where the logs reside.
- If SSMS isn't running with elevated privileges, you might encounter an error. To resolve this, right-click the SSMS shortcut and select **Run as Administrator**.

### Troubleshooting permissions

If you still encounter issues:

- Verify that your account has the appropriate group memberships.
- For domain accounts, contact your system administrator to ensure you have the correct Active Directory permissions.
- As a workaround, consider exporting the Event Log and viewing it on a machine where you have the required privileges.

For more information about how to view the Windows application log, visit [Windows Application Log (Windows)](../../relational-databases/performance/view-the-windows-application-log-windows-10.md).
