---
title: "Change the service startup account (SQL Server Configuration Manager)"
description: Learn how to change the service accounts that SQL Server and many of its services use. View limitations and restrictions on changes in service accounts.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Server services, startup account changes"
  - "startup accounts [SQL Server]"
  - "changing startup accounts for services"
---
# SQL Server Configuration Manager: Change the service startup account

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to use SQL Server Configuration Manager to change the startup options of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services and to change the service accounts that are used by the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE [tsql](../../includes/tsql-md.md)], or PowerShell. For more information about how to select an appropriate service account, see [Configure Windows service accounts and permissions](configure-windows-service-accounts-and-permissions.md).

> [!IMPORTANT]  
> When you change the service startup account for the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service (the [!INCLUDE [ssDE](../../includes/ssde-md.md)]) must be restarted for the change to take effect. When the service is restarted, all databases associated with that instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] will be unavailable until the service successfully restarts. If you have to change the service startup account of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent, make sure that you do so during regularly scheduled maintenance or when the databases can be taken offline without interrupting daily operations.

## Limitations

- Clustered servers

  Changing the service account that is used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be performed from the active node of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] cluster.

  When you run on [!INCLUDE [winserver2008-md](../../includes/winserver2008-md.md)] (in a non-default configuration using Domain groups), changing the service account that is used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent requires SQL Server Configuration Manager to stop [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by taking the resource groups offline.

- SKU Upgrade ([!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] to non-Express SKU)

  During [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] installation, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is configured to use the Network Service account but disabled. SQL Server Configuration Manager can change the account assigned for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service   but the service can't be enabled or started. After SKU upgrade from [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] to non-Express, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service isn't automatically enabled, but can be enabled when needed by using the SQL Server Configuration Manager and changing the service start mode to Manual or Automatic.

## <a id="SSMSProcedure"></a> Use SQL Server Configuration Manager

#### Change the SQL Server service startup account

1. On the **Start** menu, point to **All Programs**, point to **[!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]**, point to **Configuration Tools**, and then select **SQL Server Configuration Manager**.

   Because SQL Server Configuration Manager is a snap-in for the [!INCLUDE [msconame-md](../../includes/msconame-md.md)] Management Console program and not a stand-alone program, SQL Server Configuration Manager doesn't appear as an application in newer versions of Windows.

   | Operating system | Details |
   | --- | --- |
   | **Windows 10 and Windows 11** | To open SQL Server Configuration Manager, on the **Start Page**, type `SQLServerManager16.msc` (for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]). For other versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], replace `16` with the appropriate number. Selecting `SQLServerManager16.msc` opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click `SQLServerManager16.msc`, and then select **Open file location**. In the Windows File Explorer, right-click `SQLServerManager16.msc`, and then select **Pin to Start** or **Pin to taskbar**. |
   | **Windows 8** | To open SQL Server Configuration Manager, in the **Search** charm, under **Apps**, type `SQLServerManager<version>.msc`, such as `SQLServerManager16.msc`, and then press **Enter**. |

1. In SQL Server Configuration Manager, select **SQL Server Services**.

1. In the details pane, right-click the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance for which you want to change the service startup account, and then select **Properties**.

1. In the **SQL Server \<**_instancename_**> Properties** dialog box, select the **Log On** tab, and select a **Log on as** account type.

1. After selecting the new service startup account, select **OK**.

   A message box asks whether you want to restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service.

1. Select **Yes**, and then close SQL Server Configuration Manager.

## Related content

- [Start, stop, pause, resume, and restart SQL Server services](start-stop-pause-resume-restart-sql-server-services.md)
- [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)
