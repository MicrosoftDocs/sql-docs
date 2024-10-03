---
title: Manage the Database Engine services
description: Get acquainted with services that are available in SQL Server. See how to start SQL Server Configuration Manager, which you can use to manage various services.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/26/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Server Configuration Manager, accessing"
  - "Database Engine [SQL Server], services"
  - "managing services [SQL Server], about service management"
  - "services [SQL Server]"
  - "SQL Server Agent service, managing"
  - "SQL Server services, about SQL Server service"
  - "MSSQLServer"
  - "server configuration [SQL Server]"
  - "managing services [SQL Server]"
  - "SQL Server Agent service"
  - "services [SQL Server], managing"
  - "administering SQL Server, services"
  - "SQL Server services"
---
# Manage the Database Engine services

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs on the operating systems as a service. A service is a type of application that runs in the system background. Services usually provides core operating system features, such as Web serving, event logging, or file serving. Services can run without showing a user interface on the computer desktop. The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent, and several other [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components run as services. These services typically are started when the operating system starts. This depends on what is specified during setup; some services aren't started by default. This section describes the management of the various [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services. Before you log in to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you need to know how to start, stop, pause, resume, and restart an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. After you're logged in, you can perform tasks such as administering the server or querying a database.

## Use the SQL Server service

When you start an instance of [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], you're starting the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service. After you start the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service, users can establish new connections to the server. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service can be started and stopped as a service, either locally or remotely. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service is referred to as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (MSSQLSERVER) if it's the default instance, or MSSQL$*\<instancename\>* if it's a named instance.

## Use SQL Server Configuration Manager

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager allows you to stop, start, or pause various [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager can't manage [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] services.

You can also use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to view the properties of the selected service. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is a [!INCLUDE [msCoName](../../includes/msconame-md.md)] Management Console (MMC) snap-in. For more information about MMC and how a snap-in works, see Windows Help.

On the **Start** menu, point to **All Programs**, point to [!INCLUDE [ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then select **SQL Server Configuration Manager**.

Because SQL Server Configuration Manager is a snap-in for the [!INCLUDE [msconame-md](../../includes/msconame-md.md)] Management Console program and not a stand-alone program, SQL Server Configuration Manager doesn't appear as an application in newer versions of Windows.

| Operating system | Details |
| --- | --- |
| **Windows 10 and Windows 11** | To open SQL Server Configuration Manager, on the **Start Page**, type `SQLServerManager16.msc` (for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]). For other versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], replace `16` with the appropriate number. Selecting `SQLServerManager16.msc` opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click `SQLServerManager16.msc`, and then select **Open file location**. In the Windows File Explorer, right-click `SQLServerManager16.msc`, and then select **Pin to Start** or **Pin to taskbar**. |
| **Windows 8** | To open SQL Server Configuration Manager, in the **Search** charm, under **Apps**, type `SQLServerManager<version>.msc`, such as `SQLServerManager16.msc`, and then press **Enter**. |

## Manage services

- [Broadcast a Shutdown Message (Command Prompt)](broadcast-a-shutdown-message-command-prompt.md)
- [Change server authentication mode](change-server-authentication-mode.md)
- [Configure file system permissions for Database Engine access](configure-file-system-permissions-for-database-engine-access.md)
- [Configure Windows service accounts and permissions](configure-windows-service-accounts-and-permissions.md)
- [Database Engine Service startup options](database-engine-service-startup-options.md)
- [Log In to an Instance of SQL Server (Command Prompt)](log-in-to-an-instance-of-sql-server-command-prompt.md)
- [Run SQL Server With or Without a Network](run-sql-server-with-or-without-a-network.md)
- [Security Requirements for Managing Services](security-requirements-for-managing-services.md)
- [Single-user mode for SQL Server](start-sql-server-in-single-user-mode.md)
- [SQL Server Browser Service (Database Engine and SSAS)](sql-server-browser-service-database-engine-and-ssas.md)
- [SQL Server Configuration Manager: Change the password of the accounts used](scm-services-change-the-password-of-the-accounts-used.md)
- [SQL Server Configuration Manager: Change the service startup account](scm-services-change-the-service-startup-account.md)
- [SQL Server Configuration Manager: Configure server startup options](scm-services-configure-server-startup-options.md)
- [SQL Server Configuration Manager: Configure SQL Server error logs](scm-services-configure-sql-server-error-logs.md)
- [SQL Server Configuration Manager: Connect to another computer](scm-services-connect-to-another-computer.md)
- [SQL Server Configuration Manager: Prevent automatic startup of an instance](scm-services-prevent-automatic-startup-of-an-instance.md)
- [SQL Server Configuration Manager: Set an instance to start automatically](scm-services-set-an-instance-to-start-automatically.md)
- [SQL Writer service](sql-writer-service.md)
- [Start SQL Server with Minimal Configuration](start-sql-server-with-minimal-configuration.md)
- [Start, stop, pause, resume, and restart SQL Server services](start-stop-pause-resume-restart-sql-server-services.md)

## Related content

- [Configure SQL Server Agent](../../ssms/agent/configure-sql-server-agent.md)
- [Logging In to SQL Server](logging-in-to-sql-server.md)
