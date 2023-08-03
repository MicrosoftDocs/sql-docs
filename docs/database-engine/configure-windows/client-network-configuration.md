---
title: "Client Network Configuration"
description: Find out how client computers connect to an instance of SQL Server on a network. Learn about the tools that you can use to manage clients.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "client configuration [SQL Server], connections"
  - "Database Engine [SQL Server], network configurations"
  - "connections [SQL Server], client configuration"
  - "client connections [SQL Server], about client network connections"
  - "client computers [SQL Server]"
  - "client connections [SQL Server]"
  - "network connections [SQL Server], client configuration"
---
# Client Network Configuration
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Client software enables client computers to connect to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a network. A "client" is a front-end application that uses the services provided by a server such as the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. The computer that hosts this application is referred to as the *client computer*.  
  
 At the simplest level, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client can reside on the same machine as an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Typically, however, a client connects to one or more remote servers over a network. The client/server architecture of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows it to seamlessly manage multiple clients and servers on a network. The default client configurations suffice in most situations.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients can include applications of various types, such as:  
  
-   OLE DB consumers  
  
     These applications use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The OLE DB provider mediates between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and client applications that consume [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data as OLE DB rowsets. The **sqlcmd** command prompt utility and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], are examples of OLE DB applications.  
  
-   ODBC applications  
  
     These applications include client utilities installed with previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as the **osql** command prompt utility, as well as other applications that use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   DB-Library clients  
  
     These applications include the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **isql** command prompt utility and clients written to DB-Library. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support for client applications using DB-Library is limited to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 features.  
  
> [!NOTE]  
>  Although the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] still supports connections from existing applications using the DB-Library and Embedded SQL APIs, it does not include the files or documentation needed to do programming work on applications that use these APIs. A future version of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will drop support for connections from DB-Library or Embedded SQL applications. Do not use DB-Library or Embedded SQL to develop new applications. Remove any dependencies on either DB-Library or Embedded SQL when modifying existing applications. Instead of these APIs, use the SQLClient namespace or an API such as OLE DB or ODBC. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not include the DB-Library DLL required to run these applications. To run DB-Library or Embedded SQL applications you must have available the DB-Library DLL from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 6.5, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0, or [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)].  
  
 Regardless of the type of application, managing a client consists mainly of configuring its connection with the server components of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Depending on the requirements of your site, client management can range from little more than entering the name of the server computer to building a library of custom configuration entries to accommodate a diverse multiserver environment.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client DLL contains the network libraries and is installed by the setup program. The network protocols are not enabled during setup for new installations of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Upgraded installations enable the previously enabled protocols. The underlying network protocols are installed as part of Windows Setup (or through Networks in Control Panel). The following tools are used to manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager  
  
     Both client and server network components are managed with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, which combines the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Network Utility, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Client Network Utility, and Service Manager of previous versions. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console (MMC) snap-in. It also appears as a node in the Windows Computer Manager snap-in. Individual network libraries can be enabled, disabled, configured, and prioritized using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
-   Setup  
  
     Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to install the network components on a client computer. Individual network libraries can be enabled or disabled during setup when Setup is started from the command prompt.  
  
-   ODBC Data Source Administrator  
  
     The ODBC Data Source Administrator lets you create and modify ODBC data sources on computers running the Microsoft Windows operating system.  
  
## In This Section  
 [Configure Client Protocols](../../database-engine/configure-windows/configure-client-protocols.md)  
  
 [Create or Delete a Server Alias for Use by a Client &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/create-or-delete-a-server-alias-for-use-by-a-client.md)  
  
 [Logging In to SQL Server](../../database-engine/configure-windows/logging-in-to-sql-server.md)  
  
 [Open the ODBC Data Source Administrator](../../database-engine/configure-windows/open-the-odbc-data-source-administrator.md)  
  
 [Check the ODBC SQL Server Driver Version &#40;Windows&#41;](../../database-engine/configure-windows/check-the-odbc-sql-server-driver-version-windows.md)  
  
## Related Content  
 [Server Network Configuration](../../database-engine/configure-windows/server-network-configuration.md)  
  
 [Manage the Database Engine Services](../../database-engine/configure-windows/manage-the-database-engine-services.md)  
  
  
