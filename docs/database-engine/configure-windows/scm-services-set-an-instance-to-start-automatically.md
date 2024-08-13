---
title: "Set an instance to start automatically (SQL Server Configuration Manager)"
description: Find out how to set an instance of SQL Server to start automatically. Learn about the default configuration, and see how to set the start mode to automatic.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "automatic SQL Server startup"
  - "SQL Server, automatic startup"
  - "starting SQL Server, automatically"
---
# SQL Server Configuration Manager: Set an instance to start automatically

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to set an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to start automatically in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Configuration Manager. During setup, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is normally configured to start automatically. If this was not done, you can change that setting at any time.

## <a id="SSMSProcedure"></a> Use SQL Server Configuration Manager

#### <a id="to-set-an-instance-of-sql-server-to-start-automatically"></a> Set an instance of SQL Server to start automatically

1. On the **Start** menu, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then select **SQL Server Configuration Manager**.

   Because SQL Server Configuration Manager is a snap-in for the [!INCLUDE [msconame-md](../../includes/msconame-md.md)] Management Console program and not a stand-alone program, SQL Server Configuration Manager doesn't appear as an application in newer versions of Windows.

   | Operating system | Details |
   | --- | --- |
   | **Windows 10 and Windows 11** | To open SQL Server Configuration Manager, on the **Start Page**, type `SQLServerManager16.msc` (for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]). For other versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], replace `16` with the appropriate number. Selecting `SQLServerManager16.msc` opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click `SQLServerManager16.msc`, and then select **Open file location**. In the Windows File Explorer, right-click `SQLServerManager16.msc`, and then select **Pin to Start** or **Pin to taskbar**. |
   | **Windows 8** | To open SQL Server Configuration Manager, in the **Search** charm, under **Apps**, type `SQLServerManager<version>.msc`, such as `SQLServerManager16.msc`, and then press **Enter**. |

1. In **SQL Server Configuration Manager**, expand **Services**, and then select **SQL Server**.

1. In the details pane, right-click the name of the instance you want to start automatically, and then select **Properties**.

1. In the **SQL Server \<**_instancename_**> Properties** dialog box, set **Start Mode** to **Automatic**.

1. Select **OK**, and then close [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.

## Related content

- [SQL Server Configuration Manager: Prevent automatic startup of an instance](scm-services-prevent-automatic-startup-of-an-instance.md)
- [SQL Server Configuration Manager: Connect to another computer](scm-services-connect-to-another-computer.md)
- [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)
