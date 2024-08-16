---
title: SQL Server Configuration Manager
description: Utilizing the SQL Server Configuration Manager client
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "protocols [SQL Server], managing"
  - "network protocols [SQL Server], managing"
  - "Client Network Utility"
  - "accounts [SQL Server]"
  - "SQL Server Configuration Manager"
  - "Server Network Utility"
  - "accounts [SQL Server], services"
  - "services [SQL Server], managing"
  - "tools [SQL Server], SQL Server Configuration Manager"
  - "configuration manager [SQL Server]"
---

# SQL Server Configuration Manager

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

SQL Server Configuration Manager is a tool to manage the services associated with [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], to configure the network protocols used by [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], and to manage the network connectivity configuration from [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] client computers. In [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and later versions, you can use Configuration Manager to manage the Azure extension for SQL Server.

SQL Server Configuration Manager is installed with your SQL Server installation. SQL Server Configuration Manager is a [!INCLUDE [msCoName](../includes/msconame-md.md)] Management Console snap-in that is available from the Start menu, or can be added to any other [!INCLUDE [msCoName](../includes/msconame-md.md)] Management Console display. [!INCLUDE [msCoName](../includes/msconame-md.md)] Management Console (**mmc.exe**) uses the **SQLServerManager\<version>.msc** file (such as **SQLServerManager13.msc** for [!INCLUDE [sssql15-md](../includes/sssql16-md.md)]) to open Configuration Manager. You need the corresponding SQL Server Configuration Manager version to manage that particular version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. Here are the paths to the last five versions when Windows is installed on the C drive.

| Version | Path |
| --- | --- |
| [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] | `C:\Windows\SysWOW64\SQLServerManager16.msc` |
| [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] | `C:\Windows\SysWOW64\SQLServerManager15.msc` |
| [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] | `C:\Windows\SysWOW64\SQLServerManager14.msc` |
| [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] | `C:\Windows\SysWOW64\SQLServerManager13.msc` |
| [!INCLUDE [sssql14-md](../includes/sssql14-md.md)] | `C:\Windows\SysWOW64\SQLServerManager12.msc` |
| [!INCLUDE [sssql11-md](../includes/sssql11-md.md)] | `C:\Windows\SysWOW64\SQLServerManager11.msc` |

Because SQL Server Configuration Manager is a snap-in for the [!INCLUDE [msconame-md](../includes/msconame-md.md)] Management Console program and not a stand-alone program, SQL Server Configuration Manager doesn't appear as an application in newer versions of Windows.

| Operating system | Details |
| --- | --- |
| **Windows 10 and Windows 11** | To open SQL Server Configuration Manager, on the **Start Page**, type `SQLServerManager16.msc` (for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]). For other versions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], replace `16` with the appropriate number. Selecting `SQLServerManager16.msc` opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click `SQLServerManager16.msc`, and then select **Open file location**. In the Windows File Explorer, right-click `SQLServerManager16.msc`, and then select **Pin to Start** or **Pin to taskbar**. |
| **Windows 8** | To open SQL Server Configuration Manager, in the **Search** charm, under **Apps**, type `SQLServerManager<version>.msc`, such as `SQLServerManager16.msc`, and then press **Enter**. |

SQL Server Configuration Manager and SQL Server Management Studio use Window Management Instrumentation (WMI) to view and change some server settings. WMI provides a unified way for interfacing with the API calls that manage the registry operations requested by the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] tools. WMI also provides enhanced control and manipulation over the selected SQL services of the SQL Server Configuration Manager snap-in component. For information about configuring permissions related to WMI, see [Configure WMI to Show Server Status in SQL Server Tools](../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md).

To start, stop, pause, resume, or configure services on another computer by using SQL Server Configuration Manager, see [SQL Server Configuration Manager: Connect to another computer](../database-engine/configure-windows/scm-services-connect-to-another-computer.md).

## Manage services

Use SQL Server Configuration Manager to start, pause, resume, or stop the services, to view service properties, or to change service properties.

Use SQL Server Configuration Manager to start the [!INCLUDE [ssDE](../includes/ssde-md.md)] using startup parameters. For more information, see [SQL Server Configuration Manager: Configure server startup options](../database-engine/configure-windows/scm-services-configure-server-startup-options.md).

Beginning with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], you can use SQL Server Configuration Manager to start, pause, resume, or stop Azure extension for SQL Server.

## Change the accounts used by the services

Manage the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] services using SQL Server Configuration Manager.

> [!IMPORTANT]  
> Always use [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] tools such as SQL Server Configuration Manager to change the account used by the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] or [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Agent services, or to change the password for the account. In addition to changing the account name, SQL Server Configuration Manager performs additional configuration such as setting permissions in the Windows Registry so that the new account can read the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] settings. Other tools such as the Windows Services Control Manager can change the account name but don't change associated settings. If the service can't access the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] portion of the registry the service might not start properly.

As an extra benefit, passwords changed using SQL Server Configuration Manager, SMO, or WMI take effect immediately without restarting the service.

## Manage server and client network protocols

SQL Server Configuration Manager allows you to configure server and client network protocols, and connectivity options. After the correct protocols are enabled, you usually don't need to change the server network connections. However, you can use SQL Server Configuration Manager if you need to reconfigure the server connections so [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] listens on a particular network protocol, port, or pipe. For more information about enabling protocols, see [Enable or disable a server network protocol](../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md). For information about enabling access to protocols through a firewall, see [Configure the Windows Firewall to allow SQL Server access](../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).

SQL Server Configuration Manager allows you to manage server and client network protocols, including the ability to force protocol encryption, view alias properties, or enable/disable a protocol.

SQL Server Configuration Manager allows you to create or remove an alias, change the order in which protocols are used, or view properties for a server alias, including:

- Server Alias - The server alias used for the computer to which the client is connecting.
- Protocol - The network protocol used for the configuration entry.
- Connection Parameters - The parameters associated with the connection address for the network protocol configuration.

The SQL Server Configuration Manager also allows you to view information about failover cluster instances, though Cluster Administrator should be used for some actions such as starting and stopping the services.

### Available network protocols

[!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] supports Shared Memory, TCP/IP, and Named Pipes protocols. For information about choosing network protocols, see [Configure Client Protocols](../database-engine/configure-windows/configure-client-protocols.md). [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] doesn't support VIA, Banyan VINES Sequenced Packet Protocol (SPP), Multiprotocol, AppleTalk, or NWLink IPX/SPX network protocols. Clients previously connecting with these protocols must select a different protocol to connect to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. You can't use SQL Server Configuration Manager to configure the WinSock proxy. To configure the WinSock proxy, see your ISA Server documentation.

## Related content

- [SQL Server Configuration Manager: Connect to another computer](../database-engine/configure-windows/scm-services-connect-to-another-computer.md)
- [Start, stop, pause, resume, and restart SQL Server services](../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)
- [Start, stop, or pause the SQL Server Agent service](../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)
- [SQL Server Configuration Manager: Set an instance to start automatically](../database-engine/configure-windows/scm-services-set-an-instance-to-start-automatically.md)
- [SQL Server Configuration Manager: Prevent automatic startup of an instance](../database-engine/configure-windows/scm-services-prevent-automatic-startup-of-an-instance.md)
