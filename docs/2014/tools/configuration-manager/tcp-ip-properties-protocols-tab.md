---
title: "TCP - IP Properties (Protocols Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "TCP/IP [SQL Server], configuration options"
ms.assetid: 007638fc-3a24-4460-adbe-545ded5d6f88
author: stevestein
ms.author: sstein
manager: craigg
---
# TCP - IP Properties (Protocols Tab)
  Use the **TCP/IP Properties** dialog box to configure the options for the TCP/IP protocol. Click **TCP/IP** in the left pane, to show individual IP address configurations in the details pane.  
  
 Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be restarted before the changes take effect.  
  
## Options  
 **Enabled**  
 Possible values are **Yes** and **No**.  
  
 **Keep Alive**  
 Specify the interval (milliseconds) in which keep-alive packets are transmitted to verify that the computer at the remote end of a connection is still available.  
  
 **Listen All**  
 Specify whether SQL Server will listen on all the IP addresses that are bound to network cards on the computer. If set to **No**, configure each IP address separately using the properties dialog box for each IP address. If set to **Yes**, the settings of the **IPAll** properties box will apply to all IP addresses. Default value is **Yes**.  
  
 **No Delay**  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not implement changes to this property.  
  
## See Also  
 [Choosing a Network Protocol](../../../2014/tools/configuration-manager/choosing-a-network-protocol.md)   
 [Creating a Valid Connection String Using TCP IP](../../../2014/tools/configuration-manager/creating-a-valid-connection-string-using-tcp-ip.md)  
  
  
