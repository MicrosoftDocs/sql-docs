---
title: "TCP/IP Properties (Protocols Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "08/24/2016"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "TCP/IP [SQL Server], configuration options"
ms.assetid: 007638fc-3a24-4460-adbe-545ded5d6f88
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# TCP/IP Properties (Protocols Tab)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  Use the **TCP/IP Properties** dialog box to configure the options for the TCP/IP protocol. Click **TCP/IP** in the left pane, to show individual IP address configurations in the details pane.  
  
 Microsoft SQL Servermust be restarted before the changes take effect.  
  
## Options  
 **Enabled**  
 Possible values are **Yes** and **No**.  
  
 **Keep Alive**  
 Specify the interval (milliseconds) in which keep-alive packets are transmitted to verify that the computer at the remote end of a connection is still available.  
  
 **Listen All**  
 Specify whether SQL Server will listen on all the IP addresses that are bound to network cards on the computer. If set to **No**, configure each IP address separately using the properties dialog box for each IP address. If set to **Yes**, the settings of the **IPAll** properties box will apply to all IP addresses. Default value is **Yes**.  
  
 **No Delay**  
 SQL Serverdoes not implement changes to this property.  
  
## See Also  
 [Choosing a Network Protocol](https://msdn.microsoft.com/library/ms187892(v=sql.130).aspx)   
 [Creating a Valid Connection String Using TCP IP](creating-a-valid-connection-string-using-tcp-ip.md)  
  
  
