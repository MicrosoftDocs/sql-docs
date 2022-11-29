---
title: "SQL Server Browser service"
description: Learn how to use SQL Server Browser, a service that listens for requests for SQL Server resources and provides information about installed SQL Server instances.
ms.custom: seo-lt-2019
ms.date: "02/03/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.browseservers.local.f1"
  - "sql13.swb.browseservers.network.f1"
helpviewer_keywords: 
  - "services [SQL Server], security"
  - "SQL Browser service (See SQL Server Browser Service)"
  - "Browser Service"
  - "SQL Server Browser service"
ms.assetid: 3cc00d3a-487c-4cd9-a155-655f02485fa0
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016"
---

# SQL Server Browser service

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

The SQL Server Browser program runs as a Windows service. SQL Server Browser listens for incoming requests for [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server resources and provides information about SQL Server instances installed on the computer. SQL Server Browser contributes to the following actions:  

- Browsing a list of available servers.
- Connecting to the correct server instance.
- Connecting to [dedicated administrator connection](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md) (DAC) endpoints.
  
For each instance of the Database Engine and [SQL Server Analysis Services](/analysis-services/ssas-overview) (SSAS), the SQL Server Browser service (sqlbrowser) provides the instance name and the version number. SQL Server Browser is installed with SQL Server.  
  
SQL Server Browser can be configured during setup or by using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md). By default, the SQL Server Browser service starts automatically:  
  
- When upgrading an installation.  
- When installing on a cluster.  
- When installing a named instance of the Database Engine including all instances of SQL Server Express.
- When installing a named instance of SSAS.  
  
## Background

Prior to [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], only one instance of SQL Server could be installed on a computer. SQL Server listened for incoming requests on port 1433, assigned to SQL Server by the official Internet Assigned Numbers Authority (IANA). Only one instance of SQL Server can use a port, so when [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] introduced support for multiple instances of SQL Server, SQL Server Resolution Protocol (SSRP) was developed to listen on UDP port 1434. This listener service responded to client requests with the names of the installed instances and the ports or named pipes used by the instance.

To resolve limitations of the SSRP system, [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] introduced the SQL Server Browser service as a replacement for SSRP.  
  
## How SQL Server Browser works  

When an instance of SQL Server starts, if the [TCP/IP protocol is enabled for SQL Server](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md), the server is assigned a TCP/IP port. If the named pipes protocol is enabled, SQL Server listens on a specific named pipe. This port, or "pipe," is used by that specific instance to exchange data with client applications. During installation, TCP port 1433 and pipe `\sql\query` are assigned to the default instance, but those can be changed later by the server administrator using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

Because only one instance of SQL Server can use a port or pipe, different port numbers and pipe names are assigned for named instances, including SQL Server Express. By default, when enabled, both named instances and SQL Server Express are configured to use dynamic ports, that is, an available port is assigned when SQL Server starts.

If you want, a specific port can be assigned to an instance of SQL Server. When connecting, clients can specify a specific port; but if the port is dynamically assigned, the port number can change anytime SQL Server is restarted, so the correct port number is unknown to the client.  
  
Upon startup, SQL Server Browser starts and claims UDP port 1434. SQL Server Browser reads the registry, identifies all instances of SQL Server on the computer, and notes the ports and named pipes that they use. When a server has two or more network cards, SQL Server Browser returns the first enabled port it encounters for SQL Server. SQL Server Browser support ipv6 and ipv4.  
  
When SQL Server clients request SQL Server resources, the client network library sends a UDP message to the server using port 1434. SQL Server Browser responds with the TCP/IP port or named pipe of the requested instance. The network library on the client application then completes the connection by sending a request to the server using the port or named pipe of the desired instance.  
  
Learn how to start and stop the SQL Server Browser service in the article [Start, stop, pause, resume, restart SQL Server services](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).  
  
## Using SQL Server Browser  

If the SQL Server Browser service isn't running, you are still able to connect to SQL Server if you provide the correct port number or named pipe. For instance, you can connect to the default instance of SQL Server with TCP/IP if it's running on port 1433.  
  
However, if the SQL Server Browser service isn't running, the following connections do not work:  
  
- Any component that tries to connect to a named instance without fully specifying all the parameters (such as the TCP/IP port or named pipe).  
- Any component that generates or passes server\instance information that could later be used by other components to reconnect.  
- Connecting to a named instance without providing the port number or pipe.  
- [DAC](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md) to a named instance or the default instance if not using TCP/IP port 1433.  
- The OLAP redirector service.  
- Enumerating servers in [SQL Server Management Studio](../../ssms/menu-help/about-sql-server-management-studio.md) or [Azure Data Studio](../../azure-data-studio/download-azure-data-studio.md).
  
If you are using SQL Server in a client-server scenario (for example, when your application is accessing SQL Server across a network), if you stop or disable the SQL Server Browser service, you must assign a specific port number to each instance and write your client application code to always use that port number. This approach has the following problems:  

- You must update and maintain client application code to ensure it's connecting to the proper port.
- The port you choose for each instance may be used by another service or application on the server, causing the instance of SQL Server to be unavailable.  
  
## Clustering  

SQL Server Browser isn't a clustered resource and doesn't support failover from one cluster node to the other. Therefore, in the case of a cluster, SQL Server Browser should be installed and turned on for each node of the cluster. On clusters, SQL Server Browser listens on IP_ANY.  
  
> [!NOTE]  
>  When listening on IP_ANY, when you enable listening on specific IPs, the user must configure the same TCP port on each IP, because SQL Server Browser returns the first IP/port pair that it encounters.  
  
## Installing, uninstalling, and running from the command line  

By default, the SQL Server Browser program is installed at *C:\Program Files (x86)\Microsoft SQL Server\90\Shared\sqlbrowser.exe*.  
  
The SQL Server Browser service is uninstalled when the last instance of SQL Server is removed.  
  
SQL Server Browser can be started from the command prompt for troubleshooting, by using the **-c** switch:  
  
```dos
<drive>\<path>\sqlbrowser.exe -c  
```  

## Security
  
### Account privileges  

SQL Server Browser listens on a UDP port and accepts unauthenticated requests by using SQL Server Resolution Protocol (SSRP). SQL Server Browser should be run in the security context of a low-privileged user to minimize exposure to a malicious attack. The logon account can be changed by using the SQL Server Configuration Manager. 

The minimum user rights for SQL Server Browser are:  
  
- Deny access to this computer from the network.
- Deny logon locally.
- Deny Log on as a batch job.
- Deny Log On Through Terminal Services.
- Log on as a service.
- Read and write the SQL Server registry keys related to network communication (ports and pipes).
  
### Default account  

Setup configures SQL Server Browser to use the account selected for services during setup. Other possible accounts include:  
  
- Any **domain\local** account.
- The **local service** account.  
- The **local system** account (not recommended as has unnecessary privileges).
  
### Hiding SQL Server  

Hidden instances are instances of SQL Server that support only shared memory connections. For SQL Server, set the `HideInstance` flag to indicate that SQL Server Browser shouldn't respond with information about this server instance.  
  
### Using a firewall  

To communicate with the SQL Server Browser service on a server behind a firewall, open UDP port 1434, in addition to the TCP port used by SQL Server (e.g., 1433). For information about working with a firewall, see [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
## Next steps

Learn more about related concepts in the following articles:

- [Network Protocols and Network Libraries](../../sql-server/install/network-protocols-and-network-libraries.md)
- [Connecting Using IPv6](connecting-using-ipv6.md)