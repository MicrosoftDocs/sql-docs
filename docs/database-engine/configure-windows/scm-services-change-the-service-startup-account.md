---
title: Change the Service Startup Account (SQL Server Configuration Manager)
description: Learn how to change the service accounts that SQL Server and many of its services use. View limitations and restrictions on changes in service accounts.
author: adbadram
ms.author: adbadram
ms.date: 03/16/2026
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
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

<a id="SSMSProcedure"></a>

## Use SQL Server Configuration Manager

Open SQL Server Configuration Manager from the Windows Start menu.

[!INCLUDE [open-sql-server-configuration-manager](../../includes/paragraph-content/open-sql-server-configuration-manager.md)]

### Configure startup options

1. In SQL Server Configuration Manager, select **SQL Server Services**.

1. In the details pane, right-click the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance for which you want to change the service startup account, and then select **Properties**.

1. In the **SQL Server \<**_instancename_**> Properties** dialog box, select the **Log On** tab, and select a **Log on as** account type.

1. After selecting the new service startup account, select **OK**.

   A message box asks whether you want to restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service.

1. Select **Yes**, and then close SQL Server Configuration Manager.

## Related content

- [Start, stop, pause, resume, and restart SQL Server services](start-stop-pause-resume-restart-sql-server-services.md)
- [Configure WMI to Show Server Status in SQL Server Tools](/ssms/configure-wmi-to-show-server-status-in-sql-server-tools)
