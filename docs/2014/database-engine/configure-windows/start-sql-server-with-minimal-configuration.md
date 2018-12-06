---
title: "Start SQL Server with Minimal Configuration | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "minimal configuration [SQL Server]"
  - "starting SQL Server, minimal configuration"
ms.assetid: 4d733c99-28b3-42d8-b7f6-7b943b548173
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Start SQL Server with Minimal Configuration
  If you have configuration problems that prevent the server from starting, you can start an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the minimal configuration startup option. This is the startup option **-f**. Starting an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with minimal configuration automatically puts the server in single-user mode.  
  
 When you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in minimal configuration mode, note the following:  
  
-   Only a single user can connect, and the CHECKPOINT process is not executed.  
  
-   Remote access and read-ahead are disabled.  
  
-   Startup stored procedures do not run.  
  
 After the server has been started with minimal configuration, you should change the appropriate server option value or values, stop, and then restart the server.  
  
> [!IMPORTANT]  
>  Use the **sqlcmd** utility and the dedicated administrator connection (DAC) to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you use a typical connection, stop the SQL Server Agent service before connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in minimal configuration mode. Otherwise, the SQL Server Agent service uses the connection, thereby blocking it.  
  
## See Also  
 [Start, Stop, or Pause the SQL Server Agent Service](../../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)   
 [Diagnostic Connection for Database Administrators](diagnostic-connection-for-database-administrators.md)   
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)   
 [Database Engine Service Startup Options](database-engine-service-startup-options.md)  
  
  
