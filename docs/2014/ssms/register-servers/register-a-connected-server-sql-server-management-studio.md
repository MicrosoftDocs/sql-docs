---
title: "Register a Connected Server (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.registerserver.f1"
helpviewer_keywords: 
  - "Registered Servers [SQL Server], register connected servers"
  - "connected server registrations [SQL Server]"
ms.assetid: 77deb5f5-0f80-484f-8b8b-29afa67ec18f
author: stevestein
ms.author: sstein
manager: craigg
---
# Register a Connected Server (SQL Server Management Studio)
  This topic describes how to register servers in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. By registering the server, you can save the connection information for servers that you access frequently. A server can be registered before connecting, or at the time of connection from Object Explorer.  
  
 **In This Topic**  
  
-   **To register a server, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To register a connected server  
  
1.  In Object Explorer, right-click a server to which you already are connected, and then click **Register**.  
  
     **Server name**  
     Enter the name you want to use for the registered server. Registering a local or remote server using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] lets you store the server connection information for future connections. This field defaults to the server name entered when you were connecting to the server. You can retain this server name or enter another easy-to-use name for the server.  
  
     **Server description**  
     Enter an optional description of the server. The maximum number of characters allowed is 250.  
  
     **Select a server group**  
     Select the server group where you want to save the server registration.  
  
     **New Group**  
     Click to launch the **New Group** dialog box, where you can create a new server group for the registered server.  
  
     **Save**  
     Click to save the information you have entered and create a registered server.  
  
  
