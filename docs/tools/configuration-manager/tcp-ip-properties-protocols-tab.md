---
title: "TCP/IP Properties (Protocols Tab)"
description: Use the options in the Protocols tab of the TCP/IP Properties dialog box to configure the keep alive interval, the enabled flag, and other properties.
ms.custom: seo-lt-2019
ms.date: "08/24/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "TCP/IP [SQL Server], configuration options"
ms.assetid: 007638fc-3a24-4460-adbe-545ded5d6f88
author: markingmyname
ms.author: maghan
---
# TCP/IP Properties (Protocols Tab)
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
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
 [Choosing a Network Protocol](/previous-versions/sql/sql-server-2016/ms187892(v=sql.130))   
 [Creating a Valid Connection String Using TCP IP](creating-a-valid-connection-string-using-tcp-ip.md)  
  
