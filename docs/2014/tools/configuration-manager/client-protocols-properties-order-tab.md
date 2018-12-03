---
title: "Client Protocols Properties (Order Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "client protocols [SQL Server]"
ms.assetid: 64fd7135-1756-4885-bed9-9ab8997ecc6c
author: stevestein
ms.author: sstein
manager: craigg
---
# Client Protocols Properties (Order Tab)
  Use the **Order**page on the **Client Protocols Properties** dialog box to view and enable the client protocols.  
  
 Click a protocol, and then click **Enable** or **Disable** to move the selected protocol to the **Disabled Protocols** or **Enabled Protocols** list.  
  
 Protocols are tried in the order listed, attempting to connect using the top protocol first, and then the second listed protocol, etc. Move protocols up or down the **Enabled Protocols** list, by clicking the up arrow and down arrow buttons. When connecting to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a client on that computer, the **Shared Memory** protocol will always be tried first, if enabled.  
  
> [!NOTE]  
>  These settings are not used by [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET SqlClient. The protocol order for .NET SqlClient is first TCP, and then named pipes, which cannot be changed.  
  
## Options  
 **Disabled Protocols**  
 Lists protocols which are installed but are not currently used.  
  
 **Enabled Protocols**  
 Lists protocols which are available for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients on this computer.  
  
 **>**  
 Enables the currently highlighted protocol in the **Disabled Protocols** box, moving it to the **Enabled Protocols** box.  
  
 **\<**  
 Disables the currently highlighted protocol in the **Enabled Protocols** box, moving it to the **Disabled Protocols** box.  
  
 Up arrow  
 Moves the highlighted protocol up in the list. This allows you to increase the priority in which the Net-Library will attempt to use the selected protocol for connections.  
  
 Down arrow  
 Moves the highlighted protocol down in the list. This allows you to decrease the priority in which the Net-Library will attempt to use the selected protocol for connections.  
  
 **Enable Shared Memory Protocol**  
 Enables the shared memory protocol which is always tried first (if enabled), when connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a client on that computer.  
  
> [!NOTE]  
>  If the protocol is specified through a prefix or as part of the connection string, only the specified protocol is attempted.  
  
## See Also  
 [Choosing a Network Protocol](../../../2014/tools/configuration-manager/choosing-a-network-protocol.md)  
  
  
