---
title: Set an Instance to Start Automatically (SQL Server Configuration Manager)
description: Find out how to set an instance of SQL Server to start automatically. Learn about the default configuration, and see how to set the start mode to automatic.
author: adbadram
ms.author: adbadram
ms.date: 03/16/2026
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "automatic SQL Server startup"
  - "SQL Server, automatic startup"
  - "starting SQL Server, automatically"
---
# SQL Server Configuration Manager: Set an instance to start automatically

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to set an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to start automatically in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Configuration Manager. During setup, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is normally configured to start automatically. If this was not done, you can change that setting at any time.

<a id="SSMSProcedure"></a>

## Use SQL Server Configuration Manager

Open SQL Server Configuration Manager from the Windows Start menu.

[!INCLUDE [open-sql-server-configuration-manager](../../includes/paragraph-content/open-sql-server-configuration-manager.md)]

### Set an instance of SQL Server to start automatically

1. In **SQL Server Configuration Manager**, expand **Services**, and then select **SQL Server**.

1. In the details pane, right-click the name of the instance you want to start automatically, and then select **Properties**.

1. In the **SQL Server \<**_instancename_**> Properties** dialog box, set **Start Mode** to **Automatic**.

1. Select **OK**, and then close [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

## Related content

- [SQL Server Configuration Manager: Prevent automatic startup of an instance](scm-services-prevent-automatic-startup-of-an-instance.md)
- [SQL Server Configuration Manager: Connect to another computer](scm-services-connect-to-another-computer.md)
- [Configure WMI to Show Server Status in SQL Server Tools](/ssms/configure-wmi-to-show-server-status-in-sql-server-tools)
