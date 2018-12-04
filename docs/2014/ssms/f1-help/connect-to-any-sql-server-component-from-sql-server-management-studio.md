---
title: "Connect to Any SQL Server Component from SQL Server Management Studio | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "connections [SQL Server], SQL Server Management Studio"
  - "saving connections"
  - "components [SQL Server], connections"
  - "SQL Server Management Studio [SQL Server], connections"
ms.assetid: 5eeb41bd-b25b-4d3b-a005-a7d9e4b5978e
author: stevestein
ms.author: sstein
manager: craigg
---
# Connect to Any SQL Server Component from SQL Server Management Studio
  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides functionality for managing every component of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to connect to:  
  
-   An instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 Although [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] allows you to work with queries without first establishing a connection to a data source, most other tasks require a connection. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] provides the **Connect to Server** dialog box to configure connection properties to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components. When [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] starts, it opens the **Connect to Server** dialog box and prompts you to connect to a server. The **Connect to Server** dialog box retains the connection settings from the last time it was used.  
  
> [!NOTE]  
>  This feature can be turned off so no connection is automatically initiated. For more information, see [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md).  
  
## Saving Connections  
 You can save connections to specific servers in Registered Servers, or you can save connections in projects with Solution Explorer.  
  
### Saving Connections in Registered Servers  
 When you register a server, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] saves the connection information in Registered Servers. To connect to a registered server, double-click the server name in Registered Servers. Object Explorer then opens a connection to the server.  
  
### Saving Connections in Solution Explorer  
 Solution Explorer allows you to store related queries, scripts, connections, and other associated information in a project. Each script project contains a node called **Connections**, where you can save one or more connections. To add a connection, right-click **Connections**, and then click **New Connection**. To access a saved connection, expand **Connections** and double-click the connection. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] opens a query window associated with that connection. When saved, scripts retain their association to a specific connection.  
  
## See Also  
 [Use SQL Server Management Studio](../sql-server-management-studio-ssms.md)   
 [Object Explorer](../object/object-explorer.md)  
  
  
