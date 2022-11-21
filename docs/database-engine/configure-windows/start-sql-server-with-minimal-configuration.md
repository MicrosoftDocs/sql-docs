---
title: "Start SQL Server with Minimal Configuration"
description: Become familiar with the minimal configuration startup option in SQL Server. See when and how to use it, and learn about how it limits functionality.
author: rwestMSFT
ms.author: randolphwest
ms.date: "01/20/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "minimal configuration [SQL Server]"
  - "starting SQL Server, minimal configuration"
---
# Start SQL Server with Minimal Configuration
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  If you have configuration problems that prevent the server from starting, you can start an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the minimal configuration startup option. This is the startup option **-f**. Starting an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with minimal configuration automatically puts the server in single-user mode.  
  
 When you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in minimal configuration mode, note the following:  
  
-   Only a single user can connect, and the `CHECKPOINT` process is not executed.  
  
-   Remote access and read-ahead are disabled.  
  
-   Startup stored procedures do not run.  

-   `tempdb` is configured at the smallest possible size.

-   Audit will be disabled but Audit DDL can still be issued. In practice, **-m** should be sufficient for most cases that require SQL Serve Audit reconfiguration. For more details about security in Auditing configuration, see [Auditing in SQL Server](/previous-versions/sql/sql-server-2008/dd392015(v=sql.100)#security).
  
 After the server has been started with minimal configuration, you should change the appropriate server option value or values, stop, and then restart the server.  
  
> [!IMPORTANT]  
>  Use the **sqlcmd** utility and the dedicated administrator connection (DAC) to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you use a typical connection, stop the SQL Server Agent service before connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in minimal configuration mode. Otherwise, the SQL Server Agent service uses the connection, thereby blocking it.  
  
## See Also  
 [Start, Stop, or Pause the SQL Server Agent Service](../../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)   
 [Diagnostic Connection for Database Administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md)   
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md)  
  
