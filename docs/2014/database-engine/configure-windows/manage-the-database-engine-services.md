---
title: "Manage the Database Engine Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
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
ms.assetid: aa732e43-53ba-4eea-bb9b-089da0766fc1
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Manage the Database Engine Services
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs on the operating systems as a service. A service is a type of application that runs in the system background. Services usually provide core operating system features, such as Web serving, event logging, or file serving. Services can run without showing a user interface on the computer desktop. The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, and several other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components run as services. These services typically are started when the operating system starts. This depends on what is specified during setup; some services are not started by default. This section describes the management of the various [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services. Before you log in to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you need to know how to start, stop, pause, resume, and restart an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After you are logged in, you can perform tasks such as administering the server or querying a database.  
  
## Using the SQL Server Service  
 When you start an instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], you are starting the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. After you start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service, users can establish new connections to the server. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service can be started and stopped as a service, either locally or remotely. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is referred to as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (MSSQLSERVER) if it is the default instance, or MSSQL$*\<instancename>*if it is a named instance.  
  
## Using SQL Server Configuration Manager  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager allows you to stop, start, or pause various [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager cannot manage [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] services.  
  
 You can also use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to view the properties of the selected service. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console (MMC) snap-in. For more information about MMC and how a snap-in works, see Windows Help.  
  
 **To access SQL Server Configuration Manager**  
  
-   On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
 **To access SQL Server Configuration Manager Using Windows 8**  
  
 Because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is a snap-in for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console program and not a stand-alone program, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager not does not appear as an application when running Windows 8.0. To open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the **Search** charm, under **Apps**, type **SQLServerManager12.msc** (for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]), **SQLServerManager11.msc** (for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]), or **SQLServerManager10.msc** for ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]), and then press **Enter**.  
  
## In this Section  
  
|||  
|-|-|  
|[Security Requirements for Managing Services](security-requirements-for-managing-services.md)|[Prevent Automatic Startup of an Instance of SQL Server &#40;SQL Server Configuration Manager&#41;](scm-services-prevent-automatic-startup-of-an-instance.md)|  
|[Configure Windows Service Accounts and Permissions](configure-windows-service-accounts-and-permissions.md)|[Change the Service Startup Account for SQL Server &#40;SQL Server Configuration Manager&#41;](scm-services-change-the-service-startup-account.md)|  
|[Run SQL Server With or Without a Network](run-sql-server-with-or-without-a-network.md)|[Configure Server Startup Options &#40;SQL Server Configuration Manager&#41;](scm-services-configure-server-startup-options.md)|  
|[SQL Server Browser Service &#40;Database Engine and SSAS&#41;](sql-server-browser-service-database-engine-and-ssas.md)|[Change the Password of the Accounts Used by SQL Server &#40;SQL Server Configuration Manager&#41;](scm-services-change-the-password-of-the-accounts-used.md)|  
|[Database Engine Service Startup Options](database-engine-service-startup-options.md)|[Configure SQL Server Error Logs](scm-services-configure-sql-server-error-logs.md)|  
|[Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](start-stop-pause-resume-restart-sql-server-services.md)|[Change Server Authentication Mode](change-server-authentication-mode.md)|  
|[Start SQL Server in Single-User Mode](start-sql-server-in-single-user-mode.md)|[SQL Writer Service](sql-writer-service.md)|  
|[Start SQL Server with Minimal Configuration](start-sql-server-with-minimal-configuration.md)|[Broadcast a Shutdown Message &#40;Command Prompt&#41;](broadcast-a-shutdown-message-command-prompt.md)|  
|[Connect to Another Computer &#40;SQL Server Configuration Manager&#41;](scm-services-connect-to-another-computer.md)|[Log In to an Instance of SQL Server &#40;Command Prompt&#41;](log-in-to-an-instance-of-sql-server-command-prompt.md)|  
|[Set an Instance of SQL Server to Start Automatically &#40;SQL Server Configuration Manager&#41;](scm-services-set-an-instance-to-start-automatically.md)|[Configure File System Permissions for Database Engine Access](configure-file-system-permissions-for-database-engine-access.md)|  
  
## Related Content  
 [Configure SQL Server Agent](../../ssms/agent/sql-server-agent.md)  
  
 [Logging In to SQL Server](logging-in-to-sql-server.md)  
  
  
