---
title: "Start Database Mirroring Monitor (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "monitoring database mirroring [SQL Server]"
  - "Database Mirroring Monitor [SQL Server], starting"
  - "database mirroring [SQL Server], monitoring"
ms.assetid: 53165335-97ca-4f88-8e78-22f1839dee98
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Start Database Mirroring Monitor (SQL Server Management Studio)
  The Database Mirroring Monitor is part of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Monitor, which is launched from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
> [!NOTE]  
>  Database Mirroring Monitor is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
### To launch the Database Mirroring Monitor  
  
1.  After connecting to the principal server instance, in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and select the database to be monitored.  
  
3.  Right-click the database, select **Tasks**, and then click **Launch Database Mirroring Monitor**.  
  
4.  In the **Database Mirroring Monitor** dialog box, click **Register Mirrored Database** to register one or more mirrored database.  
  
    > [!NOTE]  
    >  When you register a database at one partner, the database is automatically registered at the other partner. If the monitor already has connection credentials for the other partner instance, those are used to connect. Otherwise the monitor attempts to connect using Windows Authentication. If you want to change the credentials used to connect to either server instance, click **Show the Manage Server Connections dialog box when I click OK**.  
  
 For more information about Database Mirroring Monitor, see [Database Mirroring Monitor Overview](database-mirroring-monitor-overview.md).  
  
## See Also  
 [Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](establish-database-mirroring-session-windows-authentication.md)  
  
  
