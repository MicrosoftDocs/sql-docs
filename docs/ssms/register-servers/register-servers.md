---
description: "Register Servers"
title: Register Servers
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.sqlserverregisteredserver.dhelp"
helpviewer_keywords: 
  - "connections [SQL Server], registered servers"
  - "registering servers"
  - "servers [SQL Server], registering"
  - "server management [SQL Server], registering servers"
  - "server registration [SQL Server]"
ms.assetid: c2a2513e-fa09-419c-99e7-a12d57c5a0db
author: markingmyname
ms.author: maghan
ms.manageR: jroth
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# Register Servers

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Registering a server in SQL Server Management Studio allows you to store the server connection information for future connections.There are three ways to register a server in SQL Server Management Studio.  
  
1.  Local instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are automatically registered during the first launch of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] after its installation.  
  
2.  You can also initiate the automatic registration process at any time, to restore the registration of local server instances.  
  
3.  Lastly, you can register a server using the Registered Servers tool in SQL Server Management Studio.  
  
## Benefits of Registered Servers  
 With Registered Servers you can:  
  
-   Register servers to preserve the connection information.  
  
-   Determine if a registered server is running.  
  
-   Easily connect Object Explorer and Query Editor to a registered server.  
  
-   Edit or delete the registration information for a registered server.  
  
-   Create groups of servers.  
  
-   Provide user-friendly names for registered servers by providing a value in the **Registered server name** box that is different from the **Server name** list.  
  
-   Provide detailed descriptions for registered servers.  
  
-   Provide detailed descriptions of registered server groups.  
  
-   Export registered server groups.  
  
-   Import registered server groups.  
  
-   View the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log files for online or offline instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Related Tasks  
 Use the following topics to get started with registered servers:  
  
|**Description**|**Topic**|  
|---------------------|---------------|  
|Register local server instances|[Register a Connected Server &#40;SQL Server Management Studio&#41;](./register-a-connected-server-sql-server-management-studio.md)|  
|Register a server|[Create a New Registered Server &#40;SQL Server Management Studio&#41;](./create-a-new-registered-server-sql-server-management-studio.md)|  
|View registered servers|[View Registered Servers in SQL Server Management Studio](./view-registered-servers-in-sql-server-management-studio.md)|  
|Remove a registered server|[Remove a Registered Server &#40;SQL Server Management Studio&#41;](./remove-a-registered-server-sql-server-management-studio.md)|  
|Change a server's registration|[Change a Server's Registration &#40;SQL Server Management Studio&#41;](./change-a-server-s-registration-sql-server-management-studio.md)|  
|Connect to a registered server|[Connect to a Registered Server &#40;SQL Server Management Studio&#41;](./connect-to-a-registered-server-sql-server-management-studio.md)|  
|Disconnect from a registered server|[Disconnect from a Registered Server &#40;SQL Server Management Studio&#41;](./disconnect-from-a-registered-server-sql-server-management-studio.md)|  
|Move a registered server or server group|[Move a Registered Server or Registered Server Group &#40;SQL Server Management Studio&#41;](./move-a-registered-server-or-registered-server-group.md)|  
|Change the name of a registered server or server group|[Change the Name of a Registered Server or Registered Server Group &#40;SQL Server Management Studio&#41;](./change-the-name-of-registered-server-or-registered-server-group.md)|  
|Create or edit a server group|[Create or Edit a Server Group &#40;SQL Server Management Studio&#41;](./create-or-edit-a-server-group-sql-server-management-studio.md)|  
|Remove a server group|[Remove a Server Group &#40;SQL Server Management Studio&#41;](./remove-a-server-group-sql-server-management-studio.md)|  
|Export registered server information|[Export Registered Server Information &#40;SQL Server Management Studio&#41;](./export-registered-server-information-sql-server-management-studio.md)|  
|Import registered server information|[Import Registered Server Information &#40;SQL Server Management Studio&#41;](./import-registered-server-information-sql-server-management-studio.md)|  
|Create a Central Management Server and Server Group|[Create a Central Management Server and Server Group &#40;SQL Server Management Studio&#41;](./create-a-central-management-server-and-server-group.md)|  
|Execute statements against multiple servers simultaneously|[Execute Statements Against Multiple Servers Simultaneously &#40;SQL Server Management Studio&#41;](./execute-statements-against-multiple-servers-simultaneously.md)|  
  
## See Also  
 [Remote Servers](../../database-engine/configure-windows/remote-servers.md)  
  
