---
title: "Client Protocols - TCP and IP Properties (Protocol Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "configmgr-client"
ms.topic: conceptual
helpviewer_keywords: 
  - "TCP/IP [SQL Server], client protocols"
  - "client protocols [SQL Server]"
ms.assetid: d04f1bce-069c-4a02-b561-c87c3282be36
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Client Protocols - TCP and IP Properties (Protocol Tab)
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, use the **Protocol** tab on the **TCP/IP Properties** dialog box to view or specify the following options. To connect to a different port, type the port number in the **Default Port** box. For more information about connection strings, see [Creating a Valid Connection String Using TCP IP](../../../2014/tools/configuration-manager/creating-a-valid-connection-string-using-tcp-ip.md).  
  
## Options  
 **Default Port**  
 Specifies the port that the TCP/IP Net-library will use to attempt to connect to the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The default value port is 1433.  
  
 When connecting to a default instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)], the client uses this value. If a default instance has been configured to listen on a different port, change this value to that port number.  
  
 When connecting to a named instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)], the client will attempt to obtain the port number from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service running on the server computer. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service is not running, the port number must be provided through this setting, or as part of the connection string.  
  
 **Enabled**  
 Possible values are **Yes** and **No**.  
  
 **Keep Alive**  
 This parameter (in milliseconds) controls how often TCP attempts to verify that an idle connection is still intact by sending a **KEEPALIVE** packet. The default is 30000 milliseconds.  
  
 **Keep Alive Interval**  
 This parameter (in milliseconds) determines the interval separating **KEEPALIVE** retransmissions until a response is received. The default is 1000 milliseconds.  
  
## See Also  
 [Choosing a Network Protocol](../../../2014/tools/configuration-manager/choosing-a-network-protocol.md)   
 [New Alias &#40;Alias Tab&#41;](../../../2014/tools/configuration-manager/new-alias-alias-tab.md)   
 [&#60;Alias&#62; Properties &#40;Alias Tab&#41;](../../../2014/tools/configuration-manager/alias-properties-alias-tab.md)  
  
  
