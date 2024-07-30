---
title: "Configure server startup options (SQL Server Configuration Manager)"
description: Learn how to set options that the SQL Server Database Engine uses when it starts. View limitations and restrictions on making changes to startup parameters.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "parameters [SQL Server], startup options"
  - "SQL Server, startup options"
  - "SQL Server, startup parameters"
  - "single-user mode [SQL Server], starting in"
  - "startup options [SQL Server]"
  - "startup parameters [SQL Server]"
  - "SQL Server services, setting startup options"
  - "SQL Server services, setting startup parameters"
---
# SQL Server Configuration Manager: Configure server startup options

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to configure startup options that are used every time the [!INCLUDE [ssDE](../../includes/ssde-md.md)] starts in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. For a list of startup options, see [Database Engine Service startup options](database-engine-service-startup-options.md).

## Limitations

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager writes startup parameters to the registry. They take effect upon the next startup of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

On a cluster, changes must be made on the active server when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is online, which take effect when the [!INCLUDE [ssDE](../../includes/ssde-md.md)] is restarted. The registry update of the startup options on the other node will occur upon the next failover.

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], when you set the **Start Mode** for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service to *Automatic* in Configuration Manager, the service starts in *Automatic (Delayed Start)* mode instead, even though the **Start Mode** shows as *Automatic*.

## Permissions

Configuring server startup options is restricted to users who can change the related entries in the registry. This includes the following users.

- Members of the local administrators group.

- The domain account that is used by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], if the [!INCLUDE [ssDE](../../includes/ssde-md.md)] is configured to run under a domain account.

## <a id="SSMSProcedure"></a> Use SQL Server Configuration Manager

### Configure startup options

1. Select the **Start** button, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then select **SQL Server Configuration Manager**.

   Because SQL Server Configuration Manager is a snap-in for the [!INCLUDE [msconame-md](../../includes/msconame-md.md)] Management Console program and not a stand-alone program, SQL Server Configuration Manager doesn't appear as an application in newer versions of Windows.

   | Operating system | Details |
   | --- | --- |
   | **Windows 10 and Windows 11** | To open SQL Server Configuration Manager, on the **Start Page**, type `SQLServerManager16.msc` (for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]). For other versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], replace `16` with the appropriate number. Selecting `SQLServerManager16.msc` opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click `SQLServerManager16.msc`, and then select **Open file location**. In the Windows File Explorer, right-click `SQLServerManager16.msc`, and then select **Pin to Start** or **Pin to taskbar**. |
   | **Windows 8** | To open SQL Server Configuration Manager, in the **Search** charm, under **Apps**, type `SQLServerManager<version>.msc`, such as `SQLServerManager16.msc`, and then press **Enter**. |

1. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, select **SQL Server Services**.

1. In the right pane, right-click **SQL Server (***<instance_name>***)**, and then select **Properties**.

1. On the **Startup Parameters** tab, in the **Specify a startup parameter** box, type the parameter, and then select **Add**.

   For example, to start in single-user mode, type `-m` in the **Specify a startup parameter** box and then select **Add**. (When you restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode, stop the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent. Otherwise, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent might connect first and prevent you from connecting as a second user.)

   The following screenshot shows the Startup Parameters tab in the SQL Server Properties dialog, where you can modify startup parameters.

   :::image type="content" source="media/scm-services-configure-server-startup-options/trace-flag.png" alt-text="Screenshot of the SQL Server (MSSQLSERVER) Properties dialog, with the Startup Parameters tab selected.":::

1. Select **OK**.

1. Restart the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

   > [!WARNING]  
   > After you're finished using single-user mode, in the Startup Parameters box, select the **-m** parameter in the **Existing Parameters** box, and then select **Remove**. Restart the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to restore [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to the typical multi-user mode.

## Related content

- [Single-user mode for SQL Server](start-sql-server-in-single-user-mode.md)
- [Connect to SQL Server when system administrators are locked out](connect-to-sql-server-when-system-administrators-are-locked-out.md)
- [Start, stop, or pause the SQL Server Agent service](../../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)
- [Database Engine Service startup options](database-engine-service-startup-options.md)
