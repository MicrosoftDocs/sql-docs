---
title: "TCP/IP Properties (IP Addresses Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "08/24/2016"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "ports [SQL Server], listening on"
  - "listening [SQL Server], on ports"
ms.assetid: 4c17ed45-9da7-4bec-bce6-970109fe7365
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# TCP/IP Properties (IP Addresses Tab)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  Use the **TCP/IP Properties (IP Addresses Tab)** dialog box to configure the TCP/IP protocol options for a specific IP address. Only **TCP Dynamic Ports** and **TCP Port** can be configured for all addresses at once by selecting **IP All**.  
  
 Changes take effect when SQL Server SQL Server is restarted. For information about starting and stopping the SQL Server Browser service, see [Start and Stop the SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).  
  
## Static vs. Dynamic Ports  
 The default instance of SQL Server listens for incoming connections on port 1433. The port can be changed for security reasons or because of a client application requirement. By default, named instances (including SQL Server Express) are configured to listen on dynamic ports. To configure a static port, leave the **TCP Dynamic Ports** box blank and provide an available port number in the **TCP Port** box. For more information about opening ports in the firewall, see Configuring the Windows Firewall to Allow SQL Server Access in Books Online.  
  
## Dynamic Ports  
 At startup, when an instance of SQL Server is configured to listen on dynamic ports, it checks with the operating system for an available port, and opens an endpoint for that port. Incoming connections must specify that port number to connect. Since the port number can change each time SQL Server starts, SQL Server provides the SQL Server Browser Service, to monitor the ports, and direct incoming connections to the current port for that instance. Using dynamic ports complicates connecting SQL Server through a firewall because the port number may change when SQL Server is restarted, requiring changes to the firewall settings. To avoid connection problems through a firewall, configure SQL Server to use a static port.  
  
## Options  
 **Active**  
 Indicates that the IP address is active on the computer. Not available for **IPAll**.  
  
 **Enabled**  
 If the **Listen All** property on the **TCP/IP Properties (Protocol Tab)** is set to **No**, this property indicates whether SQL Server is listening on the IP address. If the **Listen All** property on the **TCP/IP Properties (Protocol Tab)** is set to **Yes**, the property is disregarded. Not available for **IPAll**.  
  
 **IP Address**  
 View or change the IP address used by this connection. Lists the IP address used by the computer, and the IP loopback address, 127.0.0.1. Not available for **IPAll**. The IP address can be in either IPv4 or IPv6 format.  
  
 **TCP Dynamic Ports**  
 Blank, if dynamic ports are not enabled. To use dynamic ports, set to 0.  
  
 For **IPAll**, displays the port number of the dynamic port used.  
  
 **TCP Port**  
 View or change the port on which SQL Server listens. By default, the default instance of SQL Server listens on port 1433.  
  
 The database engine can listen on multiple ports on the same IP address, list the ports, separated by commas, in the format 1433,1500,1501. This field is limited to 2047 characters.  
  
 To configure a single IP address to listen on multiple ports, the **Listen All** parameter must also be set to **No**, on the **Protocols Tab** of the **TCP/IP Properties** dialog box. For more information, see "How to: Configure the Database Engine to Listen on Multiple TCP Ports" in SQL Server Books Online.  
  
## Adding or Removing IP Addresses  
 SQL Server Configuration Manager displays the IP addresses that were available at the time SQL Server was installed. The available IP addresses can change when network cards are added or removed, when a dynamically assigned IP address expires, when the network structure is reconfigured, or when the computer changes its physical location such as a laptop computer connecting to the network in a different building. To change an IP address, edit the **IP Address** box, and then restart SQL Server.  
  
## Additional topics in Books Online  
 Search MSDN for topics such as **Configure a Server to Listen on a Specific TCP Port (SQL Server Configuration Manager)** and **Configure the Database Engine to Listen on Multiple TCP Ports**.  
  
## See Also  
 [Choosing a Network Protocol](https://msdn.microsoft.com/library/ms187892(v=sql.120).aspx)   
 [Creating a Valid Connection String Using TCP IP](creating-a-valid-connection-string-using-tcp-ip.md)   
 [SQL Server Browser Service](sql-server-browser-service.md)  
  
  
