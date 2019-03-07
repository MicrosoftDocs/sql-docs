---
title: "Connection Properties Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.connectionproperties.f1"
helpviewer_keywords: 
  - "Connection Properties dialog box"
ms.assetid: 6df812ad-4d80-4503-8a23-47719ce85624
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Connection Properties Dialog Box
  Use this dialog box to view the current connection properties. This dialog box is available when you click **View connection properties** in various Object Explorer dialog boxes. The properties displayed on this page are read-only.  
  
 To change properties such as **Database**, connect to the desired database with Object Explorer before opening the **Connection Properties** dialog box.  
  
 Note that the query time-out period for SQL Azure is 30 minutes.  
  
## Authentication  
 View authentication properties for the current connection. Authentication properties are the login and authentication method when the connection was established. To change the authentication properties, disconnect from [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and then connect Object Explorer to the server again, using the desired connection options.  
  
 **Authentication Method**  
 The authentication method used for the current connection.  
  
 **User Name**  
 The name of the user of login used for the connection authentication.  
  
## Connection Category  
 View connection properties for the current connection. Most connection properties were selected on the **Connection Properties** tab of the **Connect to server** dialog box during the connection process.  
  
 **Database**  
 The name of the database currently connected to. To change this use, the SQL Editor toolbar.  
  
 **SPID**  
 The system process ID assigned by the server to the connection. This cannot be changed for this connection.  
  
 **Network Protocol**  
 The network protocol of the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] connection. To change this, connect again with the desired connection properties.  
  
 **Network Packet Size**  
 The packet size used when communicating with the server. To change this, connect again with the desired connection properties.  
  
 **Connection Timeout**  
 The amount of time is seconds to wait when connecting to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] before timing out and returning a failure to connect error to the user. To change this, connect again with the desired connection properties.  
  
 **Execution Timeout**  
 The amount of time in seconds to wait before execution of a task is completed on the server. To change this, connect again with the desired connection properties.  
  
 **Encrypted**  
 Whether the current connection is encrypted. To change this, connect again with the desired connection properties.  
  
## Product Category  
 View product properties for the current connection. These properties describe the server product, version, instance name, and collation. The properties are set during [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] installation.  
  
 **Product Name**  
 The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] product name.  
  
 **Product Version**  
 The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] product version.  
  
 **Server Name**  
 The name of the computer running [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **Instance Name**  
 The instance name of the server. The default instance is blank.  
  
 **Language**  
 The language version of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] product.  
  
 **Collation**  
 The collation of the server.  
  
## Server Environment Category  
 View server environment properties for the current connection related to the server hardware and operating system. The properties cannot be configured by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **Computer Name**  
 The name of the server computer that is running [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **Platform**  
 The operating system name, manufacturer name, and CPU family of the server.  
  
 **Operating System**  
 The [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows version installed on the server.  
  
 **Processors**  
 The number of processors on the server.  
  
 **Operating System Memory**  
 The total amount of physical memory on the server, in megabytes.  
  
## See Also  
 [Property Pages in SQL Server Management Studio](../ssms/property-pages-in-sql-server-management-studio.md)   
 [Connect to Server &#40;Login Page&#41; Database Engine](../ssms/f1-help/connect-to-server-login-page-database-engine.md)  
  
  
